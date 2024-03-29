﻿using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class DocumentType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ddt> Ddts { get; set; } = new List<Ddt>();
}
