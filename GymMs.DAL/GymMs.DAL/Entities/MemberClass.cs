using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Entities
{
    public class MemberClass
    {
       
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int GymClassId { get; set; }
        public GymClass? GymClass { get; set; }
    }
}
