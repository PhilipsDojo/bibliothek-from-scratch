namespace BibliothekFromScratch;

public class Buch {

    public string Titel { get; }
    public string Autor { get; }
    public Mitglied? Ausleiher { get; private set; }
    public bool IstAusgeliehen { get { return Ausleiher != null; } } // Computed Property sobald Ausleiher nicht null ist, ergibt die Property true

    public Buch(string titel, string autor) {
        this.Titel = titel;
        this.Autor = autor;

    }

    public bool Ausleihen(Mitglied mitglied) {
        if(IstAusgeliehen is true) {
            return false;
        } else {
            this.Ausleiher = mitglied;
            return IstAusgeliehen;
        }
    }
    public bool Zurueckgeben() {
        if(!IstAusgeliehen) {
            return false;
        } else {
            this.Ausleiher = null;
            return true;
        }
    }
}
