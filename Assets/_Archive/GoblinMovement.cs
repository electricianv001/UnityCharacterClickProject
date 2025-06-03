using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GoblinMovement : MonoBehaviour
{
    [Header("Настройки движения")]
    [Tooltip("Скорость ходьбы")]
    public float walkSpeed = 3f;
    [Tooltip("Скорость поворота")]
    public float turnSpeed = 180f;

    private CharacterController cc;

    void Awake()
    {
        // Захватываем ссылку на CharacterController
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Читаем оси WASD / стрелки
        float h = Input.GetAxis("Horizontal"); // A/D или ←/→
        float v = Input.GetAxis("Vertical");   // W/S или ↑/↓

        Vector3 dir = new Vector3(h, 0f, v);

        // Если есть ввод, поворачиваем модель в направлении движения
        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }

        // Двигаем CharacterController вперёд на walkSpeed
        Vector3 move = transform.forward * walkSpeed * Time.deltaTime;
        cc.Move(move);
    }
}
