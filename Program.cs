using BibliothekFromScratch.Konsole;
using BibliothekFromScratch.Domain;

namespace BibliothekFromScratch;

internal class Program {
    static void Main(string[] args) {

        var bibo = new Bibliothek();

        var buch1 = new Buch("TitelA", "AutorA");
        var buch2 = new Buch("TitelB", "AutorB");
        var buch3 = new Buch("TitelC", "AutorC");
        var buch4 = new Buch("NeuerTitel", "AutorA");

        bibo.BuchHinzufuegen(buch1);
        bibo.BuchHinzufuegen(buch2);
        bibo.BuchHinzufuegen(buch3);
        bibo.BuchHinzufuegen(buch4);

        var mitglied1 = bibo.MitgliedHinzufuegen("philip", "schacht");
        var mitglied2 = bibo.MitgliedHinzufuegen("Levi", "Finn");
        var mitglied3 = bibo.MitgliedHinzufuegen("Jacky", "Zant");

        Konsolenausgabe.StarteMenue(bibo);
    }

}
