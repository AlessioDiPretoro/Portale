using Portale.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
	[Table("UserProfileImgs")]
	public partial class UserProfileImgs
	{
		public int Id { get; set; }
		public string FileName { get; set; }
		public string? UserId { get; set; }

		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; } = null!;
	}
}