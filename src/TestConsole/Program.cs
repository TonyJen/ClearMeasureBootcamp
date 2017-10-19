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

            ReadEmployee();
            SaveEmployee();
            ReadEmployee();
        }

        private static void SaveEmployee()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>().ReverseMap());

            Core.Model.Employee adminAssistant = new Core.Model.Employee("AssistantTemp", "Someone", "Else", "Email2");
            adminAssistant.Id = Guid.NewGuid();
            var EFEmployee = new DataAccessEF.Model.Employee();

            Mapper.Map(adminAssistant, EFEmployee);
            var _employee =_context.Set<DataAccessEF.Model.Employee>();
            _employee.Add(EFEmployee);
            _context.SaveChanges();

        }

        private static void ReadEmployee()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>());
            var Employee = _context.Employees;
            foreach (var emp in Employee)
            {
                var model = Mapper.Map<DataAccessEF.Model.Employee, Core.Model.Employee>(emp);
                Console.WriteLine(emp.FirstName.ToString());
            }
        }
    }
}
