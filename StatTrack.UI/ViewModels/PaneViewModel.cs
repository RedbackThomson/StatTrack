using StatTrack.UI.Models;
using Syncfusion.Windows.Tools.Controls;

namespace StatTrack.UI.ViewModels
{
    public class PaneViewModel : Workspace
    {
        public PaneViewModel()
        {
            State = DockState.Dock;
        }

        public ViewModel ViewModel { get; set; }
    }
}
