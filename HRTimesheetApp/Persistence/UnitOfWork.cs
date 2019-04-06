using HRTimesheetApp.Interfaces;
using HRTimesheetApp.Interfaces.Repositories;
using HRTimesheetApp.Models;
using HRTimesheetApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Timesheets = new TimesheetRepository(_context);
            Users = new UserRepository(_context);
        }

        public ITimesheetRepository Timesheets { get; private set; }
        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}