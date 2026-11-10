# bibliothek-from-scratch

Eine kleine Bibliotheksverwaltung in C#, die Schritt für Schritt **von null aufgebaut** wird: erst als Konsolen-App ganz ohne Frameworks, später mit Blazor-Frontend und Datenbank.

## Warum dieses Projekt?

Im Arbeitsalltag mit .NET, Blazor, EF Core und Dependency Injection passiert vieles unsichtbar: Frameworks erzeugen Objekte, rufen Konstruktoren auf und verbinden Abhängigkeiten. Dieses Projekt baut das Fundament bewusst selbst, um zu verstehen, **was** dabei passiert und **warum**.

Die Commit-Historie zeigt den Weg: von der ersten Klasse bis zur fertigen Anwendung.

## Aktueller Stand

Konsolen-App mit Menü:

- Alle Bücher anzeigen
- Anzahl verfügbarer Bücher anzeigen
- Bücher nach Autor suchen
- Buch nach Titel suchen und ausleihen
- Buch zurückgeben
- Mitglieder mit automatischer Nummer
- Maximales Ausleihlimit 3
- Ausgeliehene Bücher eines Mitglieds anzeigen

## Architektur-Entscheidungen

**Logik und Anzeige sind getrennt.**
`Buch` und `Bibliothek` enthalten nur Logik und kein `Console`. Alle Ein- und Ausgaben liegen in `Konsolenausgabe` und `Program`. Dadurch kann die Logik später unverändert in ein Blazor-Frontend übernommen werden.

**Kapselung der Bücherliste.**
Die Bibliothek hält ihre Bücher in einem privaten Feld (`List<Buch>`) und gibt sie nach außen nur als `IReadOnlyList<Buch>` heraus. Bücher können so nur über die Methoden der Bibliothek hinzugefügt werden, nicht an ihr vorbei.

**Unveränderliche Eigenschaften.**
Titel und Autor sind get-only Properties und werden nur im Konstruktor gesetzt. Der Ausleiher hat ein private set und ändert sich nur über Ausleihen() und Zurueckgeben(). IstAusgeliehen speichert nichts selbst, sondern wird aus dem Ausleiher berechnet.

**Rückgabewerte statt Exceptions für normale Fälle.**
Ein bereits ausgeliehenes Buch oder eine Suche ohne Treffer sind keine Fehler, sondern normale Situationen. `Ausleihen()` gibt deshalb `true` oder `false` zurück, und Suchen liefern eine leere Liste oder `null`.

**Mitglieder hinzufügen.** 
Neue Mitglieder nur über die Klasse Bibliothek "MitgliedHinzufuegen" hinzufügen. So wird die Nummer automatisch vergeben.

**Single Source of Truth.**
Nur das Buch weiß, wer es ausgeliehen hat (Ausleiher). Mitglieder haben keine eigene Bücherliste. Welche Bücher ein Mitglied hat, ermittelt die Bibliothek mit BuecherVon(). So kann die Information nie an zwei Stellen auseinanderlaufen.


## Fahrplan

- [x] Klasse `Buch`, Liste und Schleifen
- [x] Ausleihen mit Prüfung
- [x] Suchen und Zählen
- [x] Menü mit Ausleihen und Zurückgeben
- [x] Mitglieder und Ausleihlimits
- [ ] Gebühren bei verspäteter Rückgabe
- [ ] Vererbung und Interfaces (weitere Medienarten)
- [ ] Exceptions für echte Fehlerfälle
- [ ] CSV-Import mit Prüfung jeder Zeile
- [ ] Unit-Tests
- [ ] Blazor-Frontend
- [ ] Datenbank mit EF Core

## Starten

Voraussetzung: [.NET SDK](https://dotnet.microsoft.com/download)

```
git clone https://github.com/PhilipsDojo/bibliothek-from-scratch.git
cd bibliothek-from-scratch
dotnet run
```

## Technik

C# · .NET · Konsolen-App (später Blazor, EF Core)
