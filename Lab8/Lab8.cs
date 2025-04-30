using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace SimpleNotepad
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string currentFilePath = string.Empty;
        private bool isTextChanged = false;

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand ExitCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            NewCommand = new RelayCommand(NewFile);
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            ExitCommand = new RelayCommand(Exit);
            SetLanguage("RussianResources");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetLanguage(string languageKey)
        {
            // Очищаем текущие ресурсы
            this.Resources.MergedDictionaries.Clear();

            // Загружаем новый ресурс в зависимости от языка
            var resources = new ResourceDictionary
            {
                Source = new Uri($"Resources/{languageKey}.xaml", UriKind.Relative)
            };

            this.Resources.MergedDictionaries.Add(resources);
        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var languageKey = menuItem.Tag.ToString();
            SetLanguage(languageKey);
        }
        private void NewFile()
        {
            if (!CheckSaveChanges()) return;
            TextEditor.Clear();
            currentFilePath = string.Empty;
            isTextChanged = false;
        }

        private void OpenFile()
        {
            if (!CheckSaveChanges()) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы(*.txt) | *.txt | Все файлы(*.*) | *.* "
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
                    currentFilePath = openFileDialog.FileName;
                    isTextChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show( $"Ошибка открытия: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileAs();
            }
            else
            {
                try
                {
                    File.WriteAllText(currentFilePath, TextEditor.Text);
                    isTextChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, TextEditor.Text);
                    currentFilePath = saveFileDialog.FileName;
                    isTextChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Exit()
        {
            if (!CheckSaveChanges()) return;
            Application.Current.Shutdown();
        }

        private bool CheckSaveChanges()
        {
            if (isTextChanged)
            {
                var result = MessageBox.Show("Вы хотите сохранить изменения?", "Сохранить?",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    SaveFile();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!CheckSaveChanges())
            {
                e.Cancel = true;
            }
        }

        private void TextEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            isTextChanged = true;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();
        public void Execute(object parameter) => execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
