namespace CourseEnrollmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressTypeEnumPropertyToAddresses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "AddressType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "AddressType");
        }
    }
}
