using HRTimesheetApp.Models;
using HRTimesheetApp.Persistence;
using HRTimesheetApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HRTimesheetApp.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public TimesheetController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var timesheets = _unitOfWork.Timesheets.GetAll().Where(x => x.UserId.Equals(UserId) && x.ClockInTime.Day == DateTime.Now.Day && x.ClockOutTime.Day == DateTime.Now.Day).ToList();
            var clockedInTimeSpan = new TimeSpan();
            foreach (var ts in timesheets)
            {
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = (ts.ClockOutTime - ts.ClockInTime);
                clockedInTimeSpan = timeSpan + clockedInTimeSpan;
            }
            var timesheet = new Timesheet();
            timesheet.UserId = User.Identity.GetUserId();
            var timesheetViewModel = new TimesheetViewModel
            {
                ClockedInTimeSpan = clockedInTimeSpan,
                Timesheets = timesheets,
                Timesheet = timesheet
            };
            return View(timesheetViewModel);
        }
        [HttpPost]
        public JsonResult PostDate(int hour, int minute,
                                                int seconds, string comment)
        {
            var clockOutDate = DateTime.Now;
            var clockInDate = DateTime.Now.Add(new TimeSpan(-hour,-minute,-seconds));
            if (hour >= 8)
                clockOutDate = clockInDate.Add(new TimeSpan(8, 0, 0));
            var timesheet = new Timesheet();
            timesheet.ClockInTime = clockInDate;
            timesheet.ClockOutTime = clockOutDate;
            timesheet.Comment = comment;
            timesheet.UserId = User.Identity.GetUserId();
            _unitOfWork.Timesheets.Add(timesheet);
            _unitOfWork.Complete();
            var clockedInTimeSpan = new TimeSpan();
            foreach(var ts in _unitOfWork.Timesheets.GetAll().Where(x => x.ClockInTime.Day == DateTime.Now.Day && x.ClockOutTime.Day == DateTime.Now.Day))
            {
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = (ts.ClockOutTime - ts.ClockInTime);
                clockedInTimeSpan = timeSpan + clockedInTimeSpan;
            }
            var timesheetViewModel = new TimesheetViewModel
            {
                ClockedInTimeSpan = clockedInTimeSpan,
                Timesheet = timesheet
            };
            return Json(timesheetViewModel);
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}