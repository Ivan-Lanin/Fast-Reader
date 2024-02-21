using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextWord;
    [SerializeField] private Button previousWord;
    [SerializeField] private Slider slider;

    public static Action OnPlayButtonPressed;
    public static Action<float> OnSliderEdited;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButton);
        nextWord.onClick.AddListener(OnNextWord);
        previousWord.onClick.AddListener(OnPreviousWord);
        slider.onValueChanged.AddListener(delegate { OnSlider(); });
    }

    private void OnPlayButton()
    {
        OnPlayButtonPressed?.Invoke();
    }

    private void OnNextWord()
    {
        Debug.Log("Next word button clicked");
    }

    private void OnPreviousWord()
    {
        Debug.Log("Previous word button clicked");
    }

    private void OnSlider()
    {
        OnSliderEdited?.Invoke(slider.value);
    }
}
