using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class EditorMenuScript : MonoBehaviour
{
    public void New()
    {
        DirectoryInfo songDirectories = new DirectoryInfo("Songs");

        DirectoryInfo[] songs = songDirectories.GetDirectories();
        List<string> names = new List<string>();

        int songDirectoryCount = songs.Length;
        for (int i = 0; i < songDirectoryCount; i++)
        {
            DirectoryInfo songDirectory = songs[i];
            string songFile = Path.Combine(songDirectory.FullName, "info.song");

            if (File.Exists(songFile))
                continue;

            names.Add(songDirectory.Name);
        }

        string[] namesArray = names.ToArray();
        SelectorDialogue.Show(namesArray, OnNewSongSelected, "Select New Song Source", "No empty folders found in the Songs folder.");
    }

    public void Open()
    {
        DirectoryInfo songDirectories = new DirectoryInfo("Songs");

        DirectoryInfo[] songs = songDirectories.GetDirectories();
        List<string> names = new List<string>();

        int songDirectoryCount = songs.Length;
        for (int i = 0; i < songDirectoryCount; i++)
        {
            DirectoryInfo songDirectory = songs[i];
            string songFile = Path.Combine(songDirectory.FullName, "info.song");

            if (!File.Exists(songFile))
                continue;

            names.Add(songDirectory.Name);
        }

        string[] namesArray = names.ToArray();
        SelectorDialogue.Show(namesArray, OnSongSelected, "Select A Song", "No songs created yet, select [NEW] instead.");
    }

    public void Cancel()
    {
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void OnNewSongSelected(string song)
    {
        if (string.IsNullOrEmpty(song))
            return;

        Song newSong = new Song(song);

        SongLoader.Instance.Load(newSong);

        Application.LoadLevel("SongFileFinder");
    }

    void OnSongSelected(string song)
    {
        if (string.IsNullOrEmpty(song))
            return;

        bool couldLoad = SongLoader.Instance.Load(song);

        if (couldLoad)
            Application.LoadLevel("TrackEditor");
    }
}
