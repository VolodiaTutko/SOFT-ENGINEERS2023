namespace FlowMeterTeamProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        PersonalAccount = c.String(nullable: false, maxLength: 128),
                        HotWater = c.Decimal(precision: 18, scale: 2),
                        ColdWater = c.Decimal(precision: 18, scale: 2),
                        Heating = c.Decimal(precision: 18, scale: 2),
                        Electricity = c.Decimal(precision: 18, scale: 2),
                        PublicService = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PersonalAccount);
            
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        ConsumersId = c.Int(nullable: false, identity: true),
                        PersonalAccount = c.String(),
                        Flat = c.Int(),
                        ConsumerOwner = c.String(),
                        HeatingArea = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumersId);
            
            CreateTable(
                "dbo.Counters",
                c => new
                    {
                        CountersId = c.Int(nullable: false, identity: true),
                        PreviousIndicator = c.Decimal(precision: 18, scale: 2),
                        CurrentIndicator = c.Decimal(precision: 18, scale: 2),
                        Account = c.String(),
                        TypeOfAccount = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CountersId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeLogin = c.String(nullable: false, maxLength: 128),
                        EmployeePassword = c.String(),
                        TypeOfUser = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeLogin);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseId = c.Int(nullable: false, identity: true),
                        HouseAddress = c.String(),
                        HeatingAreaOfHouse = c.Int(),
                        NumberOfFlat = c.Int(),
                        NumberOfResidents = c.Int(),
                    })
                .PrimaryKey(t => t.HouseId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        HouseId = c.Int(),
                        TypeOfAccount = c.String(),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
            DropTable("dbo.Houses");
            DropTable("dbo.Employees");
            DropTable("dbo.Counters");
            DropTable("dbo.Consumers");
            DropTable("dbo.Accounts");
        }
    }
}
