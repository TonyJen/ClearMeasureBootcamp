using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccessEF.Mappings;
using AutoMapper;
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
                    AutoMapper.Mapper.Initialize(cfg => {
                        cfg.CreateMap<DataAccessEF.Model.ExpenseReport, Core.Model.ExpenseReport>().ReverseMap();
                        cfg.CreateMap<DataAccessEF.Model.Employee, Core.Model.Employee>().ReverseMap();

                    });
                    var EFExpenseReport = new DataAccessEF.Model.ExpenseReport();
                    var EFSubmitter = new DataAccessEF.Model.Employee();
                    var EFApprover = new DataAccessEF.Model.Employee();
                    Mapper.Map(request.ExpenseReport, EFExpenseReport);
                    Mapper.Map(request.ExpenseReport.Submitter, EFSubmitter);
                    Mapper.Map(request.ExpenseReport.Approver, EFApprover);
                    EFExpenseReport.Status = request.ExpenseReport.Status.Code;
                    var _employee = session.Set<DataAccessEF.Model.Employee>();
                    _employee.Add(EFSubmitter);
                    _employee.Add(EFApprover);
                    var _expenseReport = session.Set<DataAccessEF.Model.ExpenseReport>();
                    _expenseReport.Add(EFExpenseReport);
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