using DomainContext.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainContext.Entities
{
    public class Customer : ObservableModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName;
        public string Day;
        public string Month;
        public string Year;
        public string Summary;
        
    }
}
