# 📓 DEVLOG – Mein erstes funktionierendes Wörterbuch

## 🗓️ 21.04.2026
- 🎯 Ziel: Umgang mit JSON sowie Speichern und Laden von Daten lernen  
- 🕒 Dauer: ca. 1 Woche  
- 🛠️ Setup: Visual Studio, C#

---

## 🚀 Fortschritte
- Menüsystem erstellt (Anzeigen, Hinzufügen, Löschen, Suchen)
- Wörterbuch lokal gespeichert (JSON-Dateien)
- Mehrere Wörterbücher erstellt und geladen
- Erste praktische Erfahrung mit JSON gesammelt

---

## ❌ Probleme
- JSON speichert nicht automatisch (nur Serialisierung)
- Verständnisprobleme beim Laden/Speichern
- Schwierigkeiten beim Wiederfinden gespeicherter Daten
- Name des Wörterbuchs wurde anfangs nicht gespeichert
- Unsicherheit, wo genau Speichern/Laden im Code hingehört

---

## ✅ Lösungen
- Verwendung von `File.WriteAllText()` und `File.ReadAllText()`
- Schrittweise Verständnis von JSON (Serialisieren/Deserialisieren)
- Strukturierung des Codes verbessert
- Fehlerbehandlung mit `try/catch` eingebaut

---

## 🧠 Learnings
- JSON ist nur für die Umwandlung von Objekten → Speichern muss man selbst
- Arbeiten mit Dateien (`File`-Klasse)
- Konstruktoren und warum mehrere sinnvoll sind
- Schleifen + `break` richtig einsetzen
- Eingaben absichern (`IsNullOrWhiteSpace`)
- Grundlagen von Fehlerbehandlung (`try/catch`)

---

## 🏁 Ergebnis
Das Programm funktioniert:
- Wörter hinzufügen, anzeigen, löschen und suchen
- Wörterbücher speichern und laden
- Mehrere Wörterbücher verwalten

---

## 💬 Persönliche Notiz
Dieses Projekt war am Anfang einfach, wurde dann aber durch JSON und Dateiverarbeitung deutlich schwieriger.  
Am Ende habe ich ein viel besseres Verständnis dafür entwickelt, wie Programme Daten speichern und laden.
