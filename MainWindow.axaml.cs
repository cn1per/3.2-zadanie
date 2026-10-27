using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private static readonly string NotesFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Notatnik",
        "notatki.json");

    public ObservableCollection<Note> Notes { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        LoadNotes();
    }

    private void SaveNote_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleInput.Text?.Trim();
        var content = ContentInput.Text?.Trim();

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
        {
            SetStatus("Uzupełnij tytuł i treść notatki.", isError: true);
            return;
        }

        var note = new Note
        {
            Title = title,
            Content = content,
            CreatedAt = DateTime.Now
        };
        var updatedNotes = Notes.Append(note).ToList();

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(NotesFilePath)!);
            var json = JsonSerializer.Serialize(updatedNotes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(NotesFilePath, json);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            SetStatus($"Nie udało się zapisać notatki: {exception.Message}", isError: true);
            return;
        }

        Notes.Add(note);
        TitleInput.Clear();
        ContentInput.Clear();
        UpdateNoteCount();
        SetStatus("Notatka została zapisana.", isError: false);
    }

    private void LoadNotes()
    {
        if (!File.Exists(NotesFilePath))
        {
            UpdateNoteCount();
            return;
        }

        try
        {
            var json = File.ReadAllText(NotesFilePath);
            var savedNotes = JsonSerializer.Deserialize<List<Note>>(json);
            if (savedNotes is null)
            {
                SetStatus("Plik z notatkami ma nieprawidłowy format.", isError: true);
                return;
            }

            foreach (var note in savedNotes)
            {
                Notes.Add(note);
            }

            UpdateNoteCount();
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or JsonException)
        {
            SetStatus($"Nie udało się wczytać notatek: {exception.Message}", isError: true);
        }
    }

    private void UpdateNoteCount()
    {
        NoteCount.Text = Notes.Count.ToString();
    }

    private void SetStatus(string message, bool isError)
    {
        StatusText.Text = message;
        StatusText.Foreground = Avalonia.Media.Brush.Parse(isError ? "#B42318" : "#21805A");
    }
}

public sealed class Note
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string Preview => Content.ReplaceLineEndings(" ").Trim();
    public string DateLabel => CreatedAt.ToString("dd.MM.yyyy");
}
