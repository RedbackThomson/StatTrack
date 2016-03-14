using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prism.Regions;
using StatTrack.UI.Views;

namespace StatTrack.UI.Services
{
    public interface IGraphManager
    {
        void NewGraph(PropertyInfo property);
        void DeleteGraph(PropertyInfo property);
    }

    public class GraphManager : IGraphManager
    {
        private readonly Dictionary<PropertyInfo, GraphView> _graphs = 
            new Dictionary<PropertyInfo, GraphView>(); 

        private readonly IRegionManager _regionManager;
        private readonly IResults _results;

        public GraphManager(IRegionManager regionManager, IResults results)
        {
            _regionManager = regionManager;
            _results = results;
        }

        public void NewGraph(PropertyInfo property)
        {
            if (_graphs.ContainsKey(property)) return;

            var graphName = property.Name;

            //Return if the graph has the same name as the new graph
            if (_regionManager.Regions["MainRegion"].ActiveViews.OfType<GraphView>()
                .Select(view => view).Any(graphView => graphView.GraphName == graphName))
                return;

            var newGraph = new GraphView(graphName);
            //newGraph.DataChanged;

            _graphs.Add(property, newGraph);
            _regionManager.Regions["MainRegion"].Add(newGraph);
        }

        public void DeleteGraph(PropertyInfo property)
        {
            if (!_graphs.ContainsKey(property)) return;

            var graph = _graphs[property];
            //TODO: Fix
            //The user can now close the graph
            graph.CanClose = true;
            graph.InitializeComponent();
            _graphs.Remove(property);
        }
    }
}
