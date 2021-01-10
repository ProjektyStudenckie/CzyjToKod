using CzyjToKod.Model;
using CzyjToKod.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CzyjToKod.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        private string _file1Path = "";
        private string _file2Path = "";

        #region Public Properties

        public string File1Path {
            get => _file1Path; 
            set
            {
                _file1Path = value;

                try
                {
                    File1Length = $"Characters: {File.ReadAllText(value).Length}";
                    File1Invalid = "";
                } catch(Exception e)
                {
                    File1Invalid = e.Message;
                    File1Length = "";
                }

                onPropertyChanged(nameof(File1Length));
                onPropertyChanged(nameof(File1Invalid));
            }
        }

        public string File1Length { get; private set; }
        public string File1Invalid { get; private set; }

        public string File2Path
        {
            get => _file2Path;
            set
            {
                _file2Path = value;

                try
                {
                    File2Length = $"Characters: {File.ReadAllText(value).Length}";
                    File2Invalid = "";
                }
                catch (Exception e)
                {
                    File2Invalid = e.Message;
                    File2Length = "";
                }

                onPropertyChanged(nameof(File2Length));
                onPropertyChanged(nameof(File2Invalid));
            }
        }

        public string File2Length { get; private set; }
        public string File2Invalid { get; private set; }

        public string InvalidInput { get; private set; }
        public string Result { get; set; }

        #endregion


        #region Commands

        private ICommand _handleGetFileClick = null;
        /// <summary>
        /// Handle get file button click.
        /// </summary>
        public ICommand HandleGetFileClick
        {
            get
            {
                if (_handleGetFileClick == null)
                {
                    _handleGetFileClick = new RelayCommand(
                        arg => {

                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                            {
                                if((string)arg == "1")
                                {
                                    File1Path = openFileDialog.FileName;
                                }
                                else if ((string)arg == "2")
                                {
                                    File2Path = openFileDialog.FileName;
                                }
                                   
                                onPropertyChanged(nameof(File1Path));
                                onPropertyChanged(nameof(File2Path));
                            }
                        },
                        arg => true
                     );
                }
                return _handleGetFileClick;
            }
        }


        private ICommand _handleCheckClick = null;
        /// <summary>
        /// Handle check button click.
        /// </summary>
        public ICommand HandleCheckClick
        {
            get
            {
                if (_handleCheckClick == null)
                {
                    _handleCheckClick = new RelayCommand(
                        arg => {

                            if (File1Invalid.Length == 0 && File2Invalid.Length == 0)
                            {
                                File.Delete(Reinterpreter.Direction + "\\result.txt");


                                PlagiarismCheck.TestForPlagiarism(File1Path, File2Path);
                                WaitForResult();
                                InvalidInput = "";
                            }
                            else
                            {
                                InvalidInput = "Nieprawidłowe pliki!";
                            }
                        },
                        arg => (File1Invalid == "" && File2Invalid == "")
                     );
                }
                return _handleCheckClick;
            }
        }

        #endregion

        private async void WaitForResult()
        {
            await Task.Run(() =>
            {
                while (!File.Exists(Reinterpreter.Direction + "\\result.txt")) ;
                System.Threading.Thread.Sleep(1000);
                float value = float.Parse(File.ReadAllText(Reinterpreter.Direction + "\\result.txt").Replace(".", ","));
                float result = value * 100;
                int resultInt = (int)result;
                Result = resultInt.ToString();
                onPropertyChanged(nameof(Result));
            });
 
        }
    }
}
