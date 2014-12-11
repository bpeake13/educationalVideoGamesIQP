using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ListView : MonoBehaviour 
{
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        scrollTransform = transform.GetChild(0);//get the first child as the scroll transform

        slider.onValueChanged += onSlide;//delegate for when the slider is moved
    }

    private void onSlide(int newValue)
    {

    }

    private RectTransform rectTransform;

    private Transform scrollTransform;//the transform to move as part of the scroll

    [SerializeField]
    private Slider slider;//The slider to use for the scroll
}
