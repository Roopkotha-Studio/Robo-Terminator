using UnityEngine;

public class AmmoGiver : MonoBehaviour
{
    [SerializeField] private int ammo = 30;
    [SerializeField] private int weapon = 0;
    [SerializeField] private GameObject sound = null;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "bullets.png", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController && playerController.weapons[weapon].GetComponent<PlayerGun>().ammo < playerController.weapons[weapon].GetComponent<PlayerGun>().maxAmmo)
            {
                playerController.weapons[weapon].GetComponent<PlayerGun>().ammo += ammo;
                if (weapon == 0) //Pistol
                {
                    playerController.showMessage(ammo + " Bullets for Pistol!");
                } else if (weapon == 1) //Assault Rifle
                {
                    playerController.showMessage(ammo + " Bullets for Assault Rifle!");
                } else if (weapon == 2) //Shotgun
                {
                    playerController.showMessage(ammo + " Shells for Shotgun!");
                }
                if (sound) Instantiate(sound, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}