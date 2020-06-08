using DomainContext;
using DomainContext.Entities;
using DomainContext.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public class PyCalVM : PyCalBaseVM<Customer, PyCalVM>
    {
        private readonly LocalDomainContext DomainContext;

        string _NickName;
        public string NickName
        {
            get => _NickName;
            set => _ = SetProperty(ref _NickName, value);
        }

        string _CustomerName;
        public string CustomerName
        {
            get => _CustomerName;
            set => _ = SetProperty(ref _CustomerName, value);
        }

        string _Day;
        public string Day
        {
            get => _Day;
            set => _ = SetProperty(ref _Day, value);
        }

        string _Month;
        public string Month
        {
            get => _Month;
            set => _ = SetProperty(ref _Month, value);
        }

        string _Year;
        public string Year
        {
            get => _Year;
            set => _ = SetProperty(ref _Year, value);
        }

        private bool _ShowPyramid = false;

        public bool ShowPyramid
        {
            get => _ShowPyramid;
            set => _ = SetProperty(ref _ShowPyramid, value);
        }

        string _Summary;
        public string Summary
        {
            get => _Summary;
            set => _ = SetProperty(ref _Summary, value);
        }

        ObservableCollection<PyInfo> _YearResults = new ObservableCollection<PyInfo>();
        public ObservableCollection<PyInfo> YearResults
        {
            get => _YearResults;
            set
            {
                _ = SetProperty(ref _YearResults, value);
            }
        }

        public ICommand RunCalCommand { get; }
        public IAppState AppState { get; }

        public PyCalVM()
        {
            RunCalCommand = new ActionCommand(async () =>
            {
                await CaculateResult();
            });
        }

        public PyCalVM(LocalDomainContext domainContext, IAppState appState) : this()
        {
            this.DomainContext = domainContext;
            AppState = appState;
        }

        public Task ExportToExcel(Stream stream)
        {
            return Task.Run(() =>
            {
                var prov = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();
                var wb = new Telerik.Windows.Documents.Spreadsheet.Model.Workbook();
                var mainWs = wb.Worksheets.Add();
                mainWs.Name = "Info";
                mainWs.Cells[1, 0].SetValue(CustomerName);


                mainWs.Cells[2, 0].SetValue("Ngay");
                mainWs.Cells[2, 1].SetValue("Thang");
                mainWs.Cells[2, 2].SetValue("Nam");
                mainWs.Cells[3, 0].SetValue(Day);
                mainWs.Cells[3, 1].SetValue(Month);
                mainWs.Cells[3, 2].SetValue(Year);

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

        public async Task SaveCustomerInfo()
        {
            using (var tran = await DomainContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var customerInfo = await this.DomainContext.Customers.FirstOrDefaultAsync(e => e.Id == Model.Id).ConfigureAwait(false);
                    var isNewCustomer = false;
                    if (customerInfo == null)
                    {
                        customerInfo = new Customer()
                        {
                            Id = Guid.NewGuid(),
                        };
                        isNewCustomer = true;
                    }
                    customerInfo.CustomerName = this.CustomerName;
                    customerInfo.Day = this.Day;
                    customerInfo.Month = this.Month;
                    customerInfo.Year = this.Year;
                    customerInfo.Summary = this.Summary;
                    customerInfo.NickName = this.NickName;

                    if (isNewCustomer)
                    {
                        this.DomainContext.Customers.Add(customerInfo);
                    }

                    var yearInfos = await this.DomainContext.YearResults.Where(e => e.CustomerId == customerInfo.Id).ToListAsync().ConfigureAwait(false);
                    this.DomainContext.YearResults.RemoveRange(yearInfos);

                    foreach (var item in this.YearResults)
                    {
                        this.DomainContext.YearResults.Add(new PyInfo
                        {
                            Id = Guid.NewGuid(),
                            Age = item.Age,
                            CustomerId = customerInfo.Id,
                            IsAMajorYear = item.IsAMajorYear,
                            Remark = item.Remark,
                            Year = item.Year,
                            YearStatus = item.YearStatus,
                            SumResult = item.SumResult,
                        });
                    }


                    await this.DomainContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    Model = customerInfo;
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task GetSelectedCustomer()
        {
            if (AppState?.SelectedCustomerId != null)
            {
                var c = await DomainContext.Customers.FirstOrDefaultAsync(e => e.Id == AppState.SelectedCustomerId);
                if (c != null)
                {
                    var years = await DomainContext.YearResults.Where(e => e.CustomerId == c.Id).OrderBy(e => e.Age).ToListAsync();

                    this.CustomerName = c.CustomerName;
                    this.Day = c.Day;
                    this.Month = c.Month;
                    this.Year = c.Year;
                    this.Summary = c.Summary;
                    this.NickName = c.NickName;
                    this.ShowPyramid = true;
                    this.YearResults.AddRange(years);

                    this.Model = c;
                }
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
