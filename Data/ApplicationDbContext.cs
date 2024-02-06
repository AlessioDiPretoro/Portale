using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Portale.Models;

namespace Portale.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{ }

		public virtual DbSet<Projects> Projects { get; set; } = null!;
		public virtual DbSet<ProjectsImgs> ProjectsImgs { get; set; } = null!;
		public virtual DbSet<Languages> Languages { get; set; } = null!;
		public virtual DbSet<ProjectsLang> ProjectsLang { get; set; } = null!;

		public virtual DbSet<Posts> Posts { get; set; } = null!;
		public virtual DbSet<PostTags> PostTags { get; set; } = null!;
		public virtual DbSet<PostImgs> PostImgs { get; set; } = null!;
		public virtual DbSet<Tags> Tags { get; set; } = null!;
		public virtual DbSet<UserProfileImgs> UserProfileImgs { get; set; } = null!;
		public virtual DbSet<CalendarAppointment> CalendarAppointments { get; set; }

		public virtual DbSet<CalendarAppointmentsNote> CalendarAppointmentsNotes { get; set; }

		public virtual DbSet<Category> Categories { get; set; }

		public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }

		public virtual DbSet<CategorySupplier> CategorySuppliers { get; set; }

		public virtual DbSet<Client> Clients { get; set; }

		public virtual DbSet<Contact> Contacts { get; set; }

		public virtual DbSet<Ddt> Ddts { get; set; }

		public virtual DbSet<DdtDetail> DdtDetails { get; set; }

		public virtual DbSet<DocumentType> DocumentTypes { get; set; }

		public virtual DbSet<Fidelity> Fidelities { get; set; }

		public virtual DbSet<Invoice> Invoices { get; set; }

		public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

		public virtual DbSet<Product> Products { get; set; }

		public virtual DbSet<ProductPicture> ProductPictures { get; set; }

		public virtual DbSet<PromoCampaign> PromoCampaigns { get; set; }

		public virtual DbSet<PromoCampaignDetail> PromoCampaignDetails { get; set; }

		public virtual DbSet<Sale> Sales { get; set; }

		public virtual DbSet<SaleDetail> SaleDetails { get; set; }

		public virtual DbSet<SerialCode> SerialCodes { get; set; }

		public virtual DbSet<Supplier> Suppliers { get; set; }

		public virtual DbSet<SupplierContact> SupplierContacts { get; set; }

		public virtual DbSet<SupplierContactRel> SupplierContactRels { get; set; }

		public virtual DbSet<SupplierNote> SupplierNotes { get; set; }

		public virtual DbSet<SupplierPicture> SupplierPictures { get; set; }

		public virtual DbSet<Vat> Vats { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ProjectsLang>()
				.HasKey(pl => new { pl.ProjectsId, pl.LanguagesId });

			modelBuilder.Entity<ProjectsLang>()
				.HasOne(pl => pl.Projects)
				.WithMany(p => p.ProjectsLangs)
				.HasForeignKey(pl => pl.ProjectsId);

			modelBuilder.Entity<ProjectsLang>()
				.HasOne(pl => pl.Languages)
				.WithMany(l => l.ProjectsLangs)
				.HasForeignKey(pl => pl.LanguagesId);

			modelBuilder.Entity<PostTags>()
				.HasKey(pl => new { pl.Id });

			modelBuilder.Entity<PostTags>()
				.HasOne(pl => pl.Posts)
				.WithMany(p => p.PostTags)
				.HasForeignKey(pl => pl.PostId);

			modelBuilder.Entity<PostTags>()
				.HasOne(pl => pl.Tags)
				.WithMany(l => l.PostTags)
			.HasForeignKey(pl => pl.TagId);

			modelBuilder.Entity<Posts>(entity =>
			{
				entity.ToTable("Posts");

				entity.Property(e => e.Name).HasMaxLength(255);

				entity.HasOne(d => d.User).WithMany(p => p.Posts)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Post_User");
			});

			modelBuilder.Entity<UserProfileImgs>()
					.HasOne(d => d.User)
					.WithMany(i => i.UserProfileImgs)
					.HasForeignKey(d => d.UserId);

			modelBuilder.Entity<CalendarAppointment>(entity =>
			{
				entity.Property(e => e.Title).HasMaxLength(50);

				entity.HasOne(d => d.IdContactNavigation).WithMany(p => p.CalendarAppointments)
					.HasForeignKey(d => d.IdContact)
					.HasConstraintName("FK_CalendarAppointments_Contacts");

				entity.HasOne(d => d.IdOwnerNavigation)
					.WithMany(i => i.CalendarAppointment)
					.HasForeignKey(d => d.IdOwner);
			});

			modelBuilder.Entity<CalendarAppointmentsNote>(entity =>
			{
				entity.ToTable("CalendarAppointmentsNote");

				entity.HasOne(d => d.IdAppointmentNavigation).WithMany(p => p.CalendarAppointmentsNotes)
					.HasForeignKey(d => d.IdAppointment)
					.HasConstraintName("FK_CalendarAppointmentsNote_CalendarAppointments1");
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.ToTable("Category");
			});

			modelBuilder.Entity<CategoryProduct>(entity =>
			{
				entity.ToTable("CategoryProduct");

				entity.HasIndex(e => e.CategoryId, "IX_CategoryProduct_CategoryId");

				entity.HasIndex(e => e.ProductId, "IX_CategoryProduct_ProductId");

				entity.HasOne(d => d.Category).WithMany(p => p.CategoryProducts).HasForeignKey(d => d.CategoryId);

				entity.HasOne(d => d.Product).WithMany(p => p.CategoryProducts).HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<CategorySupplier>(entity =>
			{
				entity.ToTable("CategorySupplier");

				entity.HasIndex(e => e.CategoryId, "IX_CategorySupplier_CategoryId");

				entity.HasIndex(e => e.SupplierId, "IX_CategorySupplier_SupplierId");

				entity.HasOne(d => d.Category).WithMany(p => p.CategorySuppliers).HasForeignKey(d => d.CategoryId);

				entity.HasOne(d => d.Supplier).WithMany(p => p.CategorySuppliers).HasForeignKey(d => d.SupplierId);
			});

			modelBuilder.Entity<Client>(entity =>
			{
				entity.ToTable("Client");

				entity.Property(e => e.PIva).HasColumnName("P_Iva");
			});

			modelBuilder.Entity<Ddt>(entity =>
			{
				entity.ToTable("Ddt");

				entity.HasIndex(e => e.DocumentTypeId, "IX_Ddt_DocumentTypeId");

				entity.HasOne(d => d.DocumentType).WithMany(p => p.Ddts).HasForeignKey(d => d.DocumentTypeId);
			});

			modelBuilder.Entity<DdtDetail>(entity =>
			{
				entity.HasIndex(e => e.DdtId, "IX_DdtDetails_DdtId");

				entity.HasIndex(e => e.ProductId, "IX_DdtDetails_ProductId");

				entity.HasOne(d => d.Ddt).WithMany(p => p.DdtDetails).HasForeignKey(d => d.DdtId);

				entity.HasOne(d => d.Product).WithMany(p => p.DdtDetails).HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<DocumentType>(entity =>
			{
				entity.ToTable("DocumentType");
			});

			modelBuilder.Entity<Fidelity>(entity =>
			{
				entity.ToTable("Fidelity");

				entity.HasIndex(e => e.ClientId, "IX_Fidelity_ClientId");

				entity.HasOne(d => d.Client).WithMany(p => p.Fidelities).HasForeignKey(d => d.ClientId);
			});

			modelBuilder.Entity<Invoice>(entity =>
			{
				entity.ToTable("Invoice");

				entity.HasIndex(e => e.ClientId, "IX_Invoice_ClientId");

				entity.HasOne(d => d.Client).WithMany(p => p.Invoices).HasForeignKey(d => d.ClientId);
			});

			modelBuilder.Entity<InvoiceDetail>(entity =>
			{
				entity.HasIndex(e => e.InvoiceId, "IX_InvoiceDetails_InvoiceId");

				entity.HasIndex(e => e.ProductId, "IX_InvoiceDetails_ProductId");

				entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails).HasForeignKey(d => d.InvoiceId);

				entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails).HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.ToTable("Product");

				entity.Property(e => e.CodeInternal).HasColumnName("Code_internal");
				entity.Property(e => e.CodeProducer).HasColumnName("Code_producer");
				entity.Property(e => e.CodeSeller).HasColumnName("Code_seller");

				entity.HasOne(d => d.IdVatNavigation).WithMany(p => p.Products)
					.HasForeignKey(d => d.IdVat)
					.HasConstraintName("FK_Product_Vat");
			});

			modelBuilder.Entity<ProductPicture>(entity =>
			{
				entity.ToTable("ProductPicture");

				entity.HasIndex(e => e.ProductId, "IX_ProductPicture_ProductId");

				entity.HasOne(d => d.Product).WithMany(p => p.ProductPictures).HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<PromoCampaign>(entity =>
			{
				entity.ToTable("PromoCampaign");

				entity.Property(e => e.Name).HasMaxLength(50);
			});

			modelBuilder.Entity<PromoCampaignDetail>(entity =>
			{
				entity.Property(e => e.NxMEndPrice).HasColumnName("NxM_endPrice");
				entity.Property(e => e.NxMQuantity).HasColumnName("NxM_quantity");

				entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.PromoCampaignDetails)
					.HasForeignKey(d => d.IdProduct)
					.HasConstraintName("FK_PromoCampaignDetails_Product");

				entity.HasOne(d => d.IdPromoCampaignNavigation).WithMany(p => p.PromoCampaignDetails)
					.HasForeignKey(d => d.IdPromoCampaign)
					.HasConstraintName("FK_PromoCampaignDetails_PromoCampaign");
			});

			modelBuilder.Entity<Sale>(entity =>
			{
				entity.ToTable("Sale");

				entity.HasIndex(e => e.ClientId, "IX_Sale_ClientId");

				entity.HasOne(d => d.Client).WithMany(p => p.Sales).HasForeignKey(d => d.ClientId);
			});

			modelBuilder.Entity<SaleDetail>(entity =>
			{
				entity.HasIndex(e => e.ProductId, "IX_SaleDetails_ProductId");

				entity.HasIndex(e => e.SaleId, "IX_SaleDetails_SaleId");

				entity.HasOne(d => d.Product).WithMany(p => p.SaleDetails).HasForeignKey(d => d.ProductId);

				entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails).HasForeignKey(d => d.SaleId);
			});

			modelBuilder.Entity<SerialCode>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.IdProduct).HasColumnName("idProduct");
				entity.Property(e => e.IsAvailabe).HasColumnName("isAvailabe");
				entity.Property(e => e.SerialCode1)
					.HasMaxLength(50)
					.HasColumnName("serialCode");

				entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SerialCodes)
					.HasForeignKey(d => d.IdProduct)
					.HasConstraintName("FK_SerialCodes_Product");
			});

			modelBuilder.Entity<Supplier>(entity =>
			{
				entity.ToTable("Supplier");

				entity.HasIndex(e => e.ProductId, "IX_Supplier_ProductId");

				entity.Property(e => e.PIva).HasColumnName("P_Iva");

				entity.HasOne(d => d.Product).WithMany(p => p.Suppliers).HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<SupplierContact>(entity =>
			{
				entity.ToTable("SupplierContact");
			});

			modelBuilder.Entity<SupplierContactRel>(entity =>
			{
				entity.ToTable("SupplierContactRel");

				entity.HasIndex(e => e.SupplierContactId, "IX_SupplierContactRel_SupplierContactId");

				entity.HasIndex(e => e.SupplierId, "IX_SupplierContactRel_SupplierId");

				entity.HasOne(d => d.SupplierContact).WithMany(p => p.SupplierContactRels).HasForeignKey(d => d.SupplierContactId);

				entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierContactRels).HasForeignKey(d => d.SupplierId);
			});

			modelBuilder.Entity<SupplierNote>(entity =>
			{
				entity.ToTable("SupplierNote");

				entity.HasIndex(e => e.SupplierId, "IX_SupplierNote_SupplierId");

				entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierNotes).HasForeignKey(d => d.SupplierId);
			});

			modelBuilder.Entity<SupplierPicture>(entity =>
			{
				entity.ToTable("SupplierPicture");

				entity.HasIndex(e => e.SupplierId, "IX_SupplierPicture_SupplierId");

				entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierPictures).HasForeignKey(d => d.SupplierId);
			});

			modelBuilder.Entity<Vat>(entity =>
			{
				entity.ToTable("Vat");

				entity.Property(e => e.VatName).HasMaxLength(50);
			});
		}
	}
}