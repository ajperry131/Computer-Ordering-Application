/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Processors table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Processors table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class ProcessorDB {
    public static void CreateTable(SQLiteConnection conn) {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Processors (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Make varchar(40)\n"
        + " ,Model varchar(40)\n"
        + " ,Price real\n"
        + " ,Base_Clock varchar(40)\n"
        + " ,Max_Turbo_Speed varchar(40)\n"
        + " ,Core_Count integer\n"
        + " ,Generation varchar(40)\n"
        + " ,Chipset varchar(40)\n"
        + " ,Overclockable integer\n" //0 or 1 for bool
        + " ,Integrated_Graphics integer);"; //0 or 1 for bool
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddProcessor(SQLiteConnection conn, Processor p) {
        string sql = string.Format(
        "INSERT INTO Processors(Make, Model, Price, Base_Clock, Max_Turbo_Speed, Core_Count, Generation, Chipset, Overclockable, Integrated_Graphics) "
        + "VALUES('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}',{8},{9})",
        p.make, p.model, p.price, p.baseClock, p.maxTurboSpeed, p.coreCount, p.generation, p.chipset, p.overclockable, p.integratedGraphics);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateProcessor(SQLiteConnection conn, Processor p) {
        string sql = string.Format(
        "UPDATE Processors SET Make='{0}', Model='{1}', Price={2}, Base_Clock='{3}', Max_Turbo_Speed='{4}', Core_Count={5}, Generation='{6}', Chipset='{7}', Overclockable={8}, Integrated_Graphics={9}"
        + " WHERE ID={10}", p.make, p.model, p.price, p.baseClock, p.maxTurboSpeed, p.coreCount, p.generation, p.chipset, p.overclockable, p.integratedGraphics, p.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteProcessor(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from Processors WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Processor> GetAllProcessors(SQLiteConnection conn) {
        List<Processor> Processors = new List<Processor>();
        string sql = "SELECT * FROM Processors";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read()) {
            Processors.Add(new Processor(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetInt32(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetBoolean(9),
            rdr.GetBoolean(10)
            ));
        }
        return Processors;
    }
    public static Processor GetProcessor(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Processors WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read()) {
            return new Processor(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetInt32(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetBoolean(9),
            rdr.GetBoolean(10)
            );
        }
        else {
            return new Processor(-1, string.Empty, string.Empty, 0.0, string.Empty, string.Empty, 0, string.Empty, string.Empty, false, false);
        }
    }
}