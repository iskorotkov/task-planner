class AppUser {
  displayName: string | null | undefined
  email: string | null | undefined
  emailVerified: boolean | null | undefined
  isAnonymous: boolean | null | undefined
  phoneNumber: string | null | undefined
  photoUrl: string | null | undefined
  providerId: string | null | undefined
  uid: string | null | undefined
  token: string | null | undefined
  refreshToken: string | null | undefined
  creationTime: string | null | undefined
  lastSignInTime: string | null | undefined

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
      creationTime: user.metadata?.creationTime ?? null,
      lastSignInTime: user.metadata?.lastSignInTime ?? null
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
  async signIn (username: string, password: string): Promise<AppUser | null> {
    try {
      const credential = await firebase.auth().signInWithEmailAndPassword(username, password)
      if (credential.user) {
        return await AppUser.create(credential.user)
      }
      return null
    } catch (error) {
      return null
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async signOut (): Promise<void> {
    return await firebase.auth().signOut()
  }

  // noinspection JSUnusedGlobalSymbols
  async register (username: string, password: string): Promise<AppUser | null> {
    try {
      const credential = await firebase.auth().createUserWithEmailAndPassword(username, password)
      if (credential.user) {
        return await AppUser.create(credential.user)
      }
      return null
    } catch (error) {
      return null
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async getToken (): Promise<string | null> {
    return await firebase.auth().currentUser?.getIdToken() ?? null
  }

  // noinspection JSUnusedGlobalSymbols
  bindAuthStateChanged (
    obj: DotNet.DotNetObject,
    signedInMethod: string = 'SignedIn',
    signedOutMethod: string = 'SignedOut'
  ): void {
    firebase.auth().onAuthStateChanged(async user => {
      if (user) {
        const appUser = await AppUser.create(user)
        await obj.invokeMethodAsync<AppUser>(signedInMethod, appUser)
      } else {
        await obj.invokeMethodAsync(signedOutMethod)
      }
    })
  }
}

// @ts-ignore
window.firebaseauth = new FirebaseAuth()
