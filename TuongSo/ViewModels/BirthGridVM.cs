using DomainContext.Entities;
using DomainContext.Generics;
using System.Collections.Generic;
using System.Linq;

namespace TuongSo.ViewModels
{
    public class SlotVM : BaseViewModel
    {
        private string _NameNumber;

        public string NameNumber
        {
            get => _NameNumber;
            set => SetProperty(ref _NameNumber, value);
        }
        private string _BirthNumber;

        public string BirthNumber
        {
            get => _BirthNumber;
            set => SetProperty(ref _BirthNumber, value);
        }

    }
    public class BirthGridVM : PyCalBaseVM
    {
        private SlotVM _Slot1;

        public SlotVM Slot1
        {
            get => _Slot1;
            set => SetProperty(ref _Slot1, value);
        }
        private SlotVM _Slot2;

        public SlotVM Slot2
        {
            get => _Slot2;
            set => SetProperty(ref _Slot2, value);
        }

        private SlotVM _Slot3;

        public SlotVM Slot3
        {
            get => _Slot3;
            set => SetProperty(ref _Slot3, value);
        }

        private SlotVM _Slot4;

        public SlotVM Slot4
        {
            get => _Slot4;
            set => SetProperty(ref _Slot4, value);
        }

        private SlotVM _Slot5;

        public SlotVM Slot5
        {
            get => _Slot5;
            set => SetProperty(ref _Slot5, value);
        }

        private SlotVM _Slot6;

        public SlotVM Slot6
        {
            get => _Slot6;
            set => SetProperty(ref _Slot6, value);
        }

        private SlotVM _Slot7;

        public SlotVM Slot7
        {
            get => _Slot7;
            set => SetProperty(ref _Slot7, value);
        }

        private SlotVM _Slot8;

        public SlotVM Slot8
        {
            get => _Slot8;
            set => SetProperty(ref _Slot8, value);
        }

        private SlotVM _Slot9;

        public SlotVM Slot9
        {
            get => _Slot9;
            set => SetProperty(ref _Slot9, value);
        }



        private Customer Customer;

        public void SetValue(string customerName, string day, string month, string year)
        {
            this.Customer = new Customer()
            {
                Day = day,
                Month = month,
                UserName = customerName,
                Year = year
            };
            this.RefreshGrid();
        }
        public void RefreshGrid()
        {
            var nameToNumber = NameToNumbers(this.Customer.UserName);
            var nameNumber = OrderIntoGroup(nameToNumber.ToArray());

            var groups = OrderIntoGroup(this.Customer.Day, this.Customer.Month, this.Customer.Year);
            var birthNumber = ReduceNumberSlotList(groups.ToArray());

            Slot1 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot1),
                BirthNumber = string.Join(',', birthNumber.Slot1),
            };

            Slot2 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot2),
                BirthNumber = string.Join(',', birthNumber.Slot2),
            };

            Slot3 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot3),
                BirthNumber = string.Join(',', birthNumber.Slot3),
            };

            Slot4 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot4),
                BirthNumber = string.Join(',', birthNumber.Slot4),
            };

            Slot5 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot5),
                BirthNumber = string.Join(',', birthNumber.Slot5),
            };

            Slot6 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot6),
                BirthNumber = string.Join(',', birthNumber.Slot6),
            };

            Slot7 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot7),
                BirthNumber = string.Join(',', birthNumber.Slot7),
            };

            Slot8 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot8),
                BirthNumber = string.Join(',', birthNumber.Slot8),
            };

            Slot9 = new SlotVM
            {
                NameNumber = string.Join(',', nameNumber.Slot9),
                BirthNumber = string.Join(',', birthNumber.Slot9),
            };
        }

        private List<NumberSlots> OrderIntoGroup(params string[] inputs)
        {
            var list = new List<NumberSlots>();
            foreach (var input in inputs)
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var numberList = input.Select(c => int.TryParse(c.ToString(), out int number) ? number : int.MinValue).Where(e => e != int.MinValue);
                    var groups = OrderIntoGroup(numberList.ToArray());
                    list.Add(groups);
                }
            }
            return list;
        }
        private NumberSlots ReduceNumberSlotList(params NumberSlots[] inputs)
        {
            return inputs.Aggregate(new NumberSlots(), (acc, current) =>
            {
                acc.Slot1.AddRange(current.Slot1);
                acc.Slot2.AddRange(current.Slot2);
                acc.Slot3.AddRange(current.Slot3);
                acc.Slot4.AddRange(current.Slot4);
                acc.Slot5.AddRange(current.Slot5);
                acc.Slot6.AddRange(current.Slot6);
                acc.Slot7.AddRange(current.Slot7);
                acc.Slot8.AddRange(current.Slot8);
                acc.Slot9.AddRange(current.Slot9);

                return acc;
            });
        }

        private NumberSlots OrderIntoGroup(params int[] inputs)
        {
            var temp = new NumberSlots();

            foreach (var number in inputs)
            {
                switch (number)
                {
                    case 1:
                        temp.Slot1.Add(number);
                        break;
                    case 2:
                        temp.Slot2.Add(number);
                        break;
                    case 3:
                        temp.Slot3.Add(number);
                        break;
                    case 4:
                        temp.Slot4.Add(number);
                        break;
                    case 5:
                        temp.Slot5.Add(number);
                        break;
                    case 6:
                        temp.Slot6.Add(number);
                        break;
                    case 7:
                        temp.Slot7.Add(number);
                        break;
                    case 8:
                        temp.Slot8.Add(number);
                        break;
                    case 9:
                        temp.Slot9.Add(number);
                        break;
                    default:
                        break;

                }
            }
            return temp;
        }

        private class NumberSlots
        {
            internal List<int> Slot1 { get; set; } = new List<int>();
            internal List<int> Slot2 { get; set; } = new List<int>();
            internal List<int> Slot3 { get; set; } = new List<int>();
            internal List<int> Slot4 { get; set; } = new List<int>();
            internal List<int> Slot5 { get; set; } = new List<int>();
            internal List<int> Slot6 { get; set; } = new List<int>();
            internal List<int> Slot7 { get; set; } = new List<int>();
            internal List<int> Slot8 { get; set; } = new List<int>();
            internal List<int> Slot9 { get; set; } = new List<int>();
        }
    }
}
