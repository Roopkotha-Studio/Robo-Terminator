using UnityEngine;

public class TouchMedkit : MonoBehaviour
{
    [SerializeField] private long heal = 15;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "first-aid-kit.png", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() && !other.GetComponent<PlayerController>().isHealthFull)
        {
            other.GetComponent<PlayerController>().addHealth(heal, true, false);
            other.GetComponent<PlayerController>().showMessage("You got " + heal + " Health!");
            Destroy(gameObject);
        }
    }
}