using System;
using System.Collections.Generic;

namespace Portale.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string? Ean { get; set; }

    public string? CodeInternal { get; set; }

    public string? CodeSeller { get; set; }

    public string? CodeProducer { get; set; }

    public string? PictureMain { get; set; }

    public bool? IsSerial { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SellPriceNoVat { get; set; }

    public int? IdVat { get; set; }

    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();

    public virtual ICollection<DdtDetail> DdtDetails { get; set; } = new List<DdtDetail>();

    public virtual Vat? IdVatNavigation { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<ProductPicture> ProductPictures { get; set; } = new List<ProductPicture>();

    public virtual ICollection<PromoCampaignDetail> PromoCampaignDetails { get; set; } = new List<PromoCampaignDetail>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual ICollection<SerialCode> SerialCodes { get; set; } = new List<SerialCode>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
