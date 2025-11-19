using System;

class Person
{
    // Feld für den Namen
    private string name;

    // Konstruktor: Wird beim Erstellen eines Objekts aufgerufen
    public Person(string name)
    {
        // 'this' verweist auf das aktuelle Objekt
        this.name = name;
    }

    // Eigenschaft mit get und set
    public string Name
    {
        // 'get' gibt den Wert des Feldes zurück
        get { return name; }
        // 'set' setzt den Wert des Feldes
        set { name = value; }
    }
}

class Program
{
    static void Main()
    {
        // Erstellen eines neuen Objekts mit dem Konstruktor
        Person person = new Person("Max");

        // Zugriff auf die Eigenschaft (get)
        Console.WriteLine(person.Name);

        // Setzen eines neuen Namens (set)
        person.Name = "Anna";
        Console.WriteLine(person.Name);
    }
}