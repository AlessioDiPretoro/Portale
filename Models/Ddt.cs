using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Ddt
{
    public int Id { get; set; }

    public int DocumentTypeId { get; set; }

    public int SupplierId { get; set; }

    public DateTime? Date { get; set; }

    public string? Number { get; set; }

    public string? Note { get; set; }

    public int? Discount { get; set; }

    public virtual ICollection<DdtDetail> DdtDetails { get; set; } = new List<DdtDetail>();

    public virtual DocumentType DocumentType { get; set; } = null!;
}
