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

    [Range(0f, 100f)]
    public float effectsVolume = 50f;
    [Range(0f, 100f)]
    public float voiceVolume = 50f;

    private bool effectsMuted;
    private bool voiceMuted;

    private HashSet<Button> registeredButtons = new HashSet<Button>();

    [Header("Effects Sounds")] 
    public AudioClip clickSound; 
    public AudioClip correctSound; 
    public AudioClip wrongSound; 
    public AudioClip winSound; 
    public enum EffectType { Click, Correct, Wrong, Win }
    public enum LanguageType { En, Fi }

    private Dictionary<string, AudioClip> wordCache = new Dictionary<string, AudioClip>();

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

    public void PlayEffect(EffectType type)
    {
        if (effectsSource == null) return;

        AudioClip clip = null;

        switch (type)
        {
            case EffectType.Click:
                clip = clickSound;
                break;

            case EffectType.Correct:
                clip = correctSound;
                break;
            
            case EffectType.Wrong:
                clip = wrongSound;
                break;
            
            case EffectType.Win:
                clip = winSound;
                break;
        }

        if (clip != null)
        {
            effectsSource.volume = effectsMuted ? 0f : effectsVolume / 100f;
            effectsSource.clip = clip;
            effectsSource.Play();
        }
    }

    public void PlayClick()
    {
        PlayEffect(EffectType.Click);
    }

    public void RegisterButton(Button btn)
    {
        if (btn == null || registeredButtons.Contains(btn))
            return;

        btn.onClick.AddListener(PlayClick);
        registeredButtons.Add(btn);
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip != null && voiceSource != null)
        {
            voiceSource.volume = voiceMuted ? 0f : voiceVolume / 100f;
            voiceSource.clip = clip;
            voiceSource.Play();
        }
    }

    public void PlayWord(string word, LanguageType lang)
    {
        if (string.IsNullOrEmpty(word)) return;

        string key = $"{lang}_{word}";

        if (!wordCache.TryGetValue(key, out AudioClip clip))
        {
            string path = $"Sounds/{lang.ToString().ToLower()}/{word}";
            clip = Resources.Load<AudioClip>(path);

            if (clip == null)
            {
                Debug.LogWarning($"Word audio not found: {path}");
                return;
            }
            else
            {
                Debug.Log("AudioClip FOUND: " + path);
            }
            wordCache[key] = clip;
        }
        PlayVoice(clip);
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
