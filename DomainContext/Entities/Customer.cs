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
        public string UserName { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Summary { get; set; }

    }
}
