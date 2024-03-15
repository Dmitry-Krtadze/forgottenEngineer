using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ����, �� ������� ����� ��������� ������
    public float smoothSpeed = 0.125f; // �������� ���������� ������
    public Vector3 offset; // ������ ������ �� ����
    public Vector2 freeMoveWindow = new Vector2(5f, 3f); // ������ ���� ���������� ���� ������

    void LateUpdate()
    {
        // ���������, ��������� �� �������� � �������� ���� ���������� ����
        if (!IsCharacterInFreeMoveWindow())
        {
            // ��������� �������, � ������� ������ ��������� ������ � ������ ���� ���������� ����
            float targetX = Mathf.Clamp(target.position.x + offset.x, transform.position.x - freeMoveWindow.x / 2, transform.position.x + freeMoveWindow.x / 2);
            float targetY = Mathf.Clamp(target.position.y  + offset.y, transform.position.y - freeMoveWindow.y / 2, transform.position.y + freeMoveWindow.y / 2);
            Vector3 desiredPosition = new Vector3(targetX, targetY, transform.position.z);

            // ������������� ������� ������� ������ � �������� �������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ������������� ������� ������
            transform.position = smoothedPosition;
        }
    }

    bool IsCharacterInFreeMoveWindow()
    {
        // ���������, ��������� �� �������� � �������� ���� ���������� ����
        return (target.position.x >= transform.position.x - freeMoveWindow.x / 2 && target.position.x <= transform.position.x + freeMoveWindow.x / 2 &&
                target.position.y >= transform.position.y - freeMoveWindow.y / 2 && target.position.y <= transform.position.y + freeMoveWindow.y / 2);
    }

    // ������������ ���� ���������� ���� �� �����
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position;
        Gizmos.DrawWireCube(center, new Vector3(freeMoveWindow.x, freeMoveWindow.y, 0));
    }
}
