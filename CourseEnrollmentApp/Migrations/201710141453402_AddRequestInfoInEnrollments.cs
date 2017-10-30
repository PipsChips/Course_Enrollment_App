namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestInfoInEnrollments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "ClientIP", c => c.String());
            AddColumn("dbo.Enrollments", "ClientDNS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "ClientDNS");
            DropColumn("dbo.Enrollments", "ClientIP");
        }
    }
}
