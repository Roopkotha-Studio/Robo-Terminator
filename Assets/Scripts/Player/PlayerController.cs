using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public long maxHealth = 100;
    [SerializeField] private float speed = 6;
    [SerializeField] private float runSpeed = 8.5f;
    [Tooltip("Inverts horizontal movement.")] [SerializeField] private bool invertHorizontal = false;
    [Tooltip("Inverts vertical movement.")] [SerializeField] private bool invertVertical = false;
    [SerializeField] private bool flipMovement = false;

    [Header("UI")]
    [SerializeField] private Slider healthBar = null;
    [SerializeField] private Text healthText = null;
    [SerializeField] private Image damageOverlay = null;
    [SerializeField] private Text message = null;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] hurtSounds = new AudioClip[0];
    [SerializeField] private AudioClip[] deathSounds = new AudioClip[0];

    [Header("Setup")]
    public GameObject[] weapons = new GameObject[0];
    [SerializeField] private GameObject blood = null;
    [SerializeField] private Transform bloodPoint = null;

    private CharacterController characterController;
    private Animator animator;
    private new Collider collider;
    private AudioSource audioSource;
    private Controls input;
    [HideInInspector] public long health = 0;
    private float walkSpeed = 0;
    [HideInInspector] public int weapon = 0;
    private bool dead = false;
    private bool damaged = false;
    [HideInInspector] public bool hasPistol = true;
    [HideInInspector] public bool hasAssaultRifle = false;
    [HideInInspector] public bool hasShotgun = false;
    private Vector2 movement;
    private Vector3 oldMousePosition;
    private Vector3 newMousePosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        walkSpeed = speed;
        weapon = 0;
        hasPistol = true;
        hasAssaultRifle = false;
        hasShotgun = false;
        if (!bloodPoint) bloodPoint = transform;
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
        input.Player.Run.performed += context => run(true);
        input.Player.Turn.performed += context => turn(context.ReadValue<Vector2>());
        input.Player.ToPistol.performed += context => weaponHotkey(0);
        input.Player.ToRifle.performed += context => weaponHotkey(1);
        input.Player.ToShotgun.performed += context => weaponHotkey(2);
        input.Player.SwitchWeaponLeft.performed += context => switchWeapon(false);
        input.Player.SwitchWeaponRight.performed += context => switchWeapon(true);
        input.Player.Move.canceled += context => move(Vector2.zero);
        input.Player.Run.canceled += context => run(false);
    }

    void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= context => move(context.ReadValue<Vector2>());
        input.Player.Run.performed -= context => run(true);
        input.Player.Turn.performed -= context => turn(context.ReadValue<Vector2>());
        input.Player.ToPistol.performed -= context => weaponHotkey(0);
        input.Player.ToRifle.performed -= context => weaponHotkey(1);
        input.Player.ToShotgun.performed -= context => weaponHotkey(2);
        input.Player.SwitchWeaponLeft.performed -= context => switchWeapon(false);
        input.Player.SwitchWeaponRight.performed -= context => switchWeapon(true);
        input.Player.Move.canceled -= context => move(Vector2.zero);
        input.Player.Run.canceled -= context => run(false);
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
        oldMousePosition = Input.mousePosition;
        if (health <= 0 && !dead)
        {
            dead = true;
            if (Camera.main.GetComponent<CameraFollow>()) Camera.main.GetComponent<CameraFollow>().enabled = false;
            characterController.enabled = false;
            animator.SetBool("Walking", false);
            if (Random.value <= 0.5f)
            {
                animator.SetTrigger("DieForward");
            } else
            {
                animator.SetTrigger("DieBack");
            }
            collider.enabled = false;
            foreach (GameObject wep in weapons) wep.SetActive(false);
            StartCoroutine(dropBody());
            GameController.instance.gameOver = true;
        }
        if (!dead && !GameController.instance.gameOver && !GameController.instance.won && !GameController.instance.paused)
        {
            if (newMousePosition != oldMousePosition)
            {
                Ray ray = Camera.main.ScreenPointToRay(newMousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Floor")))
                {
                    Vector3 hitPoint = hit.point - transform.position;
                    hitPoint.y = 0;
                    transform.rotation = Quaternion.LookRotation(hitPoint);
                }
            }
            if (movement.magnitude != 0)
            {
                characterController.Move(new Vector3(movement.x, 0, movement.y).normalized * speed * Time.deltaTime);
                animator.SetBool("Walking", true);
            } else
            {
                animator.SetBool("Walking", false);
            }
        }
        if (GameController.instance.gameOver || GameController.instance.won) animator.SetBool("Walking", false);
        if (damaged)
        {
            damageOverlay.color = new Color(1, 0, 0, 0.25f);
        } else
        {
            damageOverlay.color = Color.Lerp(damageOverlay.color, new Color(1, 0, 0, 0), 1);
        }
        damaged = false;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthText.text = health + " / " + maxHealth;
    }

    void LateUpdate()
    {
        newMousePosition = Input.mousePosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "soldier.png", false);
    }

    #region Input Functions
    void move(Vector2 direction)
    {
        if (!dead)
        {
            float x = direction.x;
            float y = direction.y;
            if (invertHorizontal) x = -x;
            if (invertVertical) y = -y;
            if (flipMovement)
            {
                float flipX = y;
                float flipY = x;
                x = flipX;
                y = flipY;
            }
            movement = new Vector2(x, y);
        }
    }

    void run(bool state)
    {
        if (!dead)
        {
            if (state)
            {
                speed = runSpeed;
            } else
            {
                speed = walkSpeed;
            }
        }
    }

    void turn(Vector2 direction)
    {
        if (!dead)
        {
            Vector3 lookDirection = new Vector3(direction.x, direction.y, 0);
            Vector3 lookRotation = Camera.main.transform.TransformDirection(lookDirection);
            lookRotation = Vector3.ProjectOnPlane(lookRotation, Vector3.up);
            if (lookRotation != Vector3.zero) transform.rotation = Quaternion.LookRotation(lookRotation);
        }
    }

    void weaponHotkey(int newWeapon)
    {
        if (!dead)
        {
            int previous = weapon;
            weapon = newWeapon;
            if (weapon == 1 && !hasAssaultRifle) weapon = previous;
            if (weapon == 2 && !hasShotgun) weapon = previous;
            if (weapon < 0)
            {
                weapon = 0;
            } else if (weapon > weapons.Length - 1)
            {
                weapon = weapons.Length - 1;
            }
            foreach (GameObject wep in weapons) wep.SetActive(false);
            weapons[weapon].SetActive(true);
        }
    }

    void switchWeapon(bool state)
    {
        if (!dead && weapon > 0 || weapon < weapons.Length - 1)
        {
            int previous = weapon;
            if (!state) //Left
            {
                --weapon;
            } else //Right
            {
                ++weapon;
            }
            if (weapon == 1 && !hasAssaultRifle) weapon = previous;
            if (weapon == 2 && !hasShotgun) weapon = previous;
            if (weapon < 0)
            {
                weapon = 0;
            } else if (weapon > weapons.Length - 1)
            {
                weapon = weapons.Length - 1;
            }
            foreach (GameObject wep in weapons) wep.SetActive(false);
            weapons[weapon].SetActive(true);
        }
    }
    #endregion

    #region Main Functions
    public void addHealth(long amount, bool heal, bool showBlood)
    {
        if (!dead && health > 0)
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
                if (audioSource)
                {
                    if (health > 0)
                    {
                        if (hurtSounds.Length > 0)
                        {
                            audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length - 1)]);
                        } else
                        {
                            audioSource.Play();
                        }
                    } else
                    {
                        if (deathSounds.Length > 0) audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length - 1)]);
                    }
                }
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

    IEnumerator dropBody()
    {
        float y = transform.position.y - 0.6f;
        for (int i = 0; i < 12; i ++)
        {
            transform.position -= new Vector3(0, 0.0075f, 0);
            if (transform.position.y <= y) transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}