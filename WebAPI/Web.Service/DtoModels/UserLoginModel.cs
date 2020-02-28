using System.ComponentModel.DataAnnotations;

namespace Web.Service.DtoModels
{
    public class UserLoginModel
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
