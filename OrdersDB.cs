/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Orders table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Orders table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class OrderDB
{
    public static void CreateTable(SQLiteConnection conn)
    {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Orders (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,Account_ID integer\n"
        + " ,Desktop_ID integer\n"
        + " ,Payment real\n"
        + " ,Delivery_Address varchar(40)\n"
        + " ,Status varchar(20));";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddOrder(SQLiteConnection conn, Order o)
    {
        string sql = string.Format(
        "INSERT INTO Orders(Account_ID, Desktop_ID, Payment, Delivery_Address, Status) "
        + "VALUES({0},{1},{2},'{3}','{4}')",
        o.account.id, o.desktop.id, o.payment, o.deliveryAddress, o.status);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateOrder(SQLiteConnection conn, Order o)
    {
        string sql = string.Format(
        "UPDATE Orders SET Account_ID={0}, Desktop_ID={1}, Payment={2}, Delivery_Address='{3}', Status='{4}'"
        + " WHERE ID={5}", o.account.id, o.desktop.id, o.payment, o.deliveryAddress, o.status, o.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteOrder(SQLiteConnection conn, int id)
    {
        Order order = OrderDB.GetOrder(conn, id);
        DesktopDB.DeleteDesktop(conn, order.desktop.id);
        string sql = string.Format("DELETE from Orders WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Order> GetAllOrders(SQLiteConnection conn)
    {
        List<Order> Orders = new List<Order>();
        string sql = "SELECT * FROM Orders";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Orders.Add(new Order(
            rdr.GetInt32(0),
            AccountDB.GetAccount(conn, rdr.GetInt32(1)),
            DesktopDB.GetDesktop(conn, rdr.GetInt32(2)),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5)
            ));
        }
        return Orders;
    }

    public static List<Order> GetAllOrdersOfAccount(SQLiteConnection conn, int id)
    {
        List<Order> Orders = new List<Order>();
        string sql = String.Format("SELECT * FROM Orders WHERE Account_ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            Orders.Add(new Order(
            rdr.GetInt32(0),
            AccountDB.GetAccount(conn, rdr.GetInt32(1)),
            DesktopDB.GetDesktop(conn, rdr.GetInt32(2)),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5)
            ));
        }
        return Orders;
    }

    public static Order GetOrder(SQLiteConnection conn, int id)
    {
        string sql = string.Format("SELECT * FROM Orders WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            return new Order(
            rdr.GetInt32(0),
            AccountDB.GetAccount(conn, rdr.GetInt32(1)),
            DesktopDB.GetDesktop(conn, rdr.GetInt32(2)),
            rdr.GetDouble(3),
            rdr.GetString(4),
            rdr.GetString(5)
            );
        }
        else
        {
            return new Order(-1, new UserAccount(), new Desktop(), 0.0, string.Empty, string.Empty);
        }
    }
}