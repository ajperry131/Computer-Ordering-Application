/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The AdminAccount class inherits from Account and provides a specific high level
  privilege to the class. It implements the abstract method provideAccountDescription(),
  returning privilege level and accesses.
*/
public class AdminAccount : Account
{
    public override String privilege { get; set; } = "high";

    public AdminAccount(int id, String userName, String firstName, String lastName)
    : base(id, userName, firstName, lastName) {
    }

    public AdminAccount(String userName, String firstName, String lastName)
    : base(userName, firstName, lastName) {
    }

    public AdminAccount() {
        
    }

    public override void provideAccountDescription() {
        Console.WriteLine("This account is a {0}-level user account which can change the status of existing orders", privilege);
    }

    public override string ToString() {
        return String.Format("{0}, Privilege: {1}",
                                base.ToString(), privilege);
    }
}