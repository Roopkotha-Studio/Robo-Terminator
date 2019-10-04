using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private long damage = 5;
    [SerializeField] private float time = 1;
    [SerializeField] private bool showBlood = false;

    private float timeTillDamage = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>())
        {
            if (timeTillDamage < time)
            {
                timeTillDamage += Time.deltaTime;
            } else
            {
                timeTillDamage = 0;
                other.GetComponent<PlayerController>().addHealth(damage, false, showBlood);
            }
        }
    }
}