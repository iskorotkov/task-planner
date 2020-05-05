class FirebaseAuth {
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
