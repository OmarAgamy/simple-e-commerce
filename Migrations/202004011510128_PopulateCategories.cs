namespace PhaseOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id,Name,No_Of_Products) VALUES(1,'Mobiles',10)");
            Sql("INSERT INTO Categories (Id,Name,No_Of_Products) VALUES(2,'Laptops',30)");
            Sql("INSERT INTO Categories (Id,Name,No_Of_Products) VALUES(3,'Electric Machines',40)");
            Sql("INSERT INTO Categories (Id,Name,No_Of_Products) VALUES(4,'Clothes',55)");
            Sql("INSERT INTO Categories (Id,Name,No_Of_Products) VALUES(5,'Toys',60)");
        }
        
        public override void Down()
        {
        }
    }
}
