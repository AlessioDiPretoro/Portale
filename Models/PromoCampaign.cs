using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class PromoCampaign
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public virtual ICollection<PromoCampaignDetail> PromoCampaignDetails { get; set; } = new List<PromoCampaignDetail>();
}
