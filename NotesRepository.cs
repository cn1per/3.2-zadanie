using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace notatnik;

public sealed class NotesRepository
{
    private readonly string _filePath;

    public NotesRepository()
        : this(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "notatnik",
            "notatki.json"))
    {
    }

    public NotesRepository(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    public IReadOnlyList<Note> Load()
    {
        string json;
        try
        {
            json = File.ReadAllText(_filePath, Encoding.UTF8);
        }
        catch (FileNotFoundException)
        {
            return Array.Empty<Note>();
        }
        catch (DirectoryNotFoundException)
        {
            return Array.Empty<Note>();
        }

        var notes = JsonSerializer.Deserialize<List<Note>>(json);
        if (notes is null || notes.Any(note =>
                note is null
                || string.IsNullOrWhiteSpace(note.Title)
                || string.IsNullOrWhiteSpace(note.Content)))
        {
            throw new InvalidDataException("Plik z notatkami zawiera nieprawidłowe dane.");
        }

        return notes;
    }

    public void Save(IEnumerable<Note> notes)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json, new UTF8Encoding(false));
    }
}
