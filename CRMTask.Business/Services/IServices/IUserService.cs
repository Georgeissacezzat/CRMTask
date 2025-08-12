using CRMTask.DataAccess.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.IServices
{
	public interface IUserService
	{
		Task<List<UserListDTO>> GetAllUsersAsync();
		Task<UserBaseDTO> GetUserByIdAsync(int id);
		Task<UserBaseDTO> CreateUserAsync(CreateUserDTO dto);
		Task<UserBaseDTO> UpdateUserAsync(UpdateUserDTO dto);
		Task<bool> DeleteUserAsync(int id);

		// Extra business-specific methods
		Task<List<UserListDTO>> GetHCPUsersAsync();
		Task<UserBaseDTO> GetUserByEmailAsync(string email);
	}
}
