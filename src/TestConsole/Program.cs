using System;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using ClearMeasure.Bootcamp.Core.Model;
using StructureMap;
using ClearMeasure.Bootcamp.TestConsole;
using ClearMeasure.Bootcamp.Core;
using System.Threading;

namespace ClearMeasure.Bootcamp.EFTestCosole
{
    class Program
    {
        private static DataContext _context;
        static void Main(string[] args)
        {

            _context = new DataContext();

            // Change to your number of menuitems.
            const int maxMenuItems = 3;
            var selector = 0;
            Console.Title = "Select the follow options:";
            while (selector != maxMenuItems)
            {
                Console.Clear();
                DrawMenu();
                bool good = int.TryParse(Console.ReadLine(), out selector);
                if (good)
                {
                    switch (selector)
                    {
                        case 1:
                            SaveReport();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        case 2:
                            ReadExpenseEmployees();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ErrorMessage();
                }
            }
        }

        private static void DrawMenu()
        {

            Console.WriteLine(" ----------------------------------------------------------------------------");
            Console.WriteLine(" 1. Create Expense Report");
            Console.WriteLine(" 2. Read Employee Table");
            Console.WriteLine(" 3. Exit");

        }

            private static void ErrorMessage()
        {
            Console.WriteLine("Typing error, press key to continue.");
        }

        private static void SaveExpenseEmployees()
        {
          
                      var random = new Random();
            var randomNumber = random.Next(0, 1000);
            Core.Model.Employee adminAssistant = new Core.Model.Employee("AssistantTemp" , "Someone" + randomNumber, "Else", "Email2");
            adminAssistant.Id = Guid.NewGuid();
            var EFEmployee = new Employee();
            
            var _employee =_context.Set<Employee>();
            _employee.Add(EFEmployee);
            _context.SaveChanges();
            Console.WriteLine("Created new Employee Someone" + randomNumber);
            Console.WriteLine();
        }

        /// <summary>
        /// Read employee and expense information
        /// </summary>
        private static void ReadExpenseEmployees()
        {
            Console.WriteLine("Values in Database: ");
            Console.WriteLine("=======================");
            Console.WriteLine();
            Console.WriteLine("Employees: ");
            Console.WriteLine("=======================");
            var Employee = _context.Employees;
            foreach (var emp in Employee)
            {
                Console.WriteLine(emp.UserName.ToString());
            }
            Console.WriteLine();

            Console.WriteLine("Expense Reports: ");
            Console.WriteLine("=======================");
            //var Expenses = _context.ExpenseReports;
            //var _coreexpense = new Core.Model.ExpenseReport();
            //foreach (var _expense in Expenses)
            //{
            //    // Need more advanced mapping, current method does not work.
            //    //Mapper.Map(_expense, _coreexpense);
            //    Console.WriteLine(_expense.Title.ToString());
            //}
        }

        private static void SaveReport()
        {
            var creator = new Core.Model.Employee( "User" + StaticRandom.Instance.Next(1, 100), "User1First", "User1Last", "user1@co.com");
            creator.Id = Guid.NewGuid();
            var assignee = new Core.Model.Employee("User" + StaticRandom.Instance.Next(1, 100), "User2First", "User2Last", "user2@co.com");
            assignee.Id = Guid.NewGuid();
            var report = new Core.Model.ExpenseReport();
            report.Submitter = creator;
            report.Approver = assignee;
            report.Id = Guid.NewGuid();
            report.Title = "foo" + StaticRandom.Instance.Next(1, 100);
            report.Description = "bar";
            report.ChangeStatus(ExpenseReportStatus.Approved);
            report.Number = "123";
            report.AddAuditEntry(new Core.Model.AuditEntry(creator, DateTime.Now, ExpenseReportStatus.Submitted,
                                                  ExpenseReportStatus.Approved));
            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            bus.Send(new ExpenseReportSaveCommand { ExpenseReport = report });
            Console.WriteLine("Report created.");
        }
    }

    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}
