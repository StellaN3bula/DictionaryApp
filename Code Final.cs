using System;
using System.IO;
using System.Text.Json;


public class Wörterbuch //definiert die Klasse (den Bauplan)
{
    public string Name { get; set; } //speichert den Name des Wörterbuchs - get = Wert auslesen; set = Wert setzen
    public Wörterbuch(string? name) //Konstruktor mit Eingabe -> wird verwendet wenn ich *new Wörterbuch("Mein Wörterbuch"); erstelle*
    {
        Name = name; // Der übergebene Name (Mein Wörterbuch) wird im Objekt gespeichert.
    }
    public Wörterbuch() //Konstruktor ohne Eingabe -> wird verwendet wenn man dem Wörterbuch keinen Namen gibt;
    {
        Name = "unbenannt"; //bekommt dann den Namen
    }
    // Den Konstruktor ohne Parameter -  braucht JSON beim Laden sonst kann er kein Objekt erstellen!

    /*Warum 2 Konstruktor? Man ist damit flexibler. Das Prinzip dahinter nennt man Konstruktor-Überladung (constructor Overloading).
     Man kann mehrer haben zb.: mit / ohne Paramater oder mit verschiedenen Datentypen.
    */

    public Dictionary<string, string> nachschlagewerk { get; set; } = new Dictionary<string, string>();

    public Dictionary<string, string> GetAlleWörter()
    {
        return nachschlagewerk;
    }
    public void Anzeigen()
    {
        Console.WriteLine("Englisch - Deutsch: ");

        if (nachschlagewerk.Count == 0)
        {
            Console.WriteLine("Keine Wörter vorhanden.");
            return;
        }
        foreach (var wort in nachschlagewerk)
        {
            Console.WriteLine($"{wort.Key} - {wort.Value}");
        }

    }
    public void Hinzufügen(string englisch, string deutsch)
    {

        if (!nachschlagewerk.ContainsKey(englisch))
        {
            nachschlagewerk.Add(englisch, deutsch);
            Console.WriteLine("Wortpaar hinzugefügt.");
        }
        else
        {
            Console.WriteLine("Wort existiert bereits!");
        }

    }

    public void Löschen(string wort)
    {

        if (nachschlagewerk.ContainsKey(wort))
        {
            nachschlagewerk.Remove(wort);
            Console.WriteLine($"Der Eintrag {wort} wurde gelöscht.");
        }
        else if (nachschlagewerk.ContainsValue(wort))
        {
            string keyZumLöschen = null;
            foreach (var eintrag in nachschlagewerk)
            {
                if (eintrag.Value == wort)
                {
                    keyZumLöschen = eintrag.Key;
                    break; //mit break löscht es nur einen Beitrag (falls es mehrere mit der gleichen übersetztung gibt)
                    /*
                    //wenn ich alle Einträge mit der selben übersetzung löschen will dann muss ich das so machen:
                    //ohne break aber mit .ToList() sonst crasht es weil man in der Schleife löscht.
                    foreach (var eintrag in nachschlagewerk.ToList())
                    {
                        if (eintrag.Value == wort)
                        {
                            nachschlagewerk.Remove(eintrag.Key);
                        }
                    }
                    */

                }
            }
            if (keyZumLöschen != null)
            {
                nachschlagewerk.Remove(keyZumLöschen);
                Console.WriteLine($"Der Eintrag {wort} wurde gelöscht.");
            }

        }
        else
        {
            Console.WriteLine($"Das Wort {wort} ist nicht vorhanden.");
        }
        /* //Lösch Methode verbessert: 
         * 
         * public void Löschen(string wort)
{
   if (nachschlagewerk.ContainsKey(wort))
   {
       nachschlagewerk.Remove(wort);
       Console.WriteLine($"Der Eintrag {wort} wurde gelöscht.");
   }
   else
   {
       var eintrag = nachschlagewerk.FirstOrDefault(x => x.Value == wort);

       if (!eintrag.Equals(default(KeyValuePair<string, string>)))
       {
           nachschlagewerk.Remove(eintrag.Key);
           Console.WriteLine($"Der Eintrag {wort} wurde gelöscht.");
       }
       else
       {
           Console.WriteLine($"Das Wort {wort} ist nicht vorhanden.");
       }
   }
}
         */

    }

