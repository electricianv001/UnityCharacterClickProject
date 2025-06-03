using UnityEngine;
using TMPro;  // если вы используете TextMeshPro для вывода текста

public class ShowInfo : MonoBehaviour
{
    [TextArea] public string descriptionText;   // Текст, который будет показан в UI
    public GameObject infoPanel;               // Ссылка на сам InfoPanel (в Canvas)
    public TMP_Text infoPanelText;             // Ссылка на компонент TextMeshPro внутри панели

    private void Start()
    {
        // Сразу, при старте, прячем InfoPanel
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log(">> ShowInfo: OnMouseDown сработал на объекте: " + gameObject.name);

        if (infoPanel != null && infoPanelText != null)
        {
            infoPanelText.text = descriptionText;
            infoPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ShowInfo: отсутствуют ссылки на infoPanel или infoPanelText!");
        }
    }
}
