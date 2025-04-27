using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Header("Какие слои кликабельны?")]
    public LayerMask clickableLayers;   // вот это — LayerMask, в инспекторе станет мульти-чекбоксом

    private Camera _cam;

    void Awake()
    {
        // находим камеру, к которой прикреплен скрипт, либо main
        _cam = GetComponent<Camera>() ?? Camera.main;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, clickableLayers))
        {
            // 1) сначала пробуем гоблина
            if (hit.collider.TryGetComponent<ClickToDance>(out var goblin))
            {
                goblin.TriggerDance();
                Debug.Log("Goblin triggered from CameraFollow");
                return;
            }

            // 2) иначе — ведьму
            if (hit.collider.TryGetComponent<ClickToAnimate>(out var witch))
            {
                witch.TriggerJump();
                Debug.Log("Witch triggered from CameraFollow");
            }
        }
    }
}
