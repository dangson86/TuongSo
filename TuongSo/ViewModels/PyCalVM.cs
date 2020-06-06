using DomainContext;
using DomainContext.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using TuongSo.Models;

namespace TuongSo.ViewModels
{
    public class PyCalVM : PyCalBaseVM<User, PyCalVM>
    {
        private readonly LocalDomainContext DomainContext;

        public string UserName
        {
            get => Model.UserName;
            set => _ = SetProperty(ref Model.UserName, value);
        }

        public string Day
        {
            get => Model.Day;
            set => _ = SetProperty(ref Model.Day, value);
        }


        public string Month
        {
            get => Model.Month;
            set => _ = SetProperty(ref Model.Month, value);
        }

        public string Year
        {
            get => Model.Year;
            set => _ = SetProperty(ref Model.Year, value);
        }

        private bool _ShowPyramid = false;

        public bool ShowPyramid
        {
            get => _ShowPyramid;
            set => _ = SetProperty(ref _ShowPyramid, value);
        }


        public string Summary
        {
            get => Model.Summary;
            set => _ = SetProperty(ref Model.Summary, value);
        }

        ObservableCollection<PyInfo> _YearResults = new ObservableCollection<PyInfo>();
        public ObservableCollection<PyInfo> YearResults
        {
            get => _YearResults;
            set
            {
                _ = SetProperty(ref _YearResults, value);
                Model.YearInfos = value.ToList();
            }
        }

        public ICommand RunCalCommand { get; }

        public PyCalVM()
        {
            RunCalCommand = new ActionCommand(async () =>
            {
                await CaculateResult();
            });
        }
        public PyCalVM(LocalDomainContext domainContext) : this()
        {
            this.DomainContext = domainContext;
        }

        public Task ExportToExcel(Stream stream)
        {
            return Task.Run(() =>
            {
                var prov = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();
                var wb = new Telerik.Windows.Documents.Spreadsheet.Model.Workbook();
                var mainWs = wb.Worksheets.Add();
                mainWs.Name = "Info";
                mainWs.Cells[1, 0].SetValue(UserName);


                mainWs.Cells[2, 0].SetValue("Ngay");
                mainWs.Cells[2, 1].SetValue("Thang");
                mainWs.Cells[2, 2].SetValue("Nam");
                mainWs.Cells[3, 0].SetValue(Day);
                mainWs.Cells[3, 1].SetValue(Month);
                mainWs.Cells[4, 2].SetValue(Year);

                mainWs.Cells[6, 0].SetValue("Chu thich");
                mainWs.Cells[7, 0].SetValue(Summary);

                var pyWs = wb.Worksheets.Add();
                pyWs.Name = "PY";
                pyWs.Cells[0, 0].SetValue("Tuoi");
                pyWs.Cells[0, 1].SetValue("Nam");
                pyWs.Cells[0, 2].SetValue("Tong");
                pyWs.Cells[0, 3].SetValue("Van Mang");
                pyWs.Cells[0, 5].SetValue("Chu Thich");
                foreach (var (item, row) in YearResults.Select((e, i) => (e, i)))
                {
                    pyWs.Cells[row + 1, 0].SetValue(item.Age);
                    pyWs.Cells[row + 1, 1].SetValue(item.Year);
                    pyWs.Cells[row + 1, 2].SetValue(item.SumResult);
                    pyWs.Cells[row + 1, 3].SetValue(item.YearStatus.ToString());

                    if (item.IsAMajorYear)
                    {
                        var cell = pyWs.Cells[row + 1, 4];

                        cell.SetValue("P");
                        cell.SetForeColor(Telerik.Documents.Common.Model.ThemableColor.FromArgb(100, 255, 0, 0));
                    }

                    pyWs.Cells[row + 1, 5].SetValue(item.Remark);
                }


                prov.Export(wb, stream);
            });
        }

        public async Task CaculateResult(int years = 100)
        {
            YearResults?.Clear();
            var tempList = new List<PyInfo>();
            await Task.Run(() =>
            {
                int yearInput = int.Parse(this.Year);

                var scd = CalScd(Day, Month, Year);
                var firstMajorYear = FirtMajorYear(Day, Month, Year);
                var secondMajorYear = firstMajorYear + 9;
                var thirdMajorYear = secondMajorYear + 9;
                var forthMajorYear = thirdMajorYear + 9;

                for (int i = 0; i < years; i++)
                {
                    var currentYear = yearInput + i;
                    var yearString = currentYear.ToString();
                    var tempSum = SumStringValue(this.Day) + SumStringValue(this.Month) + SumStringValue(yearString);
                    tempSum = SumStringValue(tempSum.ToString());
                    if (i == 0)
                    {
                        tempList.Add(new PyInfo()
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
                        tempList.Add(new PyInfo()
                        {
                            Age = (i).ToString(),
                            Year = yearString,
                            SumResult = ReducePY(tempSum.ToString()),
                            YearStatus = GetYearStatus(tempSum),
                            IsAMajorYear = i == firstMajorYear || i == secondMajorYear || i == thirdMajorYear || i == forthMajorYear,
                        });
                    }
                }
            });
            YearResults.AddRange(tempList);
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
