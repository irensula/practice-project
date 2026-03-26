using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider effectsSlider;
    public Slider voiceSlider;

    public TMP_Text txtEffectsValue;
    public TMP_Text txtVoiceValue;

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
        effectsSlider.minValue = 0;
        effectsSlider.maxValue = 100;
        voiceSlider.minValue = 0;
        voiceSlider.maxValue = 100;

        // init sliders
        effectsSlider.value = audio.effectsVolume;
        voiceSlider.value = audio.voiceVolume;

        // set texts of volume values 
        txtEffectsValue.text = Mathf.RoundToInt(effectsSlider.value).ToString();
        txtVoiceValue.text = Mathf.RoundToInt(voiceSlider.value).ToString();

        // listen to sliders' changes
        effectsSlider.onValueChanged.AddListener(audio.SetEffectsVolume);
        voiceSlider.onValueChanged.AddListener(audio.SetVoiceVolume);

        effectsSlider.onValueChanged.AddListener(OnEffectsSliderChanged);
        voiceSlider.onValueChanged.AddListener(OnVoiceSliderChanged);

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

    // show changes of effects slider
    private void OnEffectsSliderChanged(float value)
    {
        txtEffectsValue.text = Mathf.RoundToInt(value).ToString();
    }

    // show changes of voice slider
    private void OnVoiceSliderChanged(float value)
    {
        txtVoiceValue.text = Mathf.RoundToInt(value).ToString();
    }

}
