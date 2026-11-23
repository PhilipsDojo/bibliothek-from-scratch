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
- Buch zurückgeben, mit Anzeige fälliger Gebühren
- Mitglieder mit automatischer Nummer
- Maximales Ausleihlimit von 3 Büchern pro Mitglied
- Ausgeliehene Bücher eines Mitglieds anzeigen
- Leihfrist von 14 Tagen, danach 0,50 € Gebühr pro Tag
- Liste aller überfälligen Bücher mit Ausleiher, Tagen und Gebühr

## Architektur-Entscheidungen

**Logik und Anzeige sind getrennt.**
Die Logik liegt im Namespace `Domain`. Alle Ein- und Ausgaben liegen im Namespace `Konsole`. Die Abhängigkeit zeigt nur in eine Richtung: Die `Konsole` benutzt die `Domain`, die `Domain` kennt die `Konsole` nicht. Dadurch kann die Logik später unverändert in ein Blazor-Frontend übernommen werden.

**Kapselung der Bücherliste.**
Die Bibliothek hält ihre Bücher in einem privaten Feld (`List<Buch>`) und gibt sie nach außen nur als `IReadOnlyList<Buch>` heraus. Bücher können so nur über die Methoden der Bibliothek hinzugefügt werden, nicht an ihr vorbei.

**Unveränderliche Eigenschaften.**
Titel, Autor, Leihfrist und Gebühr pro Tag sind get-only Properties. Ausleiher und Ausleihdatum haben ein `private set` und ändern sich nur über `Ausleihen()` und `Zurueckgeben()`. `IstAusgeliehen` speichert nichts selbst, sondern wird aus dem Ausleiher berechnet.

**Berechnen statt speichern.**
Ob ein Buch überfällig ist und wie hoch die Gebühr ist, hängt vom Stichtag ab. Diese Werte werden deshalb bei jeder Abfrage neu berechnet und nie gespeichert, sonst würden sie veralten. Die gemeinsame Tagesrechnung steht in einer einzigen Hilfsmethode.

**Das Datum kommt von außen.**
`Buch` und `Bibliothek` rufen nie selbst `DateTime.Now` auf, sondern bekommen das Datum als Parameter. Nur das Menü in der `Konsole` bestimmt, was „heute“ ist. So lassen sich verspätete Rückgaben testen, ohne wirklich 14 Tage zu warten.

**Rückgabewerte statt Exceptions für normale Fälle.**
Ein bereits ausgeliehenes Buch, ein erreichtes Ausleihlimit oder eine Suche ohne Treffer sind keine Fehler, sondern normale Situationen. `Ausleihen()` gibt deshalb `true` oder `false` zurück, und Suchen liefern eine leere Liste oder `null`.

**Mitglieder nur über die Bibliothek anlegen.**
Neue Mitglieder werden nur über `Bibliothek.MitgliedHinzufuegen()` angelegt, nie direkt mit `new Mitglied(...)`. So vergibt die Bibliothek die Mitgliedsnummer automatisch und eindeutig.

**Single Source of Truth.**
Nur das Buch weiß, wer es ausgeliehen hat (`Ausleiher`). Mitglieder haben keine eigene Bücherliste. Welche Bücher ein Mitglied hat, ermittelt die Bibliothek mit `BuecherVon()`. So kann die Information nie an zwei Stellen auseinanderlaufen.

## Fahrplan

- [x] Klasse `Buch`, Liste und Schleifen
- [x] Ausleihen mit Prüfung
- [x] Suchen und Zählen
- [x] Menü mit Ausleihen und Zurückgeben
- [x] Mitglieder und Ausleihlimits
- [x] Gebühren bei verspäteter Rückgabe
- [ ] Unit-Tests
- [ ] Vererbung und Interfaces (weitere Medienarten)
- [ ] Exceptions für echte Fehlerfälle
- [ ] CSV-Import mit Prüfung jeder Zeile
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