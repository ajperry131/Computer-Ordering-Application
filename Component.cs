/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Component class holds broad information which all components have
    make - The make of the computer component
    model - The model of the computer component
    price - The price set for the computer component
*/
public class Component {
    public int id { get; set; } = -1;
    public String make {get; set;} = "N/A";
    public String model {get; set;} = "N/A";
    public double price {get; set;} = 0;

    public Component(int id, String make, String model, double price) {
        this.id = id;
        this.make = make;
        this.model = model;
        this.price = price;
    }

    public Component(String make, String model, double price) {
        this.make = make;
        this.model = model;
        this.price = price;
    }

    public Component() {
        
    }

    public override string ToString() {
        return String.Format("ID: {0}, Price: {1}, Make: {2}, Model: {3}", id, price, make, model);
    }
}