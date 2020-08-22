namespace AirplaneSaleTicket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            Passengers = new HashSet<Passenger>();
        }

        public int FlightID { get; set; }

        public int AirplaneID { get; set; }

        public string FromFlight { get; set; }

        public string ToFlight { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hour { get; set; }

        public float Price { get; set; }

        public int EmptySeat { get; set; }

        public virtual Airplane Airplane { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
