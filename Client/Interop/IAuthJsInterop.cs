using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Auth;

namespace TaskPlanner.Client.Interop
{
    public interface IAuthJsInterop
    {
        // JsInvokable
        Task SignedIn(FirebaseUser user);

        // JsInvokable
        Task SignedOut();
    }
}
