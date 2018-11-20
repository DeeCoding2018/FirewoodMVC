namespace FirewoodMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        public int Order_Id { get; set; }

        public int Customer_Id { get; set; }

        [DisplayName("Order Date")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime Order_Date { get; set; }

        [DisplayName("Recieved By")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime Shipped_Date { get; set; }

        [DisplayName("Shipping Address")]
        [StringLength(50)]
        public string Shipping_Address { get; set; }

        [DisplayName("City")]
        [StringLength(25)]
        public string City { get; set; }

        [DisplayName("State")]
        [StringLength(25)]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [StringLength(10)]
        public string Zip_Code { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
