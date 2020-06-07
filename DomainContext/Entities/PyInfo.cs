using DomainContext.Generics;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainContext.Entities
{
    public class PyInfo : ObservableModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Year { get; set; }
        public string Age { get; set; }
        public int SumResult { get; set; }
        public YearStatus YearStatus { get; set; }
        public bool IsAMajorYear { get; set; }
        public string Remark { get; set; }
        public Guid CustomerId { get; set; }
        public Customer CustomerInfo { get; set; }
    }

    public enum YearStatus
    {
        good, bad, average
    }
}
