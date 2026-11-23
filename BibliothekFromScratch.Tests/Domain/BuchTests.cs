using BibliothekFromScratch.Domain;
using System.ComponentModel.DataAnnotations;

namespace BibliothekFromScratch.Tests.Domain;

public class BuchTests {
    private readonly Bibliothek _bibo;
    private readonly Buch _buch;
    private readonly Mitglied _mitglied;
    public BuchTests(){
        _bibo = new Bibliothek();
        _buch = new Buch("Titel", "Autor");
        _mitglied = _bibo.MitgliedHinzufuegen("Xunit", "TestUser");
        _bibo.BuchHinzufuegen(_buch);
    }


    [Fact]
    public void BuchAusleihen_Ausgeliehen_ErgibtTrue() {
        //arrange

        //act
        _buch.Ausleihen(_mitglied, DateTime.Now);
        //assert
        Assert.True(_buch.IstAusgeliehen);
    }

    [Fact]
    public void Zurueckgeben_AusgeliehenesBuch_IstNichtMehrAusgeliehen() {
        //arrange
        _buch.Ausleihen(_mitglied, DateTime.Now);
        //act
        _buch.Zurueckgeben();
        //assert
        Assert.False(_buch.IstAusgeliehen);
    }
    [Fact]
    public void Ueberfaelliges_Buch_Ergibt_True() {
        //arrange
        var ausleihDatum = DateTime.Now.AddDays(-20);
        _buch.Ausleihen(_mitglied, ausleihDatum);
        // act

        var istUeberfaellig = _buch.IstUeberfaellig(DateTime.Now);

        // assert
        Assert.True(istUeberfaellig);
    }

    [Fact]
    public void AusgeliehensBuch_KannNicht_ErneutGeliehenWerden() {
        //arrange
        _buch.Ausleihen(_mitglied, DateTime.Now);
        var mitglied2 = _bibo.MitgliedHinzufuegen("User2", "TestUser2");
        //act
        var ausleihenKlappt = _buch.Ausleihen(mitglied2, DateTime.Now);
        //assert
        Assert.False(ausleihenKlappt);
        Assert.Same(_mitglied, _buch.Ausleiher);
    }

    [Fact]
    public void BerechneGebuehr_Nach20Tagen_Ergibt3Euro() {
        //arrange
        var ausleihDatum = DateTime.Now.AddDays(-20);
        _buch.Ausleihen(_mitglied, ausleihDatum);
        //act
        var gebuehr = _buch.CalcGesamtGebuehr(DateTime.Now);
        //assert
        Assert.Equal(3m, gebuehr);
    }

    [Fact]
    public void BerechneGebuehr_Nach14Tagen_Ergibt0Euro() {
        //arrange
        var ausleihDatum = DateTime.Now.AddDays(-14);
        _buch.Ausleihen(_mitglied, ausleihDatum);
        //act
        var gebuehr = _buch.CalcGesamtGebuehr(DateTime.Now);
        //assert
        Assert.Equal(0m, gebuehr);
    }

    [Fact]
    public void BerechneGebuehr_Nach15Tagen_Ergibt05Euro() {
        //arrange
        var ausleihDatum = DateTime.Now.AddDays(-15);
        _buch.Ausleihen(_mitglied, ausleihDatum);
        //act
        var gebuehr = _buch.CalcGesamtGebuehr(DateTime.Now);
        //assert
        Assert.Equal(0.5m, gebuehr);
    }
    [Fact]
    public void NachRueckgabe_Sind_Ausleiher_und_AusleihDatum_Null() {
        // arrange
        _buch.Ausleihen(_mitglied, DateTime.Now);
        //act
        _buch.Zurueckgeben();
        //assert
        Assert.Null(_buch.Ausleiher);
        Assert.Null(_buch.AusleihDatum);
    }
}
