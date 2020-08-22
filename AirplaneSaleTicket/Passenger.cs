namespace AirplaneSaleTicket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Passenger
    {
        public int PassengerID { get; set; }

        public int FlightID { get; set; }

        public string PNR { get; set; }

        public int SeatNumber { get; set; }

        public int? PersonID { get; set; }

        public virtual Flight Flight { get; set; }

        public virtual Person Person { get; set; }
    }
}
