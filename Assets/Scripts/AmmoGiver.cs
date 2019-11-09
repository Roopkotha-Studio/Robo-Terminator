using UnityEngine;

public class AmmoGiver : MonoBehaviour
{
    [SerializeField] private int ammo = 30;
    [SerializeField] private int weapon = 0;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "bullets.png", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController) playerController.weapons[weapon].GetComponent<PlayerGun>().ammo += ammo;
            Destroy(gameObject);
        }
    }
}