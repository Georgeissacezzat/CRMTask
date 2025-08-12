using AutoMapper;
using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.UserDTOs;
using CRMTask.DataAccess.Models;
using CRMTask.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.Serv
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepo;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepo, IMapper mapper)
		{
			_userRepo = userRepo;
			_mapper = mapper;
		}

		public async Task<List<UserListDTO>> GetAllUsersAsync()
		{
			var users = await _userRepo.GetAllAsync();
			return _mapper.Map<List<UserListDTO>>(users);
		}

		public async Task<UserBaseDTO> GetUserByIdAsync(int id)
		{
			var user = await _userRepo.GetByIdAsync(id);
			return _mapper.Map<UserBaseDTO>(user);
		}

		public async Task<UserBaseDTO> CreateUserAsync(CreateUserDTO dto)
		{
			User user = _mapper.Map<User>(source: dto);
			user.CreatedAt = DateTime.UtcNow;
			user.UpdatedAt = DateTime.UtcNow;

			var created = await _userRepo.AddAsync(user);
			return _mapper.Map<UserBaseDTO>(created);
		}

		public async Task<UserBaseDTO> UpdateUserAsync(UpdateUserDTO dto)
		{
			var user = await _userRepo.GetByIdAsync(dto.Id);
			if (user == null) return null;

			_mapper.Map(dto, user);
			user.UpdatedAt = DateTime.UtcNow;

			await _userRepo.UpdateAsync(user);
			return _mapper.Map<UserBaseDTO>(user);
		}

		public async Task<bool> DeleteUserAsync(int id)
		{
			var user = await _userRepo.GetByIdAsync(id);
			if (user == null) return false;

			await _userRepo.DeleteAsync(id);
			return true;
		}

		public async Task<List<UserListDTO>> GetHCPUsersAsync()
		{
			var hcps = await _userRepo.GetHCPUsersAsync();
			return _mapper.Map<List<UserListDTO>>(hcps);
		}

		public async Task<UserBaseDTO> GetUserByEmailAsync(string email)
		{
			var user = await _userRepo.GetByEmailAsync(email);
			return _mapper.Map<UserBaseDTO>(user);
		}
	}
}
