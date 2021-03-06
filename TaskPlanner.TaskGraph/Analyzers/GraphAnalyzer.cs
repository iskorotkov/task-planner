﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Shared.Data.Tasks;
using TaskPlanner.TaskGraph.Data.Config;
using TaskPlanner.TaskGraph.Data.Render;

namespace TaskPlanner.TaskGraph.Analyzers
{
    public class GraphAnalyzer
    {
        public GraphAnalyzer(AbstractAnalyzer abstractAnalyzer, PlacementAnalyzer placementAnalyzer,
            RenderAnalyzer renderAnalyzer)
        {
            _abstractAnalyzer = abstractAnalyzer ?? throw new ArgumentNullException(nameof(abstractAnalyzer));
            _placementAnalyzer = placementAnalyzer ?? throw new ArgumentNullException(nameof(placementAnalyzer));
            _renderAnalyzer = renderAnalyzer ?? throw new ArgumentNullException(nameof(renderAnalyzer));
        }

        private readonly AbstractAnalyzer _abstractAnalyzer;
        private readonly PlacementAnalyzer _placementAnalyzer;
        private readonly RenderAnalyzer _renderAnalyzer;

        public async Task<RenderGraph> Analyze(IEnumerable<Todo> tasks, GraphConfig? config = null)
        {
            _ = tasks ?? throw new ArgumentNullException(nameof(tasks));
            config ??= new GraphConfig();
            
            var abstractGraph = await _abstractAnalyzer.Analyze(tasks, config);
            var placementGraph = await _placementAnalyzer.Analyze(abstractGraph, config);
            return await _renderAnalyzer.Analyze(placementGraph, config);
        }
    }
}
