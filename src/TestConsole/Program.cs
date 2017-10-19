using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;

namespace TestConsole
{
    class Program
    {
        private static DataContext _context;
        static void Main(string[] args)
        {
            _context = new DataContext();
            var Employee = _context.Employees.ToList();
            foreach (var emp in Employee) {
                Console.WriteLine(emp.FirstName.ToString());
            }

        }
    }
}
