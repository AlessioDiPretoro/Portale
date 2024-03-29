﻿using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class ProductPicture
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
