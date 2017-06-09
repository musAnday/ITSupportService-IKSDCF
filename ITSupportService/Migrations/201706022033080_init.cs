namespace ITSupportService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        Phonenumber = c.String(nullable: false),
                        StreetAddress = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Guid(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                        IssueType = c.Int(nullable: false),
                        DeliveryMethod = c.Int(nullable: false),
                        ReceivedOn = c.DateTime(),
                        ApprovedOn = c.DateTime(),
                        CompletedOn = c.DateTime(),
                        EstimatedWorkHour = c.Int(nullable: false),
                        EstimatedCost = c.Double(nullable: false),
                        isPaymentDone = c.Boolean(nullable: false),
                        AssignedToId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        AssignedTo_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Employees", t => t.AssignedTo_UserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AssignedTo_UserId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        Phonenumber = c.String(nullable: false),
                        StreetAddress = c.String(),
                        isQoS = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Guid(nullable: false, identity: true),
                        RelatedTicketId = c.Guid(nullable: false),
                        Description = c.String(),
                        Rate = c.Int(nullable: false),
                        RelatedTicket_TicketId = c.Guid(),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Tickets", t => t.RelatedTicket_TicketId)
                .Index(t => t.RelatedTicket_TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "RelatedTicket_TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Tickets", "AssignedTo_UserId", "dbo.Employees");
            DropIndex("dbo.Feedbacks", new[] { "RelatedTicket_TicketId" });
            DropIndex("dbo.Tickets", new[] { "AssignedTo_UserId" });
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Employees");
            DropTable("dbo.Tickets");
            DropTable("dbo.Customers");
        }
    }
}
