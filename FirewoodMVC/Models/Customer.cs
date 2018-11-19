namespace FirewoodMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Customer_Id { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 6 )]
        [DisplayName("User Name")]
        public string User_Name { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string First_Name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string Last_Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("Zip Code")]
        public string Zip_Code { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string Phone_Number { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string Email_Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
