namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetGraduationDateToOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Educations", "GraduationDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Educations", "GraduationDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
