// eslint-disable-next-line
class Condition {
  attribute: string | undefined
  operation: firebase.firestore.WhereFilterOp | undefined
  value: object | undefined
}

class Firestore {
  private db: firebase.firestore.Firestore

  constructor () {
    this.db = firebase.firestore()
  }

  // noinspection JSUnusedGlobalSymbols
  async getDoc (path: string): Promise<firebase.firestore.DocumentData | null> {
    try {
      const doc = await this.db.doc(path).get()
      return doc.data() ?? null
    } catch (e) {
      console.error(e)
      return null
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async getCollection (path: string, conditions?: Condition[]): Promise<firebase.firestore.DocumentData[] | null> {
    try {
      let query: firebase.firestore.Query = this.db.collection(path)
      if (conditions) {
        for (const condition of conditions) {
          if (condition.attribute && condition.operation && condition.value) {
            query = query.where(condition.attribute, condition.operation, condition.value)
          } else {
            console.warn('Conditions is malformed')
          }
        }
      }

      const snapshot = await query.get()
      return snapshot.docs.map(x => x.data())
    } catch (e) {
      console.error(e)
      return null
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async addToCollection (path: string, obj: object): Promise<string | null> {
    try {
      const collection = this.db.collection(path)
      const doc = await collection.add(obj)
      return doc.id
    } catch (e) {
      console.error(e)
      return null
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async save (path: string, obj: any): Promise<void> {
    try {
      const doc = this.db.doc(path)
      await doc.set(obj)
    } catch (e) {
      console.error(e)
    }
  }

  // noinspection JSUnusedGlobalSymbols
  async delete (path: string) {
    const doc = this.db.doc(path)
    try {
      await doc.delete()
    } catch (e) {
      console.error(e)
    }
  }

  // noinspection JSUnusedGlobalSymbols
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

  // noinspection JSUnusedGlobalSymbols
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
