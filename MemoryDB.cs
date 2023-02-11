/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Memorys table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Memorys table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class MemoryDB {
    public static void CreateTable(SQLiteConnection conn) {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Memorys (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Make varchar(40)\n"
        + " ,Model varchar(40)\n"
        + " ,Price real\n"
        + " ,Size varchar(40)\n"
        + " ,Memory_DDR varchar(40)\n"
        + " ,Memory_Bandwidth varchar(40)\n"
        + " ,Frequency varchar(40)\n"
        + " ,Overclockable integer);"; //0 or 1 for bool
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddMemory(SQLiteConnection conn, Memory m) {
        string sql = string.Format(
        "INSERT INTO Memorys(Make, Model, Price, Size, Memory_DDR, Memory_Bandwidth, Frequency, Overclockable) "
        + "VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}',{7})",
        m.make, m.model, m.price, m.size, m.memoryDDR, m.memoryBandwidth, m.frequency, m.overclockable);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateMemory(SQLiteConnection conn, Memory m) {
        string sql = string.Format(
        "UPDATE Memorys SET Make='{0}', Model='{1}', Price={2}, Size='{3}', Memory_DDR='{4}', Memory_Bandwidth='{5}', Frequency='{6}', Overclockable={7}"
        + " WHERE ID={8}", m.make, m.model, m.price, m.size, m.memoryDDR, m.memoryBandwidth, m.frequency, m.overclockable, m.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteMemory(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from Memorys WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Memory> GetAllMemorys(SQLiteConnection conn) {
        List<Memory> Memorys = new List<Memory>();
        string sql = "SELECT * FROM Memorys";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read()) {
            Memorys.Add(new Memory(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetBoolean(8)
            ));
        }
        return Memorys;
    }
    public static Memory GetMemory(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Memorys WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read()) {
            return new Memory(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetBoolean(8)
            );
        }
        else {
            return new Memory(-1, string.Empty, string.Empty, 0.0, string.Empty, string.Empty, string.Empty, string.Empty, false);
        }
    }
}