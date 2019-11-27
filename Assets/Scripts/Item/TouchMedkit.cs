using UnityEngine;

public class TouchMedkit : MonoBehaviour
{
    [SerializeField] private long heal = 15;
    [SerializeField] private GameObject sound = null;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "first-aid-kit.png", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController && playerController.health < playerController.maxHealth)
            {
                other.GetComponent<PlayerController>().addHealth(heal, true, false);
                other.GetComponent<PlayerController>().showMessage("First Aid Kit!");
                if (sound) Instantiate(sound, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}