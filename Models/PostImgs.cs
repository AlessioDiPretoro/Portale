using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models
{
	[Table("PostImgs")]
	public partial class PostImgs
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int PostsId { get; set; }

		public virtual Posts Posts { get; set; } = null!;
	}
}