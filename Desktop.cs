/*******************************************************************
* Name: Andrew Perry
* Date: 9/18/2022
* Assignment: Course Project
*
* The Desktop class holds a composite variable of each component the computer is made up of. 
*/
public class Desktop {
    public int id {get; set;}
    public Motherboard motherboard { get; set; }
    public Processor processor { get; set; }
    public Memory memory { get; set; }
    public GraphicsCard graphicsCard { get; set; }
    public Storage storage { get; set; }

    public Desktop(int id, Motherboard motherboard, Processor processor, Memory memory, GraphicsCard graphicsCard, Storage storage) {
        this.id = id;
        this.motherboard = motherboard;
        this.processor = processor;
        this.memory = memory;
        this.graphicsCard = graphicsCard;
        this.storage = storage;
    }

    public Desktop(Motherboard motherboard, Processor processor, Memory memory, GraphicsCard graphicsCard, Storage storage) {
        this.motherboard = motherboard;
        this.processor = processor;
        this.memory = memory;
        this.graphicsCard = graphicsCard;
        this.storage = storage;
    }

    public Desktop() {
        this.motherboard = new Motherboard();
        this.processor = new Processor();
        this.memory = new Memory();
        this.graphicsCard = new GraphicsCard();
        this.storage = new Storage();
    }

    public override string ToString() {
        return String.Format("ID: {0}\nMotherboard: {1}\nProcessor: {2}\nMemory: {3}\nGraphics Card: {4}\nStorage: {5}",
                                id, motherboard, processor, memory, graphicsCard, storage);
    }
}