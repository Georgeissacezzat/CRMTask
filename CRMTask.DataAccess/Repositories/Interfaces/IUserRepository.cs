using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetHCPUsersAsync();
        Task<User> GetByEmailAsync(string email);
    }
}
