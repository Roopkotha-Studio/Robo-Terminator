using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private long damage = 5;
    [SerializeField] private float time = 1;
    [SerializeField] private bool instakill = false;
    [SerializeField] private bool showBlood = false;

    private float timeTillDamage = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>())
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController && playerController.health > 0)
            {
                if (timeTillDamage < time)
                {
                    timeTillDamage += Time.deltaTime;
                } else
                {
                    timeTillDamage = 0;
                    if (!instakill)
                    {
                        playerController.addHealth(damage, false, showBlood);
                    } else
                    {
                        playerController.addHealth(playerController.maxHealth, false, showBlood);
                    }
                }
            }
        }
    }
}