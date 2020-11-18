namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.operator")]
    public partial class _operator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public _operator()
        {
            history = new HashSet<history>();
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

        [Required]
        [StringLength(45)]
        [Index(IsUnique = true)]
        public string Login { get; set; }

        [Required]
        [StringLength(45)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<history> history { get; set; }

        public string FullName { get => $"{First_Name} {Last_Name} {Patronymic}"; }

        public string GetExperience
        { 
            get
            {
                var x = DateTime.Now - Hiring_date;
                return Math.Round(x.TotalDays).ToString();
            } 
        }

        public string GetShortBirthDate
        {
            get
            {
                return Date_of_Birth.ToShortDateString();
            }
        }

        public string GetShortHiringDate
        {
            get
            {
                return Hiring_date.ToShortDateString();
            }
        }

        public string GetAge
        {
            get => Math.Round((DateTime.Now - Date_of_Birth).TotalDays / 365).ToString();
        }
    }
}
