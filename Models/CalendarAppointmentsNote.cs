using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class CalendarAppointmentsNote
{
	public int Id { get; set; }

	public int? IdAppointment { get; set; }

	public string? Note { get; set; }

	public virtual CalendarAppointment? IdAppointmentNavigation { get; set; }
}