using CzyjToKod.Model;
using CzyjToKod.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CzyjToKod.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        #region Public Properties

        public string File1Name { get; set; }
        public string File1Length { get; private set; }

        public string File2Name { get; set; }
        public string File2Length { get; private set; }

        public string Result { get; set; }

        #endregion


        #region Commands

        private ICommand _handleGetFileClick = null;
        /// <summary>
        /// Handle check button click.
        /// </summary>
        public ICommand HandleGetFileClick
        {
            get
            {
                if (_handleGetFileClick == null)
                {
                    _handleGetFileClick = new RelayCommand(
                        arg => {

                            Console.WriteLine(arg);
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                            {
                                Console.Write(File.ReadAllText(openFileDialog.FileName));

                                if((string)arg == "1")
                                {
                                    File1Name = openFileDialog.FileName;
                                    File1Length = $"Characters: {File.ReadAllText(openFileDialog.FileName).Length}";
                                }
                                    

                                else if ((string)arg == "2")
                                {
                                    File2Name = openFileDialog.FileName;
                                    File2Length = $"Characters: {File.ReadAllText(openFileDialog.FileName).Length}";
                                }
                                   
                                onPropertyChanged(nameof(File1Name));
                                onPropertyChanged(nameof(File2Name));
                                onPropertyChanged(nameof(File1Length));
                                onPropertyChanged(nameof(File2Length));
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
                            PlagiarismCheck.CheckCode();
                            // test
                            Result = "58%";
                            onPropertyChanged(nameof(Result));
                        },
                        arg => true
                     );
                }
                return _handleCheckClick;
            }
        }

        #endregion
    }
}
