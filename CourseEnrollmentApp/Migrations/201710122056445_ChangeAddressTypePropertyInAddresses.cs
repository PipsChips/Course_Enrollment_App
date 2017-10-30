namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAddressTypePropertyInAddresses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "AddressType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "AddressType", c => c.Int());
        }
    }
}
