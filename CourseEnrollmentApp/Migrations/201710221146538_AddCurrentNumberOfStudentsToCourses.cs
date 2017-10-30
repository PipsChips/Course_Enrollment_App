namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentNumberOfStudentsToCourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CurrentNumOfStudents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CurrentNumOfStudents");
        }
    }
}
