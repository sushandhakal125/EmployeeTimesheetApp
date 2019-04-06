using HRTimesheetApp.Models;
using HRTimesheetApp.Persistence;
using HRTimesheetApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRTimesheetApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public ReportController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }
        public ActionResult Index(string id = null, DateTime? StartDate = null, DateTime? EndDate=null)
        {
            string UserId;
            IOrderedEnumerable<Timesheet> timesheets;
            if (id == null)
                UserId = User.Identity.GetUserId();
            else
                UserId = id;
            if(StartDate != null && EndDate != null)
            {
                timesheets = _unitOfWork.Timesheets.GetAll().Where(x => x.UserId.Equals(UserId) && x.ClockInTime.Day >= StartDate.Value.Day && x.ClockOutTime.Day <= EndDate.Value.Day).ToList().OrderBy(o => o.TimesheetId);
            }
            else
                timesheets = _unitOfWork.Timesheets.GetAll().Where(x => x.UserId.Equals(UserId) && x.ClockInTime.Day == DateTime.Now.Day && x.ClockOutTime.Day == DateTime.Now.Day).ToList().OrderBy(o => o.TimesheetId);
            string firstClockIn, lastClockOut;
            if(timesheets == null || !timesheets.Any())
            {
                firstClockIn = "";
                lastClockOut = "";
            }
            else
            {
                firstClockIn = timesheets.FirstOrDefault().ClockInTime.ToString("yyyy-MM-dd h:mm:ss tt");
                lastClockOut = timesheets.LastOrDefault().ClockOutTime.ToString("yyyy-MM-dd h:mm:ss tt");
            }
            
            var clockedInTimeSpan = new TimeSpan();
            foreach (var ts in timesheets)
            {
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = (ts.ClockOutTime - ts.ClockInTime);
                clockedInTimeSpan = timeSpan + clockedInTimeSpan;
            }
            var reportViewModel = new ReportViewModel
            {
                ClockedInTimeSpan = clockedInTimeSpan,
                Timesheets = timesheets,
                FirstClockIn = firstClockIn,
                LastClockOut = lastClockOut,
                ApplicationUsers = _unitOfWork.Users.GetAll()
            };
            return View(reportViewModel);
        }
        public ActionResult SelectUser(string id)
        {
            return RedirectToAction("Index", new { id });
        }
        public ActionResult SearchByDate(DateTime StartDate, DateTime EndDate)
        {
            return RedirectToAction("Index", new { StartDate, EndDate });
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}