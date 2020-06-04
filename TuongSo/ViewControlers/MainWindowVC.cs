using System;
using System.Collections.ObjectModel;
using TuongSo.Models;

namespace TuongSo.ViewControlers
{
    public class MainWindowVC : TuongSoBaseVC
    {
        private string _Day = "02";
        public string Day
        {
            get => _Day;
            set
            {
                _Day = value;
                OnPropertyChanged();
            }
        }

        private string _Month = "03";

        public string Month
        {
            get => _Month;
            set
            {
                _Month = value;
                OnPropertyChanged();
            }
        }
        private string _Year = "1986";

        public string Year
        {
            get => _Year;
            set
            {

                _Year = value;
                OnPropertyChanged();
            }
        }

        private bool _ShowPyramid = false;

        public bool ShowPyramid
        {
            get => _ShowPyramid;
            set
            {
                _ShowPyramid = value;
                OnPropertyChanged();
            }
        }

        private string _Summary;

        public string Summary
        {
            get => _Summary;
            set
            {
                _Summary = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<YearResultModel> YearResults { get; set; } = new ObservableCollection<YearResultModel>();

        public void CaculateResult()
        {
            int yearInput = int.Parse(this.Year);

            YearResults.Clear();

            var scd = CalScd(Day, Month, Year);
            var firstMajorYear = FirtMajorYear(Day, Month, Year);
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
                    YearResults.Add(new YearResultModel()
                    {
                        Age = (i).ToString(),
                        Year = yearString,
                        SumResult = scd,
                        YearStatus = GetYearStatus(tempSum),
                        IsAMajorYear = i == firstMajorYear || i == secondMajorYear || i == thirdMajorYear || i == forthMajorYear,
                    });
                }
                else
                {
                    YearResults.Add(new YearResultModel()
                    {
                        Age = (i).ToString(),
                        Year = yearString,
                        SumResult = ReducePY(tempSum.ToString()),
                        YearStatus = GetYearStatus(tempSum),
                        IsAMajorYear = i == firstMajorYear || i == secondMajorYear || i == thirdMajorYear || i == forthMajorYear,
                    });
                }
            }
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
