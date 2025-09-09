using System;

class Fraction
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0) throw new DivideByZeroException("Nenner darf nicht 0 sein.");
        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    // Parsen von Eingaben im Format "a b/c" oder nur "a" oder nur "b/c"
    public static Fraction Parse(string input)
    {
        input = input.Trim();
        int whole = 0;
        int numerator = 0;
        int denominator = 1;

        if (input.Contains(" "))
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            whole = int.Parse(parts[0]);

            var frac = parts[1].Split('/');
            numerator = int.Parse(frac[0]);
            denominator = int.Parse(frac[1]);
        }
        else if (input.Contains("/"))
        {
            var frac = input.Split('/');
            numerator = int.Parse(frac[0]);
            denominator = int.Parse(frac[1]);
        }
        else
        {
            whole = int.Parse(input);
        }

        int totalNumerator = whole * denominator + numerator;
        return new Fraction(totalNumerator, denominator);
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
        int denominator = a.Denominator * b.Denominator;
        return new Fraction(numerator, denominator);
    }

    private void Simplify()
    {
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
        Numerator /= gcd;
        Denominator /= gcd;

        // Minuszeichen immer nur im Zähler
        if (Denominator < 0)
        {
            Numerator *= -1;
            Denominator *= -1;
        }
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public override string ToString()
    {
        int whole = Numerator / Denominator;
        int remainder = Math.Abs(Numerator % Denominator);

        if (remainder == 0) return whole.ToString();
        if (whole == 0) return $"{Numerator}/{Denominator}";
        return $"{whole} {remainder}/{Denominator}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Bitte ersten Bruch eingeben (Format z.B. '1 1/2' oder '3/4' oder '2'):");
        string input1 = Console.ReadLine();
        Fraction f1 = Fraction.Parse(input1);

        Console.WriteLine("Bitte zweiten Bruch eingeben:");
        string input2 = Console.ReadLine();
        Fraction f2 = Fraction.Parse(input2);

        Fraction result = f1 + f2;

        Console.WriteLine($"Ergebnis: {result}");
    }
}
