/*******************************************************************
* Name: Andrew Perry
* Date: 9/24/2022
* Assignment: Course Project
*
* Class to handle all interactions with the Accounts table in the
* database, including creating the table if it doesn't exist and all
* CRUD (Create, Read Update, Delete) operations on the Accounts table.
* Note that the interactions are all done using standard SQL syntax
* that is then executed by the SQLite library.
*/
using System.Data.SQLite;
public class AccountDB {
    public static void CreateTable(SQLiteConnection conn) {
        // SQL statement for creating a new table
        string sql =
        "CREATE TABLE IF NOT EXISTS Accounts (\n"
        + " ID integer PRIMARY KEY\n"
        + " ,User_Name varchar(20)\n"
        + " ,First_Name varchar(20)\n"
        + " ,Last_Name varchar(20)\n"
        + " ,Privilege varchar(20));";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void AddAccount(SQLiteConnection conn, Account a) {
        string sql = string.Format(
        "INSERT INTO Accounts(User_Name, First_Name, Last_Name, Privilege) "
        + "VALUES('{0}','{1}','{2}','{3}')",
        a.userName, a.firstName, a.lastName, a.privilege);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void UpdateAccount(SQLiteConnection conn, Account a) {
        string sql = string.Format(
        "UPDATE Accounts SET User_Name='{0}', First_Name='{1}', Last_Name='{2}', Privilege='{3}'"
        + " WHERE ID={4}", a.userName, a.firstName, a.lastName, a.privilege, a.id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static void DeleteAccount(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from Accounts WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
    public static List<Account> GetAllAccounts(SQLiteConnection conn) {
        List<Account> Accounts = new List<Account>();
        string sql = "SELECT * FROM Accounts";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read()) {
            if (rdr.GetString(4) == "low") { //if privilege is low
                Accounts.Add(new UserAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                ));
            }
            else { //if privilege is high
                Accounts.Add(new AdminAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                ));
            }
        }
        return Accounts;
    }
    public static Account GetAccount(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Accounts WHERE ID = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read()) {
            if (rdr.GetString(4) == "low") { //if privilege is low
                return new UserAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                );
            }
            else { //if privilege is high
                return new AdminAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                );
            }
        }
        else {
            return new UserAccount(-1, string.Empty, string.Empty, string.Empty);
        }
    }

    public static Account GetAccount(SQLiteConnection conn, String userName)
    {
        string sql = string.Format("SELECT * FROM Accounts WHERE User_Name = '{0}'", userName);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
        {
            if (rdr.GetString(4) == "low")
            { //if privilege is low
                return new UserAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                );
            }
            else if (rdr.GetString(4) == "high")
            { //if privilege is high
                return new AdminAccount(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3)
                );
            }
            return new UserAccount(-1, string.Empty, string.Empty, string.Empty);
        }
        else
        {
            return new UserAccount(-1, string.Empty, string.Empty, string.Empty);
        }
    }
}