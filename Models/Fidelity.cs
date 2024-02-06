using Portale.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portale.Models;

public partial class Fidelity
{
    public int Id { get; set; }


    public string Number { get; set; } = null!;

    public bool? ActivePromo { get; set; }
	public string? UserId { get; set; }

	[ForeignKey("UserId")]
	public ApplicationUser User { get; set; } = null!;
}
