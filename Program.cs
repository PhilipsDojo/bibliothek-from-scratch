
namespace BibliothekFromScratch;

internal class Program {
    static void Main(string[] args) {

        var bibo = new Bibliothek();

        var buch1 = new Buch("TitelA", "AutorA");
        var buch2 = new Buch("TitelB", "AutorB");
        var buch3 = new Buch("TitelC", "AutorC");
        var buch4 = new Buch("NeuerTitel", "AutorA");

        bibo.Buchhinzufuegen(buch1);
        bibo.Buchhinzufuegen(buch2);
        bibo.Buchhinzufuegen(buch3);
        bibo.Buchhinzufuegen(buch4);

        Konsolenausgabe.StarteMenue(bibo);
    }

}
