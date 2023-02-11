/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Motherboards table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Motherboards table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class MotherboardDB
{
    public static void CreateTable(SQLiteConnection conn) {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Motherboards (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Make varchar(40)\n"
        + " ,Model varchar(40)\n"
        + " ,Price real\n"
        + " ,Form_Factor varchar(40)\n"
        + " ,Socket_Type varchar(40)\n"
        + " ,Chipset varchar(40)\n"
        + " ,Memory_DDR_Slot_Type varchar(40)\n"
        + " ,PCIE_Slot varchar(40)\n"
        + " ,M2_NVME_Support integer);"; //0 or 1 for bool
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddMotherboard(SQLiteConnection conn, Motherboard m) {
        string sql = string.Format(
        "INSERT INTO Motherboards(Make, Model, Price, Form_Factor, Socket_Type, Chipset, Memory_DDR_Slot_Type, PCIE_Slot, M2_NVME_Support) "
        + "VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8})",
        m.make, m.model, m.price, m.formFactor, m.socketType, m.chipset, m.memoryDDRSlotType, m.pcieSlot, m.m2nvmeSupport);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateMotherboard(SQLiteConnection conn, Motherboard m) {
        string sql = string.Format(
        "UPDATE Motherboards SET Make='{0}', Model='{1}', Price={2}, Form_Factor='{3}', Socket_Type='{4}', Chipset='{5}', Memory_DDR_Slot_Type='{6}', PCIE_Slot='{7}', M2_NVME_Support={8}"
        + " WHERE ID={9}", m.make, m.model, m.price, m.formFactor, m.socketType, m.chipset, m.memoryDDRSlotType, m.pcieSlot, m.m2nvmeSupport, m.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteMotherboard(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from Motherboards WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Motherboard> GetAllMotherboards(SQLiteConnection conn) {
        List<Motherboard> Motherboards = new List<Motherboard>();
        string sql = "SELECT * FROM Motherboards";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read()) {
            Motherboards.Add(new Motherboard(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetBoolean(9)
            ));
        }
        return Motherboards;
    }
    public static Motherboard GetMotherboard(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Motherboards WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read()) {
            return new Motherboard(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetBoolean(9)
            );
        }
        else {
            return new Motherboard(-1, string.Empty, string.Empty, 0.0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
        }
    }
}