using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    public CameraController cameraController;

    void OnEnable()
    {
        Time.timeScale = 0f;              // DỪNG GAME
        cameraController.canLook = false; // KHÓA CAMERA
        cameraController.UnlockCursor();  // MỞ CHUỘT
    }

    void OnDisable()
    {
        Time.timeScale = 1f;              // CHẠY GAME
        cameraController.canLook = true;  // MỞ CAMERA
        cameraController.LockCursor();    // KHÓA CHUỘT
    }
}
