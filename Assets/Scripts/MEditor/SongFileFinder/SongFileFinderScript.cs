using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class SongFileFinderScript : MonoBehaviour
{
    enum FileCheckerState
    {
        CheckingFiles,
        MultipleFilesError,
        NoFileFoundError,
        ConfirmFile,
        Done
    }

    void Start()
    {
        ChangeState(FileCheckerState.CheckingFiles);
    }

    public void CheckFile()
    {
        Song song = SongLoader.Instance.GetSong();

        string songDir = song.SongDir;

        DirectoryInfo dirInfo = new DirectoryInfo(songDir);

        FileInfo[] songFiles = dirInfo.GetFiles();
        List<string> extensions = new List<string>() { "*.wav", "*.ogg", ".mp3", ".mp2" };

        FileInfo[] files = (from file in songFiles where extensions.Contains(file.Extension.ToLower()) select file).ToArray<FileInfo>();

        if(songFiles.Length == 0)
        {
            ChangeState(FileCheckerState.NoFileFoundError);
            return;
        }
        else if(songFiles.Length > 1)
        {
			ChangeState(FileCheckerState.MultipleFilesError);
            return;
        }

        foundFile = songFiles[0];
        ChangeState(FileCheckerState.ConfirmFile);
    }

    public void Ok()
    {
        switch (state)
        {
            case FileCheckerState.CheckingFiles:
                break;
            case FileCheckerState.ConfirmFile:
                ChangeState(FileCheckerState.Done);
                break;
            case FileCheckerState.NoFileFoundError:
                CheckFile();
                break;
            case FileCheckerState.MultipleFilesError:
                CheckFile();
                break;
            case FileCheckerState.Done:
                break;
        }
    }

    public void Cancel()
    {
        switch (state)
        {
            case FileCheckerState.CheckingFiles:
                break;
            case FileCheckerState.ConfirmFile:
                CheckFile();
                break;
            case FileCheckerState.NoFileFoundError:
                break;
            case FileCheckerState.MultipleFilesError:
                break;
            case FileCheckerState.Done:
                break;
        }
    }

    private void ChangeState(FileCheckerState state)
    {
        this.state = state;

        switch(state)
        {
            case FileCheckerState.CheckingFiles:
                OnCheckingFiles();
                break;
            case FileCheckerState.ConfirmFile:
                OnConfirmFile();
                break;
            case FileCheckerState.NoFileFoundError:
                OnNoFileFoundError();
                break;
            case FileCheckerState.MultipleFilesError:
                OnMultipleFilesError();
                break;
            case FileCheckerState.Done:
                OnDone();
                break;
        }
    }

    private void OnCheckingFiles()
    {
        CheckFile();
    }

    private void OnNoFileFoundError()
    {
        string songDir = SongLoader.Instance.GetSong().SongDir;
        message.text = string.Format("No audio file was found in {0}, please put in a single .wav, .mp3, .mp2, or .ogg file to use as the audio file then press [Ok].", songDir);
    }

    private void OnMultipleFilesError()
    {
        string songDir = SongLoader.Instance.GetSong().SongDir;
        message.text = string.Format("Multiple audio files were found inside {0}, please make sure that there is one audio file, then press [Ok]", songDir);
    }

    private void OnConfirmFile()
    {
        string fileName = foundFile.Name;
        message.text = string.Format("{0} was the audio file that was found, press [Ok] to use this audio file", fileName);
    }

    private void OnDone()
    {
        Song song = SongLoader.Instance.GetSong();
        song.AudioFileName = foundFile.Name;

        Application.LoadLevel("BeatEditor");
    }

    [SerializeField]
    private Text message;

    [SerializeField]
    private GameObject okButton;

    [SerializeField]
    private GameObject cancelButton;

    private FileCheckerState state;

    private FileInfo foundFile;
}
