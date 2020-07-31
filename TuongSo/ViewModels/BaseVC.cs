using DomainContext.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TuongSo.ViewModels
{
    public abstract class PyCalBaseVM : BaseViewModel
    {
        protected List<int> NameToNumbers(string inputName)
        {
            var dic = new Dictionary<int, List<string>>();
            dic[1] = new List<string> { "a", "j", "s" };
            dic[2] = new List<string> { "b", "k", "t" };
            dic[3] = new List<string> { "c", "l", "u" };
            dic[4] = new List<string> { "d", "m", "v" };
            dic[5] = new List<string> { "e", "n", "w" };
            dic[6] = new List<string> { "f", "o", "x" };
            dic[7] = new List<string> { "g", "p", "y" };
            dic[8] = new List<string> { "h", "q", "z" };
            dic[9] = new List<string> { "i", "r" };
            var numbers = new List<int>();
            if (!string.IsNullOrEmpty(inputName))
            {
                foreach (var c in inputName)
                {
                    foreach (var list in dic)
                    {
                        if (list.Value.Contains(c.ToString(), StringComparer.OrdinalIgnoreCase))
                        {
                            numbers.Add(list.Key);
                        }
                    }
                }
            }
            
            return numbers;
        }
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




    public abstract class PyCalBaseVM<T, K> : PyCalBaseVM, IValueType<T> where K : PyCalBaseVM<T, K>, IValueType<T>, new() where T : new()
    {
        public T Model { get; set; }
        public PyCalBaseVM()
        {
            Model = System.Activator.CreateInstance<T>();
        }
    }

}
