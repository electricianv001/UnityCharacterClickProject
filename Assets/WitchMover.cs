using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WitchMover : MonoBehaviour
{
    [Tooltip("Точка, к которой должна идти ведьма (JumpZone).")]
    public Transform target;

    [Tooltip("Скорость движения ведьмы (м/с).")]
    public float moveSpeed = 3f;

    // Минимальное расстояние, на котором мы считаем, что ведьма "дошла" до цели
    [Tooltip("Минимальное расстояние до цели, чтобы остановиться.")]
    public float stopDistance = 1.5f;

    private Rigidbody rb;

    private void Awake()
    {
        // Получаем Rigidbody на ведьме. Он нужен, чтобы система физики правильно отрабатывала OnTrigger.
        rb = GetComponent<Rigidbody>();
        
        // Обратите внимание: делаем Rigidbody кинематическим, 
        // чтобы на ведьму не действовала гравитация, а движение шло только по коду.
        rb.isKinematic = true;
    }

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError($"{name}: В поле target не назначен Transform JumpZone! Перетащите объект JumpZone в это поле.");
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        // Считаем текущее расстояние до цели по горизонтали (XZ-плоскости).
        Vector3 toTarget = target.position - transform.position;
        float distance = toTarget.magnitude;

        // Если мы ещё не в пределах stopDistance, двигаемся дальше
        if (distance > stopDistance)
        {
            // Нормализуем направление
            Vector3 direction = toTarget.normalized;

            // Новый вектор скорости
            Vector3 velocity = direction * moveSpeed;

            // Поскольку Rigidbody кинематический, двигаем через MovePosition
            rb.MovePosition(transform.position + velocity * Time.deltaTime);

            // Поворачиваем ведьму «лицом» по направлению движения
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
        else
        {
            // Когда мы внутри stopDistance, останавливаем движение.
            // Можно добавить сюда какую-то анимацию или лог, если требуется.
        }
    }
}
