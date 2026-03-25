using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider effectsSlider;
    public Slider voiceSlider;

    public Button effectsMuteButton;
    public Button voiceMuteButton;

    public Sprite muteIcon;
    public Sprite unmuteIcon;

    public Image effectsMuteIcon;
    public Image voiceMuteIcon;

    void Start()
    {
        var audio = AudioManager.Instance;

        // init sliders
        effectsSlider.value = audio.effectsVolume;
        voiceSlider.value = audio.voiceVolume;

        effectsSlider.onValueChanged.AddListener(audio.SetEffectsVolume);
        voiceSlider.onValueChanged.AddListener(audio.SetVoiceVolume);

        // mute buttons
        effectsMuteButton.onClick.AddListener(() =>
        {
            audio.ToggleEffectsMute();
            UpdateUI();
        });

        voiceMuteButton.onClick.AddListener(() =>
        {
            audio.ToggleVoiceMute();
            UpdateUI();
        });

        UpdateUI();
    }

    void UpdateUI()
    {
        var audio = AudioManager.Instance;

        bool effectsMuted = PlayerPrefs.GetInt("EffectsMuted", 0) == 1;
        bool voiceMuted = PlayerPrefs.GetInt("VoiceMuted", 0) == 1;

        effectsMuteIcon.sprite = effectsMuted ? muteIcon : unmuteIcon;
        voiceMuteIcon.sprite = voiceMuted ? muteIcon : unmuteIcon;

    }

    public void OnSliderChanged(float value)
    {
        AudioManager.Instance.SetEffectsVolume(value);
        Debug.Log("Slider: volume was changed");
    }

}
