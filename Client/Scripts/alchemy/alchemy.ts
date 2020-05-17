class AlchemyNode {
  id: string | undefined
  caption: string | undefined
}

class AlchemyEdge {
  source: string | undefined
  target: string | undefined
  caption: string | undefined
  id: string | null | undefined
}

class AlchemyGraph {
  nodes: AlchemyNode[] | undefined
  edges: AlchemyEdge[] | undefined
}

interface GraphBuilder {
  render (graph: AlchemyGraph): void
}

class Alchemy implements GraphBuilder {
  render (graph: AlchemyGraph) {
    // @ts-ignore
    const config = {
      dataSource: graph,
      nodeCaption: (node: AlchemyNode) => node.caption
    }
    // @ts-ignore
    alchemy.begin(config)
  }
}

// @ts-ignore
window.graphbuilder = new Alchemy()
