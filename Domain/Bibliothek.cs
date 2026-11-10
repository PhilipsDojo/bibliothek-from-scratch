namespace BibliothekFromScratch.Domain;

public class Bibliothek {
    private List<Buch> _buecherliste = new List<Buch>();
    private List<Mitglied> _mitgliederliste = new List<Mitglied>();
    private int _naechsteMitgliedsNummer = 101;
    public int MaxBuecherProMitglied { get; } = 3;
    public IReadOnlyList<Mitglied> Mitglieder { get { return _mitgliederliste; } }
    public IReadOnlyList<Buch> Buecher { get { return _buecherliste; } }


    public void BuchHinzufuegen(Buch buch) {
        _buecherliste.Add(buch);
    }

    public Mitglied MitgliedHinzufuegen(string vorname, string nachname) {
        var mitglied = new Mitglied(_naechsteMitgliedsNummer, vorname, nachname);
        _naechsteMitgliedsNummer += 1;
        _mitgliederliste.Add(mitglied);
        return mitglied;
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

    public Mitglied? SucheNachMitglied(int mitgliedsNummer) {
        foreach(var mitglied in _mitgliederliste) {
            if(mitglied.MitgliedsNummer == mitgliedsNummer) {
                return mitglied;
            }
        }
        return null;
    }
    public Buch? SucheNachTitel(string titel) {

        foreach(var buch in _buecherliste) {
            if(buch.Titel == titel) {
                return buch;
            }
        }
        return null;
    }

    public IReadOnlyList<Buch> SucheNachAusgeliehen() {
        List<Buch> trefferList = new List<Buch>();
        foreach(var buch in _buecherliste) {
            if(buch.IstAusgeliehen == true) {
                trefferList.Add(buch);
            }
        }
        return trefferList;
    }

    public IReadOnlyList<Buch> BuecherVon(Mitglied mitglied) {
        List<Buch> hatAusgeliehen = new List<Buch>();
        foreach(var buch in _buecherliste) {
            if(buch.Ausleiher == mitglied) {
                hatAusgeliehen.Add(buch);
            }
        }
        return hatAusgeliehen;
    }

    public bool Ausleihen(Mitglied mitglied, Buch buch) {
        if(BuecherVon(mitglied).Count < MaxBuecherProMitglied) {
            var result = buch.Ausleihen(mitglied);
            return result;
        } else {
            return false;
        }
    }
}
