using System.ComponentModel.DataAnnotations;

namespace MillionAndUpApi.DTO_s
{
    public class CredentialsDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
