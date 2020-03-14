namespace SoftRazborki.WebApi.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUsersService
    {

        Task Register(string username, string password);

        Task<string> Login(string username, string password);
    }
}
