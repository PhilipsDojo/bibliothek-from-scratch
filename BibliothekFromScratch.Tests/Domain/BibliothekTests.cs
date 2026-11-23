using BibliothekFromScratch.Domain;

namespace BibliothekFromScratch.Tests.Domain;

public class BibliothekTests {
    private readonly Bibliothek _bibo;

    public BibliothekTests() {
        _bibo = new Bibliothek();
    }

    [Fact]
    public void BuchHinzufuegen_Buch_IstInBuecherEnthalten() {
        var buch = new Buch("TestTitel", "TestAutor");

        _bibo.BuchHinzufuegen(buch);

        Assert.Contains(buch, _bibo.Buecher);
    }
    [Fact]
    public void MitgliedHinzufuegen_Mitglied_IstInMitgliedernEnthalten() {

        var mitglied = _bibo.MitgliedHinzufuegen("Xunit", "TestUser");

        Assert.Contains(mitglied, _bibo.Mitglieder);
    }

    [Fact]
    public void Ausleihen_DreiBuecherAusgeliehen_ViertesWirdAbgelehnt() {
        var mitglied = _bibo.MitgliedHinzufuegen("Xunit", "TestUser");
        for(var i = 1; i <= 3; i++) {
            var buch = new Buch($"Buch{i}", $"Autor{i}");
            _bibo.BuchHinzufuegen(buch);
            _bibo.Ausleihen(mitglied, buch, DateTime.Now);
        }
        var buch4 = new Buch("Buch4", "Autor4");
        _bibo.BuchHinzufuegen(buch4);

        var wurdeAusgeliehen = _bibo.Ausleihen(mitglied, buch4, DateTime.Now);

        Assert.False(wurdeAusgeliehen);
    }

    [Fact]
    public void Ausleihen_ZweiBuecherAusgeliehen_DrittesKlappt() {
        var mitglied = _bibo.MitgliedHinzufuegen("XUnit", "TestUser");
        for(var i = 1; i < 3; i++) {
            var buch = new Buch($"Buch{i}", $"Autor{i}");
            _bibo.BuchHinzufuegen(buch);
            _bibo.Ausleihen(mitglied, buch, DateTime.Now);
        }
        var buch3 = new Buch("Buch3", "Buch3");
        _bibo.BuchHinzufuegen(buch3);

        var wurdeAusgeliehen = _bibo.Ausleihen(mitglied, buch3, DateTime.Now);
        Assert.True(wurdeAusgeliehen);
    }

    [Fact]
    public void MitgliedHinzufuegen_ZweiMitglieder_BekommenFortlaufendeNummern() {
        var mitglied1 = _bibo.MitgliedHinzufuegen("Xunit1", "Testuser1");
        var mitglied2 = _bibo.MitgliedHinzufuegen("Xunit1", "Testuser2");

        Assert.Equal(101, mitglied1.MitgliedsNummer);
        Assert.Equal(102, mitglied2.MitgliedsNummer);
    }
    [Fact]
    public void SucheNachAutor_ZweiBuecherVomAutor_LiefertBeide() {

        var buch1 = new Buch("TitelA", "AutorA");
        var buch2 = new Buch("TitelAB", "AutorA");
        var buch3 = new Buch("TitelB", "AutorB");
        _bibo.BuchHinzufuegen(buch1);
        _bibo.BuchHinzufuegen(buch2);
        _bibo.BuchHinzufuegen(buch3);

        var autorenListe = _bibo.SucheNachAutor("AutorA");

        Assert.Equal(2, autorenListe.Count);
        Assert.Contains(buch1, autorenListe);
        Assert.Contains(buch2, autorenListe);
    }
    [Fact]
    public void SucheNachAutor_Unbekannt_Liefert_LeereListe() {
        var buch = new Buch("TitelA", "TitelB");
        _bibo.BuchHinzufuegen(buch);

        var autorenListe = _bibo.SucheNachAutor("Unknown");

        Assert.Empty(autorenListe);
    }


    [Fact]
    public void SucheNachTitel_BekannterTitel_LiefertDasBuch() {
        var buch = new Buch("TitelA", "AutorA");
        _bibo.BuchHinzufuegen(buch);

        var gefunden = _bibo.SucheNachTitel("TitelA");

        Assert.Same(buch, gefunden);
    }

    [Fact]
    public void SucheNachTitel_UnbekannterTitel_LiefertNull() {
        var buch = new Buch("TitelA", "AutorA");
        _bibo.BuchHinzufuegen(buch);

        var gefunden = _bibo.SucheNachTitel("FalscherTitel");

        Assert.Null(gefunden);
    }

    [Fact]
    public void SucheNachMitglied_BekannteNummer_LiefertDasMitglied() {
        var mitglied = _bibo.MitgliedHinzufuegen("Vorname", "Nachname");

        var gefunden = _bibo.SucheNachMitglied(mitglied.MitgliedsNummer);

        Assert.Same(mitglied, gefunden);
    }

    [Fact]
    public void SucheNachMitglied_UnbekannteNummer_LiefertNull() {
        _bibo.MitgliedHinzufuegen("Vorname", "Nachname");

        var gefunden = _bibo.SucheNachMitglied(999);

        Assert.Null(gefunden);
    }

}
