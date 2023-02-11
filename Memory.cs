/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Memory class inherits from the Component class and Descriptor interface.
  The Memory class holds information relative to a memory stick's features.
    size - The size of the memory (8GB, 16GB)
    ramDDR - The type of memory DDR (DDR3, DDR4)
    memoryBandwidth - The rate at which it can read or write data to/from the processor (25600 MB/s)
    frequency - The maximum number of commands it can handle per second (2400 MT/s, 2666 MT/s)
    overclockable - A true or false variable for whether the memory can be manually pushed past its normal speeds
  It provides an implementation of provideDescription().
*/
public class Memory : Component, Descriptor {
    public String size {get; set;} = "N/A";
    public String memoryDDR { get; set; } = "N/A";
    public String memoryBandwidth { get; set; } = "N/A";
    public String frequency { get; set; } = "N/A";
    public bool overclockable { get; set; } = false;

    public Memory(int id, String make, String model, double price, String size, 
        String memoryDDR, String memoryBandwidth, String frequency, bool overclockable)
        : base(id, make, model, price) {
        
        this.size = size;
        this.memoryDDR = memoryDDR;
        this.memoryBandwidth = memoryBandwidth;
        this.frequency = frequency;
        this.overclockable = overclockable;
    }

    public Memory(String make, String model, double price, String size,
        String memoryDDR, String memoryBandwidth, String frequency, bool overclockable)
        : base(make, model, price) {

        this.size = size;
        this.memoryDDR = memoryDDR;
        this.memoryBandwidth = memoryBandwidth;
        this.frequency = frequency;
        this.overclockable = overclockable;
    }

    public Memory() {

    }

    public void provideDescription() {
        Console.WriteLine("The memory (RAM) is the short term memory where data is stored as the processor needs it");
    }

    public override string ToString() {
        return String.Format("{0}, Size: {1}, Memory DDR: {2}, Memory Bandwidth: {3}, Frequency {4}, Overclockable {5}",
                                base.ToString(), size, memoryDDR, memoryBandwidth, frequency, overclockable);
    }

    public static Memory CreateMemory() {
        String make;
        String model;
        double price;
        String size;
        String memoryDDR;
        String memoryBandwidth;
        String frequency;
        bool overclockable;

        make = InputProcessor.GetStringInput("Enter the make");
        model = InputProcessor.GetStringInput("Enter the model");
        price = InputProcessor.GetDoubleInput("Enter the price");
        size = InputProcessor.GetStringInput("Enter the size");
        memoryDDR = InputProcessor.GetStringInput("Enter the memory DDR");
        memoryBandwidth = InputProcessor.GetStringInput("Enter the memory bandwidth");
        frequency = InputProcessor.GetStringInput("Enter the frequency");
        overclockable = InputProcessor.GetBoolInput("Is it overclockable? (true or false)");

        Memory memory = new Memory(make, model, price, size, memoryDDR, 
                                    memoryBandwidth, frequency, overclockable);

        return memory;
    }
}