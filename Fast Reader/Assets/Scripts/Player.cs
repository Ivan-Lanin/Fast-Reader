using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text left;
    [SerializeField] private TMP_Text right;
    [SerializeField] private TMP_Text center;

    private GameManager gameManager;
    private FileManger fileManger;
    private List<string> words;
    private int currentWordIndex;
    private bool isPlaying;
    private float speed;

    public static Action<int> OnWordIndexChange;

    private void Awake()
    {
        isPlaying = false;
        Controls.OnPlayButtonPressed += Play;
        Controls.OnPauseButtonPressed += Pause;
        Controls.OnCahngeWordButtonPressed += ChangeWordOnce;
        Controls.OnSliderEdited += SetSpeed;
        gameManager = FindObjectOfType<GameManager>();
        fileManger = FindObjectOfType<FileManger>();
        currentWordIndex = fileManger.GetCurrentWordIndex();
        if (currentWordIndex > 10)
        {
            currentWordIndex -= 10;
        }
    }

    private void OnEnable()
    {
        left.text = "Left";
        right.text = "Right";
        center.text = "Center";
    }

    private void Start()
    {
        BookToWords(gameManager.book);
        speed = 0.2f;
    }

    public void BookToWords(TextAsset book)
    {
        words = new List<string>(book.text.Split(new char[] { ' ', '\n' }));
    }

    public void SetSpeed(float coefficient)
    {
        speed = 0.1f * (1 + coefficient);
    }

    public void Play()
    {
        if (isPlaying == true)
        {
            Pause();
        }
        else
        {
            isPlaying = true;
            if (!IsInvoking("ReadNextWord"))
            {
                StartCoroutine(ReadNextWord());
            }
        }
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void ChangeWordOnce(int step)
    {
        if (currentWordIndex < words.Count - 1)
        {
            center.text = words[currentWordIndex + step];
            if (currentWordIndex > 0)
            {
                left.text = words[currentWordIndex + step - 1];
            }
            else { left.text = ""; }
            if (currentWordIndex < words.Count - 1)
            {
                right.text = words[currentWordIndex + step + 1];
            }
            else { right.text = ""; }
            currentWordIndex += step;
            OnWordIndexChange?.Invoke(currentWordIndex);
        }
        else
        {
            Debug.Log("End of book");
        }
    }

    public IEnumerator ReadNextWord()
    {
        float wordLifeDuration = (words[currentWordIndex].Count() * 0.025f) + speed;
        
        if (wordLifeDuration < speed)
        {
            wordLifeDuration = speed;
        }

        ChangeWordOnce(1);

        yield return new WaitForSeconds(wordLifeDuration);
        if (isPlaying)
        {
            StartCoroutine(ReadNextWord());
        }
    }
}
