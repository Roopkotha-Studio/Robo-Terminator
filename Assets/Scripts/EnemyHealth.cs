using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private long health = 100;
    public Transform bloodPoint;

    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void takeDamage(long damage)
    {
        if (damage > 0)
        {
            health -= damage;
        } else
        {
            --health;
        }
    }
}
