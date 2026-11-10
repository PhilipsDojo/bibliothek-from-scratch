using BibliothekFromScratch.Domain;

namespace BibliothekFromScratch.Konsole;

public static class Konsolenausgabe {
    private static void AusleihenUndMelden(Bibliothek bibo, Buch buch, Mitglied mitglied) {
        var wurdeAusgeliehen = bibo.Ausleihen(mitglied, buch);
        if(wurdeAusgeliehen is true) {
            Console.WriteLine($"Das Buch vom Autor: {buch.Autor} wurde ausgeliehen");
        } else {
            Console.WriteLine($"Das Buch vom Autor: {buch.Autor} konnte nicht ausgeliehen werden");
        }
    }

    private static void ZurueckGebenUndMelden(Buch buch) {
        var wurdeZurueckGegeben = buch.Zurueckgeben();
        if(wurdeZurueckGegeben is true) {
            Console.WriteLine($"Das Buch {buch.Titel} vom Autor: {buch.Autor} wurde zurückgegeben");
        } else {
            Console.WriteLine($"Das Buch {buch.Titel} vom Autor: {buch.Autor} konnte nicht zurückgegeben werden");
        }
    }

    private static void AutorSuchenUndAnzeigen(Bibliothek bibo, string autor) {
        var result = bibo.SucheNachAutor(autor);
        if(result.Count == 0) {
            Console.WriteLine("Keine Treffer gefunden");
        } else {
            foreach(var x in result) {

                Console.WriteLine($"Titel {x.Titel} vom gesuchten Autor: {x.Autor}");
            }
        }
    }

