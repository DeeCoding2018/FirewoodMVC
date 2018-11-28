namespace FirewoodMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName(displayName: "Product Name")]
        public int Product_ID { get; set; }

        [DisplayName("Price")]
        [Column(TypeName = "money")]
        public decimal? Unit_Price { get; set; }

        // ? after type
        /*The built-in DefaultModelBinder in MVC will perform required and data type validation on value types like int, DateTime, decimal, etc. This will happen even if you don't explicitly specify validation using someting like [Required].

In order to make this optional, you will have to define it as nullable:
        */

        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
