using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class PromoCampaignDetail
{
    public int Id { get; set; }

    public int? IdPromoCampaign { get; set; }

    public int? IdProduct { get; set; }

    public double? StartPrice { get; set; }

    public double? EndPrice { get; set; }

    public bool? IsFidelityNeeded { get; set; }

    public bool? IsNxM { get; set; }

    public int? NxMQuantity { get; set; }

    public double? NxMEndPrice { get; set; }

    public double? PercentageDiscount { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual PromoCampaign? IdPromoCampaignNavigation { get; set; }
}
