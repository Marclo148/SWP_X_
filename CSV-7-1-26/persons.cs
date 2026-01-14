using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace CsvConsoleApp
{
    // Repräsentiert eine Zeile aus der CSV-Datei
    public class Person
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Adresse { get; set; }
        public string? Unicode { get; set; }
    }

    // Map CSV headers to properties (handles lowercase 'unicode')
    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Fullname).Name("Fullname");
            Map(m => m.Email).Name("Email");
            Map(m => m.Telefon).Name("Telefon");
            Map(m => m.Adresse).Name("Adresse");
            Map(m => m.Unicode).Name("unicode");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "persons.csv";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("CSV-Datei nicht gefunden!");
                return;
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                MissingFieldFound = null,
            };

            using (var reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<PersonMap>();
                var records = csv.GetRecords<Person>().ToList();

                Console.WriteLine("=== Personen aus CSV ===\n");

                foreach (var person in records)
                {
                    Console.WriteLine($"Name    : {person.Fullname}");
                    Console.WriteLine($"Email   : {person.Email}");
                    Console.WriteLine($"Telefon : {person.Telefon}");
                    Console.WriteLine($"Adresse : {person.Adresse}");
                    Console.WriteLine($"Unicode : {person.Unicode}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("\nEinlesen abgeschlossen.");
        }
    }
}
