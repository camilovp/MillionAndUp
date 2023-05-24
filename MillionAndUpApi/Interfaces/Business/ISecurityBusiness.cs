using Microsoft.AspNetCore.Mvc;
using MillionAndUpApi.DTO_s;

namespace MillionAndUpApi.Interfaces.Business
{
    public interface ISecurityBusiness
    {
        Task<SecurityTokenDTO> GetToken(CredentialsDTO credentialsDTO);
    }
}
