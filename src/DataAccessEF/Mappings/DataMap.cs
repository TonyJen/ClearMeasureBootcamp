using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.DataAccessEF.Model;

namespace ClearMeasure.Bootcamp.DataAccessEF.Mappings
{
    class DataMap
    {
        public DataMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>();
            });
        }
    }
}
