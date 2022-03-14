using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }


        public string PhoneNumber { get; set; }


        public string Username { get; set; }    


        public DbSet<Member> Members { get; set; }
    }
}
