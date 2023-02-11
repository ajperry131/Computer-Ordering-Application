/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Processor class inherits from the Component class and Descriptor interface.
  The Processor class holds information relative to a processor's features.
    baseClock - The base clock speed of the processor (ex. 3.1GHz)
    maxTurboSpeed - The max speed increase for a temporary performance boost when needed (ex. +0.4GHz)
    coreCount - The amount of microprocessors that make up a processor
    generation - The generation of the processor (Threadripper, 8th gen & 9th gen)
    chipset - The chipset of the processor (X570, B550)
    overclockable - A true or false variable for whether the CPU can be manually pushed past its base speed
    integratedGraphics - A true or false variable for whether or not the CPU can render graphics without a graphics card
  It provides an implementation of provideDescription().
*/
public class Processor : Component, Descriptor {
    public String baseClock {get; set;} = "N/A";
    public String maxTurboSpeed { get; set; } = "N/A";
    public int coreCount { get; set; } = 0;
    public String generation { get; set; } = "N/A";
    public String chipset { get; set; } = "N/A";
    public bool overclockable { get; set; }
    public bool integratedGraphics { get; set; } = false;

    public Processor(int id, String make, String model, double price, String baseClock, String maxTurboSpeed,
        int coreCount, String generation, String chipset, bool overclockable, bool integratedGraphics) 
        : base(id, make, model, price) {
        
        this.baseClock = baseClock;
        this.maxTurboSpeed = maxTurboSpeed;
        this.coreCount = coreCount;
        this.generation = generation;
        this.chipset = chipset;
        this.overclockable = overclockable;
        this.integratedGraphics = integratedGraphics;
    }

    public Processor(String make, String model, double price, String baseClock, String maxTurboSpeed,
        int coreCount, String generation, String chipset, bool overclockable, bool integratedGraphics)
        : base(make, model, price) {

        this.baseClock = baseClock;
        this.maxTurboSpeed = maxTurboSpeed;
        this.coreCount = coreCount;
        this.generation = generation;
        this.chipset = chipset;
        this.overclockable = overclockable;
        this.integratedGraphics = integratedGraphics;
    }

    public Processor() {

    }

    public void provideDescription() {
        Console.WriteLine("The processor is the brain of your computer which provides the instructions and processing power the computer needs to do its work");
    }

    public override string ToString() {
        return String.Format("{0}, Base Clock: {1}, Max Turbo Speed: {2}, Core Count: {3}, Generation: {4}, Chipset: {5}, Overclockable: {6}, Integrated Graphics: {7}",
                                base.ToString(), baseClock, maxTurboSpeed, coreCount, generation, chipset, overclockable, integratedGraphics);
    }

    public static Processor CreateProcessor() {
        String make;
        String model;
        double price;
        String baseClock;
        String maxTurboSpeed;
        int coreCount;
        String generation;
        String chipset;
        bool overclockable;
        bool integratedGraphics;

        make = InputProcessor.GetStringInput("Enter the make");
        model = InputProcessor.GetStringInput("Enter the model");
        price = InputProcessor.GetDoubleInput("Enter the price");
        baseClock = InputProcessor.GetStringInput("Enter the base clock");
        maxTurboSpeed = InputProcessor.GetStringInput("Enter the max turbo speed");
        coreCount = InputProcessor.GetIntInput("Enter the core count");
        generation = InputProcessor.GetStringInput("Enter the generation");
        chipset = InputProcessor.GetStringInput("Enter the chipset");
        overclockable = InputProcessor.GetBoolInput("Is it overclockable?");
        integratedGraphics = InputProcessor.GetBoolInput("Does it have integrated graphics? (true or false)");

        Processor processor = new Processor(make, model, price, baseClock, maxTurboSpeed, coreCount, 
                                            generation, chipset, overclockable, integratedGraphics);
        
        return processor;
    }
}