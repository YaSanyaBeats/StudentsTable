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
    public class Mark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Mark(int value = 0)
        {
            mark = value;
            UpdateColor();
        }
        public Mark(string str)
        {
            try
            {
                mark = int.Parse(str);
            }
            catch
            {
                mark = -1;
            }
            
            UpdateColor();
        }
        public virtual int mark { get; set; }
        internal string markStr;
        private string color;
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                this.color = value;
                NotifyPropertyChanged();
            }
        }
        public virtual void UpdateColor()
        {
            if(mark == 0)
            {
                Color = "#DE4848";
            }
            else if(mark == 1)
            {
                Color = "#FFDE48";
            }
            else if(mark == 2)
            {
                Color = "#84DE48";
            }
            else
            {
                Color = "White";
            }
        }
        public virtual string MarkStr
        {
            get {
                if (mark == -1)
                {
                    markStr = "ERROR";
                    return markStr;
                }
                markStr = mark.ToString();
                return markStr; 
            }
            set {
                this.markStr = value;
                try
                {
                    mark = int.Parse(value);
                    if (mark < 0 || mark > 2)
                    {
                        mark = -1;
                    }
                }
                catch
                {
                    mark = -1;
                }
                UpdateColor();
                NotifyPropertyChanged();
            }
        }
    }
}
