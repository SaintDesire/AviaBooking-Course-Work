using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class Airline
    {
        [Column("airline_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
    }
}
