using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Documents.Common.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using TuongSo.ViewModels;

namespace TuongSo.Views
{
    /// <summary>
    /// Interaction logic for PyYearCalView.xaml
    /// </summary>
    public partial class PyYearCalView : UserControl
    {
        public PyCalVM Context => this.DataContext as PyCalVM;
        public PyYearCalView()
        {
            InitializeComponent();
        }
        private async void CalculateFortune(object sender, RoutedEventArgs e)
        {
            if (Context.IsValid())
            {
                await Context.CaculateResult();
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

        private async void Button_Click_Excel(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = String.Format("Excel |*.xlsx");

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var stream = dialog.OpenFile())
                    {
                        await this.Context.ExportToExcel(stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Khong the mo file", "Bao loi");
                }

            }

        }

        private async void SaveCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.Context.SaveCustomerInfo();
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to save data", "Error");
            }
            
        }
    }
}
