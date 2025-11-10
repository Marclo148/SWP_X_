using System;

class Bruch
{
    // Attribute der Klasse Bruch
    private int ganzzahl;
    private int zaehler;
    private int nenner;

    public Bruch(string bruchtext)
    {
        try
        {
            bruchtext = bruchtext.Trim();

            if (bruchtext.Contains(" "))
            {
                string[] parts = bruchtext.Split(' ');
                this.ganzzahl = int.Parse(parts[0]);

                string[] fractionParts = parts[1].Split('/');
                int z = int.Parse(fractionParts[0]);
                int n = int.Parse(fractionParts[1]);

                if (n == 0)
                    throw new ArgumentException("Nenner darf nicht 0 sein.");

                this.zaehler = z + (this.ganzzahl * n);
                this.nenner = n;
            }
            else
            {
                string[] teile = bruchtext.Split('/');
                if (teile.Length != 2)
                    throw new FormatException();

                this.zaehler = int.Parse(teile[0]);
                this.nenner = int.Parse(teile[1]);

                if (this.nenner == 0)
                    throw new ArgumentException("Nenner darf nicht 0 sein.");
            }
        }
        catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is IndexOutOfRangeException)
        {
            Console.WriteLine(ex is ArgumentException ? ex.Message : "Ungültiges Format für den Bruchtext.");
            Environment.Exit(1);
        }
    }

    public Bruch addiere(Bruch andererBruch)
    {
        try
        {
            int neuerZaehler = this.zaehler * andererBruch.nenner + andererBruch.zaehler * this.nenner;
            int neuerNenner = this.nenner * andererBruch.nenner;
            return new Bruch($"{neuerZaehler}/{neuerNenner}");
        }
        catch
        {
            Console.WriteLine("Fehler beim Addieren der Brüche.");
            Environment.Exit(1);
            return null;
        }
    }

    public string ToStringBruch()
    {
        try
        {
            // Kürzen des Bruchs
            int teiler = Math.Min(Math.Abs(zaehler), Math.Abs(nenner));
            while (teiler > 1)
            {
                if (zaehler % teiler == 0 && nenner % teiler == 0)
                {
                    zaehler /= teiler;
                    nenner /= teiler;
                }
                teiler--;
            }

            // Umwandlung in gemischten Bruch
            int ganzeZahl = zaehler / nenner;
            int restZaehler = Math.Abs(zaehler % nenner);

            if (ganzeZahl != 0 && restZaehler != 0)
                return $"{ganzeZahl} {restZaehler}/{nenner}";
            else if (ganzeZahl != 0)
                return $"{ganzeZahl}";
            else
                return $"{zaehler}/{nenner}";
        }
        catch
        {
            Console.WriteLine("Fehler beim Konvertieren des Bruchs in einen String.");
            Environment.Exit(1);
            return null;
        }
    }
}
