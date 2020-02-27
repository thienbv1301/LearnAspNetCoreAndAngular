using System.ComponentModel.DataAnnotations;

namespace Web.Service.DtoModels
{
    public class UserRegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
