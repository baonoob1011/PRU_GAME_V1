using UnityEngine;

public class ButtonOK : MonoBehaviour
{
    public GameObject tutorialPanel;
    public CameraController cameraController;
    public TypewriterText typewriterText;

    public void OnClickOK()
    {
        // ❌ chưa chạy xong chữ thì KHÔNG làm gì
        if (typewriterText != null && !typewriterText.IsFinished)
            return;

        // ✅ chữ chạy xong → cho vào game
        tutorialPanel.SetActive(false);

        if (cameraController != null)
            cameraController.canLook = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }
}
