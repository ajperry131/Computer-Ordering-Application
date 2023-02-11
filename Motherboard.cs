/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Motherboard class inherits from the Component class and Descriptor interface.
  The Motherboard class holds information relative to a motherboard's features.
    formFactor - The build size of the motherboard (standard-ATX, Micro-ATX)
    socketType - The type of socket that determines which CPU the motherboard is compatible with (AMD Threadripper, Intel 8th & 9th gen)
    chipset - The type of chipset that determines which CPU the motherboard is compatible with (X570, B550)
    memorySlotType - The type of memory slot that determines which memory the motherboard is compatible with (DDR3, DDR4)
    pcieSlot - The type of pcie slot that determines which graphics cards the motherboard is compatible with (PCI-E 3.0, PCI-E 4.0)
    m2nvmeSupport - A true or false variable for whether or not the motherboard supports M.2 NVME SSD storage
  It provides an implementation of provideDescription().
*/
public class Motherboard : Component, Descriptor {
    public String formFactor {get; set;} = "N/A";
    public String socketType { get; set; } = "N/A";
    public String chipset { get; set; } = "N/A";
    public String memoryDDRSlotType { get; set; } = "N/A";
    public String pcieSlot { get; set; } = "N/A";
    public bool m2nvmeSupport { get; set; } = false;

    public Motherboard(int id, String make, String model, double price, String formFactor, 
        String socketType, String chipset, String ramDDRSlotType, String pcieSlot, bool m2nvmeSupport)
        : base(id, make, model, price) {
        
        this.formFactor = formFactor;
        this.socketType = socketType;
        this.chipset = chipset;
        this.memoryDDRSlotType = ramDDRSlotType;
        this.pcieSlot = pcieSlot;
        this.m2nvmeSupport = m2nvmeSupport;
    }

    public Motherboard(String make, String model, double price, String formFactor,
        String socketType, String chipset, String ramDDRSlotType, String pcieSlot, bool m2nvmeSupport)
        : base(make, model, price) {

        this.formFactor = formFactor;
        this.socketType = socketType;
        this.chipset = chipset;
        this.memoryDDRSlotType = ramDDRSlotType;
        this.pcieSlot = pcieSlot;
        this.m2nvmeSupport = m2nvmeSupport;
    }

    public Motherboard() {

    }

    public void provideDescription() {
        Console.WriteLine("The motherboard the foundation of your computer which allows each component connected to it to communicate with eachother");
    }

    public override string ToString() {
        return String.Format("{0}, Form Factor: {1}, Socket Type: {2}, Chipset: {3}, Memory-Slot Type: {4}, PCIE Slot: {5}, M.2 NVME Support: {6}", 
                                base.ToString(), formFactor, socketType, chipset, memoryDDRSlotType, pcieSlot, m2nvmeSupport);
    }

    public static Motherboard CreateMotherboard() {
        String make;
        String model;
        double price;
        String formFactor;
        String socketType;
        String chipset;
        String memoryDDRSlotType;
        String pcieSlot;
        bool m2nvmeSupport;

        make = InputProcessor.GetStringInput("Enter the make");
        model = InputProcessor.GetStringInput("Enter the model");
        price = InputProcessor.GetDoubleInput("Enter the price");
        formFactor = InputProcessor.GetStringInput("Enter the form factor");
        socketType = InputProcessor.GetStringInput("Enter the socket type");
        chipset = InputProcessor.GetStringInput("Enter the chipset");
        memoryDDRSlotType = InputProcessor.GetStringInput("Enter the Memory DDR Slot Type");
        pcieSlot = InputProcessor.GetStringInput("Enter the PCIE Slot");
        m2nvmeSupport = InputProcessor.GetBoolInput("Does it support M.2 NVME? (true or false)");

        Motherboard motherboard = new Motherboard(make, model, price, formFactor, socketType,
                                                    chipset, memoryDDRSlotType, pcieSlot, m2nvmeSupport);
    
        return motherboard;
    }

}