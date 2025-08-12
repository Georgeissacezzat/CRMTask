using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.DTOs.UserDTOs
{
	public class UserBaseDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsHCP { get; set; }
	}

}
