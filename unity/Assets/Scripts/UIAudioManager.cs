using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("UI Sounds")]
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    private AudioSource audioSource;

    private HashSet<Button> registeredButtons = new HashSet<Button>();

    void Awake()
    {
        // singlton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void RegisterButtonsInScene()
    {
        // find all buttons in the scene
        Button[] buttons = FindObjectsByType<Button>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Button btn in buttons)
        {
            if (registeredButtons.Contains(btn))
                continue;

            btn.onClick.AddListener(PlayClick);
            registeredButtons.Add(btn);
        }
    }

    void Start()
    {
        RegisterButtonsInScene();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;     
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterButtonsInScene();
    }

    public void PlayClick()
    {
        if (clickSound != null && audioSource != null)
            audioSource.PlayOneShot(clickSound, volume);
    }

    public void RegisterButton(Button btn)
    {
        if (btn == null)
            return;

        if(registeredButtons.Contains(btn))
            return;

        btn.onClick.AddListener(PlayClick);
        registeredButtons.Add(btn);
    }
}
