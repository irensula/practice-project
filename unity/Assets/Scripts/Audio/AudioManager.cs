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
    public float effectsVolume = 50f;
    [Range(0f, 1f)]
    public float voiceVolume = 50f;

    private bool effectsMuted;
    private bool voiceMuted;

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
        {
            effectsSource.clip = clickSound;
            effectsSource.Play();
        }
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip != null && voiceSource != null)
        {
            effectsSource.volume = effectsMuted ? 0f : effectsVolume;
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
        effectsVolume = Mathf.Clamp(value, 0f, 100f);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        UpdateAudioSources();
    }

    public void SetVoiceVolume(float value)
    {
        voiceVolume = value;
        PlayerPrefs.SetFloat("VoiceVolume", voiceVolume);
        UpdateAudioSources();
    }

    private void ApplySavedVolumes()
    {
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", effectsVolume);
        voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", voiceVolume);

        effectsVolume = Mathf.Clamp(effectsVolume, 0f, 100f);
        voiceVolume = Mathf.Clamp(voiceVolume, 0f, 100f);

        effectsMuted = PlayerPrefs.GetInt("EffectsMuted", 0) == 1;
        voiceMuted = PlayerPrefs.GetInt("VoiceMuted", 0) == 1;

        UpdateAudioSources();
    }

    public void ToggleEffectsMute()
    {
        effectsMuted = !effectsMuted;
        PlayerPrefs.SetInt("EffectsMuted", effectsMuted ? 1 : 0);
        UpdateAudioSources();
    }

    public void ToggleVoiceMute()
    {
        voiceMuted = !voiceMuted;
        PlayerPrefs.SetInt("VoiceMuted", voiceMuted ? 1 : 0);
        UpdateAudioSources();
    }

    private void UpdateAudioSources()
    {
        if (effectsSource != null)
            effectsSource.volume = effectsMuted ? 0f : effectsVolume / 100f;

        if (voiceSource != null)
            voiceSource.volume = voiceMuted ? 0f : voiceVolume / 100f;
    }
}
