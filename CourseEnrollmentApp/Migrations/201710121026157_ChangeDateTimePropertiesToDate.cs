namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimePropertiesToDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "DateOfBirth", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Educations", "GraduationDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Educations", "GraduationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
