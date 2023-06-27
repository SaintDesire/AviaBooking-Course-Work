using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaBooking.Models
{
    public class ReviewModel
    {
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }   
        public string ReviewText { get; set; }
        public int Score { get; set; }
    }

}
