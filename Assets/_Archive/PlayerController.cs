
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour

{
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        if (dir.magnitude > 0.1f)
        {
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
