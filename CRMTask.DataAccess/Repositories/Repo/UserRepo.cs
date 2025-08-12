using CRMTask.DataAccess.Data;
using CRMTask.DataAccess.Models;
using CRMTask.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Repositories.Repo
{
    public class UserRepo : Repo<User>, IUserRepository
    {
        public UserRepo(AppDbContext context) : base(context) { }

        public async Task<List<User>> GetHCPUsersAsync()
        {
            return await _context.User.Where(u => u.IsHCP).ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
