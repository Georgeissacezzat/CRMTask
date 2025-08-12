using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRMTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		// GET: api/user
		[HttpGet]
		public async Task<ActionResult<List<UserListDTO>>> GetAll()
		{
			var users = await _userService.GetAllUsersAsync();
			return Ok(users);
		}

		// GET: api/user/{id}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserBaseDTO>> GetById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
				return NotFound();

			return Ok(user);
		}

		// GET: api/user/email/{email}
		[HttpGet("email/{email}")]
		public async Task<ActionResult<UserBaseDTO>> GetByEmail(string email)
		{
			var user = await _userService.GetUserByEmailAsync(email);
			if (user == null)
				return NotFound();

			return Ok(user);
		}

		// GET: api/user/hcp
		[HttpGet("hcp")]
		public async Task<ActionResult<List<UserListDTO>>> GetHCPUsers()
		{
			var users = await _userService.GetHCPUsersAsync();
			return Ok(users);
		}

		// POST: api/user
		[HttpPost]
		public async Task<ActionResult<UserBaseDTO>> Create(CreateUserDTO dto)
		{
			var createdUser = await _userService.CreateUserAsync(dto);
			return Ok(User);
		}

		// PUT: api/user/{id}
		[HttpPut("{id:int}")]
		public async Task<ActionResult<UserBaseDTO>> Update(int id, UpdateUserDTO dto)
		{
			if (id != dto.Id)
				return BadRequest("ID in route does not match ID in body.");

			var updatedUser = await _userService.UpdateUserAsync(dto);
			if (updatedUser == null)
				return NotFound();

			return Ok(updatedUser);
		}

		// DELETE: api/user/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _userService.DeleteUserAsync(id);
			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}
