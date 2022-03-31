using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using StudentsTable.ViewModels;
using StudentsTable.Models;

namespace StudentsTable.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("LoadButton").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.LoadFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("SaveButton").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.SaveFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("ExitButton").Click += delegate
            {
                Close();
            };
        }
        private void CellEdited(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            ObservableCollection<Student> studentCollection = context.StudentCollection;

            if (context != null)
            {
                context.UpdateAverage();
                context.UpdateAverageItemMarks();
                context.StudentCollection = studentCollection;
            }
        }
        private async void OpenAbout(object control, RoutedEventArgs arg)
        {
            await new About().ShowDialog((Window)this.VisualRoot);
        }
        private void Exit()
        {
            Close();
        }
    }
}
