namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "TelephoneNumber", c => c.Int());
            AlterColumn("dbo.Students", "MobileNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "MobileNumber", c => c.String());
            AlterColumn("dbo.Students", "TelephoneNumber", c => c.String());
        }
    }
}
