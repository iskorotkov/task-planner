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

        private FirebaseUser? _user;
        private bool _initialized;

        public FirebaseAuthManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _objectReference = DotNetObjectReference.Create(this);
        }

        private async Task Init()
        {
            // TODO: Init AuthManager in different place.
            await _jsRuntime.InvokeVoidAsync("firebaseauth.bindAuthStateChanged", _objectReference);
        }

        private async Task EnsureInitialized()
        {
            if (!_initialized)
            {
                _initialized = true;
                await Init();
            }
        }

        public async Task SignIn(SignInUser user)
        {
            await EnsureInitialized();
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.signIn", user.Username, user.Password)
                .ConfigureAwait(false);
            Console.WriteLine($"User {credential.Email} signed in.");
        }

        public async Task StartUi()
        {
            await EnsureInitialized();
            await _jsRuntime.InvokeVoidAsync("firebaseauth.startUi");
        }

        public async Task<FirebaseUser?> GetUser()
        {
            await EnsureInitialized();
            return _user;
        }

        public async Task SignOut()
        {
            await EnsureInitialized();
            await _jsRuntime.InvokeVoidAsync("firebaseauth.signOut");
        }

        public async Task Register(RegisterUser user)
        {
            await EnsureInitialized();
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.register", user.Username, user.Password)
                .ConfigureAwait(false);
            Console.WriteLine($"User {credential.Email} signed out.");
        }

        [JSInvokable]
        public Task SignedIn(FirebaseUser user)
        {
            Console.WriteLine($"Email: {user.Email}.");
            _user = user;
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task SignedOut()
        {
            Console.WriteLine("User has signed out.");
            _user = null;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _objectReference?.Dispose();
        }
    }
}
