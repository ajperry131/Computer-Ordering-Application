/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The GraphicsCard class inherits from the Component class and Descriptor interface.
  The GraphicsCard class holds information relative to a graphics card's features.
    gpuType - The type of build the graphics card has (Nvidia, AMD)
    coreCount - The amount of cores within the graphics Card (2304 Stream Processors, 896 CUDA Cores)
    coreSpeed - The speed of each core in the graphics card (1465MHz)
    memoryGDDR - The memory type of the graphics card (GDDR5, GDDR5x, GDDR6)
    memorySize - The amount of VRAM of the graphics card (4GB, 8GB)
    memoryBandwidth - How fast the VRAM on the graphics card can be accessed and used (25600 MB/s)
    pcieType - The type of PCI-E the graphics card has (PCI-E 3.0, PCI-E 4.0)
  It provides an implementation of provideDescription().
*/
public class GraphicsCard : Component, Descriptor {
    public String gpuType { get; set; } = "N/A";
    public int coreCount { get; set; } = 0;
    public String coreSpeed { get; set; } = "N/A";
    public String memoryGDDR { get; set; } = "N/A";
    public String memorySize { get; set; } = "N/A";
    public String memoryBandwidth { get; set; } = "N/A";
    public String pcieType { get; set; } = "N/A";

    public GraphicsCard(int id, String make, String model, double price, String gpuType, int coreCount, 
        String coreSpeed, String memoryDDR, String memorySize, String memoryBandwidth, String pcieType)
        : base(id, make, model, price) {

        this.gpuType = gpuType;
        this.coreCount = coreCount;
        this.coreSpeed = coreSpeed;
        this.memoryGDDR = memoryDDR;
        this.memorySize = memorySize;
        this.memoryBandwidth = memoryBandwidth;
        this.pcieType = pcieType;
    }

    public GraphicsCard(String make, String model, double price, String gpuType, int coreCount,
        String coreSpeed, String memoryDDR, String memorySize, String memoryBandwidth, String pcieType)
        : base(make, model, price) {

        this.gpuType = gpuType;
        this.coreCount = coreCount;
        this.coreSpeed = coreSpeed;
        this.memoryGDDR = memoryDDR;
        this.memorySize = memorySize;
        this.memoryBandwidth = memoryBandwidth;
        this.pcieType = pcieType;
    }

    public GraphicsCard() {

    }

    public void provideDescription() {
        Console.WriteLine("The graphics card is the primary component for rendering graphics onto screens");
    }

    public override string ToString() {
        return String.Format("{0}, GPU Type: {1}, Core Count: {2}, Core Speed: {3}, Memory GDDR: {4}, Memory Size: {5}, Memory Bandwidth: {6}, PCIE Type: {7}",
                                base.ToString(), gpuType, coreCount, coreSpeed, memoryGDDR, memorySize, memoryBandwidth, pcieType);
    }

    public static GraphicsCard CreateGraphicsCard() {
        String make;
        String model;
        double price;
        String gpuType;
        int coreCount;
        String coreSpeed;
        String memoryDDR;
        String memorySize;
        String memoryBandwidth;
        String pcieType;

        make = InputProcessor.GetStringInput("Enter the make");
        model = InputProcessor.GetStringInput("Enter the model");
        price = InputProcessor.GetDoubleInput("Enter the price");
        gpuType = InputProcessor.GetStringInput("Enter the GPU type");
        coreCount = InputProcessor.GetIntInput("Enter the core count");
        coreSpeed = InputProcessor.GetStringInput("Enter the core speed");
        memoryDDR = InputProcessor.GetStringInput("Enter the memory DDR");
        memorySize = InputProcessor.GetStringInput("Enter the memory size");
        memoryBandwidth = InputProcessor.GetStringInput("Enter the memory bandwidth");
        pcieType = InputProcessor.GetStringInput("Enter the PCIE type");

        GraphicsCard graphicsCard = new GraphicsCard(make, model, price, gpuType, coreCount, coreSpeed,
                                                        memoryDDR, memorySize, memoryBandwidth, pcieType);
    
        return graphicsCard;
    }

}