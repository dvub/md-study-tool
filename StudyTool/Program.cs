using Newtonsoft.Json;
Console.Title = "MDStudy";

String target = "";
String fileName = "";
String text = "";
String[] words = {};
String jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");

if (File.Exists(jsonPath)) {
    Console.WriteLine("Found config.json file. Reading...");
    
    using (StreamReader r = new StreamReader(jsonPath))
    {   
        string json = r.ReadToEnd();
        Dictionary<String, String> items = JsonConvert.DeserializeObject<Dictionary<String, String>>(json)!;
        fileName = items["file"];
        target = items["target"];
    }   

} else {
    Console.WriteLine("Did not find a config.json file.");
    Console.WriteLine("Please enter a file path to read:");
    fileName = Console.ReadLine()!;
    //String fileName = "";

    Console.WriteLine("Please enter a target to search for:");
    target = Console.ReadLine()!;

}

text = File.ReadAllText(fileName);
Console.WriteLine("Successfully read file: " + fileName);
words = text.Split(' ');   

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
Console.WriteLine("Press 'enter' to begin.");
Console.ReadLine();
Console.Clear();

String input = "";
int c = 0;
while (!input.Equals("exit"))
{
    KeyValuePair<String, String> kv = terms.ElementAt(c);
    int len = ("Define?" + kv.Key).Length;
    Console.Write(new string(' ', (Console.WindowWidth - len) / 2));
    Console.Write("Define");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(kv.Key + "?");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("");

    Console.ReadLine(); // wait for user before displaying definition
    Console.WriteLine(kv.Value);
    input = Console.ReadLine()!.ToLower(); // get next action, i.e. exit, next, last, enter

    // change our counter depending on what the user inputs
    if (input.Equals("") || input.Equals("next") || input.Equals("n")) {
        c++;
    }
    if ((input.Equals("last") || input.Equals("l")) && c > 0) {
        c--;
    }
    if (input.Equals("r") || input.Equals("random")) {
        Random r = new Random();
        c = r.Next(0, terms.Count);
    
    }
    
    // clear the console just so things are easier to read
    Console.Clear();
    
}

