// ClickToAnimate.cs
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClickToAnimate : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Это остаётся для клика по модели
    void OnMouseDown()
    {
        TriggerJump();
        Debug.Log("Witch was clicked!");

    }

    // Новый публичный метод для UI-кнопки
    public void TriggerJump()
    {
        if (animator != null)
        {
            Debug.Log("TriggerJump called");
            animator.SetTrigger("JumpTrigger");
        }
    }
}
