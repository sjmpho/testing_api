using System.ComponentModel.DataAnnotations;

namespace TESTING_API.Models
{
    public class loginModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public string emailAdress { get; set; }
        public string lastName { get; set; }
        public string phone_number { get; set; }
        public int identifier { get; set; }

    }
}
