using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class SerialCode
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public string? SerialCode1 { get; set; }

    public bool? IsAvailabe { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
