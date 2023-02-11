using System.Data.SQLite;

public class Application {
    public static Order ORDER = new Order(-1, new UserAccount(), new Desktop(), 0.0, "N/A", "In Progress");

    public static Account ACCOUNT = new UserAccount();
    public static string DBNAME = "andrew.db";
    public static SQLiteConnection CONN = SQLiteDatabase.Connect(DBNAME);

    public static void Main(String[] args) {
        AccountDB.CreateTable(CONN);
        DesktopDB.CreateTable(CONN);
        MotherboardDB.CreateTable(CONN);
        ProcessorDB.CreateTable(CONN);
        MemoryDB.CreateTable(CONN);
        GraphicsCardDB.CreateTable(CONN);
        StorageDB.CreateTable(CONN);
        OrderDB.CreateTable(CONN);

        displayRules();
        StartMenu();
    }

    //rules to assist the user
    public static void displayRules() {
        Console.WriteLine("-----RULES and GUIDELINES-----");
        Console.WriteLine("1. Enter number assigned to use the options");
        Console.WriteLine("2. Add 'ec_' at the beginning of username during signup to create admin account (ec_andrew.perry)");
        Console.WriteLine("3. One entry per component to choose from by default, admins can create more components");
        Console.WriteLine("   for the user to select during order creation.");
        Console.WriteLine("4. Some user accounts available with orders are jane.doe and andrew.perry");
    }

