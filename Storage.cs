/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Storage class inherits from the Component class and Descriptor interface.
  The Storage class holds information relative to a storage's features.
    formFactor - The build of the storage (2.5" SSD Drive, M.2 Drive, Portable SSD)
    interfaceType - The interface of the storage (SATA II, SATA III, PCI-E Gen 3)
    readSpeed - How quickly large files can be accessed (160MB/s, 560MB/s)
    writeSpeed - How quickly large files can take in data (160MB/s, 530MB/s)
    storageCapacity - The amount of available space to storage files (80GB, 4TB)
  It provides an implementation of provideDescription().
*/
public class Storage : Component, Descriptor {
    public String formFactor { get; set; } = "N/A";
    public String interfaceType { get; set; } = "N/A";
    public String readSpeed { get; set; } = "N/A";
    public String writeSpeed { get; set; } = "N/A";
    public String storageCapacity { get; set; } = "N/A";

    public Storage(int id, String make, String model, double price, String formFactor, 
        String interfaceType, String readSpeed, String writeSpeed, String storageCapacity)
        : base(id, make, model, price) {
        
        this.formFactor = formFactor;
        this.interfaceType = interfaceType;
        this.readSpeed = readSpeed;
        this.writeSpeed = writeSpeed;
        this.storageCapacity = storageCapacity;
    }

    public Storage(String make, String model, double price, String formFactor,
        String interfaceType, String readSpeed, String writeSpeed, String storageCapacity)
        : base(make, model, price) {

        this.formFactor = formFactor;
        this.interfaceType = interfaceType;
        this.readSpeed = readSpeed;
        this.writeSpeed = writeSpeed;
        this.storageCapacity = storageCapacity;
    }

    public Storage() {

    }

    public void provideDescription() {
        Console.WriteLine("The storage is the long term memory of the computer which holds your saved files.");
    }

    public override string ToString() {
        return String.Format("{0}, Form Factor: {1}, Interface Type: {2}, Read Speed: {3}, Write Speed: {4}, Capacity: {5}",
                                base.ToString(), formFactor, interfaceType, readSpeed, writeSpeed, storageCapacity);
    }

    public static Storage CreateStorage() {
        String make;
        String model;
        double price;
        String formFactor;
        String interfaceType;
        String readSpeed;
        String writeSpeed;
        String storageCapacity;

        make = InputProcessor.GetStringInput("Enter the make");
        model = InputProcessor.GetStringInput("Enter the model");
        price = InputProcessor.GetDoubleInput("Enter the price");
        formFactor = InputProcessor.GetStringInput("Enter the form factor");
        interfaceType = InputProcessor.GetStringInput("Enter the interface type");
        readSpeed = InputProcessor.GetStringInput("Enter the read speed");
        writeSpeed = InputProcessor.GetStringInput("Enter the write speed");
        storageCapacity = InputProcessor.GetStringInput("Enter the storage capacitiy");

        Storage storage = new Storage(make, model, price, formFactor, interfaceType, 
                                        readSpeed, writeSpeed, storageCapacity);
        
        return storage;
    }
}