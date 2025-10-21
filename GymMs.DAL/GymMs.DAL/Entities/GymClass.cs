using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class GymClass
    {
        

        public int Id { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(100)]
        public string Title { get; set; }
        public int Capacity { get; set; }
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
        public ICollection<MemberClass>? MemberClasses { get; set; }
    }
}
