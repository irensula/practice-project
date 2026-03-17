using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
public class TMPLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text textComponent;
    public GameObject loginPanel;
    public GameObject registerPanel;
    
    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            textComponent,
            eventData.position,
            eventData.pressEventCamera
        );


        if (linkIndex != -1)
        {
            string linkId = textComponent.textInfo.linkInfo[linkIndex].GetLinkID();
            
            switch (linkId)
            {
                case "register":
                    ShowRegister();
                    break;

                case "login":
                    ShowLogin();
                    break;
            }
        }
    }

    private void ShowRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
}

    private void ShowLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
}
