using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
