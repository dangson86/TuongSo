using System;
using System.Collections.Generic;
using System.Text;

namespace DomainContext.Interfaces
{
    public interface IAppState
    {
        Guid? SelectedCustomerId { get; set; }
    }
    public interface IAppEvents
    { }
}
