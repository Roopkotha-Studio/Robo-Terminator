using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private long damage = 10;
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private float aggroDistance = 25;
    [SerializeField] private float attackDistance = 2;
    [SerializeField] private string deathAnimation = "";

    private Animator animator;
    private new Animation animation;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private EnemyHealth enemyHealth;
    private bool chasing = false;
    private bool attacking = false;
    private float nextAttack = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (!enemyHealth.dead && !GameController.instance.gameOver && !GameController.instance.won)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            navMeshAgent.enabled = true;
            if (player)
            {
                if (!GameController.instance.gameOver && !GameController.instance.won && !GameController.instance.paused)
                {
                    if (!chasing && Vector3.Distance(transform.position, player.transform.position) <= aggroDistance) chasing = true;
                    if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
                    {
                        attacking = true;
                    } else
                    {
                        attacking = false;
                    }
                    if (player && attacking && nextAttack <= 0)
                    {
                        player.addHealth(damage, false, true);
                        nextAttack = attackTime;
                    }
                    if (nextAttack > 0) nextAttack -= Time.deltaTime;
                } else
                {
                    chasing = false;
                    attacking = false;
                }
                if (chasing)
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(player.transform.position);
                    if (animator) animator.SetBool("Walking", true);
                    if (animation && !animation.isPlaying) animation.Play();
                } else
                {
                    navMeshAgent.isStopped = true;
                    if (animator) animator.SetBool("Walking", false);
                    if (animation && animation.isPlaying) animation.Stop();
                }
            } else
            {
                chasing = false;
                attacking = false;
                navMeshAgent.isStopped = true;
                if (animator) animator.SetBool("Walking", false);
                if (animation && animation.isPlaying) animation.Stop();
            }
        } else
        {
            chasing = false;
            attacking = false;
            navMeshAgent.enabled = false;
            if (animator) animator.SetBool("Walking", false);
            if (animation && animation.isPlaying) animation.Stop();
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "enemy.png", false);
    }
    
    public void playDeathAnimation()
    {
        bool c = false;
        if (animator)
        {
            animator.SetBool("Walking", false);
            animator.SetTrigger("Die");
            c = true;
        }
        if (!c && animation) //If no Animator was found
        {
            animation.Play(deathAnimation);
            c = true;
        }
    }
}