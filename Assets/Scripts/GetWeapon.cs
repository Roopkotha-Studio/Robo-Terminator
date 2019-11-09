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
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() && other.GetComponent<PlayerGun>())
        {
            /*
            if (CompareTag("Pistol"))
            {
                if (!other.GetComponent<PlayerGun>().hasPistol)
                {
                    other.GetComponent<PlayerGun>().hasPistol = true;
                    other.GetComponent<PlayerController>().showMessage("You got the Pistol!");
                } else
                {
                    other.GetComponent<PlayerGun>().pistol.ammo += 10;
                    other.GetComponent<PlayerController>().showMessage("You got 10 Bullets!");
                }
            } else if (CompareTag("AssaultRifle"))
            {
                if (!other.GetComponent<PlayerGun>().hasAssaultRifle)
                {
                    other.GetComponent<PlayerGun>().hasAssaultRifle = true;
                    other.GetComponent<PlayerController>().showMessage("You got the Assault Rifle!");
                } else
                {
                    other.GetComponent<PlayerGun>().assaultRifle.ammo += 30;
                    other.GetComponent<PlayerController>().showMessage("You got 30 Bullets!");
                }
            } else if (CompareTag("BurstRifle"))
            {
                if (!other.GetComponent<PlayerGun>().hasBurstRifle)
                {
                    other.GetComponent<PlayerGun>().hasBurstRifle = true;
                    other.GetComponent<PlayerController>().showMessage("You got the Burst Rifle!");
                } else
                {
                    other.GetComponent<PlayerGun>().burstRifle.ammo += 30;
                    other.GetComponent<PlayerController>().showMessage("You got 30 Bullets!");
                }
            } else if (CompareTag("Shotgun"))
            {
                if (!other.GetComponent<PlayerGun>().hasShotgun)
                {
                    other.GetComponent<PlayerGun>().hasShotgun = true;
                    other.GetComponent<PlayerController>().showMessage("You got the Shotgun!");
                } else
                {
                    other.GetComponent<PlayerGun>().shotgun.ammo += 6;
                    other.GetComponent<PlayerController>().showMessage("You got 6 Shells!");
                }
            } else
            {
                valid = false;
                Debug.LogError("Invalid weapon type");
            }
            */
            if (valid) Destroy(gameObject);
        }
    }
}
