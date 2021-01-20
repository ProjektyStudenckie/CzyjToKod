using CzyjToKod.Model;
using CzyjToKod.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.Json;

namespace CzyjToKod.ViewModel
{
    /// <summary>
    /// View model of the main window of our application
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region Private Properties

        private const string nameLevenshtein = "1. Levenshtein: ";
        private const string nameDamerau = "2. Damerau Levenshtein: ";
        private const string nameHamming = "3. Hamming: ";
        private const string nameCosine= "1. Cosine: ";
        private const string nameJaro = "2. Jaro: ";
        private const string nameJaroWinkler = "3. Jaro Winkler: ";

        private string _file1Path = "";
        private string _file2Path = "";

        private int _s1Value=0;
        private int _s2Value=0;
        private int _s3Value=0;

        private string _Value1 = nameLevenshtein;
        private string _Value2 = nameDamerau;
        private string _Value3 = nameHamming;
        private string _ValueDif1 = nameCosine;
        private string _ValueDif2 = nameJaro;
        private string _ValueDif3 = nameJaroWinkler;

        #endregion


        #region Public Properties

        public int s1Value
        {
            get => _s1Value;
            set => _s1Value = value;
        }

        public int s2Value
        {
            get => _s2Value;
            set => _s2Value = value;
        }

        public int s3Value
        {
            get => _s3Value;
            set => _s3Value = value;
        }

        public string Value1
        {
            get => _Value1;
            set
            {
                _Value1 = nameLevenshtein + value;
                onPropertyChanged(nameof(Value1));

            }
        }

        public string Value2
        {
            get => _Value2;
            set
            {
                _Value2 = nameDamerau + value;
                onPropertyChanged(nameof(Value2));

            }
        }

        public string Value3
        {
            get => _Value3;
            set
            {
                _Value3 = nameHamming + value;
                onPropertyChanged(nameof(Value3));

            }
        }

        public string ValueDif1
        {
            get => _ValueDif1;
            set
            {
                _ValueDif1 = nameCosine + value;
                onPropertyChanged(nameof(ValueDif1));

            }
        }

        public string ValueDif2
        {
            get => _ValueDif2;
            set
            {
                _ValueDif2 = nameJaro + value;
                onPropertyChanged(nameof(ValueDif2));

            }
        }

        public string ValueDif3
        {
            get => _ValueDif3;
            set
            {
                _ValueDif3 = nameJaroWinkler + value;
                onPropertyChanged(nameof(ValueDif3));

            }
        }

        public string File1Path
        {
            get => _file1Path;
            set
            {
                _file1Path = value;

                try
                {
                    File1Length = $"Characters: {File.ReadAllText(value).Length}";
                    File1Invalid = "";
                }
                catch (Exception e)
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

        // Result of plagiarism check
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
                        arg =>
                        {

                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                            {
                                if ((string)arg == "1")
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
                        arg =>
                        {

                            if (File1Invalid.Length == 0 && File2Invalid.Length == 0)
                            {
                                File.Delete(Reinterpreter.Direction + "\\results.txt");


                                PlagiarismCheck.TestForPlagiarism(File1Path, File2Path);
                                WaitForResult();
                                InvalidInput = "";
                            }
                            else
                            {
                                InvalidInput = "Nieprawidłowe pliki!";
                            }
                        },
                        arg => (File1Invalid == "" && File2Invalid == "" && !(s1Value.Equals(0) && s2Value.Equals(0) && s3Value.Equals(0)))
                     );
                }
                return _handleCheckClick;
            }
        }
        #endregion


        #region Private Methods

        private async void WaitForResult()
        {
            await Task.Run(() =>
            {
                while (!File.Exists(Reinterpreter.Direction + "\\results.txt")) ;
                System.Threading.Thread.Sleep(1000);
                var jsonString = File.ReadAllText(Reinterpreter.Direction + "\\results.txt");
                var results = JsonSerializer.Deserialize<ResultsData>(jsonString);

                SetUiResults(results);

                float wholeWeight = s1Value + s2Value + s3Value;

                float valueCosine = results.cosine * s1Value;
                float valueJaro = results.jaro_similarity * s2Value;
                float valueJaroWinkler = results.jaro_winkler_similarity * s3Value;
                float result = ((valueCosine + valueJaro + valueJaroWinkler) / wholeWeight) * 100;
                int resultInt = (int)result;
                Result = $"{resultInt}%";
                onPropertyChanged(nameof(Result));
            });

        }

        private void SetUiResults(ResultsData results)
        {
            Value1 = results.levenshtein_distance.ToString();
            Value2 = results.damerau_levenshtein_distance.ToString();
            Value3 = results.hamming_distance.ToString();
            ValueDif1 = results.cosine.ToString();
            ValueDif2 = results.jaro_similarity.ToString();
            ValueDif3 = results.jaro_winkler_similarity.ToString();
        }

        #endregion

    }
}
