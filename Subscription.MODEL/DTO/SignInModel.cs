using System.ComponentModel.DataAnnotations;

namespace Subscription.MODEL.DTO
{
    public class SignInModel
    {
        [Required]
        public string Service_Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
