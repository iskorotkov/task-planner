class FirebaseAuth {
  // noinspection JSUnusedGlobalSymbols
  startUi (
    requireDisplayName: boolean = false,
    signInFlow: 'popup' | 'redirect' = 'popup',
    autoUpgradeAnonymousUsers: boolean = false,
    signInSuccessUrl: string = '/',
    tosUrl: string = '/',
    privacyPolicyUrl: string = '/',
    uiElementId: string = '#firebaseui-auth-container'
  ) {
    const ui = new firebaseui.auth.AuthUI(firebase.auth())
    const uiConfig: firebaseui.auth.Config = {
      signInFlow: signInFlow,
      signInSuccessUrl: signInSuccessUrl,
      autoUpgradeAnonymousUsers: autoUpgradeAnonymousUsers,
      signInOptions: [
        {
          provider: firebase.auth.EmailAuthProvider.PROVIDER_ID,
          requireDisplayName: requireDisplayName
        }
      ],
      tosUrl: tosUrl,
      credentialHelper: firebaseui.auth.CredentialHelper.GOOGLE_YOLO,
      privacyPolicyUrl: privacyPolicyUrl
    }

    ui.start(uiElementId, uiConfig)
  }
  
  // noinspection JSUnusedGlobalSymbols
  async signIn (username: string, password: string): Promise<string | firebase.User> {
    try {
      let credential = await firebase.auth().signInWithEmailAndPassword(username, password)
      return credential.user
    } catch (error) {
      return await error.message
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async signOut (): Promise<void> {
    return await firebase.auth().signOut()
  }

  // noinspection JSUnusedGlobalSymbols
  async register (username: string, password: string): Promise<string | firebase.User> {
    try {
      let credential = await firebase.auth().createUserWithEmailAndPassword(username, password)
      return credential.user
    } catch (error) {
      return await error.message
    }
  }

  // noinspection JSUnusedGlobalSymbols
  bindAuthStateChanged (
    obj: DotNet.DotNetObject,
    signedInMethod: string = 'SignedIn',
    signedOutMethod: string = 'SignedOut'
  ): void {
    firebase.auth().onAuthStateChanged(async user => {
        if (user) {
          await obj.invokeMethodAsync<firebase.User>(signedInMethod, user)
        } else {
          await obj.invokeMethodAsync(signedOutMethod)
        }
      }
    )
  }
}

// @ts-ignore
window.firebaseauth = new FirebaseAuth()
