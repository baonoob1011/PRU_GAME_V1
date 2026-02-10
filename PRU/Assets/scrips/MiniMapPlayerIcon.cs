using UnityEngine;

public class MiniMapPlayerIcon : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null) return;

        // xoay icon theo hướng player
        transform.localRotation = Quaternion.Euler(
            0,
            0,
            -player.eulerAngles.y
        );
    }
}
