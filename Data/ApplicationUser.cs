using Microsoft.AspNetCore.Identity;
using Portale.Models;

namespace Portale.Data
{
	public class ApplicationUser : IdentityUser
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? EmailPec { get; set; }
		public string? Cell { get; set; }
		public string? Country { get; set; }
		public string? Cap { get; set; }
		public string? Prov { get; set; }
		public string? City { get; set; }
		public string? Address { get; set; }
		public string? Description { get; set; }
		public string? FiscalCode { get; set; }
		public string? PIva { get; set; }
		public bool? IsPrivate { get; set; }
		public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
		public virtual ICollection<UserProfileImgs> UserProfileImgs { get; set; } = new List<UserProfileImgs>();
		public virtual ICollection<CalendarAppointment> CalendarAppointment { get; set; } = new List<CalendarAppointment>();
		
		
		public virtual ICollection<Fidelity> Fidelities { get; set; } = new List<Fidelity>();
		public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
		public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

	}
}