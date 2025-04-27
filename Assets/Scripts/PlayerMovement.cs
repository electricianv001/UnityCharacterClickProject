using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Считываем ввод
        float h = Input.GetAxis("Horizontal"); // A/D или ←/→
        float v = Input.GetAxis("Vertical");   // W/S или ↑/↓

        // Создаём вектор движения в плоскости XZ
        Vector3 move = new Vector3(h, 0f, v) * speed;

        // Применяем к Rigidbody
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
