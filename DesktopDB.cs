/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Desktops table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Desktops table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class DesktopDB
{
    public static void CreateTable(SQLiteConnection conn)
    {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Desktops (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Motherboard_ID integer\n"
        + " ,Processor_ID integer\n"
        + " ,Memory_ID integer\n"
        + " ,Graphics_Card_ID integer\n"
        + " ,Storage_ID integer);";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddDesktop(SQLiteConnection conn, Desktop d)
    {
        //adds the desktop to the database with integer references to the components
        string sql = string.Format(
        "INSERT INTO Desktops(Motherboard_ID, Processor_ID, Memory_ID, Graphics_Card_ID, Storage_ID) "
        + "VALUES({0},{1},{2},{3},{4})",
        d.motherboard.id, d.processor.id, d.memory.id, d.graphicsCard.id, d.storage.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateDesktop(SQLiteConnection conn, Desktop d)
    {
        MotherboardDB.UpdateMotherboard(conn, d.motherboard);
        ProcessorDB.UpdateProcessor(conn, d.processor);
        MemoryDB.UpdateMemory(conn, d.memory);
        GraphicsCardDB.UpdateGraphicsCard(conn, d.graphicsCard);
        StorageDB.UpdateStorage(conn, d.storage);
        string sql = string.Format(
        "UPDATE Desktops SET Motherboard_ID='{0}', Processor_ID='{1}', Memory_ID='{2}', Graphics_Card_ID='{3}', Storage_ID='{4}'"
        + " WHERE ID={4}", d.motherboard.id, d.processor.id, d.memory.id, d.graphicsCard.id, d.storage.id, d.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteDesktop(SQLiteConnection conn, int id)
    {
        Desktop d = DesktopDB.GetDesktop(conn, id);
        MotherboardDB.DeleteMotherboard(conn, d.motherboard.id);
        ProcessorDB.DeleteProcessor(conn, d.processor.id);
        MemoryDB.DeleteMemory(conn, d.memory.id);
        GraphicsCardDB.DeleteGraphicsCard(conn, d.graphicsCard.id);
        StorageDB.DeleteStorage(conn, d.storage.id);
        string sql = string.Format("DELETE from Desktops WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Desktop> GetAllDesktops(SQLiteConnection conn)
    {
        List<Desktop> Desktops = new List<Desktop>();
        string sql = "SELECT * FROM Desktops";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Desktops.Add(new Desktop(
            rdr.GetInt32(0),
            MotherboardDB.GetMotherboard(conn, rdr.GetInt32(1)),
            ProcessorDB.GetProcessor(conn, rdr.GetInt32(2)),
            MemoryDB.GetMemory(conn, rdr.GetInt32(3)),
            GraphicsCardDB.GetGraphicsCard(conn, rdr.GetInt32(4)),
            StorageDB.GetStorage(conn, rdr.GetInt32(5))));
        }
        return Desktops;
    }
    public static Desktop GetDesktop(SQLiteConnection conn, int id)
    {
        string sql = string.Format("SELECT * FROM Desktops WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            return new Desktop(
            rdr.GetInt32(0),
            MotherboardDB.GetMotherboard(conn, rdr.GetInt32(1)),
            ProcessorDB.GetProcessor(conn, rdr.GetInt32(2)),
            MemoryDB.GetMemory(conn, rdr.GetInt32(3)),
            GraphicsCardDB.GetGraphicsCard(conn, rdr.GetInt32(4)),
            StorageDB.GetStorage(conn, rdr.GetInt32(5))
            );
        }
        else
        {
            return new Desktop(-1, new Motherboard(), new Processor(), new Memory(), new GraphicsCard(), new Storage());
        }
    }

    public static Desktop GetLastRow(SQLiteConnection conn)
    {
        string sql = string.Format("SELECT * FROM Desktops WHERE ID = (SELECT MAX(ID) FROM Desktops)");
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            return new Desktop(
            rdr.GetInt32(0),
            MotherboardDB.GetMotherboard(conn, rdr.GetInt32(1)),
            ProcessorDB.GetProcessor(conn, rdr.GetInt32(2)),
            MemoryDB.GetMemory(conn, rdr.GetInt32(3)),
            GraphicsCardDB.GetGraphicsCard(conn, rdr.GetInt32(4)),
            StorageDB.GetStorage(conn, rdr.GetInt32(5))
            );
        }
        else
        {
            return new Desktop(-1, new Motherboard(), new Processor(), new Memory(), new GraphicsCard(), new Storage());
        }
    }
    
}