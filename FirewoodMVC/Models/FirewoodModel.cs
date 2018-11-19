namespace FirewoodMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FirewoodModel : DbContext
    {
        public FirewoodModel()
            : base("name=FirewoodModelConfig")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.User_Name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.First_Name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Last_Name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Zip_Code)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone_Number)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email_Address)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.Unit_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Shipping_Address)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.City)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.State)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Zip_Code)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
