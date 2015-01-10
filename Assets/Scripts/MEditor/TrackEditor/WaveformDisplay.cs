using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class WaveformDisplay : UIBehaviour
{
    void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    /// <summary>
    /// Loads a song into the waveform display.
    /// </summary>
    /// <param name="song">The song to load.</param>
    public void Load(Song song)
    {
        AudioClip clip = song.Clip;

        if (!clip)
            return;

        StartCoroutine(CreateTexture(clip));//load the texture as an async
    }

    /// <summary>
    /// Loads an audio clip into the waveform display.
    /// </summary>
    /// <param name="clip">The audio clip to load in.</param>
    [Obsolete("Use Load with song instead")]
    public void Load(AudioClip clip)
    {
        StartCoroutine(CreateTexture(clip));
    }

    /// <summary>
    /// Sets the scroller to be at a location on the waveform.
    /// </summary>
    /// <param name="location">The location between 0-1.</param>
    public void SetScrollLocation(float location)
    {
        location = Mathf.Clamp01(location);

        Vector3 position = scrollLocation.anchoredPosition3D;

        float width = rectTransform.rect.width;//get the width of the scroll area

        position.x = location * width;
        scrollLocation.anchoredPosition3D = position;
    }

    private IEnumerator CreateTexture(AudioClip clip)
    {
        Rect rect = rectTransform.rect;

        Vector2 dimensions = new Vector2(rect.width, rect.height) * pixelsPerUnit;//get the number of pixels to use
        dimensions.x = Mathf.Ceil(dimensions.x);
        dimensions.y = Mathf.Ceil(dimensions.y);

        int width = (int)dimensions.x;
        int height = (int) dimensions.y;

        yield return null;//wait a frame before creating the texture

        Texture2D newTexture = new Texture2D(width, height);//create the new texture
        int size = width * height;//get the size in color data of the texture

        Color[] data = new Color[size];//create the array of data that we will use to populate the image
        for(int i = 0; i < size; i++)//clear the buffer to the correct color
        {
            data[0] = backgroundColor;
        }

        newTexture.SetPixels(data);

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), pixelsPerUnit);

        Image image = GetComponent<Image>();
        image.overrideSprite = newSprite;

        yield return null;

        int sampleCount = clip.samples;//get the number of samples in the audio clip
        float[] samples = new float[sampleCount];//create a buffer for the samples
        clip.GetData(samples, 0);//load the clip data into the buffer

        yield return null;

        Debug.Log("Loading in waveform!");

        for (int i = 0; i < sampleCount; i++)
        {
            float sample = samples[i];

            int x = (int)((float)width * ((float)i / (float)sampleCount));
            int y = (int)(height * ((sample + 1f) / 2f));

            int address = y * width + x;//get the 1d array address of the pixel

            data[address] = waveColor;

            if (i % 100000 == 0)
            {
                newTexture.SetPixels(data);
                newTexture.Apply();
                yield return new WaitForEndOfFrame();
            }
        }

        newTexture.SetPixels(data);//put the buffer into the texture

        newTexture.Apply();

        Debug.Log("Waveform sprite created!");
    }

    private RectTransform rectTransform;


    [SerializeField]
    private float pixelsPerUnit = 5;//how many pixels per unit of space to use

    [SerializeField]
    private Color backgroundColor = new Color(0, 0, 0, 0);

    [SerializeField]
    private Color waveColor = Color.blue;

    [SerializeField]
    private RectTransform scrollLocation;
}
