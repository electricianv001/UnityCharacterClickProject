using UnityEngine;
using UnityEngine.UI;

public class GameUIButtonsController : MonoBehaviour
{
    [Header("Скрипты на ведьме")]
    public MonoBehaviour walkControlScript;           // например, WitchMovementKeyboard
    public MonoBehaviour randomAnimationScript;       // например, RandomAnimationPlayer

    [Header("UI-кнопки")]
    public Button playButton;
    public Button stopButton;

    private bool isPaused = false;
    private Text stopButtonText;

    private void Awake()
    {
        // отключаем скрипты на старте
        SetScriptEnabled(walkControlScript, false);
        SetScriptEnabled(randomAnimationScript, false);

        // обработчики кнопок
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayPressed);

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(OnStopPressed);
            stopButtonText = stopButton.GetComponentInChildren<Text>();
        }

        Time.timeScale = 1f;
    }

    private void OnPlayPressed()
    {
        Debug.Log("[UI] PLAY pressed");

        SetScriptEnabled(walkControlScript, true);
        SetScriptEnabled(randomAnimationScript, true);

        Time.timeScale = 1f;
        isPaused = false;
        UpdateStopButtonText();
    }

    private void OnStopPressed()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        Debug.Log(isPaused ? "[UI] PAUSE enabled" : "[UI] Game resumed");
        UpdateStopButtonText();
    }

    private void UpdateStopButtonText()
    {
        if (stopButtonText != null)
            stopButtonText.text = isPaused ? "RESUME" : "STOP";
    }

    private void SetScriptEnabled(MonoBehaviour script, bool enabled)
    {
        if (script != null)
            script.enabled = enabled;
    }
}
