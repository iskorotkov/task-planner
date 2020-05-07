class Firestore {
  private db: firebase.firestore.Firestore

  constructor () {
    this.db = firebase.firestore()
  }

  getDoc (path: string): firebase.firestore.QueryDocumentSnapshot | any {
    return this.db.doc(path).get().then(query => query).catch(error => {
      console.log(error)
      return error
    })
  }

  getCollection (path: string): firebase.firestore.QuerySnapshot | any {
    return this.db.collection(path).get().then(query => query).catch(error => {
      console.log(error)
      return error
    })
  }

  save (path: string, obj: any): string {
    const doc = this.db.doc(path)
    doc.set(obj)
    return doc.id
  }

  delete (path: string) {
    const doc = this.db.doc(path)
    doc.delete()
  }

  subscribeToCollection (subscriber: DotNet.DotNetObject,
    path: string,
    onSnapshotMethod = 'OnCollectionSnapshot',
    onErrorMethod = 'OnCollectionError'): object {
    return this.db.collection(path).onSnapshot(snapshot => {
      subscriber.invokeMethodAsync(onSnapshotMethod, snapshot)
    }, error => {
      subscriber.invokeMethodAsync(onErrorMethod, error)
    })
  }

  subscribeToDocument (subscriber: DotNet.DotNetObject,
    path: string,
    onSnapshotMethod = 'OnDocSnapshot',
    onErrorMethod = 'OnDocError'): object {
    return this.db.doc(path).onSnapshot(snapshot => {
      subscriber.invokeMethodAsync(onSnapshotMethod, snapshot)
    }, error => {
      subscriber.invokeMethodAsync(onErrorMethod, error)
    })
  }
}

// @ts-ignore
window.firestore = new Firestore()
