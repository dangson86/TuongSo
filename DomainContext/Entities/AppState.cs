using DomainContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainContext.Entities
{
    public class AppState : IAppState
    {
        public Guid? SelectedCustomerId { get; set; }
    }
}
