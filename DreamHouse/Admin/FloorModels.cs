namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FloorModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FloorModels()
        {
            PavilionModels = new HashSet<PavilionModels>();
        }

        public int Id { get; set; }

        public int NumberFloor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PavilionModels> PavilionModels { get; set; }
        public FloorModels(int NumberFloor, ObservableCollection<PavilionModels> PavilionList)
        {
            this.NumberFloor = NumberFloor;
            this.PavilionModels = PavilionList; 
        }
    }
}
