namespace Gighub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert INTO Genres(Id,Name) Values (1,'Jazz')");
            Sql("Insert INTO Genres(Id,Name) Values (2,'Blues')");
            Sql("Insert INTO Genres(Id,Name) Values (3,'Rock')");
            Sql("Insert INTO Genres(Id,Name) Values (4,'Country')");
        }

        public override void Down()
        {
            Sql("Delete From Genres Where Id IN (1,2,3,4)");
        }
    }
}