    public void Suchen(string gesuchtesWort)
    {
        if (nachschlagewerk.ContainsKey(gesuchtesWort))
        {
            Console.WriteLine($"Englisch: {gesuchtesWort} - Deutsch {nachschlagewerk[gesuchtesWort]}"); //zeigt das Englische und das Deutsche Wort an.
        }
        else if (nachschlagewerk.ContainsValue(gesuchtesWort))
        {
            foreach (var wort in nachschlagewerk)
            {
                if (wort.Value == gesuchtesWort)
                {
                    Console.WriteLine($"Deutsch: {gesuchtesWort} - Englisch: {wort.Key}"); //zeigt das Deutsche und das Englische Wort an.
                }
            }

        }
        else
        {
            Console.WriteLine($"Das Wort {gesuchtesWort} ist nicht vorhanden.");
        }
    }

}

public class Program
{
    static void Main(string[] args)
    {

        Wörterbuch myWörterbuch;

        string userEingabe = "";
        string dateiName = "";
        string vollerPfad = "";

        Console.WriteLine("Willkommen im Wörterbuch.");

        string ordner = AppDomain.CurrentDomain.BaseDirectory;
        string[] dateien = Directory.GetFiles(ordner, "*.json");

        if (dateien.Length == 0)
        {
            Console.WriteLine("Keine Wörterbücher gefunden.");
        }
        else
        {
            Console.WriteLine("Liste deiner vorhandenen Wörterbücher:");
            foreach (string datei in dateien)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(datei));
            }
        }


        while (true)
        {
            Console.WriteLine("L = Laden | N = Neues Wörterbuch");
            userEingabe = Console.ReadLine()?.ToLower() ?? "";

            if (userEingabe == "l")
            {
                //laden: 
                Console.WriteLine("Welches Wörterbuch laden?");
                string auswahl = Console.ReadLine(); // kein .ToLower() weil er dein Namen dann nicht findet wenn der mit einem Großbuchstaben anfängt. 

                dateiName = auswahl + ".json";
                vollerPfad = Path.Combine(ordner, dateiName);

                if (File.Exists(vollerPfad))
                {
                    string jsonLaden = File.ReadAllText(vollerPfad);

                    try //fangt einen Möglichen Crash ab falls die Datei beschädigt oder ähnlich ist.
                    {
                        myWörterbuch = JsonSerializer.Deserialize<Wörterbuch>(jsonLaden)
                                      ?? new Wörterbuch("Fehler");
                    }
                    catch
                    {
                        Console.WriteLine("Fehler beim Laden! Neues Wörterbuch wird erstellt.");
                        myWörterbuch = new Wörterbuch("Neu");
                    }

                    Console.WriteLine($"Wörterbuch \"{myWörterbuch.Name}\" wurde geladen.");
                    break;
                }
                else
                {
                    Console.WriteLine("Dieses Wörterbuch existiert nicht!");
                }
            }
            else if (userEingabe == "n")
            {
                Console.WriteLine("Wie soll dein Wörterbuch heißen?");
                string name = Console.ReadLine();

                myWörterbuch = new Wörterbuch(name);

                Console.WriteLine($"Wörterbuch \"{name}\" wurde erstellt.");
                break;
            }
            else
            {
                Console.WriteLine("Falsche Eingabe! Bitte L oder N.");
            }
        }



        do
        {
            userEingabe = "";
            Console.WriteLine("Was möchstest du tun?");
            Console.WriteLine("A - Anzeigen, B - Hinzufügen, D - Löschen, E - Suchen, X - Beenden.");
            userEingabe = Console.ReadLine()?.ToLower() ?? "";
            if (userEingabe == "x")
            {
                break;
            }
            else
            {

                switch (userEingabe)
                {
                    case "a":

                        Console.WriteLine("Folgende Wörter sind in der Liste:");
                        myWörterbuch.Anzeigen();
                        Console.WriteLine("Drücke H um ins Hauptmenü zurückzukehren.");
                        userEingabe = Console.ReadLine()?.ToLower() ?? "";
                        break;
                    /*
                    case "b":
                    // Diese Block fängt die "Leer" eingaben nicht ab
                    do
                    {
                        Console.WriteLine("Bitte zuerst das Englische Wort eingeben, danach bitte das Deutsche Wort eingeben : ");
                        string englisch = Console.ReadLine();
                        string deutsch = Console.ReadLine();
                        myWörterbuch.Hinzufügen(englisch, deutsch);

                        Console.WriteLine("Enter Dücken um das nächstes Wortpaar einzugeben oder drück H um ins Hauptmenü zurückzukehren.");
                        userEingabe = Console.ReadLine()?.ToLower() ?? ""; 

                        if (userEingabe == "h")
                        {
                            break;
                        }
                    }
                    while (userEingabe != "h");
                    break;
                    */
                    case "b":
                        do
                        {
                            Console.WriteLine("Bitte zuerst das Englische Wort eingeben:");
                            string englisch = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(englisch))
                            {
                                Console.WriteLine("Ungültige Eingabe! Bitte ein Wort eingeben.");
                                continue; // geht zurück zum Anfang der Schleife
                            }

                            Console.WriteLine("Jetzt das Deutsche Wort eingeben:");
                            string deutsch = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(deutsch))
                            {
                                Console.WriteLine("Ungültige Eingabe! Bitte ein Wort eingeben.");
                                continue;
                            }

                            myWörterbuch.Hinzufügen(englisch, deutsch);

                            Console.WriteLine("Enter drücken für nächstes Wort oder H für Hauptmenü.");
                            userEingabe = Console.ReadLine()?.ToLower() ?? "";

                            if (userEingabe == "h")
                            {
                                break;
                            }

                        } while (userEingabe != "h");
                        break;



                    case "d":
                        do
                        {

                            Console.WriteLine("Welches Wort möchtest du löschen? Bitte eingeben: ");
                            string eingabeLöschen = Console.ReadLine()?.ToLower() ?? "";
                            myWörterbuch.Löschen(eingabeLöschen);

                            Console.WriteLine("Enter Dücken um ein weiters Wort zu löschen oder drück H um ins Hauptmenü zurückzukehren.");
                            userEingabe = Console.ReadLine()?.ToLower() ?? "";

                            if (userEingabe == "h")
                            {
                                break;
                            }
                        }
                        while (userEingabe != "h");
                        break;

                    case "e":
                        do
                        {
                            //man muss die genaue Schreibweise nehmen wie beim speichern sonst findet er es nicht.
                            Console.WriteLine("Nach welchem Wort möchtest du Suchen? Bitte Wort eingeben: ");
                            string eingabeSuchen = Console.ReadLine()?.ToLower() ?? "";
                            myWörterbuch.Suchen(eingabeSuchen);

                            Console.WriteLine("Möchtest du ein weiters Wort suchen? Bitte Enter drücken oder drück H um ins Hauptmenü zurückzukehren.");
                            userEingabe = Console.ReadLine()?.ToLower() ?? "";

                            if (userEingabe == "h")
                            {
                                break;
                            }
                        }
                        while (userEingabe != "h");
                        break;


                    default:
                        Console.WriteLine("Falsche Eingabe");
                        break;
                }

            }
        }
        while (userEingabe != "x");
        Console.WriteLine("Bis zum nächsten Mal.");

        //Speichern:
        ordner = AppDomain.CurrentDomain.BaseDirectory;
        dateiName = myWörterbuch.Name.Replace(" ", "_") + ".json";
        vollerPfad = Path.Combine(ordner, dateiName);

        string jsonSpeichern = JsonSerializer.Serialize(myWörterbuch);
        File.WriteAllText(vollerPfad, jsonSpeichern);

        Console.WriteLine("Wörterbuch gespeichert!");


    }
}
