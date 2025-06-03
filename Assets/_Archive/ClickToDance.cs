using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class ClickToDance : MonoBehaviour
{
    [Header("Animator Settings")]
    [Tooltip("Animator с контроллером, где есть триггер для танца")]
    public Animator animator;

    [Tooltip("Имя Trigger-параметра в Animator, запускающего танец")]
    public string danceTrigger = "DanceTrig";

    private void Reset()
    {
        // Автоматически подхватим Animator, если он на том же GameObject
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        // Пропустить, если клик попал на UI-элемент
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        // Запускаем танец
        TriggerDance();
    }

    /// <summary>
    /// Запустить анимацию танца программно.
    /// Можно вызывать извне (из другого скрипта).
    /// </summary>
    public void TriggerDance()
    {
        if (animator != null)
        {
            animator.SetTrigger(danceTrigger);
            Debug.Log($"{gameObject.name} started dancing!");
        }
        else
        {
            Debug.LogWarning($"[{nameof(ClickToDance)}] Animator не назначен на {gameObject.name}");
        }
    }
}
