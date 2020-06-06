using DomainContext;
using DomainContext.Entities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace TuongSo.ViewModels
{
    public class PyCalVM : PyCalBaseVM
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
        private readonly LocalDomainContext domainContext;

        public string Summary
        {
            get => _Summary;
            set
            {
                _Summary = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PyInfo> YearResults { get; set; } = new ObservableCollection<PyInfo>();

        public PyCalVM()
        {

        }
        public PyCalVM(LocalDomainContext domainContext)
        {
            this.domainContext = domainContext;
        }

        public void ExportToExcel(Stream stream)
        {
            var prov = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();
            var wb = new Telerik.Windows.Documents.Spreadsheet.Model.Workbook();
            var mainWs = wb.Worksheets.Add();
            mainWs.Name = "Info";

            mainWs.Cells[1, 0].SetValue("Ngay");
            mainWs.Cells[1, 1].SetValue("Thang");
            mainWs.Cells[1, 2].SetValue("Nam");
            mainWs.Cells[2, 0].SetValue(Day);
            mainWs.Cells[2, 1].SetValue(Month);
            mainWs.Cells[2, 2].SetValue(Year);

            mainWs.Cells[4, 0].SetValue("Chu thich");
            mainWs.Cells[5, 0].SetValue(Summary);




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
        }

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
                    YearResults.Add(new PyInfo()
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
                    YearResults.Add(new PyInfo()
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
