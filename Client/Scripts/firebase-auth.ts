class AppUser {
  constructor (
    public displayName: string | null,
    public email: string | null,
    public emailVerified: boolean | null,
    public isAnonymous: boolean | null,
    public phoneNumber: string | null,
    public photoUrl: string | null,
    public providerId: string | null,
    public uid: string | null,
    public token: string | null,
    public refreshToken: string | null,
    public creationTime: string | null,
    public lastSignInTime: string | null
  ) { }

  static async create (user: firebase.User): Promise<AppUser> {
    return {
      displayName: user.displayName,
      email: user.email,
      emailVerified: user.emailVerified,
      isAnonymous: user.isAnonymous,
      phoneNumber: user.phoneNumber,
      photoUrl: user.photoURL,
      providerId: user.providerId,
      uid: user.uid,
      token: await user.getIdToken(),
      refreshToken: user.refreshToken,
      creationTime: user.metadata?.creationTime,
      lastSignInTime: user.metadata?.lastSignInTime
    }
  }
}

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
  async signIn (username: string, password: string): Promise<string | AppUser> {
    try {
      const credential = await firebase.auth().signInWithEmailAndPassword(username, password)
      return await AppUser.create(credential.user)
    } catch (error) {
      return await error.message
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async signOut (): Promise<void> {
    return await firebase.auth().signOut()
  }

  // noinspection JSUnusedGlobalSymbols
  async register (username: string, password: string): Promise<string | AppUser> {
    try {
      const credential = await firebase.auth().createUserWithEmailAndPassword(username, password)
      return await AppUser.create(credential.user)
    } catch (error) {
      return await error.message
    }
  }

  async getToken (): Promise<string> {
    return await firebase.auth().currentUser.getIdToken()
  }

  // noinspection JSUnusedGlobalSymbols
  bindAuthStateChanged (
    obj: DotNet.DotNetObject,
    signedInMethod: string = 'SignedIn',
    signedOutMethod: string = 'SignedOut'
  ): void {
    firebase.auth().onAuthStateChanged(async user => {
      if (user) {
        let appUser = await AppUser.create(user)
        await obj.invokeMethodAsync<AppUser>(signedInMethod, appUser)
      } else {
        await obj.invokeMethodAsync(signedOutMethod)
      }
    })
  }
}

// @ts-ignore
window.firebaseauth = new FirebaseAuth()
