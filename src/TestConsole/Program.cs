using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using ClearMeasure.Bootcamp.DataAccessEF.Model;
using ClearMeasure.Bootcamp.Core.Model;
using AutoMapper;
using StructureMap;
using ClearMeasure.Bootcamp.TestConsole;
using ClearMeasure.Bootcamp.Core;

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
            SaveReport();
            Console.ReadLine();
        }

        private static void SaveEmployee()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>().ReverseMap());
            var random = new Random();
            var randomNumber = random.Next(0, 1000);
            Core.Model.Employee adminAssistant = new Core.Model.Employee("AssistantTemp" , "Someone" + randomNumber, "Else", "Email2");
            adminAssistant.Id = Guid.NewGuid();
            var EFEmployee = new DataAccessEF.Model.Employee();

            Mapper.Map(adminAssistant, EFEmployee);
            var _employee =_context.Set<DataAccessEF.Model.Employee>();
            _employee.Add(EFEmployee);
            _context.SaveChanges();
            Console.WriteLine("Created new Employee Someone" + randomNumber);
            Console.WriteLine();
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
            Console.WriteLine();
        }

        private static void SaveReport()
        {
            var creator = new Core.Model.Employee( "1", "1", "1", "1");
            creator.Id = Guid.NewGuid();
            var assignee = new Core.Model.Employee("2", "2", "2", "2");
            assignee.Id = Guid.NewGuid();
            var report = new Core.Model.ExpenseReport();
            report.Submitter = creator;
            report.Approver = assignee;
            report.Id = Guid.NewGuid();
            report.Title = "foo";
            report.Description = "bar";
            report.ChangeStatus(ExpenseReportStatus.Approved);
            report.Number = "123";
            report.AddAuditEntry(new Core.Model.AuditEntry(creator, DateTime.Now, ExpenseReportStatus.Submitted,
                                                  ExpenseReportStatus.Approved));
            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            bus.Send(new ExpenseReportSaveCommand { ExpenseReport = report });
        }
    }
}
