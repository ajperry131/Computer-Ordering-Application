/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Account abstract class holds information related to an account.
    id - A unique identifier for the account
    username - The username the user created to represent them
    firstName - The first name of the user
    lastName - The last name of the user
    privilege - The level of access the account is given
  Account has concrete method provideGeneralDescription() and abstract
  method provideAccountDescription().
    provideGeneralDescription() prints formatted general information about the account
    provideAccountDescription() is meant to provide formatted account information such as
    privileges and accesses of the account.
*/
public abstract class Account {
    public int id { get; set; }
    public String userName { get; set; } = "";
    public String firstName { get; set; } = "";
    public String lastName { get; set; } = "";
    public virtual String privilege { get; set; } = "";

    protected Account(int id, String userName, String firstName, String lastName) {
        this.id = id;
        this.userName = userName;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    protected Account(String userName, String firstName, String lastName)
    {
        this.userName = userName;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    protected Account() {
        
    }

    public void provideGeneralDescription() {
        Console.WriteLine("This account ({0}) is owned by {1} {2}", userName, firstName, lastName);
    }
    public abstract void provideAccountDescription();

    public override string ToString() {
        return String.Format("ID: {0}, Username: {1}, First Name: {2}, Last Name: {3}",
                                id, userName, firstName, lastName);
    }
}