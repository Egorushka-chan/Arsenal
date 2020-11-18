namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.inventarisation")]
    public partial class inventarisation
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column("Storage ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Storage_ID { get; set; }

        [Key]
        [Column("Item ID", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item_ID { get; set; }

        [Column("Nominal Quantity")]
        public int Nominal_Quantity { get; set; }

        [Column("Real Quantity")]
        public int Real_Quantity { get; set; }

        public virtual storage storage { get; set; }

        public string GetShortDate
        {
            get => Date.ToShortDateString();
        }
    }
}
