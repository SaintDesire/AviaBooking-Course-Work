using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class Passenger
    {
        [Column("passenger_id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("gender")]
        public char Gender { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("flight_id")]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }
    }

}
