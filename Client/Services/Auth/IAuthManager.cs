using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Auth;

namespace TaskPlanner.Client.Services.Auth
{
    public interface IAuthManager
    {
        Task SignOut();
        Task Register(RegisterUser user);
        Task SignIn(SignInUser user);
        Task StartUi();
    }
}
