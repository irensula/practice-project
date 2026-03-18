using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
public class WordItem : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private VocabularyUI gameManager;
    private TMP_Text text;
    private Image background;
    private Color normalColor = new Color32(222, 226, 255, 255);
    private Color selectedColor = new Color32(131, 106, 234, 255);
    private bool isMatched = false;

    [Header("Audio")]
    public string audioUrl;
    public AudioClip wordAudio;
    private AudioSource audioSource;

    void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        background = GetComponent<Image>();

        // AudioSource initialization
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // void Start()
    // {
    //     if (UIAudioManager.Instance != null && UIAudioManager.Instance.voiceSource != null)
    //     {
    //         audioSource.volume = UIAudioManager.Instance.voiceSource.volume;
    //     }
    // }

    public void Setup(int itemId, string word, VocabularyUI manager, string audioUrl = null)
    {
        id = itemId;
        text.text = word;
        gameManager = manager;

        if (!string.IsNullOrEmpty(audioUrl))
        {
            this.audioUrl = audioUrl;
            StartCoroutine(LoadAudio(audioUrl));
        }
    }

    IEnumerator LoadAudio(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                wordAudio = DownloadHandlerAudioClip.GetContent(www);
            }
            else
            {
                Debug.LogError("Failed to load audio: " + www.error);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMatched) return;

        gameManager.SelectWord(this);

        // play audio on click
        if (wordAudio != null)
        {
            audioSource.Stop();
            audioSource.clip = wordAudio;
            audioSource.Play();
        }
    }

    public void SetMatched()
    {
        isMatched = true;
        text.color = new Color32(222, 226, 255, 255);
    } 

    public void SetSelected(bool value)
    {
        background.color = value ? selectedColor : normalColor;
    }

    public bool IsMatched()
    {
        return isMatched;
    }

    public void ResetItem()
    {
        isMatched = false;
        background.color = normalColor;
        text.color = Color.white;
    }
}
