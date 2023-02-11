public class InputProcessor {
    private InputProcessor() {}

    public static int GetIntInput(String question) {
        Console.WriteLine(question);
        String? userInput = Console.ReadLine();
        if (String.IsNullOrEmpty(userInput) || !Int32.TryParse(userInput, out int result))
        {
            userInput = "0";
        }
        return Int32.Parse(userInput);
    }

    public static String GetStringInput(String question) {
        Console.WriteLine(question);
        String? userInput = Console.ReadLine();
        if (String.IsNullOrEmpty(userInput)) {
            userInput = "N/A";
        }
        return userInput;
    }

    public static bool GetBoolInput(String question) {
        Console.WriteLine(question);
        String? userInput = Console.ReadLine();
        if (String.IsNullOrEmpty(userInput) || userInput.ToLower() != "false" || userInput.ToLower() != "true") userInput = "false";
        return bool.Parse(userInput);
    }

    public static double GetDoubleInput(String question) {
        Console.WriteLine(question);
        String? userInput = Console.ReadLine();
        if (String.IsNullOrEmpty(userInput) || !double.TryParse(userInput, out double result)) userInput = "0.0";
        return double.Parse(userInput);
    }
}