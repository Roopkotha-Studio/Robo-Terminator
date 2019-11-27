using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private long maxHealth = 100;
    [SerializeField] private bool useDeathAnim = true;
    [Tooltip("Only used if useDeathAnim is set to false.")] [SerializeField] private GameObject sound = null;
    public Transform bloodPoint;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound = null;
    [SerializeField] private AudioClip deathSound = null;

    private new Collider collider;
    private AudioSource audioSource;
    private EnemyController enemyController;
    private long health = 100;
    [HideInInspector] public bool dead = false;
    [HideInInspector] public GameObject dot;

    void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        enemyController = GetComponent<EnemyController>();
        health = maxHealth;
        dead = false;
    }

    void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            foreach (Collider collider in GetComponents<Collider>()) collider.enabled = false;
            if (useDeathAnim)
            {
                if (enemyController) enemyController.playDeathAnimation();
            } else
            {
                if (sound)
                {
                    Transform point;
                    if (bloodPoint)
                    {
                        point = bloodPoint;
                    } else
                    {
                        point = transform;
                    }
                    Instantiate(sound, point.position, point.rotation);
                }
                Destroy(gameObject);
            }
            if (dot) Destroy(dot);
        }
        if (health < 0)
        {
            health = 0;
        } else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void takeDamage(long damage)
    {
        if (health > 0 && !dead)
        {
            if (damage > 0)
            {
                health -= damage;
            } else
            {
                --health;
            }
            if (audioSource)
            {
                if (health > 0)
                {
                    if (hurtSound)
                    {
                        audioSource.PlayOneShot(hurtSound);
                    } else
                    {
                        audioSource.Play();
                    }
                } else
                {
                    if (deathSound) audioSource.PlayOneShot(deathSound);
                }
            }
        }
    }
}