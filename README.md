# Null Reference Exption in Klassen

![NET](https://img.shields.io/badge/NET-10.0-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2025.1-yellow.svg)]

Überlegungen zum Design und Erstellungen von Klassen die kein unbehandeltes *null* zurück geben.
Die **Null Reference Exception** ist eine der häufigsten Ursachen für Laufzeitfehler in Programmen. In diesem Repository werden verschiedene Ansätze und Muster vorgestellt, um Klassen zu entwerfen, die sicherstellen, dass sie niemals "null" zurückgeben.\
Hierzu wurde die Klasse **Return\<T>** erstellt, die als Wrapper für Rückgabewerte dient und sicherstellt, dass diese Werte immer gültig sind.

## Beispiel
```csharp
private static Return<long> ParseLong(string? value)
{
    if (int.TryParse(value, out var n))
    {
        return Return<long>.Success(n);
    }

    return Return<long>.Fail();
}
```

## Ergebnis
```csharp
Return<long> numberLong = ParseLong("123");
if (numberLong.IsSuccess)
{
    Console.WriteLine($"Erfolgreich geparst: {numberLong.Value}");
}
else
{
    Console.WriteLine("Fehler beim Parsen");
}
```

Die Methoden **Success** und **Fail** ermöglichen es, den Rückgabewert klar zu definieren, ohne dass "null" verwendet wird.\
Zusätzlich bietet die Klasse **Return\<T>** Eigenschaften wie **IsSuccess**, **IsFail** und **Value**, um den Status und den Wert der Rückgabe zu überprüfen.
Die Methoden **Success** und **Fail** ermöglichens es auch einen zusätzliche Beschreibung zu übergeben, zu **Fail** kann zusätzlich die entstandene Exception mitgegeben werden, um detailliertere Fehlermeldungen zu liefern.

In dem Demo sind noch weitere Beispiele und Anwendungsfälle enthalten, die zeigen, wie diese Klasse **Return\<T>** in verschiedenen Szenarien weiter verwendet werden kann.


