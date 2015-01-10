using UnityEngine;
using System.Collections;
using System.IO;

public class SongLoader : MonoBehaviour
{
    public static SongLoader Instance
    {
        get
        {
            if (loader)
                return loader;

            GameObject obj = new GameObject("SongLoader", typeof(SongLoader));
            loader = obj.GetComponent<SongLoader>();

            return loader;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Song GetSong()
    {
        return song;
    }

    public void Load(Song song)
    {
        this.song = song;
    }

    public bool Load(string song)
    {
        string file = "Songs\\" + song + "\\info.song";
        FileInfo fileInfo = new FileInfo(file);

        if (!fileInfo.Exists)
            return false;

        FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(stream);

        Song newSong = Song.Deserialize(reader);

        reader.Close();

        this.song = newSong;

        return newSong != null;
    }

    private Song song;

    private static SongLoader loader;
}
