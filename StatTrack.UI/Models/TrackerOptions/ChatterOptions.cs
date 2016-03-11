using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrack.UI.Models.TrackerOptions
{
    public enum ChatterOptions
    {
        [Description("Viewers lolwhat")]
        Viewers,

        [Description("Moderators")]
        Moderators,

        [Description("Staff")]
        Staff
    }
}
