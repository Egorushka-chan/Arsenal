namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.delivery")]
    public partial class delivery
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PN { get; set; }

        [Key]
        [Column("Storage ID", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Storage_ID { get; set; }

        [Key]
        [Column("Item ID", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item_ID { get; set; }

        public virtual loader loader { get; set; }

        public virtual storage storage { get; set; }
    }
}
