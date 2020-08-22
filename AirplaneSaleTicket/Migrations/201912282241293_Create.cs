namespace AirplaneSaleTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airplanes",
                c => new
                    {
                        AirplaneID = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Name = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AirplaneID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        AirplaneID = c.Int(nullable: false),
                        FromFlight = c.String(),
                        ToFlight = c.String(),
                        Date = c.DateTime(nullable: false),
                        Hour = c.DateTime(nullable: false),
                        Price = c.Single(nullable: false),
                        EmptySeat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Airplanes", t => t.AirplaneID, cascadeDelete: true)
                .Index(t => t.AirplaneID);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        FlightID = c.Int(nullable: false),
                        PNR = c.String(),
                        SeatNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PassengerID)
                .ForeignKey("dbo.Flights", t => t.FlightID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.FlightID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        ID = c.Long(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.PersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "PersonID", "dbo.People");
            DropForeignKey("dbo.Passengers", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.Flights", "AirplaneID", "dbo.Airplanes");
            DropIndex("dbo.Passengers", new[] { "FlightID" });
            DropIndex("dbo.Passengers", new[] { "PersonID" });
            DropIndex("dbo.Flights", new[] { "AirplaneID" });
            DropTable("dbo.People");
            DropTable("dbo.Passengers");
            DropTable("dbo.Flights");
            DropTable("dbo.Airplanes");
        }
    }
}
