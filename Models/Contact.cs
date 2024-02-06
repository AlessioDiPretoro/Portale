using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? EmailPec { get; set; }

    public string? Phone { get; set; }

    public string? Cell { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Cap { get; set; }

    public string? Prov { get; set; }

    public string? FiscalCode { get; set; }

    public virtual ICollection<CalendarAppointment> CalendarAppointments { get; set; } = new List<CalendarAppointment>();
}
