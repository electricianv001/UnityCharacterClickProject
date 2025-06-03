using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    [Tooltip("UI-объект, который надо показать при столкновении")]
    public GameObject popupImage;

    private void Start()
    {
        // сразу скрываем PopupImage
        if (popupImage != null) popupImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // срабатывает только если другой объект имеет Tag "Witch"
        if (other.CompareTag("Witch") && popupImage != null)
        {
            popupImage.SetActive(true);
            Debug.Log("ShowOnTrigger: OnTriggerEnter сработал на объекте: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Witch") && popupImage != null)
        {
            popupImage.SetActive(false);
            Debug.Log("ShowOnTrigger: OnTriggerExit сработал на объекте: " + other.name);
        }
    }
}
