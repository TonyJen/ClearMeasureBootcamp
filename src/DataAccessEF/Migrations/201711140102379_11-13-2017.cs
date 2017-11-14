namespace DataAccessEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11132017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditEntry",
                c => new
                    {
                        ExpenseReportId = c.Guid(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmployeeName = c.String(),
                        EmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.ExpenseReportId, t.Sequence })
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        EmailAddress = c.String(nullable: false, maxLength: 100),
                        Type = c.String(maxLength: 100),
                        AdminAssistantId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AdminAssistant_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.AdminAssistant_Id)
                .Index(t => t.AdminAssistant_Id);
            
            CreateTable(
                "dbo.ExpenseReport",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 4000),
                        SubmitterId = c.Guid(nullable: false),
                        ApproverId = c.Guid(),
                        Number = c.String(nullable: false, maxLength: 5),
                        MilesDriven = c.Int(nullable: false),
                        Created = c.DateTime(),
                        FirstSubmitted = c.DateTime(),
                        LastSubmitted = c.DateTime(),
                        LastWithdrawn = c.DateTime(),
                        LastCancelled = c.DateTime(),
                        LastApproved = c.DateTime(),
                        LastDeclined = c.DateTime(),
                        Total = c.Decimal(nullable: false, storeType: "money"),
                        Employee_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.ApproverId)
                .ForeignKey("dbo.Employee", t => t.SubmitterId, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Id)
                .Index(t => t.SubmitterId)
                .Index(t => t.ApproverId)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        ExpenseReportId = c.Guid(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.ExpenseReportId, t.Sequence });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditEntry", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "AdminAssistant_Id", "dbo.Employee");
            DropForeignKey("dbo.ExpenseReport", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.ExpenseReport", "SubmitterId", "dbo.Employee");
            DropForeignKey("dbo.ExpenseReport", "ApproverId", "dbo.Employee");
            DropIndex("dbo.ExpenseReport", new[] { "Employee_Id" });
            DropIndex("dbo.ExpenseReport", new[] { "ApproverId" });
            DropIndex("dbo.ExpenseReport", new[] { "SubmitterId" });
            DropIndex("dbo.Employee", new[] { "AdminAssistant_Id" });
            DropIndex("dbo.AuditEntry", new[] { "EmployeeId" });
            DropTable("dbo.Expense");
            DropTable("dbo.ExpenseReport");
            DropTable("dbo.Employee");
            DropTable("dbo.AuditEntry");
        }
    }
}
