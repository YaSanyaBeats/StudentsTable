using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using StudentsTable.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace StudentsTable.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Student> students = new List<Student>() { new Student(), new Student(), new Student() };
        ObservableCollection<Student> studentCollection;
        public List<MarkAverage> averageItemMarks = new List<MarkAverage>() { new MarkAverage(1), new MarkAverage(1), new MarkAverage(1), new MarkAverage(1) };
        ObservableCollection<MarkAverage> averageItemMarksCollection;
        public MainWindowViewModel()
        {
            studentCollection = new ObservableCollection<Student>(students);
            averageItemMarksCollection = new ObservableCollection<MarkAverage>(averageItemMarks);
            AddStudent = ReactiveCommand.Create(() => addStudent());
            RemoveStudent = ReactiveCommand.Create(() => removeStudents());
            UpdateAverageItemMarks();
        }
        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> RemoveStudent { get; }
        public ObservableCollection<Student> StudentCollection
        {
            get => studentCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref studentCollection, value);
            }
        }
        public ObservableCollection<MarkAverage> AverageItemMarksCollection
        {
            get => averageItemMarksCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref averageItemMarksCollection, value);
            }
        }
        public void UpdateAverageItemMarks()
        {
            foreach (var mark in AverageItemMarksCollection)
            {
                mark.markDouble = 0;
            }
            for (int i = 0; i < AverageItemMarksCollection.Count; i++)
            {
                
                for (int j = 0; j < students.Count; j++)
                {
                    Student student = students[j];
                    if (student.Marks[i].mark == -1)
                    {
                        AverageItemMarksCollection[i].markDouble = -1;
                        break;
                    }
                    AverageItemMarksCollection[i].markDouble += student.Marks[i].mark;
                }
            }
            foreach(var mark in AverageItemMarksCollection)
            {
                if(mark.markDouble != -1)
                {
                    mark.markDouble /= students.Count;
                }
                mark.MarkStr = mark.markDouble.ToString();
            }
        }
        public void UpdateAverage()
        {
            foreach (var student in studentCollection)
            {
                student.UpdateAverage();
            }
        }
        private void addStudent()
        {
            students.Add(new Student());
            StudentCollection = new ObservableCollection<Student>(students);
            UpdateAverageItemMarks();
        }
        private void removeStudents()
        {
            foreach (Student student in StudentCollection)
            {
                if (student.IsSelected)
                {
                    students.Remove(student);
                }
            }
            StudentCollection = new ObservableCollection<Student>(students);
            UpdateAverageItemMarks();
        }
        public void SaveFile(string path)
        {
            File.WriteAllText(path, "");
            List<string> fileData = new List<string>();
            foreach (Student student in StudentCollection)
            {
                fileData.Add(student.Name);
                foreach (Mark mark in student.Marks)
                {
                    fileData.Add(mark.mark.ToString());
                }
            }
            File.WriteAllLines(path, fileData);
        }
        public void LoadFile(string path)
        {
            List<Student> students = new List<Student>();

            StreamReader file = new StreamReader(path);
            try
            {
                while (!file.EndOfStream)
                {
                    List<Mark> marks = new List<Mark>();
                    string studentName = file.ReadLine();

                    for (int i = 0; i < 4; i++)
                    {
                        string mark = file.ReadLine();
                        marks.Add(new Mark(mark));
                    }

                    students.Add(new Student(studentName, marks));
                }
                file.Close();
            }
            catch
            {
                file.Close();
            }
            StudentCollection = new ObservableCollection<Student>(students);
            UpdateAverage();
            UpdateAverageItemMarks();
        }
    }
}
