using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class Member
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invailid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubscriptionEnd { get; set; }
        public bool IsActive => SubscriptionEnd >= DateTime.Today;

        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<MemberClass>? MemberClasses { get; set; }
    }
}
