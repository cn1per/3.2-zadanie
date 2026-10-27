using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private static readonly Regex NoteHeaderPattern = new(
        @"^## (\d{4}-\d{2}-\d{2} \d{2}:\d{2}) — ([^\r\n]*)\r?$",
        RegexOptions.Multiline | RegexOptions.CultureInvariant);
    private static readonly string NotesFilePath = Path.Combine(FindProjectDirectory(), "notatki.md");

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
        var updatedNotes = new List<Note> { note };
        updatedNotes.AddRange(Notes);

        try
        {
            File.WriteAllText(NotesFilePath, FormatNotes(updatedNotes), new UTF8Encoding(false));
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            SetStatus($"Nie udało się zapisać notatki: {exception.Message}", isError: true);
            return;
        }

        Notes.Insert(0, note);
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
            var markdown = File.ReadAllText(NotesFilePath, Encoding.UTF8);
            if (!TryParseNotes(markdown, out var savedNotes))
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
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            SetStatus($"Nie udało się wczytać notatek: {exception.Message}", isError: true);
        }
    }

    private static string FindProjectDirectory()
    {
        foreach (var startDirectory in new[] { AppContext.BaseDirectory, Environment.CurrentDirectory })
        {
            DirectoryInfo? directory = new DirectoryInfo(startDirectory);
            while (directory is not null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "notatnik.csproj")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }
        }

        return Environment.CurrentDirectory;
    }

    private static string FormatNotes(IEnumerable<Note> notes)
    {
        return string.Join(
            $"{Environment.NewLine}{Environment.NewLine}",
            notes.Select(note =>
                $"## {note.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)} — {note.Title}{Environment.NewLine}{note.Content}"));
    }

    private static bool TryParseNotes(string markdown, out List<Note> notes)
    {
        notes = new List<Note>();
        var headers = NoteHeaderPattern.Matches(markdown);
        if (headers.Count == 0)
        {
            return string.IsNullOrWhiteSpace(markdown);
        }

        if (!string.IsNullOrWhiteSpace(markdown[..headers[0].Index]))
        {
            return false;
        }

        for (var index = 0; index < headers.Count; index++)
        {
            var header = headers[index];
            if (!DateTime.TryParseExact(
                    header.Groups[1].Value,
                    "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var createdAt))
            {
                notes.Clear();
                return false;
            }

            var contentStart = header.Index + header.Length;
            var contentEnd = index + 1 < headers.Count ? headers[index + 1].Index : markdown.Length;
            var content = markdown[contentStart..contentEnd].Trim('\r', '\n');
            notes.Add(new Note
            {
                Title = header.Groups[2].Value,
                Content = content,
                CreatedAt = createdAt
            });
        }

        return true;
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
    public string DateLabel => CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
}
