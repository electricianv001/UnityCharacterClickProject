using System.Collections;
using UnityEngine;

public class RandomAnimationPlayer : MonoBehaviour
{
    [Tooltip("Animator с нужными триггерами")]
    public Animator animator;
    [Tooltip("Имена Trigger-параметров в Animator")]
    public string[] triggerNames;
    [Tooltip("Минимальный интервал между анимациями, сек")]
    public float minInterval = 2f;
    [Tooltip("Максимальный интервал между анимациями, сек")]
    public float maxInterval = 5f;

    private void Start()
    {
        // Если ничего не назначено — сразу деактивируем себя, чтобы не было NRE
        if (animator == null || triggerNames == null || triggerNames.Length == 0)
        {
            Debug.LogWarning($"[RandomAnimationPlayer] Отключаюсь, т.к. " +
                             $"{(animator == null ? "animator не задан" : "")} " +
                             $"{(triggerNames == null || triggerNames.Length == 0 ? "triggerNames пуст" : "")}");
            enabled = false;
            return;
        }
        StartCoroutine(PlayRandom());
    }

    private IEnumerator PlayRandom()
    {
        while (true)
        {
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);

            // На всякий случай проверяем, всё ли ещё заповнено
            if (triggerNames.Length == 0) yield break;

            int idx = Random.Range(0, triggerNames.Length);
            animator.SetTrigger(triggerNames[idx]);
        }
    }
}
