using System.ComponentModel.DataAnnotations;

namespace BusPortal.Web.Models.DTO
{
    public class LoginViewModel
    {

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
