using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class ClickToDance : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Этот метод срабатывает, когда вы кликаете мышкой по объекту с этим коллайдером:
    void OnMouseDown()
    {
        Debug.Log("Goblin was clicked via OnMouseDown()");
        TriggerDance();
    }

    // Вызывается из OnMouseDown или из CameraFollow:
    public void TriggerDance()
    {
        if (animator == null) return;

        Debug.Log("TriggerDance called");
        animator.SetTrigger("DanceTrig");    // ← здесь важный вызов
    }
}
