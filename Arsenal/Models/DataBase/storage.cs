namespace Arsenal.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    [Table("arsenal.storage")]
    public partial class storage : INotifyPropertyChanged
    {
        private int item_ID;
        private int quantity;
        private ICollection<delivery> delivery1;
        private ICollection<inventarisation> inventarisation1;
        private ICollection<issue> issue1;
        private item item1;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public storage()
        {
            delivery = new HashSet<delivery>();
            inventarisation = new HashSet<inventarisation>();
            issue = new HashSet<issue>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column("Item ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item_ID { get => item_ID; set { item_ID = value; OnPropertyChanged(nameof(Item_ID)); } }

        public int Quantity { get => quantity; set { quantity = value; OnPropertyChanged(nameof(Quantity)); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<delivery> delivery { get => delivery1; set { delivery1 = value; OnPropertyChanged(nameof(delivery)); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inventarisation> inventarisation { get => inventarisation1; set { inventarisation1 = value; OnPropertyChanged(nameof(inventarisation)); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<issue> issue { get => issue1; set { issue1 = value; OnPropertyChanged(nameof(issue)); } }

        public virtual item item { get => item1; set { item1 = value; OnPropertyChanged(nameof(item)); } }

        public string FullInfo
        {
            get
            {
                return $"{ID} {item.Name} {Quantity}";
            }
        }

    }
}
