namespace BibliothekFromScratch.Domain;

public class Buch {

    public string Titel { get; }
    public string Autor { get; }
    public Mitglied? Ausleiher { get; private set; }
    public bool IstAusgeliehen { get { return Ausleiher != null; } } // Computed Property sobald Ausleiher nicht null ist, ergibt die Property true
    public decimal VerspaetungGebuehrProTag { get; } = 0.5M;
    public int MaximalAusleihTage { get; } = 14;
    public DateTime? AusleihDatum { get; private set; }


    public Buch(string titel, string autor) {
        this.Titel = titel;
        this.Autor = autor;

    }

    public bool Ausleihen(Mitglied mitglied, DateTime ausleihDatum) {
        if(IstAusgeliehen is true) {
            return false;
        } else {
            this.Ausleiher = mitglied;
            this.AusleihDatum = ausleihDatum;
            return IstAusgeliehen;
        }
    }
    public bool Zurueckgeben() {
        if(!IstAusgeliehen) {
            return false;
        } else {
            this.Ausleiher = null;
            this.AusleihDatum = null;
            return true;
        }
    }

    public bool IstUeberfaellig(DateTime dayToCheck) {
        var duedays = CalcOverDueDays(dayToCheck);
        return (duedays > 0);
    }

    public int CalcOverDueDays(DateTime rueckgabeDatum) {
        if(this.AusleihDatum is null) {
            return 0;
        } else {
            TimeSpan calcTotalDays = rueckgabeDatum - this.AusleihDatum.Value;
            var totalDays = calcTotalDays.Days; // gibt nur ganze Tage zurück. Passt besser erstmal.
            var dueDays = totalDays - MaximalAusleihTage;
            return dueDays;
        }
    }

    public decimal CalcGesamtGebuehr(DateTime rueckgabeDatum) {

        var dueDays = CalcOverDueDays(rueckgabeDatum);
        decimal gebuehr = dueDays * VerspaetungGebuehrProTag;
        if(gebuehr > 0) {
            return gebuehr;
        } else {
            return 0;
        }
    }
}

