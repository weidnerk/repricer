using System.Data.Entity;

public class DataModelsDB : DbContext
{
    static DataModelsDB()
    {
        //do not try to create a database 
        Database.SetInitializer<DataModelsDB>(null);
    }

    public DataModelsDB()
        : base("name=OPWContext")
    {
    }

    public DbSet<PostedListing> Listings { get; set; }

}
