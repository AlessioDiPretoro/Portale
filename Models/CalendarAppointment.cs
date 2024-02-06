using Portale.Data;
using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class CalendarAppointment
{
	public int Id { get; set; }

	public string? IdOwner { get; set; }

	public int? IdContact { get; set; }

	public DateOnly? Date { get; set; }

	public string? Title { get; set; }

	public string? Description { get; set; }

	public virtual ICollection<CalendarAppointmentsNote> CalendarAppointmentsNotes { get; set; } = new List<CalendarAppointmentsNote>();

	public virtual Contact? IdContactNavigation { get; set; }
	public virtual ApplicationUser? IdOwnerNavigation { get; set; }
}