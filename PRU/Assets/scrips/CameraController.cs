using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1.7f, -4f);

    float x;
    float y;

    public float xSpeed = 5f;
    public float ySpeed = 4f;

    public float yMin = 5f;
    public float yMax = 45f;

    [HideInInspector]
    public bool canLook = true;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = 15f;
        // ❌ KHÔNG LOCK CHUỘT Ở ĐÂY
    }

    void LateUpdate()
    {
        if (!canLook) return;

        x += Input.GetAxis("Mouse X") * xSpeed;
        y -= Input.GetAxis("Mouse Y") * ySpeed;
        y = Mathf.Clamp(y, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.position = rotation * offset + target.position;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
