using System.ComponentModel;
using Syncfusion.Windows.Shared;

namespace StatTrack.UI.Models
{
    public class Workspace : NotificationObject, IDockElement
    {
        private string header;

        [ReadOnly(true)]
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        private DockState state;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DockState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
    }
}
