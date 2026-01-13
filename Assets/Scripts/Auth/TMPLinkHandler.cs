using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class TMPLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text linkRegistration;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            linkRegistration,
            eventData.position,
            null
        );

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = linkRegistration.textInfo.linkInfo[linkIndex];
            string linkId = linkInfo.GetLinkID();

            if (linkId == "register")
            {
                SceneManager.LoadScene("RegistrationScene");
            }
        }
    }
}
