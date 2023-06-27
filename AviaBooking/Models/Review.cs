using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class Review
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("airline_id")]
        public int AirlineId { get; set; }
        [Column("client_id")]
        public int ClientId { get; set; }
        [Column("review_text")]
        public string Text { get; set; }
        [Column("score")]
        public int Score { get; set; }

        public virtual Airline Airline { get; set; }
        public virtual Client Client { get; set; }
    }

}
