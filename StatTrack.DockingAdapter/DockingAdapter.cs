using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using Syncfusion.Windows.Tools.Controls;

namespace StatTrack.DockingAdapter
{
    public class DockingAdapter : RegionAdapterBase<DockingManager>
    {
        public DockingAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, DockingManager regionTarget)
        {
            region.Views.CollectionChanged += delegate
            {
                foreach (var child in region.Views.Cast<FrameworkElement>())
                {
                    if (!regionTarget.Children.Contains(child))
                    {
                        regionTarget.BeginInit();
                        regionTarget.Children.Add(child);
                        regionTarget.EndInit();
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
