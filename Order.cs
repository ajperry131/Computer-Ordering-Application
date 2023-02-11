/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Order class holds details related to the order of a desktop.
    id - A unique identifier for the order
    account - The account the purchase was made with
    desktop - The desktop that is being ordered
    payment - The total payment of the order
    deliveryAddress - The address to send the desktop once built
    status - The current status of the order (Cancelled, In Progress, Completed)
*/
public class Order {
    public int id { get; set; }
    public Account account { get; set; }
    public Desktop desktop { get; set; }
    public double payment { get; set; }
    public String deliveryAddress { get; set; }
    public String status { get; set; }

    public Order(int id, Account account, Desktop desktop, double payment, String deliveryAddress, String status) {
        this.id = id;
        this.account = account;
        this.desktop = desktop;
        this.payment = payment;
        this.deliveryAddress = deliveryAddress;
        this.status = status;
    }

    public Order(Account account, Desktop desktop, double payment, String deliveryAddress, String status) {
        this.account = account;
        this.desktop = desktop;
        this.payment = payment;
        this.deliveryAddress = deliveryAddress;
        this.status = status;
    }

    public override string ToString() {
        return String.Format("Order ID: {0}\nAccount: {1}\nDesktop: {2}\nPayment: {3}\nDelivery Address: {4}\nStatus: {5}",
                                id, account, desktop, payment, deliveryAddress, status);
    }
}