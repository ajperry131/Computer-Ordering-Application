/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Storages table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Storages table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class StorageDB
{
    public static void CreateTable(SQLiteConnection conn)
    {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Storages (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Make varchar(40)\n"
        + " ,Model varchar(40)\n"
        + " ,Price real\n"
        + " ,Form_Factor varchar(40)\n"
        + " ,Interface_Type varchar(40)\n"
        + " ,Read_Speed varchar(40)\n"
        + " ,Write_Speed varchar(40)\n"
        + " ,Storage_Capacity varchar(40));";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddStorage(SQLiteConnection conn, Storage s)
    {
        string sql = string.Format(
        "INSERT INTO Storages(Make, Model, Price, Form_Factor, Interface_Type, Read_Speed, Write_Speed, Storage_Capacity) "
        + "VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}')",
        s.make, s.model, s.price, s.formFactor, s.interfaceType, s.readSpeed, s.writeSpeed, s.storageCapacity);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateStorage(SQLiteConnection conn, Storage s)
    {
        string sql = string.Format(
        "UPDATE Storages SET Make='{0}', Model='{1}', Price={2}, Form_Factor='{3}', Interface_Type='{4}', Read_Speed='{5}', Write_Speed='{6}', Storage_Capacity='{7}'"
        + " WHERE ID={8}", s.make, s.model, s.price, s.formFactor, s.interfaceType, s.readSpeed, s.writeSpeed, s.storageCapacity, s.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteStorage(SQLiteConnection conn, int id)
    {
        string sql = string.Format("DELETE from Storages WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Storage> GetAllStorages(SQLiteConnection conn)
    {
        List<Storage> Storages = new List<Storage>();
        string sql = "SELECT * FROM Storages";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Storages.Add(new Storage(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8)
            ));
        }
        return Storages;
    }
    public static Storage GetStorage(SQLiteConnection conn, int id)
    {
        string sql = string.Format("SELECT * FROM Storages WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            return new Storage(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8)
            );
        }
        else
        {
            return new Storage(-1, string.Empty, string.Empty, 0.0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}