using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextWord;
    [SerializeField] private Button previousWord;
    [SerializeField] private Button holdPlayButton;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField WordIndexInputField;

    public static Action OnPlayButtonPressed;
    public static Action OnPauseButtonPressed; 
    public static Action<int> OnCahngeWordButtonPressed;
    public static Action<float> OnSliderEdited;
    public static Action<int> OnWordIndexInputFieldEdited;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButton);
        nextWord.onClick.AddListener(OnNextWord);
        previousWord.onClick.AddListener(OnPreviousWord);
        
        slider.onValueChanged.AddListener(delegate { OnSlider(); });

        WordIndexInputField.onEndEdit.AddListener(delegate { OnWordIndexInputField(); });
    }

    public void  OnHoldPlayButtonDown()
    {
        OnPlayButton();
    }

    public void OnHoldPlayButtonUp()
    {
        OnPauseButton();
    }

    private void OnPlayButton()
    {
        OnPlayButtonPressed?.Invoke();
    }

    private void OnPauseButton()
    {
        OnPauseButtonPressed?.Invoke();
    }

    private void OnNextWord()
    {
        OnCahngeWordButtonPressed?.Invoke(1);
    }

    private void OnPreviousWord()
    {
        OnCahngeWordButtonPressed?.Invoke(-1);
    }

    private void OnSlider()
    {
        OnSliderEdited?.Invoke(slider.value);
    }

    public void OnWordIndexInputField()
    {
        OnWordIndexInputFieldEdited?.Invoke(int.Parse(WordIndexInputField.text));
    }
}
