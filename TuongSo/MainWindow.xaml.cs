using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Documents.Common.Model;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using TuongSo.ViewControlers;

namespace TuongSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVC Context => this.DataContext as MainWindowVC;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateFortune(object sender, RoutedEventArgs e)
        {
            if (Context.IsValid())
            {
                Context.CaculateResult();
                Context.ShowPyramid = true;
                this.pyramidView.SetBaseValue();
            }
            else
            {
                MessageBox.Show("Thong tin khong dung", "Bao loi");
            }
        }
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Button_Click_Excel(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = String.Format("Excel |*.xlsx");

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var stream = dialog.OpenFile())
                    {
                        var prov = new XlsxFormatProvider();
                        var wb = new Telerik.Windows.Documents.Spreadsheet.Model.Workbook();
                        var mainWs = wb.Worksheets.Add();
                        mainWs.Name = "Info";

                        mainWs.Cells[1, 0].SetValue("Ngay");
                        mainWs.Cells[1, 1].SetValue("Thang");
                        mainWs.Cells[1, 2].SetValue("Nam");
                        mainWs.Cells[2, 0].SetValue(Context.Day);
                        mainWs.Cells[2, 1].SetValue(Context.Month);
                        mainWs.Cells[2, 2].SetValue(Context.Year);

                        mainWs.Cells[4, 0].SetValue("Chu thich");
                        mainWs.Cells[5, 0].SetValue(Context.Summary);




                        var pyWs = wb.Worksheets.Add();
                        pyWs.Name = "PY";
                        pyWs.Cells[0, 0].SetValue("Tuoi");
                        pyWs.Cells[0, 1].SetValue("Nam");
                        pyWs.Cells[0, 2].SetValue("Tong");
                        pyWs.Cells[0, 3].SetValue("Van Mang");
                        pyWs.Cells[0, 5].SetValue("Chu Thich");
                        foreach (var (item, row) in Context.YearResults.Select((e, i) => (e, i)))
                        {
                            pyWs.Cells[row + 1, 0].SetValue(item.Age);
                            pyWs.Cells[row + 1, 1].SetValue(item.Year);
                            pyWs.Cells[row + 1, 2].SetValue(item.SumResult);
                            pyWs.Cells[row + 1, 3].SetValue(item.YearStatus.ToString());

                            if (item.IsAMajorYear)
                            {
                                var cell = pyWs.Cells[row + 1, 4];
                              
                                cell.SetValue("P");
                                cell.SetForeColor(ThemableColor.FromArgb(100, 255, 0, 0));
                            }

                            pyWs.Cells[row + 1, 5].SetValue(item.Remark);
                        }


                        prov.Export(wb, stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Khong the mo file", "Bao loi");
                }

            }

        }
    }
}
