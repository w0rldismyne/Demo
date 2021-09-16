using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Memory : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private MemorySO myMemory;
    private Image myImage = null;
    private Image fullscreenImage = null;

    private GameObject fullscreenObject = null;

    public void Setup(GameObject obj, Image img, MemorySO memory) {
        myMemory = memory;

        myImage = GetComponent<Image>();
        myImage.sprite = myMemory.sprite;

        fullscreenImage = img;
        fullscreenObject = obj;
    }

    // Required for OnPointerUp to work
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) {
        fullscreenImage.sprite = myMemory.eventSprite;
        fullscreenObject.SetActive(true);
    }
}
