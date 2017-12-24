namespace Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Counters
    {
        public int Id { get; set; }

        public int CounterForUpdate { get; set; }
    }
}
