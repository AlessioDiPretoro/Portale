using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Category
{
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();

	public virtual ICollection<CategorySupplier> CategorySuppliers { get; set; } = new List<CategorySupplier>();
}