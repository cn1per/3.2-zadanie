# Zadanie 3.2 – Notatnik Avalonia

## 1. Opis projektu

Celem zadania było stworzenie aplikacji desktopowej Notatnik z wykorzystaniem technologii Avalonia UI, języka C# oraz platformy .NET.

Aplikacja umożliwia tworzenie, zapisywanie, przeglądanie i edytowanie notatek. Dane przechowywane są lokalnie w pliku `notatki.md`.

Projekt został podzielony na cztery rundy pracy z agentem AI, podczas których stopniowo rozwijano funkcjonalność programu oraz analizowano otrzymane rezultaty.

## 2. Informacje o wykorzystanym AI

- Narzędzie: Cursor
- Model: Grok 4.6
- Data wykonania: 27.10.2026
- Środowisko: Cursor / Visual Studio Code
- Technologia: Avalonia UI
- Język: C#
- Platforma: .NET

## 3. Przebieg pracy

### Runda 1 – Utworzenie notatnika

W pierwszej rundzie przygotowano podstawową aplikację Notatnik.

Utworzono interfejs zawierający listę notatek, pole edycji oraz przyciski umożliwiające zapisanie i dodanie nowego wpisu.

Logika programu została umieszczona w pliku `MainWindow.axaml.cs`, natomiast wygląd okna zdefiniowano w `MainWindow.axaml`.

### Runda 2 – Zapisywanie notatek

W drugiej rundzie agent dostosował aplikację do zapisywania danych w pliku `notatki.md`.

Zastosowano kodowanie UTF-8. Nowe wpisy umieszczane są na początku pliku, dzięki czemu najnowsza notatka wyświetlana jest jako pierwsza.

Dodano również mechanizm odczytywania zapisanych danych podczas uruchamiania aplikacji.

### Runda 3 – Weryfikacja działania

Trzecia runda została przeznaczona na sprawdzenie działania aplikacji i analizę otrzymanych rezultatów.

Zakres weryfikacji obejmował zapis notatek, ich kolejność oraz zgodność formatu pliku z wymaganiami zadania.

### Runda 4 – Kontrola końcowa

Czwarta runda obejmowała końcową analizę rozwiązania.

Sprawdzano poprawność działania interfejsu, obsługę zapisanych notatek oraz sposób przechowywania danych w pliku Markdown.

## 4. Struktura projektu

```text
Notatnik/
│
├── MainWindow.axaml
├── MainWindow.axaml.cs
├── App.axaml
├── App.axaml.cs
├── Program.cs
├── notatki.md
├── README.md
│
└── rundy/
    ├── runda-1.md
    ├── runda-2.md
    ├── runda-3.md
    └── runda-4.md
```

## 5. Uruchomienie aplikacji

Aby uruchomić projekt, należy otworzyć terminal w głównym folderze aplikacji i wykonać polecenie:

```powershell
dotnet restore
dotnet run
```

W przypadku instalacji .NET na dysku D można użyć:

```powershell
& "D:\dotnet\dotnet.exe" run
```

## 6. Wykorzystanie agenta AI

Agent Cursor został wykorzystany do generowania kodu, rozbudowy funkcjonalności oraz przygotowania mechanizmu zapisywania notatek.

Wyniki poszczególnych rund dokumentowano w folderze `rundy`.

Podczas realizacji zadania zwrócono uwagę na znaczenie precyzyjnego formułowania poleceń dla AI oraz konieczność samodzielnej kontroli wygenerowanego kodu.

## 7. Napotkane problemy

Podczas pracy pojawiła się konieczność dodania przycisku nowej notatki, ponieważ po zapisaniu wpis pozostawał zaznaczony i kolejny zapis mógł nadpisać istniejącą notatkę.

W drugiej rundzie agent zgłosił również, że budowanie projektu nie zostało uruchomione, ponieważ prośba o jego wykonanie została odrzucona.

Pokazało to, że wygenerowanie kodu przez AI nie jest równoznaczne z potwierdzeniem jego poprawnego działania.

## 8. Wnioski końcowe

Realizacja zadania pozwoliła zapoznać się z tworzeniem aplikacji desktopowych w Avalonia UI oraz obsługą plików tekstowych w języku C#.

Praca z agentem AI ułatwiła przygotowanie poszczególnych elementów programu, jednak wymagała kontrolowania wprowadzanych zmian i analizowania rezultatów.

Najważniejszym wnioskiem jest konieczność sprawdzania rzeczywistego działania aplikacji, a nie polegania wyłącznie na deklaracjach agenta.

Dokumentacja czterech rund umożliwia porównanie kolejnych etapów pracy oraz ocenę sposobu realizacji zadania.
