using HRTimesheetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.ViewModels
{
    public class ReportViewModel
    {
        public TimeSpan ClockedInTimeSpan { get; set; }
        public IOrderedEnumerable<Timesheet> Timesheets { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public string FirstClockIn { get; set; }
        public string LastClockOut { get; set; }
    }
}