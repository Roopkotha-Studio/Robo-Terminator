using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private long damage = 10;
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private float aggroDistance = 25;
    [SerializeField] private float attackDistance = 2;

    private Animator animator;
    private new Animation animation;
    private NavMeshAgent navMeshAgent;
    private bool chasing = false;
    private bool attacking = false;
    private float nextAttack = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player)
        {
            if (!GameController.instance.gameOver && !GameController.instance.won)
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
    }
    
    void OnDrawGizmos()
    {
        string s = name.ToLower();
        if (s.Contains("spider"))
        {
            Gizmos.DrawIcon(transform.position, "spider.png", false);
        } else if (s.Contains("scorpion"))
        {
            Gizmos.DrawIcon(transform.position, "scorpion.png", false);
        } else if (s.Contains("raz"))
        {
            Gizmos.DrawIcon(transform.position, "alien.png", false);
        } else if (s.Contains("alien"))
        {
            Gizmos.DrawIcon(transform.position, "alien.png", false);
        } else if (s.Contains("robot"))
        {
            Gizmos.DrawIcon(transform.position, "support.png", false);
        } else
        {
            Gizmos.DrawIcon(transform.position, "enemy.png", false);
        }
    }
}