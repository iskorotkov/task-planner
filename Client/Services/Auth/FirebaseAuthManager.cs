using System.Threading.Tasks;
using TaskPlanner.Client.Data.Auth;

namespace TaskPlanner.Client.Services.Auth
{
    public class FirebaseAuthManager: IAuthManager
    {
        public Task SignIn(SignInUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task SignOut()
        {
            throw new System.NotImplementedException();
        }

        public Task Register(RegisterUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
