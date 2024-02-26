// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.IO;
using System.Data.SQLite;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
CopyData();
static void CopyData()
{
    string? Firm;
    int Index;
    string? Region, Area, City, Street, Home, Frame, Structure, Flat;

    string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB.db");
    SqlConnection sqlConnection = new("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Konvert\\bin\\Debug\\net5.0-windows\\DB.mdf;Integrated Security=True;Connect Timeout=30");
    SQLiteConnection sqliteConnection = new("Data Source = " + path);
    sqlConnection.Open();
    sqliteConnection.Open();

    SqlCommand sqlcommand = new("SELECT * FROM Recipient", sqlConnection);
    SqlDataReader sqlreader = sqlcommand.ExecuteReader();

    SQLiteCommand command = new("INSERT INTO Recipient (Firm, Indexs, Region, Area, City, Street, Home, Frame, Structure, Flat)" +
                " VALUES (@Firm, @Index, @Region, @Area, @City, @Street, @Home, @Frame, @Structure, @Flat)", sqliteConnection);
    

    while (sqlreader.Read())
    {
        
        Firm = sqlreader[1].ToString();
        Index = Convert.ToInt32(sqlreader[2], CultureInfo.CurrentCulture);
        Region = sqlreader[3].ToString();
        Area = sqlreader[4].ToString();
        City = sqlreader[5].ToString();
        Street = sqlreader[6].ToString();
        Home = sqlreader[7].ToString();
        Frame = sqlreader[8].ToString();
        Structure = sqlreader[9].ToString();
        Flat = sqlreader[10].ToString();

        Console.WriteLine(Firm);

        command.Parameters.AddWithValue("@Firm", Firm);
        command.Parameters.AddWithValue("@Index", Index);
        command.Parameters.AddWithValue("@Region", Region);
        command.Parameters.AddWithValue("@Area", Area);
        command.Parameters.AddWithValue("@City", City);
        command.Parameters.AddWithValue("@Street", Street);
        command.Parameters.AddWithValue("@Home", Home);
        command.Parameters.AddWithValue("@Frame", Frame);
        command.Parameters.AddWithValue("@Structure", Structure);
        command.Parameters.AddWithValue("@Flat", Flat);
        command.ExecuteNonQuery();
    }
    sqlreader.Close();
    
    sqlConnection.Close();
    sqliteConnection.Close();
}