using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private long damage = 20;
    [SerializeField] private float RPM = 750;
    [SerializeField] private float spread = 0.05f;
    public int ammo = 50;
    [SerializeField] private int maxAmmo = 200;
    [Tooltip("Amount of shots in a bullet.")] [SerializeField] private int shots = 1;
    [Tooltip("Amount of bullets to fire in a shot.")] [SerializeField] private int bulletsFired = 1;
    [SerializeField] private bool auto = false;

    [Header("UI")]
    [SerializeField] private Text ammoText = null;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip fireSound = null;

    [Header("Setup")]
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private GameObject blood = null;
    [SerializeField] private GameObject bulletHole = null;

    private AudioSource audioSource;
    private Light muzzleLight;
    private PlayerController playerController;
    private Controls input;
    private bool firing = false;
    private bool holding = false;
    private float nextShot = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        muzzleLight = muzzle.GetComponent<Light>();
        playerController = GetComponent<PlayerController>();
        resetEffects();
    }

    void Awake()
    {
        input = new Controls();
    }

    void OnEnable()
    {
        input.Enable();
        input.Player.Fire.performed += context => shoot(true);
        input.Player.Fire.canceled += context => shoot(false);
    }

    void OnDisable()
    {
        input.Disable();
        input.Player.Fire.performed -= context => shoot(true);
        input.Player.Fire.canceled -= context => shoot(false);
    }

    void Update()
    {
        ammoText.text = "Ammo: " + ammo + "/" + maxAmmo;
    }

    #region Input Functions
    void shoot(bool state)
    {
        if (state)
        {
            if (ammo > 0 && Time.time >= nextShot)
            {
                if (!auto)
                {
                    if (!holding)
                    {
                        StartCoroutine(fire(damage, RPM, spread, shots, bulletsFired));
                        holding = true;
                    }
                } else
                {
                    holding = true;
                    StartCoroutine(autofire());
                }
            }
        } else
        {
            holding = false;
            StopCoroutine(autofire());
        }
    }
    #endregion

    #region Main Functions
    IEnumerator fire(long damage, float RPM, float spread, int shots, int bulletsFired)
    {
        if (!firing)
        {
            int c = 0;
            int shotsToFire = bulletsFired;
            if (shotsToFire < 1) shotsToFire = 1;
            firing = true;
            while (c < shotsToFire)
            {
                if (Time.time >= nextShot)
                {
                    ++c;
                    --ammo;
                    nextShot = Time.time + 60 / RPM;
                    if (muzzleLight) muzzleLight.enabled = true;
                    if (audioSource && fireSound) audioSource.PlayOneShot(fireSound);
                    CancelInvoke("resetEffects");
                    Invoke("resetEffects", 0.075f);
                    for (int i = 0; i < shots; i++)
                    {
                        Vector3 bulletDirection = muzzle.forward;
                        bulletDirection.x += Random.Range(-spread, spread);
                        bulletDirection.y += Random.Range(-spread, spread);
                        Ray bulletRay = new Ray(muzzle.position, muzzle.forward);
                        Debug.DrawRay(bulletRay.origin, bulletRay.direction * 500, Color.yellow, 3);
                        if (Physics.Raycast(bulletRay, out RaycastHit bulletHit, 100))
                        {
                            if (bulletHit.collider.CompareTag("Enemy") && bulletHit.collider.GetComponent<EnemyHealth>())
                            {
                                bulletHit.collider.GetComponent<EnemyHealth>().takeDamage(damage);
                                if (blood)
                                {
                                    Vector3 bloodPosition;
                                    if (bulletHit.collider.GetComponent<EnemyHealth>().bloodPoint)
                                    {
                                        bloodPosition = new Vector3(bulletHit.point.x, bulletHit.collider.GetComponent<EnemyHealth>().bloodPoint.position.y, bulletHit.point.z);
                                    } else
                                    {
                                        bloodPosition = new Vector3(bulletHit.point.x, bulletHit.collider.transform.position.y, bulletHit.point.z);
                                    }
                                    Instantiate(blood, bloodPosition, bulletHit.collider.transform.rotation);
                                }
                            } else
                            {
                                if (bulletHole) Instantiate(bulletHole, bulletHit.point, Quaternion.FromToRotation(Vector3.forward, bulletHit.normal));
                            }
                        }
                    }
                } else
                {
                    yield return null;
                }
            }
            firing = false;
        } else
        {
            yield return null;
        }
    }

    IEnumerator autofire()
    {
        while (holding)
        {
            if (ammo > 0 && Time.time >= nextShot) StartCoroutine(fire(damage, RPM, spread, shots, 1));
            yield return null;
        }
    }

    void resetEffects()
    {
        if (muzzleLight) muzzleLight.enabled = false;
    }
    #endregion
}