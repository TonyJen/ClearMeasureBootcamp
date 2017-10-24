using System;
using System.Web;
using ClearMeasure.Bootcamp.DataAccessEF;
using StructureMap;

namespace ClearMeasure.Bootcamp.TestConsole
{
    public class DependencyRegistrarModule 
	{
		private static bool _dependenciesRegistered;
		private static readonly object Lock = new object();
	    internal static IContainer Container = null;

		public void Init()
		{
		
		}

		public void Dispose() {}

		private static void context_BeginRequest(object sender, EventArgs e)
		{
			EnsureDependenciesRegistered();
		}

		public static IContainer EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (Lock)
				{
					if (!_dependenciesRegistered)
					{
					    Initialize();
						_dependenciesRegistered = true;
					}
				}
			}

		    return Container;
		}

	    private static void Initialize()
	    {
	        var container = new Container(new StructureMapRegistry());
	        Container = container;
	    }

	}
}