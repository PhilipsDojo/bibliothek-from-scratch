namespace BibliothekFromScratch;

public static class Konsolenausgabe {
    public static void AusleihenUndMelden(Buch buch) {
        var wurdeAusgeliehen = buch.Ausleihen();
        if(wurdeAusgeliehen is true) {
            Console.WriteLine($"Das Buch vom Autor: {buch.Autor} wurde ausgeliehen");
        } else {
            Console.WriteLine($"Das Buch vom Autor: {buch.Autor} konnte nicht ausgeliehen werden");
        }
    }

    public static void ZurueckGebenUndMelden(Buch buch) {
        var wurdeZurueckGegeben = buch.Zurueckgeben();
        if(wurdeZurueckGegeben is true) {
            Console.WriteLine($"Das Buch {buch.Titel} vom Autor: {buch.Autor} wurde zurückgegeben");
        } else {
            Console.WriteLine($"Das Buch {buch.Titel} vom Autor: {buch.Autor} konnte nicht zurückgegeben werden");
        }
    }

    public static void AutorSuchenUndAnzeigen(Bibliothek bibo, string autor) {
        var result = bibo.SucheNachAutor(autor);
        if(result.Count == 0) {
            Console.WriteLine("Keine Treffer gefunden");
        } else {
            foreach(var x in result) {

                Console.WriteLine($"Titel {x.Titel} vom gesuchten Autor: {x.Autor}");
            }
        }
    }

    public static Buch? UserBuchSuche(Bibliothek bibo) {
        Console.WriteLine("Bitte Titel eingeben oder mit Enter suche abrechen");
        var titelEingabe = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(titelEingabe)) {
            return null;
        } else {
            var buch = bibo.SucheNachTitel(titelEingabe);
            return buch;
        }
    }

    public static void UserBuchLeihen(Bibliothek bibo) {
        var buch = UserBuchSuche(bibo);
        if(buch is null) {
            Console.WriteLine("Kein Buch gefunden oder Suche abgebrochen");
            return;
        }
        Console.WriteLine($"Gefunden: {buch.Titel} von {buch.Autor}");
        Console.WriteLine("1 Eingeben um Buch auszuleihen || 2 Eingeben oder Enter drücken um Vorgang abzubrechen");
        var userEingabe = Console.ReadLine();
        if(userEingabe == "1"){
            AusleihenUndMelden(buch);
        } else {
            Console.WriteLine("Vorgang abgebrochen");
        }
    }
}

