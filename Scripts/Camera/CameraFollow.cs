using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ÷ель, за которой будет следовать камера
    public float smoothSpeed = 0.125f; // —корость следовани€ камеры
    public Vector3 offset; // ќтступ камеры от цели
    public Vector2 freeMoveWindow = new Vector2(5f, 3f); // –азмер окна свободного хода камеры

    void LateUpdate()
    {
        // ѕровер€ем, находитс€ ли персонаж в пределах окна свободного хода
        if (!IsCharacterInFreeMoveWindow())
        {
            // ¬ычисл€ем позицию, к которой должна двигатьс€ камера с учетом окна свободного хода
            float targetX = Mathf.Clamp(target.position.x + offset.x, transform.position.x - freeMoveWindow.x / 2, transform.position.x + freeMoveWindow.x / 2);
            float targetY = Mathf.Clamp(target.position.y  + offset.y, transform.position.y - freeMoveWindow.y / 2, transform.position.y + freeMoveWindow.y / 2);
            Vector3 desiredPosition = new Vector3(targetX, targetY, transform.position.z);

            // »нтерполируем текущую позицию камеры к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ”станавливаем позицию камеры
            transform.position = smoothedPosition;
        }
    }

    bool IsCharacterInFreeMoveWindow()
    {
        // ѕровер€ем, находитс€ ли персонаж в пределах окна свободного хода
        return (target.position.x >= transform.position.x - freeMoveWindow.x / 2 && target.position.x <= transform.position.x + freeMoveWindow.x / 2 &&
                target.position.y >= transform.position.y - freeMoveWindow.y / 2 && target.position.y <= transform.position.y + freeMoveWindow.y / 2);
    }

    // ќтрисовываем окно свободного хода на сцене
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position;
        Gizmos.DrawWireCube(center, new Vector3(freeMoveWindow.x, freeMoveWindow.y, 0));
    }
}
