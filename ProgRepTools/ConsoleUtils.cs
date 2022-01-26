namespace ProgRepTools;

public static class ConsoleUtils
{
    public delegate bool TryParseDelegate<T>(string value, out T parsedValue);
    
    public static T Prompt<T>(string prompt, TryParseDelegate<T> parseDelegate)
    {
        T value;
        do
        {
            Console.WriteLine(prompt);
        } while (!parseDelegate(Console.ReadLine() ?? "", out value));

        return value;
    }
}