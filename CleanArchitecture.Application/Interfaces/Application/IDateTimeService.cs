using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces.Application
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
