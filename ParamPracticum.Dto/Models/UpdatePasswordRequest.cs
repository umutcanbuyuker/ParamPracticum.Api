using ParamPracticum.Base;
using System.ComponentModel.DataAnnotations;

namespace ParamApi.Dto
{
    public class UpdatePasswordRequest
    {
        [Required]
        [PasswordAttribute]
        public string OldPassword { get; set; }

        [Required]
        [Password]
        public string NewPassword { get; set; }
    }
}
