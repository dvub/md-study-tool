Console.WriteLine("Please enter a file path to read:");
String fileName = Console.ReadLine()!;
//String fileName = "";

String text = "";

try
{
  text = File.ReadAllText(fileName);
} catch (Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("Successfully read file: " + fileName);

Console.WriteLine("Please enter a target to search for:");
String target = Console.ReadLine()!;

String[] words = text.Split(' ');


Dictionary<String, String> terms = new Dictionary<String, String>();


Console.WriteLine("Populating dictionary...");

for (int i = 0; i < words.Length; i++)
{
    if (words[i].Equals(target)) {

        String def = "";
        String name = "";
        int offset = 1;
        while (!words[i + offset].Equals("-"))
        {
            name += " " + words[i + offset];
            offset++;
        }
        int j = i + 1 + offset;

        while (!words[j].Contains("\n")) {

            def += words[j] + " ";
            j++;
        }
        def += words[j].Substring(0, words[j].IndexOf('\n'));
        terms.Add(name, def);
    }
}
Console.WriteLine("Dictionary Populated... Found " + terms.Count + " terms.");
Console.WriteLine("");
Console.WriteLine("Press 'enter' if can define the words. If not, type 'n' to see the definition.");



int count = 0;
foreach (String word in terms.Keys)
{
    count++;
    Console.Write(count + ". Define ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(word + "?");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("");

    String a = Console.ReadLine()!.ToLower();

    if (a.Equals("exit"))
    {
        break;
     
    }
    Console.WriteLine(word + " - " + terms[word]);
    Console.ReadLine();
    Console.Clear();
    
}

