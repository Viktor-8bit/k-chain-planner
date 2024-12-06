namespace Infrastructure.Data;

public class ConnectionString
{
    public static string connectionString 
        = "Host=localhost;Port=5432;Database=killchan;Username=postgres;Password=4z5636spxr1p8wxkb186akyr84e4e7o78";
    
    //PS C:\Users\amomomogugus> docker run -p 5432:5432 --name postgres -e POSTGRES_PASSWORD=4z5636spxr1p8wxkb186akyr84e4e7o78 -d postgres
    //3f158b670883257ff2e591fa635f9b37ae8583325c7a13af47efb16c354a350a
}