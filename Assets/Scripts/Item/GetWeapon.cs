using UnityEngine;

public class GetWeapon : MonoBehaviour
{
    private bool valid = true;

    void OnDrawGizmos()
    {
        if (CompareTag("Pistol"))
        {
            Gizmos.DrawIcon(transform.position, "gun.png", false);
        } else if (CompareTag("AssaultRifle"))
        {
            Gizmos.DrawIcon(transform.position, "assault-rifle.png", false);
        } else if (CompareTag("BurstRifle"))
        {
            Gizmos.DrawIcon(transform.position, "assault-rifle.png", false);
        } else if (CompareTag("Shotgun"))
        {
            Gizmos.DrawIcon(transform.position, "shotgun.png", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>())
        {
            if (CompareTag("Pistol"))
            {
                if (!other.GetComponent<PlayerController>().hasPistol)
                {
                    other.GetComponent<PlayerController>().hasPistol = true;
                    other.GetComponent<PlayerController>().showMessage("Pistol!");
                } else
                {
                    other.GetComponent<PlayerController>().weapons[0].GetComponent<PlayerGun>().ammo += 12;
                    other.GetComponent<PlayerController>().showMessage("12 Bullets for Pistol!");
                }
            } else if (CompareTag("AssaultRifle"))
            {
                if (!other.GetComponent<PlayerController>().hasAssaultRifle)
                {
                    other.GetComponent<PlayerController>().hasAssaultRifle = true;
                    other.GetComponent<PlayerController>().showMessage("Assault Rifle!");
                } else
                {
                    other.GetComponent<PlayerController>().weapons[1].GetComponent<PlayerGun>().ammo += 30;
                    other.GetComponent<PlayerController>().showMessage("30 Bullets for Assault Rifle!");
                }
            } else if (CompareTag("Shotgun"))
            {
                if (!other.GetComponent<PlayerController>().hasShotgun)
                {
                    other.GetComponent<PlayerController>().hasShotgun = true;
                    other.GetComponent<PlayerController>().showMessage("Shotgun!");
                } else
                {
                    other.GetComponent<PlayerController>().weapons[2].GetComponent<PlayerGun>().ammo += 8;
                    other.GetComponent<PlayerController>().showMessage("8 Shells for Shotgun!");
                }
            } else
            {
                valid = false;
                Debug.LogError("Invalid weapon type");
            }
            if (valid) Destroy(gameObject);
        }
    }
}
