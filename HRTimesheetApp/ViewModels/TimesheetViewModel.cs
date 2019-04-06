using HRTimesheetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.ViewModels
{
    public class TimesheetViewModel
    {
        public TimeSpan ClockedInTimeSpan { get; set; }
        public Timesheet Timesheet { get; set; }
        public List<Timesheet> Timesheets { get; set; }
    }
}