using DomainContext.Generics;
using System.Collections.Generic;

namespace TuongSo.ViewModels
{
    public abstract class PyCalBaseVM : BaseViewModel
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


    

    public abstract class PyCalBaseVM<T, K> : PyCalBaseVM, IValueType<T> where K : PyCalBaseVM<T, K>, IValueType<T>, new() where T:new()
    {
        public T Model { get; set; }
        public PyCalBaseVM()
        {
            Model = System.Activator.CreateInstance<T>();
        }
    }

}
