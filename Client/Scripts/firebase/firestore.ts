class Firestore {
  private db: firebase.firestore.Firestore

  constructor () {
    this.db = firebase.firestore()
  }

  async getDoc (path: string): Promise<firebase.firestore.DocumentData | null> {
    try {
      const doc = await this.db.doc(path).get()
      return doc.data()
    } catch (e) {
      console.log(e)
      return null
    }
  }

  async getCollection (path: string): Promise<firebase.firestore.DocumentData[] | null> {
    try {
      const collection = await this.db.collection(path).get()
      const docs = collection.docs.map(x => x.data())
      console.log(docs)
      return docs
    } catch (e) {
      console.log(e)
      return null
    }
  }

  async save (path: string, obj: any): Promise<void> {
    try {
      const doc = this.db.doc(path)
      await doc.set(obj)
    } catch (e) {
      console.log(e)
    }
  }

  delete (path: string) {
    const doc = this.db.doc(path)
    doc.delete().catch(error => console.log(error))
  }

  subscribeToCollection (subscriber: DotNet.DotNetObject,
    path: string,
    onSnapshotMethod = 'OnCollectionSnapshot',
    onErrorMethod = 'OnCollectionError'): object {
    return this.db.collection(path).onSnapshot(async snapshot => {
      const docs = snapshot.docs.map(x => x.data())
      await subscriber.invokeMethodAsync(onSnapshotMethod, docs)
    }, async error => {
      await subscriber.invokeMethodAsync(onErrorMethod, error)
    })
  }

  subscribeToDocument (subscriber: DotNet.DotNetObject,
    path: string,
    onSnapshotMethod = 'OnDocSnapshot',
    onErrorMethod = 'OnDocError'): object {
    return this.db.doc(path).onSnapshot(async snapshot => {
      await subscriber.invokeMethodAsync(onSnapshotMethod, snapshot.data())
    }, async error => {
      await subscriber.invokeMethodAsync(onErrorMethod, error)
    })
  }
}

// @ts-ignore
window.firestore = new Firestore()
