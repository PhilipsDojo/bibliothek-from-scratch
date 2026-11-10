namespace BibliothekFromScratch;

public class Mitglied {

    public int MitgliedsNummer { get; }
    public string Vorname { get; } = "";
    public string Nachname { get; } = "";

    public Mitglied(int mitgliedsNummer, string vorname, string nachname) {
        this.MitgliedsNummer = mitgliedsNummer;
        this.Vorname = vorname;
        this.Nachname = nachname;
    }
}