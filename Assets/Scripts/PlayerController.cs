using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private long maxHealth = 100;
    [SerializeField] private float speed = 6;
    [Tooltip("Inverts horizontal movement.")] [SerializeField] private bool invertHorizontal = false;
    [Tooltip("Inverts vertical movement.")] [SerializeField] private bool invertVertical = false;
    [SerializeField] private bool flipMovement = false;

    [Header("UI")]
    [SerializeField] private Text healthText = null;
    [SerializeField] private Image damageOverlay = null;
    [SerializeField] private Text message = null;

    [Header("Miscellanous")]
    public bool isHealthFull = true;

    [Header("Setup")]
    [SerializeField] private GameObject blood = null;
    [SerializeField] private Transform bloodPoint = null;

    private CharacterController characterController;
    private Animator animator;
    private AudioSource audioSource;
    private PlayerGun playerGun;
    private Controls input;
    private long health = 0;
    private Vector2 movement;
    private bool damaged = false;
    private float timeTillHazardDamage = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerGun = GetComponent<PlayerGun>();
        health = maxHealth;
        resetMessage();
    }

    void Awake()
    {
        input = new Controls();
    }

    void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += context => move(context.ReadValue<Vector2>());
        input.Player.Move.canceled += context => move(Vector2.zero);
    }

    void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= context => move(context.ReadValue<Vector2>());
        input.Player.Move.canceled -= context => move(Vector2.zero);
    }

    void Update()
    {
        if (health < 0) //Checks if health is less than 0
        {
            health = 0;
        } else if (health > maxHealth) //Checks if health is more than the max amount of health
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            GameController.instance.gameOver = true;
            if (Camera.main.GetComponent<CameraFollow>()) Camera.main.GetComponent<CameraFollow>().enabled = false;
            foreach (Transform mesh in transform)
            {
                if (mesh.GetComponent<SkinnedMeshRenderer>()) mesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
        }
        if (!GameController.instance.gameOver && !GameController.instance.won)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Floor")))
            {
                Vector3 hitPoint = hit.point - transform.position;
                hitPoint.y = 0;
                transform.rotation = Quaternion.LookRotation(hitPoint);
            }
            if (movement.magnitude > 0)
            {
                characterController.SimpleMove(new Vector3(1, 0, 1).normalized * speed * Time.deltaTime);
                animator.SetBool("Walking", true);
            } else
            {
                animator.SetBool("Walking", false);
            }
            print(movement);
        }
        if (GameController.instance.gameOver || GameController.instance.won) animator.SetBool("Walking", false);
        if (damaged)
        {
            damageOverlay.color = new Color(1, 0, 0, 1);
        } else
        {
            damageOverlay.color = Color.Lerp(damageOverlay.color, new Color(1, 0, 0, 0), 7.5f * Time.deltaTime);
        }
        damaged = false;
        if (health < maxHealth)
        {
            isHealthFull = false;
        } else
        {
            isHealthFull = true;
        }
        healthText.text = "Health: " + health + "/" + maxHealth;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "soldier.png", false);
    }

    #region Input Functions
    void move(Vector2 direction)
    {
        movement = direction;
        /*
        if (invertHorizontal) movement = new Vector2(-movement.x, movement.y);
        if (invertVertical) movement = new Vector2(movement.x, -movement.y);
        if (flipMovement)
        {
            float x = movement.x;
            float y = movement.y;
            movement = new Vector2(y, x);
        }*/
        print(movement);
    }
    #endregion

    #region Main Functions
    public void addHealth(long amount, bool heal, bool showBlood)
    {
        if (!heal) //Damages the player
        {
            if (amount > 0)
            {
                health -= amount;
            } else
            {
                --health;
            }
            if (audioSource) audioSource.Play();
            if (blood && showBlood)
            {
                if (bloodPoint)
                {
                    Instantiate(blood, bloodPoint.position, transform.rotation);
                } else
                {
                    Instantiate(blood, transform.position, transform.rotation);
                }
            }
            damaged = true;
        } else //Heals the player
        {
            if (amount > 0)
            {
                health += amount;
            } else
            {
                ++health;
            }
        }
    }
    
    public void showMessage(string text)
    {
        if (message)
        {
            message.text = text;
            CancelInvoke("resetMessage");
            Invoke("resetMessage", 1.5f);
        }
    }

    void resetMessage()
    {
        if (message) message.text = "";
    }
    #endregion
}