namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxNumberOfStudentsPropertyToStudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "MaxNumberOfStudents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "MaxNumberOfStudents");
        }
    }
}
