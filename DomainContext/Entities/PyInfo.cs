using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainContext.Entities
{
    public class PyInfo
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
