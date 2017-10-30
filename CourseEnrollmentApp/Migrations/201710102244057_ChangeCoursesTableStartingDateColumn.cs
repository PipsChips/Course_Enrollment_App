namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCoursesTableStartingDateColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "StartingDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "StartingDate", c => c.DateTime(nullable: false));
        }
    }
}
