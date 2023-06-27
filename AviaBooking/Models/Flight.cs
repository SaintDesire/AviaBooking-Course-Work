using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class Flight
    {
        [Column("flight_id")]
        public int Id { get; set; }

        [Column("airline_id")]
        public int AirlineId { get; set; }

        [Column("departure")]
        public string Departure { get; set; }

        [Column("arrival")]
        public string Destination { get; set; }

        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }

        [Column("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [Column("seats_available")]
        public int AvailableSeats { get; set; }

        [Column("class")]
        public string Class { get; set; }
    }

}
