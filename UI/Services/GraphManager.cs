using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prism.Regions;
using StatTrack.UI.Models;
using StatTrack.UI.Views;

namespace StatTrack.UI.Services
{
    public interface IGraphManager
    {
        void NewGraph(GraphableProperty property);
        void DeleteGraph(GraphableProperty property);
    }

    public class GraphManager : IGraphManager
    {
        private readonly Dictionary<GraphableProperty, GraphView> _graphs = 
            new Dictionary<GraphableProperty, GraphView>(); 

        private readonly IRegionManager _regionManager;
        private readonly IResults _results;

        public GraphManager(IRegionManager regionManager, IResults results)
        {
            _regionManager = regionManager;
            _results = results;
        }

        public void NewGraph(GraphableProperty property)
        {
            if (_graphs.ContainsKey(property)) return;

            var graphName = property.Attribute.Name;

            //Return if the graph has the same name as the new graph
            if (_regionManager.Regions["MainRegion"].ActiveViews.OfType<GraphView>()
                .Select(view => view).Any(graphView => graphView.GraphName == graphName))
                return;

            var dataSet = _results.GetCurrentDataSet(property);
            if(dataSet == null)
                throw new KeyNotFoundException(string.Format("Could not find endpoint for given property {0}",
                    property.Property.Name));

            var newGraph = new GraphView(graphName);
            newGraph.DataSet = dataSet;

            _graphs.Add(property, newGraph);
            _regionManager.Regions["MainRegion"].Add(newGraph);
        }

        public void DeleteGraph(GraphableProperty property)
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
