namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("arsenal.history")]
    public partial class history
    {
        [Key]
        [Column("Entrance Num", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entrance_Num { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionID { get; set; }

        public int PN { get; set; }

        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Actions { get; set; }

        public virtual _operator _operator { get; set; }
    }
}
