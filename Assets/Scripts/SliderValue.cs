using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateValueText(slider.value);
        slider.onValueChanged.AddListener(UpdateValueText);
    }

    // Update is called once per frame
    void UpdateValueText(float value)
    {
        valueText.text = Mathf.RoundToInt(value).ToString();
    }
}
