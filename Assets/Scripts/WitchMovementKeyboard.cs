// WitchMovementKeyboard.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class WitchMovementKeyboard : MonoBehaviour
{
    public float walkSpeed = 4f; 
    public float runSpeed = 8f;  
    public float jumpHeight = 1.2f;
    public float gravity = 9.8f; 
    public float turnSpeed = 180f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;
    private bool isTurningAround = false; 

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!this.enabled) return;

        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical"); // Это значение для расчета силы движения вперед/назад

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Логика разворота при движении назад (S или стрелка вниз)
        if (verticalInput < -0.1f) 
        {
            if (!isTurningAround) 
            {
                transform.Rotate(0f, 180f, 0f);
                isTurningAround = true; 
            }
            // После разворота, verticalInput для расчета forwardMove должен стать положительным
            verticalInput = Mathf.Abs(verticalInput); 
        }
        else
        {
            isTurningAround = false; 
        }

        // Поворот по горизонтали
        transform.Rotate(0f, horizontalInput * turnSpeed * Time.deltaTime * -1f, 0f);
        
        // Рассчитываем вектор движения вперед/назад
        // verticalInput здесь будет положительным для W или для S (после разворота)
        Vector3 forwardMove = transform.forward * verticalInput * currentSpeed;

        // Логика прыжка
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime; // Прижимаем к земле

            bool jumpKeyDown = Input.GetButtonDown("Jump"); // Стандартная кнопка прыжка (Пробел)
            bool wJustPressed = Input.GetKeyDown(KeyCode.W);    // Проверяем, была ли только что нажата W

            // Прыгаем, если нажата кнопка "Jump" ИЛИ если только что была нажата 'W' 
            // (и мы не в процессе разворота после нажатия 'S')
            if (jumpKeyDown || (wJustPressed && !isTurningAround)) 
            {
                // Убедимся, что не прыгаем постоянно, если W удерживается,
                // GetKeyDown уже решает эту проблему для W.
                // animator.GetBool("IsJumping") может быть полезен, если анимация прыжка длинная.
                
                Debug.Log("Jump initiated by W or Jump button!"); 
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }
        else
        {
            // Применяем гравитацию, если в воздухе
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        // Собираем итоговый вектор движения:
        // forwardMove отвечает за горизонтальное перемещение (вперед/назад по XZ)
        // verticalVelocity отвечает за вертикальное перемещение (прыжок/падение по Y)
        moveDirection = new Vector3(forwardMove.x, verticalVelocity, forwardMove.z);
        
        // Применяем движение
        controller.Move(moveDirection * Time.deltaTime);

        // Анимация ходьбы
        // Используем исходное значение verticalInput до его возможной инверсии для разворота,
        // чтобы анимация корректно отражала нажатие W или S.
        // Или, если isTurningAround, то verticalInput уже Abs, так что все ок.
        bool isMoving = Mathf.Abs(verticalInput) > 0.1f; 
        animator.SetBool("IsWalking", isMoving);
    }
}
