/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the GraphicsCards table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the GraphicsCards table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class GraphicsCardDB {
    public static void CreateTable(SQLiteConnection conn) {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS GraphicsCards (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Make varchar(40)\n"
        + " ,Model varchar(40)\n"
        + " ,Price real\n"
        + " ,GPU_Type varchar(40)\n"
        + " ,Core_Count integer\n"
        + " ,Core_Speed varchar(40)\n"
        + " ,Memory_GDDR varchar(40)\n"
        + " ,Memory_Size varchar(40)\n"
        + " ,Memory_Bandwidth varchar(40)\n"
        + " ,PCIE_Type varchar(40));";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddGraphicsCard(SQLiteConnection conn, GraphicsCard g) {
        string sql = string.Format(
        "INSERT INTO GraphicsCards(Make, Model, Price, GPU_Type, Core_Count, Core_Speed, Memory_GDDR, Memory_Size, Memory_Bandwidth, PCIE_Type) "
        + "VALUES('{0}','{1}',{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}')",
        g.make, g.model, g.price, g.gpuType, g.coreCount, g.coreSpeed, g.memoryGDDR, g.memorySize, g.memoryBandwidth, g.pcieType);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateGraphicsCard(SQLiteConnection conn, GraphicsCard g) {
        string sql = string.Format(
        "UPDATE GraphicsCards SET Make='{0}', Model='{1}', Price={2}, GPU_Type='{3}', Core_Count={4}, Core_Speed='{5}', Memory_GDDR='{6}', Memory_Size='{7}', Memory_Bandwidth='{8}', PCIE_Type='{9}'"
        + " WHERE ID={10}", g.make, g.model, g.price, g.gpuType, g.coreCount, g.coreSpeed, g.memoryGDDR, g.memorySize, g.memoryBandwidth, g.pcieType, g.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteGraphicsCard(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from GraphicsCards WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<GraphicsCard> GetAllGraphicsCards(SQLiteConnection conn) {
        List<GraphicsCard> GraphicsCards = new List<GraphicsCard>();
        string sql = "SELECT * FROM GraphicsCards";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read()) {
            GraphicsCards.Add(new GraphicsCard(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetInt32(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetString(9),
            rdr.GetString(10)
            ));
        }
        return GraphicsCards;
    }
    public static GraphicsCard GetGraphicsCard(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM GraphicsCards WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read()) {
            return new GraphicsCard(
            rdr.GetInt32(0),
            rdr.GetString(1),
            rdr.GetString(2),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetInt32(5),
            rdr.GetString(6),
            rdr.GetString(7),
            rdr.GetString(8),
            rdr.GetString(9),
            rdr.GetString(10)
            );
        }
        else {
            return new GraphicsCard(-1, string.Empty, string.Empty, 0.0, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}