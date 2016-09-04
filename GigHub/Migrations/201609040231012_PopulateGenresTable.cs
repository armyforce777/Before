namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (Id, Name) Values(1,'Jazz')");
            Sql("Insert into Genres (Id, Name) Values(2,'Blue')");
            Sql("Insert into Genres (Id, Name) Values(3,'Rock')");
            Sql("Insert into Genres (Id, Name) Values(4,'Country')");
        }
        
        public override void Down()
        {
            //What happens in the Down Method is the reverse of what happened in Up Method
            Sql("Delete From Genres Where Id In (1,2,3,4)");
        }
    }
}
