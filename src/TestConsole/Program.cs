using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using ClearMeasure.Bootcamp.DataAccessEF.Model;
using ClearMeasure.Bootcamp.Core.Model;
using AutoMapper;

namespace ClearMeasure.Bootcamp.TestCosole
{
    class Program
    {
        private static DataContext _context;
        static void Main(string[] args)
        {
            
            _context = new DataContext();
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>());
            var Employee = _context.Employees;
            foreach (var emp in Employee) {
                var model = Mapper.Map<DataAccessEF.Model.Employee, Core.Model.Employee>(emp);
                Console.WriteLine(emp.FirstName.ToString());
            }

        }
    }
}
