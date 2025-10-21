using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class Payment
    {
       
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Please, Enter Payment Method ")]
        public string Method { get; set; } 
    }
}
