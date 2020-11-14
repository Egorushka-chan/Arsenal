namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.item")]
    public partial class item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item()
        {
            storage = new HashSet<storage>();
        }

        [Key]
        [Column("Item ID")]
        public int Item_ID { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Properities { get; set; }

        [StringLength(45)]
        public string Manufacturer { get; set; }

        [Column("Type ID")]
        public int Type_ID { get; set; }

        public virtual type type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<storage> storage { get; set; }
    }
}
