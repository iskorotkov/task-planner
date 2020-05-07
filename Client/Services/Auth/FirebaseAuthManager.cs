using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using TaskPlanner.Shared.Data.Auth;

namespace TaskPlanner.Client.Services.Auth
{
    // TODO: Move class to different project.
    public class FirebaseAuthManager : AuthenticationStateProvider, IAuthManager
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly DotNetObjectReference<FirebaseAuthManager> _objectReference;

        private bool _initialized;
        private readonly AuthenticationState _anonymous;
        private AuthenticationState _currentUser;

        public FirebaseAuthManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _objectReference = DotNetObjectReference.Create(this);

            _anonymous = new AuthenticationState(new ClaimsPrincipal());
            _currentUser = _anonymous;
        }

        private async Task EnsureInitialized()
        {
            if (!_initialized)
            {
                _initialized = true;
                await Init().ConfigureAwait(false);
            }
        }

        private async Task Init()
        {
            await _jsRuntime.InvokeVoidAsync("firebaseauth.bindAuthStateChanged", _objectReference);
        }

        public async Task SignIn(SignInUser user)
        {
            await EnsureInitialized().ConfigureAwait(false);
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.signIn", user.Username, user.Password)
                .ConfigureAwait(false);
            Console.WriteLine($"User {credential.Email} signed in.");
        }

        public async Task StartUi()
        {
            await EnsureInitialized().ConfigureAwait(false);
            await _jsRuntime.InvokeVoidAsync("firebaseauth.startUi");
        }

        public async Task SignOut()
        {
            await EnsureInitialized().ConfigureAwait(false);
            await _jsRuntime.InvokeVoidAsync("firebaseauth.signOut");
        }

        public async Task Register(RegisterUser user)
        {
            await EnsureInitialized().ConfigureAwait(false);
            var credential = await _jsRuntime
                .InvokeAsync<FirebaseUser>("firebaseauth.register", user.Username, user.Password)
                .ConfigureAwait(false);
            Console.WriteLine($"User {credential.Email} signed out.");
        }

        [JSInvokable]
        public Task SignedIn(FirebaseUser user)
        {
            Console.WriteLine($"Email: {user.Email}.");
            _currentUser = BuildAuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(_currentUser));
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task SignedOut()
        {
            Console.WriteLine("User has signed out.");
            _currentUser = _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(_currentUser));
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _objectReference?.Dispose();
        }

        private AuthenticationState BuildAuthenticationState(FirebaseUser user)
        {
            var claims = user.Claims();
            var identity = new ClaimsIdentity(claims, "firebase");
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await EnsureInitialized().ConfigureAwait(false);
            return _currentUser;
        }
    }
}
