namespace BibliothekFromScratch;

public class Buch {

    public  string Titel { get; }
    public  string Autor { get;  }
    public bool IstAusgeliehen { get; private set; }

    public Buch(string titel, string autor) {
        this.Titel = titel;
        this.Autor = autor;

    }

    public bool Ausleihen() {
        if(IstAusgeliehen is true) {
            return false;
        } else {
            this.IstAusgeliehen = true;
            return IstAusgeliehen;
        }
    }
    public bool Zurueckgeben() {
        if(!IstAusgeliehen) {
            return false;
        } else {
            this.IstAusgeliehen = false;
            return true;
        }
    }
}
