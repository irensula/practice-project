using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EffectsAudioController : MonoBehaviour
{
    [Header("UI Elements")]
    public Image icon;
    public Sprite volumeOn;
    public Sprite volumeOff;
    public Slider slider;
    public TMP_Text valueText;

    private bool isMuted;
    private float lastVolume = 50f;
    
    // load saved state
    void Start()
    {
        float initialVolume = isMuted ? 0f : lastVolume;
        slider.value = initialVolume;
        AudioListener.volume = initialVolume;

        UpdateIcon();
        UpdateValueText(slider.value);

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    // toggle the state
    public void ToggleMute()
    {
        if (isMuted)
        {
            slider.value = lastVolume;
            isMuted = false;
        }
        else
        {
            lastVolume = slider.value > 0f ? slider.value : lastVolume;
            slider.value = 0f;
            isMuted = true;
        }

        ApplyVolume();
    }

    public void OnSliderChanged(float value)
    {
        AudioListener.volume = value;

        if (value > 0f)
            isMuted = false;
        else
            isMuted = true;

        lastVolume = value > 0f ? value : lastVolume;
        
        ApplyVolume();
    }

    // change the state
    public void ApplyVolume()
    {
        AudioListener.volume = slider.value;
        UpdateIcon();
        UpdateValueText(slider.value);

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", lastVolume);
        PlayerPrefs.Save();
    }

    private void UpdateIcon()
    {
        icon.sprite = isMuted || slider.value == 0f ? volumeOff : volumeOn;
    }

    private void UpdateValueText(float value)
    {
        valueText.text = Mathf.RoundToInt(value).ToString();
    }
}

