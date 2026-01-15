using UnityEngine;
using UnityEngine.UI;

// changes layout group according to the screen width
public class ResponsiveButtonLayout : MonoBehaviour
{
    public GameObject horizontalContainer;
    public GameObject verticalContainer;
    public float mobileWidthThreshold = 800f;

    void Start()
    {
        UpdateLayout(Screen.width);
    }

    void Update()
    {
        UpdateLayout(Screen.width);
    }

    void UpdateLayout(float screenWidth)
    {
        if (screenWidth <= mobileWidthThreshold)
        {
            horizontalContainer.SetActive(false);
            verticalContainer.SetActive(true);
        }
        else
        {
            horizontalContainer.SetActive(true);
            verticalContainer.SetActive(false);
        }
    }
}
