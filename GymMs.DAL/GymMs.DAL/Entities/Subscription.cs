using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class Subscription
    {
        
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please, Specify type of subscription")]
        public string Type { get; set; } 
        public ICollection<Payment>? Payments { get; set; }
    }
}
