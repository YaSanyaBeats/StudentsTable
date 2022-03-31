using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace StudentsTable.Models
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Student()
        {
            this.Name = "Default Name";
            this.marks = new Mark[] { new Mark(1), new Mark(2), new Mark(1), new Mark(0) };
        }
        public Student(string name, List<Mark> marksArray)
        {
            this.Name = name;
            this.marks = new Mark[4];
            for (int i = 0; i < 4; i++)
            {
                this.marks[i] = marksArray[i];
            }
        }
        public bool IsSelected { get; set; }
        private Mark[] marks;
        public Mark[] Marks
        {
            get {
                UpdateAverage();
                return this.marks;
            }
            set {
                this.marks = value;
            }
        }
        public string Name { get; set; }
        private string average;
        public string Average
        {
            get
            {
                return average;
            }
            set
            {
                this.average = value;
                NotifyPropertyChanged();
            }
        }
        private string averageColor;
        public string AverageColor
        {
            get
            {
                return averageColor;
            }
            set
            {
                this.averageColor = value;
                NotifyPropertyChanged();
            }
        }
        public void UpdateAverage()
        {
            double average = 0;
            double count = 0;
            bool withError = false;
            foreach (var mark in this.marks)
            {
                if(mark.mark == -1)
                {
                    Average = "ERROR";
                    withError = true;
                    break;
                }
                average += mark.mark;
                count++;
            }
            double result = average / count;
            if (!withError)
            {
                if (result < 1)
                {
                    AverageColor = "#DE4848";
                }
                else if (result < 1.5)
                {
                    AverageColor = "#FFDE48";
                }
                else if (result <= 2)
                {
                    AverageColor = "#84DE48";
                }
                Average = result.ToString();
            }
            else
            {
                AverageColor = "White";
            }
        }
    }
}
