using System.ComponentModel.Design;
using System.Dynamic;

namespace BibliothekFromScratch;

internal class Program {
    static void Main(string[] args) {

        Console.WriteLine("Hello, World!");

        var bibo = new Bibliothek();


        var buch1 = new Buch("TitelA", "AutorA");
        var buch2 = new Buch("TitelB", "AutorB");
        var buch3 = new Buch("TitelC", "AutorC");
        var buch4 = new Buch("NeuerTitel", "AutorA");

        bibo.Buchhinzufuegen(buch1);
        bibo.Buchhinzufuegen(buch2);
        bibo.Buchhinzufuegen(buch3);
        bibo.Buchhinzufuegen(buch4);


        string eingabe = "a";
        while(eingabe != "q") {
            Console.WriteLine("q eingeben um Programm zu beenden");
            Console.WriteLine("1 eingeben um alle Bücher anzuzeigen ");
            Console.WriteLine("2 eingeben um Anzahl der Verfügbaren Bücher anzuzeigen");
            Console.WriteLine("3 eingeben um Nach Autor zu suchen");
            Console.WriteLine("4 eingeben um nach Titel zu suchen und Auszuleihen");
            Console.WriteLine("5 eingeben um Buch zurückzugeben");
            eingabe = Console.ReadLine();

            if(eingabe == "1") {
                foreach(var buch in bibo.Buecher) {
                    Console.WriteLine($"Titel: {buch.Titel} vom Autor : {buch.Autor}");
                }
            } else if(eingabe == "2") {
                Console.WriteLine($" Die Anzahl der verfügbaren bücher: {bibo.AnzahlVerfuegbar().ToString()}");
            } else if(eingabe == "3") {
                Console.WriteLine($"Bitte Name des Autors eingeben oder mit Enter suche abrechen");
                var autorEingabe = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(autorEingabe)) {
                    Console.WriteLine("Suche abgebrochen");
                } else {
                    Konsolenausgabe.AutorSuchenUndAnzeigen(bibo, autorEingabe);
                }
            } else if(eingabe == "4") {
                Konsolenausgabe.UserBuchLeihen(bibo);
            }else if(eingabe == "5") {
                Konsolenausgabe.UserBuchZurueck(bibo);
            }
            else if(eingabe == "q") {
                Console.WriteLine("Auf Wiedersehen!");
                Console.ReadKey();
            } else {
                Console.WriteLine("Eingabe Ungültig");
            }
        }


    }

}
