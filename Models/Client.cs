using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Client
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

    public string? PIva { get; set; }

    public bool? IsPrivate { get; set; }

    public virtual ICollection<Fidelity> Fidelities { get; set; } = new List<Fidelity>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
