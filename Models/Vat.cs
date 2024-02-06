using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Vat
{
    public int Id { get; set; }

    public string? VatName { get; set; }

    public int? VatValue { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
