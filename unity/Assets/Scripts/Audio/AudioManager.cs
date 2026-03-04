using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource effectsSource;
    public AudioSource voiceSource;

    [Header("UI Sounds")]
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float effectsVolume = 0.5f;
    [Range(0f, 1f)]
    public float voiceVolume = 0.5f;

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

        // audio listener
        AudioListener listener = GetComponent<AudioListener>();
        if (listener == null)
            gameObject.AddComponent<AudioListener>();

        if (effectsSource == null) 
            effectsSource = gameObject.AddComponent<AudioSource>();
        if (voiceSource == null) 
            voiceSource = gameObject.AddComponent<AudioSource>();

        effectsSource.playOnAwake = false;
        voiceSource.playOnAwake = false;

        effectsSource.spatialBlend = 0;
        voiceSource.spatialBlend = 0;

        ApplySavedVolumes();
    }

    public void PlayClick()
    {
        if (clickSound != null && effectsSource != null)
            effectsSource.PlayOneShot(clickSound, effectsVolume);
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip != null && voiceSource != null)
        {
            voiceSource.clip = clip;
            voiceSource.Play();
        }
    }

    public void RegisterButton(Button btn)
    {
        if (btn == null || registeredButtons.Contains(btn))
            return;

        btn.onClick.AddListener(PlayClick);
        registeredButtons.Add(btn);
    }

    public void SetEffectsVolume(float value)
    {
        effectsVolume = value;
        if (effectsSource != null) effectsSource.volume = effectsVolume;
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
    }

    public void SetVoiceVolume(float value)
    {
        voiceVolume = value;
        if (voiceSource != null) voiceSource.volume = voiceVolume;
        PlayerPrefs.SetFloat("VoiceVolume", voiceVolume);
    }

    private void ApplySavedVolumes()
    {
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", effectsVolume);
        voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", voiceVolume);

        if (effectsSource != null) effectsSource.volume = effectsVolume;
        if (voiceSource != null) voiceSource.volume = voiceVolume;
    }
}
