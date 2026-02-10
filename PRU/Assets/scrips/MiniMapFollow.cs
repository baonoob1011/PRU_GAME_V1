using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 50f; // độ cao minimap

    void LateUpdate()
    {
        if (player == null) return;

        // Theo X, Z của player – giữ Y cố định
        transform.position = new Vector3(
            player.position.x,
            height,
            player.position.z
        );
    }
}
