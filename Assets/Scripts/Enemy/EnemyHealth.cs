using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private long maxHealth = 100;
    [SerializeField] private float deathPositionY = 0.5f;
    [SerializeField] private float deathRotationZ = 180;
    [SerializeField] private DeathTypes deathType = DeathTypes.Animated;
    private GameObject effect = null;
    public Transform bloodPoint;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound = null;
    [SerializeField] private AudioClip deathSound = null;

    private new Collider collider;
    private AudioSource audioSource;
    private EnemyController enemyController;
    [SerializeField] private enum DeathTypes {Animated, Scripted, None}
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
            foreach (NavMeshAgent navMeshAgent in GetComponents<NavMeshAgent>()) navMeshAgent.enabled = false;
            if (effect)
            {
                if (bloodPoint)
                {
                    Instantiate(effect, bloodPoint.position, bloodPoint.rotation);
                } else
                {
                    Instantiate(effect, transform.position, transform.rotation);
                }
            }
            if (dot) Destroy(dot);
            if (deathType == DeathTypes.Animated)
            {
                enemyController.playDeathAnimation();
            } else if (deathType == DeathTypes.Scripted)
            {
                transform.position += new Vector3(0, deathPositionY, 0);
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + deathRotationZ);
            } else if (deathType == DeathTypes.None)
            {
                Destroy(gameObject);
            }
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