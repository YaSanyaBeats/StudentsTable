using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsTable.Models
{
    public class MarkAverage : Mark
    {
        public MarkAverage(int value) : base(1){
            markDouble = value;
        }
        public double markDouble { get; set; }
        public override void UpdateColor()
        {
            if(markDouble >= 0)
            {
                if (markDouble < 1)
                {
                    Color = "#DE4848";
                }
                else if (markDouble < 1.5)
                {
                    Color = "#FFDE48";
                }
                else if (markDouble <= 2)
                {
                    Color = "#84DE48";
                }
            }
            else
            {
                Color = "White";
            }
        }
        public override string MarkStr
        {
            get
            {
                if (markDouble == -1)
                {
                    markStr = "ERROR";
                    return markStr;
                }
                markStr = markDouble.ToString();
                return markStr;
            }
            set
            {
                this.markStr = value;
                try
                {
                    markDouble = Math.Round(double.Parse(value), 3);
                    if (mark < 0 || mark > 2)
                    {
                        mark = -1;
                    }
                }
                catch
                {
                    markDouble = -1;
                }
                UpdateColor();
                NotifyPropertyChanged();
            }
        }
    }
    
}
