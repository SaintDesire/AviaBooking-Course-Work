using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class Payment
    {
        [Column("payment_id")]
        public int Id { get; set; }
        [Column("client_id")]
        public int ClientId { get; set; }
        [Column("flight_id")]
        public int FlightId { get; set; }
        [Column("payment_date")]
        public DateTime PaymentDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
