using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ClearMeasure.Bootcamp.DataAccessEF
{
    public class ExpenseReportSaveCommandHandler : IRequestHandler<ExpenseReportSaveCommand, SingleResult<ExpenseReport>>
    {
        public SingleResult<ExpenseReport> Handle(ExpenseReportSaveCommand request)
        {
            try
            {
                using (var session = new DataContext())
                {
                  
                    var _expenseReport = session.Set<ExpenseReport>();
                    _expenseReport.Add(request.ExpenseReport);
                   
                    session.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return new SingleResult<ExpenseReport>(request.ExpenseReport);
        }
    }

   
}