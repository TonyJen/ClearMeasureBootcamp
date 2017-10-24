﻿using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace ClearMeasure.Bootcamp.TestCosole
{
    public class StructureMapDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(Container container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }

            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}