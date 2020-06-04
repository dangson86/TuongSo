using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using TuongSo.Models;

namespace TuongSo.ViewControlers
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
    public class MainWindowVC : BaseVC
    {
        private string _Day = "02";
        public string Day
        {
            get { return _Day; }
            set
            {
                _Day = value;
                OnPropertyChanged();
            }
        }



        private string _Month = "03";

        public string Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                OnPropertyChanged();
            }
        }
        private string _Year = "1986";

        public string Year
        {
            get { return _Year; }
            set
            {

                _Year = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<YearResultModel> YearResults { get; set; } = new ObservableCollection<YearResultModel>();
        public void CaculateResult()
        {
            int yearInput = int.Parse(this.Year);

            YearResults.Clear();

            var scd = CalScd(Day, Month, Year);
            var firstMajorYear = 0;
            if (scd == 22)
            {
                firstMajorYear = 36 - 4;

            }
            else
            {
                firstMajorYear = 36 - scd;
            }

            var secondMajorYear = firstMajorYear + 9;
            var thirdMajorYear = secondMajorYear + 9;
            var forthMajorYear = thirdMajorYear + 9;

            for (int i = 0; i < 100; i++)
            {
                var currentYear = yearInput + i;
                var yearString = currentYear.ToString();
                var tempSum = SumStringValue(this.Day) + SumStringValue(this.Month) + SumStringValue(yearString);
                tempSum = SumStringValue(tempSum.ToString());
                if (i == 0)
                {
                    YearResults.Add(new Models.YearResultModel()
                    {
                        Age = (i).ToString(),
                        Year = yearString,
                        SumResult = scd,
                        YearStatus = GetYearStatus(tempSum),
                        IsAMajorYear = i == firstMajorYear || i == secondMajorYear || i == thirdMajorYear || i == forthMajorYear
                    });
                }
                else
                {
                    YearResults.Add(new Models.YearResultModel()
                    {
                        Age = (i).ToString(),
                        Year = yearString,
                        SumResult = ReducePY(tempSum.ToString()),
                        YearStatus = GetYearStatus(tempSum),
                        IsAMajorYear = i == firstMajorYear || i == secondMajorYear || i == thirdMajorYear || i == forthMajorYear
                    });
                }
            }
        }
        private int ReducePY(string number)
        {
            if (number.Length > 1)
            {
                return ReducePY(SumStringValue(number).ToString());
            }
            return int.Parse(number);
        }

        private YearStatus GetYearStatus(int py)
        {
            if (py == 9 || py == 1)
            {
                return YearStatus.good;
            }
            else if (py == 4 || py == 7)
            {
                return YearStatus.bad;
            }
            else
            {
                return YearStatus.average;
            }
        }

        private int CalScd(string day, string month, string year)
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

        private int SumStringValue(string input)
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
        public bool IsValid()
        {
            try
            {
                int.Parse(Day);
                int.Parse(Month);
                int.Parse(Year);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
