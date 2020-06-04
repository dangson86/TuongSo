using System;
using System.Collections.Generic;
using System.Text;

namespace TuongSo.Models
{
    public class YearResultModel
    {
        public string Year { get; set; }
        public string Age { get; set; }
        public int SumResult { get; set; }
        public YearStatus YearStatus { get; set; }
        public bool IsAMajorYear { get; set; }
        public string Remark { get; set; }
    }

    public enum YearStatus
    {
        good, bad, average
    }
}
