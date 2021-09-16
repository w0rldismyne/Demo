using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Evidence : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string description = "";
    private Text descriptionText = null;
    private Image myImage = null;

    public void Setup(Text t, EvidenceSO evidence) {
        myImage = GetComponent<Image>();
        descriptionText = t;
        myImage.sprite = evidence.sprite;
        description = evidence.description;

    }
    public void OnPointerEnter(PointerEventData eventData) => descriptionText.text = description;
    public void OnPointerExit(PointerEventData eventData) => descriptionText.text = "";
}
