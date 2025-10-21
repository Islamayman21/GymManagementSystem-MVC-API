using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class Trainer
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        [StringLength(100)]
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public ICollection<GymClass> ?Classes { get; set; }
    }
}
