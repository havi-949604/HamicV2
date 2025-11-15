using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Models;

public partial class HarmicContext : DbContext
{
    public HarmicContext()
    {
    }

    public HarmicContext(DbContextOptions<HarmicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAccount> TbAccounts { get; set; }

    public virtual DbSet<TbAdminmenu> TbAdminmenus { get; set; }

    public virtual DbSet<TbBlog> TbBlogs { get; set; }

    public virtual DbSet<TbBlogcomment> TbBlogcomments { get; set; }

    public virtual DbSet<TbCart> TbCarts { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCheckout> TbCheckouts { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbHomeslider> TbHomesliders { get; set; }

    public virtual DbSet<TbMenu> TbMenus { get; set; }

    public virtual DbSet<TbNews> TbNews { get; set; }

    public virtual DbSet<TbOrder> TbOrders { get; set; }

    public virtual DbSet<TbOrderdetail> TbOrderdetails { get; set; }

    public virtual DbSet<TbOrderstatus> TbOrderstatuses { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbProductcategory> TbProductcategories { get; set; }

    public virtual DbSet<TbProductreview> TbProductreviews { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbWishlish> TbWishlishes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=harmic;Uid=root;Pwd=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("tb_account");

            entity.HasIndex(e => e.RoleId, "FK_tb_Account_tb_Role");

            entity.Property(e => e.AccountId).HasColumnType("int(11)");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LastLogin)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Role).WithMany(p => p.TbAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Account_tb_Role");
        });

        modelBuilder.Entity<TbAdminmenu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PRIMARY");

            entity.ToTable("tb_adminmenu");

            entity.Property(e => e.MenuId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ParentId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Positon)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PRIMARY");

            entity.ToTable("tb_blog");

            entity.HasIndex(e => e.AccountId, "FK_tb_Blog_tb_Account");

            entity.HasIndex(e => e.CategoryId, "FK_tb_Blog_tb_Category");

            entity.Property(e => e.BlogId).HasColumnType("int(11)");
            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Detail).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.SeoDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoKeywords)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoTitle)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Account).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Blog_tb_Account");

            entity.HasOne(d => d.Category).WithMany(p => p.TbBlogs)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Blog_tb_Category");
        });

        modelBuilder.Entity<TbBlogcomment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity.ToTable("tb_blogcomment");

            entity.HasIndex(e => e.BlogId, "FK_tb_BlogComment_tb_Blog");

            entity.HasIndex(e => e.AccountId, "UserId");

            entity.Property(e => e.CommentId).HasColumnType("int(11)");
            entity.Property(e => e.AccountId).HasColumnType("int(11)");
            entity.Property(e => e.BlogId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Detail)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Account).WithMany(p => p.TbBlogcomments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tb_blogcomment_ibfk_1");

            entity.HasOne(d => d.Blog).WithMany(p => p.TbBlogcomments)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_BlogComment_tb_Blog");
        });

        modelBuilder.Entity<TbCart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PRIMARY");

            entity.ToTable("tb_cart");

            entity.HasIndex(e => e.IdCustomer, "FK_tb_Cart_tb_Customer");

            entity.HasIndex(e => e.IdProduct, "FK_tb_Cart_tb_Product");

            entity.Property(e => e.IdCart).HasColumnType("int(11)");
            entity.Property(e => e.IdCustomer)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.IdProduct)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.TbCarts)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Cart_tb_Customer");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TbCarts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Cart_tb_Product");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("tb_category");

            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.SeoDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoKeywords)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoTitle)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbCheckout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tb_checkout");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.DatePaid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("OrderID");
            entity.Property(e => e.OrderInfo).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PRIMARY");

            entity.ToTable("tb_contact");

            entity.Property(e => e.ContactId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsRead)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Message).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("tb_customer");

            entity.HasIndex(e => e.RoleId, "RoleID");

            entity.Property(e => e.CustomerId).HasColumnType("int(11)");
            entity.Property(e => e.Avatar)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Birthday)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Role).WithMany(p => p.TbCustomers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tb_customer_ibfk_1");
        });

        modelBuilder.Entity<TbHomeslider>(entity =>
        {
            entity.HasKey(e => e.HomeSliderId).HasName("PRIMARY");

            entity.ToTable("tb_homeslider");

            entity.Property(e => e.HomeSliderId).HasColumnType("int(11)");
            entity.Property(e => e.AboveImg)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.BelowImg)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.BigTxt)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.MainImg)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.SmallTxt)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TextBtn)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UrlBtn)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PRIMARY");

            entity.ToTable("tb_menu");

            entity.Property(e => e.MenuId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Levels)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbNews>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PRIMARY");

            entity.ToTable("tb_news");

            entity.HasIndex(e => e.CategoryId, "FK_tb_News_tb_Category");

            entity.Property(e => e.NewsId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Detail).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.SeoDescription)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoKeywords)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SeoTitle)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Category).WithMany(p => p.TbNews)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_News_tb_Category");
        });

        modelBuilder.Entity<TbOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("tb_order");

            entity.HasIndex(e => e.CustomerId, "FK_tb_Order_tb_Customer");

            entity.HasIndex(e => e.OrderStatusId, "FK_tb_Order_tb_OrderStatus");

            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'")
                .IsFixedLength();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.OrderStatusId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Quanlity)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.TotalAmount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Customer).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Order_tb_Customer");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Order_tb_OrderStatus");
        });

        modelBuilder.Entity<TbOrderdetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.ToTable("tb_orderdetail");

            entity.HasIndex(e => e.OrderId, "FK_tb_OrderDetail_tb_Order");

            entity.Property(e => e.OrderDetailId).HasColumnType("int(11)");
            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Price).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Order).WithMany(p => p.TbOrderdetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_OrderDetail_tb_Order");
        });

        modelBuilder.Entity<TbOrderstatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PRIMARY");

            entity.ToTable("tb_orderstatus");

            entity.Property(e => e.OrderStatusId).HasColumnType("int(11)");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("tb_product");

            entity.HasIndex(e => e.CategoryProductId, "FK_tb_Product_tb_ProductCategory");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CategoryProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Detail).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.PriceSale)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Star)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UnitInStock)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.CategoryProduct).WithMany(p => p.TbProducts)
                .HasForeignKey(d => d.CategoryProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_Product_tb_ProductCategory");
        });

        modelBuilder.Entity<TbProductcategory>(entity =>
        {
            entity.HasKey(e => e.CategoryProductId).HasName("PRIMARY");

            entity.ToTable("tb_productcategory");

            entity.Property(e => e.CategoryProductId).HasColumnType("int(11)");
            entity.Property(e => e.Alias)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Icon)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbProductreview>(entity =>
        {
            entity.HasKey(e => e.ProductReviewId).HasName("PRIMARY");

            entity.ToTable("tb_productreview");

            entity.HasIndex(e => e.CustomerId, "CustomerId");

            entity.HasIndex(e => e.ProductId, "FK_tb_ProductReview_tb_Product");

            entity.Property(e => e.ProductReviewId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnType("int(11)");
            entity.Property(e => e.Detail)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Star)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Customer).WithMany(p => p.TbProductreviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tb_productreview_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.TbProductreviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_tb_ProductReview_tb_Product");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("tb_role");

            entity.Property(e => e.RoleId).HasColumnType("int(11)");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<TbWishlish>(entity =>
        {
            entity.HasKey(e => e.WishlishId).HasName("PRIMARY");

            entity.ToTable("tb_wishlish");

            entity.HasIndex(e => e.AccountId, "AccountID");

            entity.HasIndex(e => e.ProductId, "ProductID");

            entity.Property(e => e.WishlishId)
                .HasColumnType("int(11)")
                .HasColumnName("WishlishID");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("AccountID");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Account).WithMany(p => p.TbWishlishes)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tb_wishlish_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.TbWishlishes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("tb_wishlish_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
