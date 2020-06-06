using DomainContext.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainContext.Entities
{
    public class User : ObservableModel
    {
        public string UserName;
        public string Day;
        public string Month;
        public string Year;
        public string Summary;
        public List<PyInfo> YearInfos = new List<PyInfo>();
    }

    public class PyInfo: ObservableModel
    {
        [Key]
        public Guid Id { get; set; }
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
