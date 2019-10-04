using UnityEngine;

public class AmmoGiver : MonoBehaviour
{
    [SerializeField] private int ammo = 30;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "bullets.png", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() && other.GetComponent<PlayerGun>())
        {
            if (CompareTag("Pistol"))
            {
                other.GetComponent<PlayerGun>().pistol.ammo += ammo;
                other.GetComponent<PlayerController>().showMessage("You got " + ammo + " Bullets!");
            } else if (CompareTag("AssaultRifle"))
            {
                other.GetComponent<PlayerGun>().assaultRifle.ammo += ammo;
                other.GetComponent<PlayerController>().showMessage("You got " + ammo + " Bullets!");
            } else if (CompareTag("Shotgun"))
            {
                other.GetComponent<PlayerGun>().shotgun.ammo += ammo;
                other.GetComponent<PlayerController>().showMessage("You got " + ammo + " Shells!");
            } else
            {
                Debug.LogError("Invalid weapon type");
            }
            Destroy(gameObject);
        }
    }
}