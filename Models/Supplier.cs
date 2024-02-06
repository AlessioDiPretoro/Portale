using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Cap { get; set; }

    public string? Prov { get; set; }

    public string? PIva { get; set; }

    public string? Email { get; set; }

    public string? EmailPec { get; set; }

    public string? PhoneDefault { get; set; }

    public string? PhoneSecondary { get; set; }

    public string? Fax { get; set; }

    public string? Logo { get; set; }

    public int? ProductId { get; set; }

    public virtual ICollection<CategorySupplier> CategorySuppliers { get; set; } = new List<CategorySupplier>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<SupplierContactRel> SupplierContactRels { get; set; } = new List<SupplierContactRel>();

    public virtual ICollection<SupplierNote> SupplierNotes { get; set; } = new List<SupplierNote>();

    public virtual ICollection<SupplierPicture> SupplierPictures { get; set; } = new List<SupplierPicture>();
}
