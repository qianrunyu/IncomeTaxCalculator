using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Data;
using Payment_GaryQian.Model;
using System.Collections.ObjectModel;
using Payment_GaryQian.BusinessLogic;
using System.Text;
using Newtonsoft.Json;

namespace Payment_GaryQian.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyChange
    {
        private MainEntry mainForm;
        private ManipulateOutput outputLogic;
        public MainWindowViewModel()
        {
            importRowCollection = new List<EmployeeDetail>();
            outputRowCollection = new List<EmployeeOutputDetail>();

            ClickMinimizeCommand = new RelayCommand(ClickMinimizeBtnExecute);
            OpenImportFilePathCommand = new RelayCommand(OpenImportFilePathCommandExecute);
            SaveToFileCommand = new RelayCommand(SaveToFileCommandExecute);
            WindowClosingCommand = new RelayCommand(WindowClosingCommandExecute);
            FormStateChangedCommand = new RelayCommand(FormStateChangedCommandExecute);
            ConnectionDialogCloseCommand = new RelayCommand(DialogCloseExecute); //when user click close button    
            CalculateCommand = new RelayCommand(CalculateCommandExecute);
        }
        //trigger from app.xaml.cs
        public void WPFFormShow()
        {
            mainForm = new MainEntry(this);
            mainForm.Show();
        }
        #region Command executer from  buttons
        //user clicks save as dialog
        public void SaveToFileCommandExecute(object obj)
        {
            // System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            using (var dlg = new System.Windows.Forms.SaveFileDialog())
            {
                dlg.DefaultExt = ".csv"; // Default file extension
                dlg.Filter = "CSV File|*.csv|JSON File|*.json"; // Filter files by extension
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveToDrive = dlg.FileName;
                }
            }
        }
        //user clicks open file dialog
        public void OpenImportFilePathCommandExecute(object obj)
        {
            using (var fb = new System.Windows.Forms.OpenFileDialog())
            {
                fb.Filter = "CSV File |*.csv|JSON File|*.json";
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FilePath = fb.FileName;
                    importRowCollection.Clear();
                    outputRowCollection.Clear();
                }
            }
        }
        //user clicks wpf form minimize button
        public void ClickMinimizeBtnExecute(object obj)
        {
            mainForm.WindowState = System.Windows.WindowState.Minimized;
        }


        public void DialogCloseExecute(object obj)
        {

            mainForm.Close();

        }
        public void WindowClosingCommandExecute(object obj)
        {
            //leave it blank
        }
        public void FormStateChangedCommandExecute(object obj)
        {
            if (mainForm.WindowState == WindowState.Maximized)
            {
                mainForm.WindowState = WindowState.Normal;
            }
        }
        //user clicks calculate button. Core feature
        public void CalculateCommandExecute(object obj)
        {
            try
            {
                if (filePath.Length > 0)
                {
                    importRowCollection.Clear();
                    outputRowCollection.Clear();
                    switch (Path.GetExtension(filePath))
                    {
                        case ".csv":
                            string[] allLines = File.ReadAllLines(filePath);
                            if (allLines.Count() == 0)
                                throw new Exception();
                            for (int line = 1; line < allLines.Count(); line++)
                            {
                                if (!string.IsNullOrEmpty(allLines[line]))
                                {
                                    string[] aa = allLines[line].Split(',');
                                    importRowCollection.Add(new EmployeeDetail(aa[0], aa[1], aa[2], aa[3], aa[4]));
                                }
                            }
                            break;
                        case ".json":
                            using (StreamReader r = File.OpenText(filePath))
                            {
                                string json = r.ReadToEnd();
                                //dynamic array = JsonConvert.DeserializeObject(json);
                                List<EmployeeDetail> array = JsonConvert.DeserializeObject<List<EmployeeDetail>>(json);
                                foreach (var item in array)
                                {
                                    importRowCollection.Add(new EmployeeDetail(item.FirstName, item.LastName, item.AnnualSalary, item.SuperRate, item.Period));
                                }
                            }
                            break;
                    }
                    //populdate output collection
                    foreach (var oneRow in importRowCollection)
                    {
                        outputLogic = new ManipulateOutput(oneRow);
                        outputRowCollection.Add(outputLogic.CalculatePayment());
                    }
                    SearchText = string.Empty; //reset search text box
                    DisplayCollection = outputRowCollection.ToList();//populate display collection for UI
                    MessageBox.Show("Process is completed.", "Successful"); 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error find in import file", "File Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SaveToDriveEnabled && !string.IsNullOrEmpty(SaveToDrive))
            {
                try
                {
                    string outputString = null;
                    switch (Path.GetExtension(SaveToDrive))
                    {
                        case ".csv":
                            var csv = new StringBuilder();
                            var outputHeader = string.Format("{0},{1},{2},{3},{4},{5}", "name", "pay period", "gross income", "income tax", "net income", "super");
                            csv.AppendLine(outputHeader);
                            foreach (var row in outputRowCollection)
                            {
                                var newLine = string.Format("{0},{1},{2},{3},{4},{5}", row.Name, row.Period, row.GrossIncome, row.IncomeTax, row.NetIncome, row.Super);
                                csv.AppendLine(newLine);
                            }
                            outputString = csv.ToString();
                            break;
                        case ".json":
                            outputString = JsonConvert.SerializeObject(outputRowCollection.ToArray(), Formatting.Indented);
                            break;
                    }
                    File.WriteAllText(SaveToDrive, outputString);
                }
                catch (Exception)
                {

                    MessageBox.Show("File can not be saved.", "File Save Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        #endregion


        #region variables
        private string calBtnContent = "Calculate";
        public string CalBtnContent
        {
            get
            {
                return calBtnContent;
            }
            set
            {
                calBtnContent = value;
                OnPropertyChanged("CalBtnContent");
            }
        }
        private bool fileImportHasContent;
        public bool FileImportHasContent
        {
            get
            {
                return fileImportHasContent;
            }
            set
            {
                fileImportHasContent = value;
                OnPropertyChanged("FileImportHasContent");
            }
        }
        private bool saveToDriveEnabled;
        public bool SaveToDriveEnabled
        {
            get
            {
                return saveToDriveEnabled;
            }
            set
            {
                saveToDriveEnabled = value;
                if (value == false)
                {
                    SaveToDrive = string.Empty;
                    CalBtnContent = "Calculate";
                }
                else
                    CalBtnContent = "Calculate & Save";
                OnPropertyChanged("SaveToDriveEnabled");
            }
        }
        private string saveToDrive;
        public string SaveToDrive
        {
            get
            {
                return saveToDrive;
            }
            set
            {
                saveToDrive = value;
                OnPropertyChanged("SaveToDrive");
            }
        }
        private string filePath = string.Empty;
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                FileImportHasContent = FilePath.Length > 0 ? true : false;
                OnPropertyChanged("FilePath");
            }
        }
        private List<EmployeeDetail> importRowCollection;

        private List<EmployeeOutputDetail> outputRowCollection;

        private List<EmployeeOutputDetail> disPlayCollection;
        public List<EmployeeOutputDetail> DisplayCollection
        {
            get
            {
                return disPlayCollection;
            }
            set
            {
                disPlayCollection = value;
                OnPropertyChanged("DisplayCollection");
            }
        }
        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value.ToLower();
                DisplayCollection = outputRowCollection.Where(item => item.Name.ToLower().Contains(searchText)).ToList();
                OnPropertyChanged("SearchText");
            }
        }
        #endregion

        #region ReplayCommands
        public IRelayCommand ClickMinimizeCommand
        {
            get;
            internal set;
        }

        public IRelayCommand SaveToFileCommand
        {
            get;
            internal set;
        }
        public IRelayCommand OpenImportFilePathCommand
        {
            get;
            internal set;
        }
        public IRelayCommand WindowClosingCommand
        {
            get;
            internal set;
        }
        public IRelayCommand ConnectionDialogCloseCommand
        {
            get;
            internal set;
        }
        public IRelayCommand FormStateChangedCommand
        {
            get;
            internal set;
        }
        public IRelayCommand CalculateCommand
        {
            get;
            internal set;
        }


        #endregion
    }

}
