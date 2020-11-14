namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.loader")]
    public partial class loader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loader()
        {
            delivery = new HashSet<delivery>();
            issue = new HashSet<issue>();
        }

        [Key]
        public int PN { get; set; }

        [Column("First Name")]
        [Required]
        [StringLength(45)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [Required]
        [StringLength(45)]
        public string Last_Name { get; set; }

        [StringLength(45)]
        public string Patronymic { get; set; }

        [Column("Date of Birth", TypeName = "date")]
        public DateTime Date_of_Birth { get; set; }

        [Column("passport ID")]
        public int passport_ID { get; set; }

        [Column("Hiring date", TypeName = "date")]
        public DateTime Hiring_date { get; set; }

        [Column("Dismissal date", TypeName = "date")]
        public DateTime? Dismissal_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<delivery> delivery { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<issue> issue { get; set; }

        public string FullName { get => $"{First_Name} {Last_Name} {Patronymic}"; }
    }
}
