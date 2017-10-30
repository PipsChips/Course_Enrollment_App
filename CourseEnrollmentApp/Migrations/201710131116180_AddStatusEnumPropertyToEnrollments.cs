namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusEnumPropertyToEnrollments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Enrollments", "AcceptEnrollment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "AcceptEnrollment", c => c.Boolean());
            DropColumn("dbo.Enrollments", "Status");
        }
    }
}
