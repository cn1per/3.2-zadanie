# Runda 1

## Odpowiedź agenta

Po zapisie notatka zostaje zaznaczona, więc kolejny zapis nadpisałby ją — dodaję przycisk nowej notatki, żeby dało się dodać kolejną.

## Wygenerowany kod

```xml
<Button x:Name="SaveButton"
        DockPanel.Dock="Bottom"
        Content="Zapisz"
```

```csharp
private void OnNewClick(object? sender, RoutedEventArgs e)
{
    NotesList.SelectedItem = null;
```

## Wynik uruchomienia

Liczba błędów: 0

Czas, który upłynął: 00:00:01.50

Dostępne są aktualizacje obciążenia. Uruchom polecenie `dotnet workload list`, aby uzyskać więcej informacji.

Notatnik jest gotowy w projekcie Avalonia. Okno ma pole
