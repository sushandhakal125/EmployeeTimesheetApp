using HRTimesheetApp.Interfaces.Repositories;
using HRTimesheetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.Persistence.Repositories
{
    public class TimesheetRepository : Repository<Timesheet>, ITimesheetRepository
    {
        public TimesheetRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}