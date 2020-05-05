using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using TaskPlanner.Client.Data.Auth;
using TaskPlanner.Client.Interop;
using TaskPlanner.Shared.Data.Auth;

namespace TaskPlanner.Client.Services.Auth
{
    public class FirebaseAuthManager : IAuthManager, IAuthJsInterop
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DotNetObjectReference<FirebaseAuthManager> _objectReference;

        public FirebaseAuthManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _objectReference = DotNetObjectReference.Create(this);
            _jsRuntime.InvokeVoidAsync("firebaseauth.bindAuthStateChanged", this);
        }

        public async Task SignIn(SignInUser user)
        {
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.signIn", user.Username, user.Password);
            Console.WriteLine($"User {credential.Email} signed in.");
        }

        public async Task StartUi()
        {
            await _jsRuntime.InvokeVoidAsync("firebaseauth.startUi");
        }

        public async Task SignOut()
        {
            await _jsRuntime.InvokeVoidAsync("firebaseauth.signOut");
        }

        public async Task Register(RegisterUser user)
        {
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.register", user.Username, user.Password);
            Console.WriteLine($"User {credential.Email} signed out.");
        }

        [JSInvokable]
        public Task SignedIn(FirebaseUser user)
        {
            Console.WriteLine($"Email: {user.Email}, token: {user.RefreshToken}.");
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task SignedOut()
        {
            Console.WriteLine("User has signed out.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _objectReference?.Dispose();
        }
    }
}
