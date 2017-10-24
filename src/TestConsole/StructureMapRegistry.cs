using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ClearMeasure.Bootcamp.TestConsole
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<Employee>();
                scanner.AssemblyContainingType<DataContext>();
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof (IRequestHandler<,>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        }
    }
}