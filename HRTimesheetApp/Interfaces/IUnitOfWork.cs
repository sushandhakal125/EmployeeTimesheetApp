using HRTimesheetApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITimesheetRepository Timesheets { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}