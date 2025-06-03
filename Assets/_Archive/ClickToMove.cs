using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class ClickToMove : MonoBehaviour {
    public float moveSpeed = 5f;
    private Vector3 _targetPos;
    private bool _isMoving;
    private Animator _anim;

    void Start() {
        _anim = GetComponent<Animator>();
    }

    void Update() {
        if (!_isMoving) return;

        // Двигать к цели
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, step);

        // Как только пришли — остановиться и выключить ходьбу
        if (Vector3.Distance(transform.position, _targetPos) < 0.1f) {
            _isMoving = false;
            _anim.SetBool("isWalking", false);
        }
    }

    // Этот метод сработает, когда пользователь кликнет по коллайдеру гоблина
    void OnMouseDown() {
        // Устанавливаем цель чуть вперёд по локальной оси Z (чтобы вытащить из‐за камня)
        // Можно также вычислять точку по Raycast’у, если нужно «кликнуть куда угодно»
        _targetPos = transform.position + transform.forward * 2f;
        _isMoving = true;

        // Запустить анимацию ходьбы
        _anim.SetBool("isWalking", true);
    }
}
