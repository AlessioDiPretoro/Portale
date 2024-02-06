using Portale.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models;

public partial class Invoice
{
    public int Id { get; set; }


    public string? CheckOut { get; set; }

    public DateTime Date { get; set; }

    public string? Fidelity { get; set; }

    public string? Extra { get; set; }

    public int? Discount { get; set; }
	public string? UserId { get; set; }

	[ForeignKey("UserId")]
	public ApplicationUser User { get; set; } = null!;

	public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