    private static Buch? BuchSuche(Bibliothek bibo) {
        Console.WriteLine("Bitte Titel eingeben oder mit Enter suche abbrechen");
        var titelEingabe = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(titelEingabe)) {
            return null;
        } else {
            var buch = bibo.SucheNachTitel(titelEingabe);
            return buch;
        }
    }

    private static Mitglied? MitgliederSuche(Bibliothek bibo) {
        Console.WriteLine("Bitte Mitgliedsnummer eingeben oder mit Enter suche abbrechen");
        var nummerEingabe = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(nummerEingabe)) {
            return null;
        } else {
            if(!int.TryParse(nummerEingabe, out int number)) {
                return null;
            } else {
                var mitglied = bibo.SucheNachMitglied(number);
                return mitglied;
            }
        }
    }

    public static void StarteMenue(Bibliothek bibo) {
        Console.WriteLine(" === Herzlich Willkommen ===");
        string? eingabe = "a";
        while(eingabe != "q") {
            Console.WriteLine("q eingeben um Programm zu beenden");
            Console.WriteLine("1 eingeben um alle Bücher anzuzeigen ");
            Console.WriteLine("2 eingeben um Anzahl der Verfügbaren Bücher anzuzeigen");
            Console.WriteLine("3 eingeben um Nach Autor zu suchen");
            Console.WriteLine("4 eingeben um nach Titel zu suchen und Auszuleihen");
            Console.WriteLine("5 eingeben um Buch zurückzugeben");
            Console.WriteLine("6 eingeben um Ihre ausgeliehenen Bücher einzusehen");
            eingabe = Console.ReadLine();

            if(eingabe == "1") {
                AlleBuecherAnzeigen(bibo);
            } else if(eingabe == "2") {
                VerfuegbareAnzahlAnzeigen(bibo);
            } else if(eingabe == "3") {
                NachAutorSuchen(bibo);
            } else if(eingabe == "4") {
                BuchAusleihen(bibo);
            } else if(eingabe == "5") {
                BuchZurueckgeben(bibo);
            } else if(eingabe == "6") {
                MitgliederAusgeliehenZeigen(bibo);
            } else if(eingabe == "q") {
                Console.WriteLine("Auf Wiedersehen!");
                Console.ReadKey();
            } else {
                Console.WriteLine("Eingabe Ungültig");
            }
        }
    }

    // === User Menü Auswahl 1 ===
    private static void AlleBuecherAnzeigen(Bibliothek bibo) {
        foreach(var buch in bibo.Buecher) {
            Console.WriteLine($"Titel: {buch.Titel} vom Autor : {buch.Autor}");
        }
    }
    // === User Menü Auswahl 2 ===
    private static void VerfuegbareAnzahlAnzeigen(Bibliothek bibo) {
        Console.WriteLine($" Die Anzahl der verfügbaren bücher: {bibo.AnzahlVerfuegbar().ToString()}");
    }
    // === User Menü Auswahl 3 ===
    private static void NachAutorSuchen(Bibliothek bibo) {
        Console.WriteLine($"Bitte Name des Autors eingeben oder mit Enter suche abrechen");
        var autorEingabe = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(autorEingabe)) {
            Console.WriteLine("Suche abgebrochen");
        } else {
            AutorSuchenUndAnzeigen(bibo, autorEingabe);
        }
    }
    // === User Menü Auswahl 4 ===
    private static void BuchAusleihen(Bibliothek bibo) {
        var buch = BuchSuche(bibo);
        if(buch is null) {
            Console.WriteLine("Kein Buch gefunden oder Suche abgebrochen");
            return;
        }
        Console.WriteLine($"Gefunden: {buch.Titel} von {buch.Autor}");
        var mitglied = MitgliederSuche(bibo);
        if(mitglied is null) {
            Console.WriteLine("Kein Mitglied gefunden oder Suche abgebrochen");
            return;
        }
        Console.WriteLine($"Herzlich willkommen {mitglied.Vorname} {mitglied.Nachname}");
        Console.WriteLine("1 Eingeben um Buch auszuleihen || 2 Eingeben oder Enter drücken um Vorgang abzubrechen");
        var userEingabe = Console.ReadLine();
        if(userEingabe == "1") {
            AusleihenUndMelden(bibo, buch, mitglied);
        } else {
            Console.WriteLine("Vorgang abgebrochen");
        }
    }
    // === User Menü Auswahl 5 ===
    private static void BuchZurueckgeben(Bibliothek bibo) {
        var ausgeliehen = bibo.SucheNachAusgeliehen();

        if(ausgeliehen.Count == 0) {
            Console.WriteLine("Keine ausgeliehenen Bücher gefunden");
        } else {
            Console.WriteLine($"Folgende Titel wurden ausgeliehen");
            foreach(var x in ausgeliehen) {
                Console.WriteLine($"{x.Titel} vom Autor {x.Autor}");
            }
            Console.WriteLine("Titel eingeben um Buch zurückzugeben || 2 Eingeben oder Enter drücken um Vorgang abzubrechen");
            var eingabeTitel = Console.ReadLine();
            if(eingabeTitel == "2" || string.IsNullOrWhiteSpace(eingabeTitel)) {
                Console.WriteLine("Vorgang abgebrochen");
            } else {
                var buch = bibo.SucheNachTitel(eingabeTitel);
                if(buch is null) {
                    Console.WriteLine("Titel nicht gefunden");
                } else {
                    ZurueckGebenUndMelden(buch);
                }
            }

        }
    }
    // === User Menü Auswahl 6 ===

    private static void MitgliederAusgeliehenZeigen(Bibliothek bibo) {
        var mitglied = MitgliederSuche(bibo);
        if(mitglied is null) {
            Console.WriteLine("Mitgliedsnummer nicht gefunden");
        } else {
            var buecherliste = bibo.BuecherVon(mitglied);
            if(buecherliste.Count == 0) {
                Console.WriteLine("keine ausgeliehenen Bücher gefunden");
            } else {
                foreach(var buch in buecherliste) {
                    Console.WriteLine($"{buch.Titel} vom Autor {buch.Autor}");
                }
            }
        }
    }
}

