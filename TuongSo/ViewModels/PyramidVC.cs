using DomainContext.Entities;
using System.Collections.Generic;

namespace TuongSo.ViewModels
{
    public class PyramidVM : PyCalBaseVM
    {
        private int? _Day;
        public int? BaseDay
        {
            get => _Day;
            set
            {
                _Day = value;
                OnPropertyChanged();
            }
        }

        private int? _Month;
        public int? BaseMonth
        {
            get => _Month;
            set
            {
                _Month = value;
                OnPropertyChanged();
            }
        }

        private int? _Year;
        public int? BaseYear
        {
            get => _Year;
            set
            {
                _Year = value;
                OnPropertyChanged();
            }
        }

        private int? _SumMonthDay;
        public int? SumMonthDay
        {
            get => _SumMonthDay;
            set
            {
                _SumMonthDay = value;
                OnPropertyChanged();
            }
        }

        private int? _SumDayYear;
        public int? SumDayYear
        {
            get => _SumDayYear;
            set
            {
                _SumDayYear = value;
                OnPropertyChanged();
            }
        }

        private int? _MajorYear1;
        public int? MajorYear1
        {
            get => _MajorYear1;
            set
            {
                _MajorYear1 = value;
                OnPropertyChanged();
            }
        }

        private int? _MajorYear2;
        public int? MajorYear2
        {
            get => _MajorYear2;
            set
            {
                _MajorYear2 = value;
                OnPropertyChanged();
            }
        }
        private int? _MajorYear3;
        public int? MajorYear3
        {
            get => _MajorYear3;
            set
            {
                _MajorYear3 = value;
                OnPropertyChanged();
            }
        }
        private int? _MajorYear4;
        public int? MajorYear4
        {
            get => _MajorYear4;
            set
            {
                _MajorYear4 = value;
                OnPropertyChanged();
            }
        }

        private int? _SumSecondLevel;
        public int? SumSecondLevel
        {
            get => _SumSecondLevel;
            set
            {
                _SumSecondLevel = value;
                OnPropertyChanged();
            }
        }

        private int? _SumThirdLevel;
        public int? SumThirdLevel
        {
            get => _SumThirdLevel;
            set
            {
                _SumThirdLevel = value;
                OnPropertyChanged();
            }
        }

        public void SetValue(string day, string month, string year)
        {
            this.BaseDay = ReducePY(day);
            this.BaseMonth = ReducePY(month);
            this.BaseYear = ReducePY(year);

            this.SumDayYear = ReducePY(BaseDay + BaseYear);
            this.SumMonthDay = ReducePY(BaseMonth + BaseDay);

            this.MajorYear1 = FirtMajorYear(day, month, year);

            this.MajorYear2 = MajorYear1 + 9;
            this.MajorYear3 = MajorYear2 + 9;
            this.MajorYear4 = MajorYear3 + 9;

            this.SumSecondLevel = CalSecondAndThridLevel(SumMonthDay ?? 0, SumDayYear ?? 0);
            this.SumThirdLevel = CalSecondAndThridLevel(BaseMonth ?? 0, BaseYear ?? 0);
        }

        private int CalSecondAndThridLevel(int left, int right)
        {
            int value;
            var acceptNumbers = new List<int> { 10, 11, 22 };
            var total = left + right;
            if (acceptNumbers.Contains(total))
            {
                value = total;
            }
            else
            {
                value = ReducePY(total);
            }

            return value;
        }
    }
}
