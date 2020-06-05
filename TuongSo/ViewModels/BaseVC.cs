using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuongSo.ViewModels
{
    public abstract class BaseVC : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public abstract class PyCalBaseVM: BaseVC 
    {
        protected int ReducePY(int? number)
        {
            return ReducePY(number?.ToString());
        }
        protected int ReducePY(string number)
        {
            var value = -1;
            if (number?.Length > 1)
            {
                return ReducePY(SumStringValue(number).ToString());
            }
            if (int.TryParse(number, out int result))
            {
                value = result;
            }
           
            return value;
        }

        protected int SumStringValue(string input)
        {
            var value = 0;
            if (!string.IsNullOrEmpty(input))
            {
                var inputNumer = int.Parse(input);

                foreach (var c in input)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        value += number;
                    }
                }
            }

            return value;
        }
        protected int CalScd(string day, string month, string year)
        {
            var acceptNumbers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 22 };

            var daySum = SumStringValue(day);
            var monthSum = SumStringValue(month);
            var yearSum = this.SumStringValue(year);
            var total = daySum + monthSum + yearSum;

            if (!acceptNumbers.Contains(total))
            {
                total = SumStringValue(total.ToString());
            }

            return total;
        }
        protected int FirtMajorYear(string day, string month, string year)
        {
            var scd = CalScd(day, month, year);
            int firstMajorYear;
            if (scd == 22)
            {
                firstMajorYear = 36 - 4;
            }
            else
            {
                firstMajorYear = 36 - scd;
            }
            return firstMajorYear;
        }

    }

}
