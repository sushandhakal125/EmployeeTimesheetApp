using HRTimesheetApp.Interfaces.Repositories;
using HRTimesheetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRTimesheetApp.Persistence.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}