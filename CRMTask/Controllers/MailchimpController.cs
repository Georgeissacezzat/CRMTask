using CRMTask.Business.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CRMTask.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class MailchimpController : ControllerBase
	{
		private readonly IMailchimpService _mailchimpService;

		public MailchimpController(IMailchimpService mailchimpService)
		{
			_mailchimpService = mailchimpService;
		}

		/// <summary>
		/// Adds a user to the Mailchimp list
		/// </summary>
		/// <param name="request">User information to add to the list</param>
		/// <returns>Success status</returns>
		[HttpPost("add-user-to-list")]
		public async Task<IActionResult> AddUserToList([FromBody] AddUserToListRequest request)
		{
			try
			{
				// Validate required fields
				if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.UserEmail))
				{
					return BadRequest(new
					{
						success = false,
						message = "UserId and UserEmail are required"
					});
				}

				// Call service method
				var result = await _mailchimpService.AddUserToListAsync(
					request.UserId,
					request.UserEmail,
					request.FirstName,
					request.LastName,
					request.ListId
				);

				// Return response
				return Ok(new
				{
					success = result,
					message = result ? "User added to list successfully" : "Failed to add user to list",
					data = new
					{
						userId = request.UserId,
						email = request.UserEmail,
						firstName = request.FirstName,
						lastName = request.LastName
					}
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new
				{
					success = false,
					message = $"An error occurred: {ex.Message}"
				});
			}
		}

		/// <summary>
		/// Sends a template to a specific user
		/// </summary>
		/// <param name="request">Template and user information</param>
		/// <returns>Success status</returns>
		[HttpPost("send-template-to-user")]
		public async Task<IActionResult> SendTemplateToUser([FromBody] SendTemplateToUserRequest request)
		{
			try
			{
				// Validate required fields
				if (string.IsNullOrEmpty(request.TemplateId) ||
					string.IsNullOrEmpty(request.UserId) ||
					string.IsNullOrEmpty(request.UserEmail))
				{
					return BadRequest(new
					{
						success = false,
						message = "TemplateId, UserId, and UserEmail are required"
					});
				}

				// Call service method
				var result = await _mailchimpService.SendTemplateToUserAsync(
					request.TemplateId,
					request.UserId,
					request.UserEmail,
					request.Subject,
					request.MergeFields
				);

				// Return response
				return Ok(new
				{
					success = result,
					message = result ? "Template sent to user successfully" : "Failed to send template to user",
					data = new
					{
						templateId = request.TemplateId,
						userId = request.UserId,
						email = request.UserEmail,
						subject = request.Subject
					}
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new
				{
					success = false,
					message = $"An error occurred: {ex.Message}"
				});
			}
		}
	}

	#region Request DTOs
	/// <summary>
	/// Request model for adding a user to Mailchimp list
	/// </summary>
	public class AddUserToListRequest
	{
		/// <summary>
		/// Unique user identifier (required)
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// User's email address (required)
		/// </summary>
		public string UserEmail { get; set; }

		/// <summary>
		/// User's first name (optional)
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// User's last name (optional)
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Specific Mailchimp list ID (optional - uses default if not provided)
		/// </summary>
		public string ListId { get; set; }
	}

	/// <summary>
	/// Request model for sending template to user
	/// </summary>
	public class SendTemplateToUserRequest
	{
		/// <summary>
		/// Mailchimp template ID (required)
		/// </summary>
		public string TemplateId { get; set; }

		/// <summary>
		/// Unique user identifier (required)
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// User's email address (required)
		/// </summary>
		public string UserEmail { get; set; }

		/// <summary>
		/// Custom email subject line (optional)
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Merge fields for email personalization (optional)
		/// </summary>
		public Dictionary<string, string> MergeFields { get; set; }
	}
	#endregion
}
