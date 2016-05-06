using System.Collections.Generic;
using System.Collections.ObjectModel;
using StatTrack.UI.Models;

namespace StatTrack.UI
{
	public interface IOptions
	{
		ObservableCollection<GraphableProperty> Current { get; set; }
	}
	
	public class Options : IOptions
	{
		public ObservableCollection<GraphableProperty> Current { get; set; }
		
		public Options()
		{
			Current = new ObservableCollection<GraphableProperty>();	
		}
	}
}