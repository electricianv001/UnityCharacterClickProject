using UnityEngine;
using UnityEngine.UI;

public class ObstacleTrigger : MonoBehaviour
{
    [Tooltip("Сюда перетащите вашу UI-Image из Canvas")]
    public GameObject popupImage;

    private void OnTriggerEnter(Collider other)
    {
        // проверяем, что входим именно в наш JumpZone
        if (other.CompareTag("Obstacle"))
        {
            popupImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // и скрываем, когда уходим из зоны
        if (other.CompareTag("Obstacle"))
        {
            popupImage.SetActive(false);
        }
    }
}
