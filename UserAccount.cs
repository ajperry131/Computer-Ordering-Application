/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The UserAccount class inherits from Account and provides a specific low level
  privilege to the class. It implements the abstract method provideAccountDescription(),
  returning privilege level and accesses.
*/
public class UserAccount : Account {
    public override String privilege { get; set; } = "low";
    
    public UserAccount(int id, String userName, String firstName, String lastName)
    : base(id, userName, firstName, lastName) {
    }

    public UserAccount(String userName, String firstName, String lastName)
    : base(userName, firstName, lastName) {
    }

    public UserAccount() {
        
    }

    public override void provideAccountDescription() {
        Console.WriteLine("This account is a {0}-level user account which can create orders", privilege);
    }

    public override string ToString()
    {
        return String.Format("{0}, Privilege: {1}",
                                base.ToString(), privilege);
    }
}