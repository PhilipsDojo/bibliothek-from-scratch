
using System.Security.Cryptography.X509Certificates;

namespace BibliothekFromScratch;

public class Bibliothek {

    private List<Buch> _buecherliste = new List<Buch>();

    public IReadOnlyList<Buch> Buecher { get { return _buecherliste; } }

    public void Buchhinzufuegen(Buch buch) {
        _buecherliste.Add(buch);
    }

    public int AnzahlVerfuegbar() {
        int zaehler = 0;
        for(int i = 0; i < _buecherliste.Count; i++) {
            if(!_buecherliste[i].IstAusgeliehen) {
                zaehler++;
            }
        }
        return zaehler;
    }

    public IReadOnlyList<Buch> SucheNachAutor(string autor) {

        List<Buch> treffer = new List<Buch>();

        foreach(var buch in _buecherliste) {
            if(buch.Autor == autor) {
                treffer.Add(buch);
            }
        }
        return treffer;
    }
    public Buch? SucheNachTitel(string titel) {

        foreach(var buch in _buecherliste) {
            if(buch.Titel == titel) {
                return buch;
            }
        }
        return null;
    }
}
