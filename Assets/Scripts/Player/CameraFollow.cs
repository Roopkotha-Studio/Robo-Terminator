using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
        if (playerController.health <= 0) enabled = false;
    }
}
