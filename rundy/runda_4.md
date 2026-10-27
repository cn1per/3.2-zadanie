# Runda 4 – Notatnik Avalonia

## Cel rundy

Celem czwartej rundy było sprawdzenie poprawności działania aplikacji Notatnik, zapisywania danych do pliku Markdown oraz ich ponownego odczytu po uruchomieniu programu.

## Wykonane czynności

1. Sprawdzenie działania przycisku dodawania nowej notatki.
2. Sprawdzenie możliwości zapisywania i edytowania istniejących notatek.
3. Weryfikacja formatu pliku `notatki.md`.
4. Sprawdzenie kolejności wyświetlania zapisanych notatek.
5. Kontrola poprawności obsługi polskich znaków w kodowaniu UTF-8.

## Opis rozwiązania

Aplikacja została przygotowana w technologii Avalonia UI z wykorzystaniem języka C# i platformy .NET.

Interfejs użytkownika znajduje się w pliku `MainWindow.axaml`, natomiast logika działania programu została umieszczona w `MainWindow.axaml.cs`.

Notatki przechowywane są lokalnie w pliku `notatki.md`. Nowe wpisy powinny pojawiać się na początku listy, a zapisane dane powinny być ponownie wczytywane podczas uruchamiania aplikacji.

## Wnioski

Czwarta runda służy weryfikacji kompletności rozwiązania oraz sprawdzeniu zgodności działania aplikacji z wymaganiami zadania.

Szczególną uwagę należy zwrócić na zachowanie oryginalnego formatu pliku, poprawność zapisu danych oraz możliwość ich ponownego odczytu.

## Informacje końcowe

Narzędzie: Cursor  
Technologia: Avalonia UI  
Język programowania: C#  
Platforma: .NET  
Format zapisu: Markdown (.md)  
Kodowanie: UTF-8