    //displays a start menu which prompts for either sign in or sign up
    public static void StartMenu() {
        Console.WriteLine("-----START MENU-----");
        Console.WriteLine("1. sign in");
        Console.WriteLine("2. sign up");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput) {
            case 1:
                SignInMenu();
                break;
            case 2:
                SignUpMenu();
                break;
            default:
                StartMenu();
                break;
        }
    }

    //sign in menu for when the user wants to sign in with an already-created account
    public static void SignInMenu() {
        Console.WriteLine("-----SIGN IN MENU-----");
        String username = InputProcessor.GetStringInput("Please enter your username (1 to exit)");
        if (username == "1") { //if user wants to back out
            StartMenu();
            return;
        }
        
        if (username == "N/A") { //if the account name is not valid
            Console.WriteLine("No username entered");
            SignInMenu();
        }
        else {
            ACCOUNT = AccountDB.GetAccount(CONN, username);
            if (ACCOUNT.id == -1) { //if the account does not exist
                Console.WriteLine("An account does not exist with this username");
                SignInMenu();
            }
            else { //the account does exist
                if (ACCOUNT is UserAccount) { //if the account is a user account
                    UserMenu();
                }
                else if (ACCOUNT is AdminAccount) { //if the account is an admin account
                    AdminMenu();
                }
            }
        }
    }

    //sign up menu that prompt for username, checks if it is valid
    //if valid username then prompts for first name and last name
    public static void SignUpMenu()
    {
        Console.WriteLine("-----SIGN UP MENU-----");
        String? username = InputProcessor.GetStringInput("Enter the username you want (1 to exit)").ToLower(); //get username as lowercase
        if (username == "1") { //if user wants to back out
            StartMenu();
            return; 
        }

        Account account = AccountDB.GetAccount(CONN, username); //search database for username
        if (account.id != -1)
        { //if username is found in database then it already exists
            Console.WriteLine("There is already an account with this username");
            SignUpMenu();
        }
        else
        { //if the username is not found in the database, then ask for first and last name
            String? firstName = InputProcessor.GetStringInput("Enter your first name");
            String? lastName = InputProcessor.GetStringInput("Enter your last name");

            //add the account based on account name
            if (username.StartsWith("ec_"))
            {
                Console.WriteLine(username.StartsWith("ec_"));
                AccountDB.AddAccount(CONN, new AdminAccount(username, firstName, lastName));
            }
            else
            {
                Console.WriteLine(username.StartsWith("ec_"));
                AccountDB.AddAccount(CONN, new UserAccount(username, firstName, lastName));
            }

            StartMenu();
        }
    }

    //menu of options for user accounts
    public static void UserMenu() {
        Console.WriteLine("-----MAIN MENU-----");
        ACCOUNT.provideGeneralDescription();
        ACCOUNT.provideAccountDescription();
        Console.WriteLine("1. view my orders");
        Console.WriteLine("2. create order");
        Console.WriteLine("3. back");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput)
        {
            case 1:
                ViewMyOrders();
                break;
            case 2:
                ORDER.account = ACCOUNT;
                CreateOrder();
                break;
            case 3:
                StartMenu();
                break;
            default:
                UserMenu();
                break;
        }
    }

    //prints all orders of the user account
    public static void ViewMyOrders() {
        Console.WriteLine("-----VIEW MY ORDERS-----");
        List<Order> orders = OrderDB.GetAllOrdersOfAccount(CONN, ACCOUNT.id);
        for (int i = 0; i < orders.Count; i++) {
            Console.WriteLine(orders[i]);
        }
        UserMenu();
    }

    //menu of options to select components and fill in details
    public static void CreateOrder() {
        List<Motherboard> motherboards = MotherboardDB.GetAllMotherboards(CONN);
        List<Processor> processors = ProcessorDB.GetAllProcessors(CONN);
        List<Memory> memorys = MemoryDB.GetAllMemorys(CONN);
        List<GraphicsCard> graphicsCards = GraphicsCardDB.GetAllGraphicsCards(CONN);
        List<Storage> storages = StorageDB.GetAllStorages(CONN);
        Console.WriteLine("-----CREATE ORDER-----");
        Console.WriteLine("1. select motherboard");
        Console.WriteLine("2. select processor");
        Console.WriteLine("3. select memory");
        Console.WriteLine("4. select graphics card");
        Console.WriteLine("5. select storage");
        Console.WriteLine("6. Enter delivery address");
        Console.WriteLine("7. continue");
        Console.WriteLine("8. back");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput) {
            case 1: //shows all motherboard in the Motherboards table for user to choose from to add to the order
                Console.WriteLine("-----SELECT MOTHERBOARD-----");
                new Motherboard().provideDescription();
                for (int i = 0; i < motherboards.Count; i++) {
                    Console.WriteLine(motherboards[i].id + ". " + motherboards[i]);
                }
                int motherboardID = InputProcessor.GetIntInput("Select a motherboard");
                Motherboard motherboard = MotherboardDB.GetMotherboard(CONN, motherboardID);
                ORDER.desktop.motherboard = motherboard;
                CreateOrder();
                break;
            case 2: //shows all processors in the Processors table for user to choose from to add to the order
                Console.WriteLine("-----SELECT PROCESSOR-----");
                new Processor().provideDescription();
                for (int i = 0; i < processors.Count; i++) {
                    Console.WriteLine(processors[i].id + ". " + processors[i]);
                }
                int processorID = InputProcessor.GetIntInput("Select a processor");
                Processor processor = ProcessorDB.GetProcessor(CONN, processorID);
                ORDER.desktop.processor = processor;
                CreateOrder();
                break;
            case 3: //shows all memory cards in the Memorys table for user to choose from to add to the order
                Console.WriteLine("-----SELECT MEMORY-----");
                new Memory().provideDescription();
                for (int i = 0; i < memorys.Count; i++) {
                    Console.WriteLine(memorys[i].id + ". " + memorys[i]);
                }
                int memoryID = InputProcessor.GetIntInput("Select a memory card");
                Memory memory = MemoryDB.GetMemory(CONN, memoryID);
                ORDER.desktop.memory = memory;
                CreateOrder();
                break;
            case 4: //shows all graphics cards in the GraphicsCards table for user to choose from to add to the order
                Console.WriteLine("-----SELECT GRAPHICS CARD-----");
                new GraphicsCard().provideDescription();
                for (int i = 0; i < graphicsCards.Count; i++) {
                    Console.WriteLine(graphicsCards[i].id + ". " + graphicsCards[i]);
                }
                int graphicsCardID = InputProcessor.GetIntInput("Select a graphics card");
                GraphicsCard graphicsCard = GraphicsCardDB.GetGraphicsCard(CONN, graphicsCardID);
                ORDER.desktop.graphicsCard = graphicsCard;
                CreateOrder();
                break;
            case 5: //shows all storages in the Storages table for user to choose from to add to the order
                Console.WriteLine("-----SELECT STORAGE-----");
                new Storage().provideDescription();
                for (int i = 0; i < storages.Count; i++) {
                    Console.WriteLine(storages[i].id + ". " + storages[i]);
                }
                int storageID = InputProcessor.GetIntInput("Select a storage");
                Storage storage = StorageDB.GetStorage(CONN, storageID);
                ORDER.desktop.storage = storage;
                CreateOrder();
                break;
            case 6: //prompts the user to enter a delivery address for the order
                Console.WriteLine("-----ENTER DELIVERY ADDRESS-----");
                ORDER.deliveryAddress = InputProcessor.GetStringInput("Please enter the delivery address");
                CreateOrder();
                break;
            case 7: //confirms the purchase
                ConfirmPurchase();
                break;
            case 8: //backs up to the user menu
                UserMenu();
                break;
            default: //repeats this menu
                CreateOrder();
                break;
        }
    }

    //shows all details of the order for the user to review and confirm or back out to make changes
    public static void ConfirmPurchase() {
        Console.WriteLine("-----SUMMARY-----");
        Console.WriteLine(ORDER.desktop);
        ORDER.payment = ORDER.desktop.motherboard.price + ORDER.desktop.processor.price +
                        ORDER.desktop.memory.price + ORDER.desktop.graphicsCard.price +
                        ORDER.desktop.storage.price;
        ORDER.account = ACCOUNT;
        Console.WriteLine("$" + ORDER.payment + " is your total.");
        Console.WriteLine("The delivery address is " + ORDER.deliveryAddress);
        Console.WriteLine("1. confirm purchase");
        Console.WriteLine("2. back");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput) {
            case 1: // confirms the purchase and returns to the user menu
                DesktopDB.AddDesktop(CONN, ORDER.desktop); //adds desktop to database
                ORDER.desktop = DesktopDB.GetLastRow(CONN); //assigned the most recent desktop in database to order for accurate id
                OrderDB.AddOrder(CONN, ORDER);
                Console.WriteLine("Purchase confirmed");
                UserMenu();
                break;
            case 2: //returns to the create order menu
                CreateOrder();
                break;
            default: //repeats this menu
                ConfirmPurchase();
                break;
        }
    }

    //menu of options for admin account
    public static void AdminMenu() {
        Console.WriteLine("-----MAIN MENU-----");
        ACCOUNT.provideGeneralDescription();
        ACCOUNT.provideAccountDescription();
        Console.WriteLine("1. view orders for an account");
        Console.WriteLine("2. view all orders");
        Console.WriteLine("3. change status of order");
        Console.WriteLine("4. create a component");
        Console.WriteLine("5. back");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput) {
            case 1:
                ViewAccountOrders();
                break;
            case 2: 
                ViewAllOrders();
                break;
            case 3:
                changeStatusOfOrder();
                break;
            case 4:
                CreateComponent();
                break;
            case 5:
                StartMenu();
                break;
            default:
                AdminMenu();
                break;
        }
    }

    //view all orders of an account by entering the username of the person
    public static void ViewAccountOrders() {
        Console.WriteLine("-----VIEW ORDERS FOR AN ACCOUNT-----");
        String? username = InputProcessor.GetStringInput("Enter the username of the account");
        Account account = AccountDB.GetAccount(CONN, username);
        List<Order> orders = OrderDB.GetAllOrdersOfAccount(CONN, account.id);
        if (orders.Count == 0) {
            Console.WriteLine("No orders found");
        }
        else {
            for (int i = 0; i < orders.Count; i++) {
                Console.WriteLine(orders[i]);
            }
        }
        AdminMenu();
    }

    //view all orders in the database
    public static void ViewAllOrders() {
        Console.WriteLine("-----VIEW ALL ORDERS-----");
        List<Order> orders = OrderDB.GetAllOrders(CONN);
        if (orders.Count == 0) {
            Console.WriteLine("No orders found");
        }
        else {
            for (int i = 0; i < orders.Count; i++) {
                Console.WriteLine(orders[i]);
            }
        }
        AdminMenu();
    }

    //change the status of an order by entering its ID and then its new status
    public static void changeStatusOfOrder() {
        Console.WriteLine("-----CHANGE STATUS OF ORDER-----");
        int id = InputProcessor.GetIntInput("Enter the id of the order to change"); //get id of order
        Order order = OrderDB.GetOrder(CONN, id); //find the id in the order database
        if (order.id == -1) { //if the order does not exist
            Console.WriteLine("No order found with that id");
        }
        else { //if the order does exist then have user enter a new status and update the order in the database
            String status = InputProcessor.GetStringInput("Enter the new status");
            order.status = status;
            OrderDB.UpdateOrder(CONN, order);
            Console.WriteLine("Order " + order.id + " updated");
        }
        AdminMenu();
    }

    //prompts the user to pick a component type
    //then prompts the user for these values of the component
    //then adds the component to the database for use by user accounts
    public static void CreateComponent() {
        Console.WriteLine("-----CREATE COMPONENT-----");
        Console.WriteLine("1. motherboard");
        Console.WriteLine("2. processor");
        Console.WriteLine("3. memory");
        Console.WriteLine("4. graphics card");
        Console.WriteLine("5. storage");
        Console.WriteLine("6. back");
        int userInput = InputProcessor.GetIntInput("Choose an option");
        switch (userInput) {
            case 1:
                Motherboard motherboard = Motherboard.CreateMotherboard();
                MotherboardDB.AddMotherboard(CONN, motherboard);
                break;
            case 2:
                Processor processor = Processor.CreateProcessor();
                ProcessorDB.AddProcessor(CONN, processor);
                break;
            case 3:
                Memory memory = Memory.CreateMemory();
                MemoryDB.AddMemory(CONN, memory);
                break;
            case 4:
                GraphicsCard graphicsCard = GraphicsCard.CreateGraphicsCard();
                GraphicsCardDB.AddGraphicsCard(CONN, graphicsCard);
                break;
            case 5:
                Storage storage = Storage.CreateStorage();
                StorageDB.AddStorage(CONN, storage);
                break;
            case 6:
                AdminMenu();
                break;
            default:
                break;
        }
        CreateComponent();
    }
}