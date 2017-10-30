namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnrollmentsTableAcceptEnrollmentPropertyOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "AcceptEnrollment", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "AcceptEnrollment", c => c.Boolean(nullable: false));
        }
    }
}
