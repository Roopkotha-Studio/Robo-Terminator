using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct G_Pistol
{
    public long minDamage;
    public long maxDamage;
    public float RPM;
    public float spread;
    public int ammo;
    public int maxAmmo;
    public int shotAmount;
    public int shotsFired;
}

[System.Serializable]
public struct G_AssaultRifle
{
    public long minDamage;
    public long maxDamage;
    public float RPM;
    public float spread;
    public int ammo;
    public int maxAmmo;
    public int shotAmount;
    public int shotsFired;
}

[System.Serializable]
public struct G_BurstRifle
{
    public long minDamage;
    public long maxDamage;
    public float RPM;
    public float spread;
    public int ammo;
    public int maxAmmo;
    public int shotAmount;
    public int shotsFired;
}

[System.Serializable]
public struct G_Shotgun
{
    public long minDamage;
    public long maxDamage;
    public float RPM;
    public float spread;
    public int ammo;
    public int maxAmmo;
    public int shotAmount;
    public int shotsFired;
}

public class PlayerGun : MonoBehaviour
{
    [Header("Settings")]
    public G_Pistol pistol;
    public G_AssaultRifle assaultRifle;
    public G_BurstRifle burstRifle;
    public G_Shotgun shotgun;

    [Header("UI")]
    [SerializeField] private Text ammoText = null;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip fireSound = null;

    [Header("Miscellanous")]
    public bool hasPistol = true;
    public bool hasAssaultRifle = false;
    public bool hasBurstRifle = false;
    public bool hasShotgun = false;
    public Weapons weapon = Weapons.Pistol;
    public enum Weapons
    {
        Pistol,
        AssaultRifle,
        BurstRifle,
        Shotgun
    }

    [Header("Setup")]
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private GameObject blood = null;
    [SerializeField] private GameObject bulletHole = null;

    private AudioSource audioSource;
    private Light muzzleLight;
    private PlayerController playerController;
    private Controls input;
    private int ammo = 0;
    private int maxAmmo = 0;
    private bool firing = false;
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
        //input.Player.Fire.performed += context => shoot(true);
        //input.Player.Fire.canceled += context => shoot(false);
    }

    void OnDisable()
    {
        //input.Player.Fire.performed -= context => shoot(true);
        //input.Player.Fire.canceled -= context => shoot(false);
    }

    void Update()
    {
        if (weapon == Weapons.Pistol)
        {
            ammo = pistol.ammo;
            maxAmmo = pistol.maxAmmo;
        } else if (weapon == Weapons.AssaultRifle)
        {
            ammo = assaultRifle.ammo;
            maxAmmo = assaultRifle.maxAmmo;
        } else if (weapon == Weapons.BurstRifle)
        {
            ammo = burstRifle.ammo;
            maxAmmo = burstRifle.maxAmmo;
        } else if (weapon == Weapons.Shotgun)
        {
            ammo = shotgun.ammo;
            maxAmmo = shotgun.maxAmmo;
        }
        if (!GameController.instance.gameOver && !GameController.instance.won)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && hasPistol)
            {
                weapon = Weapons.Pistol;
                playerController.showMessage("Selected Pistol");
            } else if (Input.GetKeyDown(KeyCode.Alpha2) && hasAssaultRifle)
            {
                weapon = Weapons.AssaultRifle;
                playerController.showMessage("Selected Assault Rifle");
            } else if (Input.GetKeyDown(KeyCode.Alpha3) && hasBurstRifle)
            {
                weapon = Weapons.BurstRifle;
                playerController.showMessage("Selected Burst Rifle");
            } else if (Input.GetKeyDown(KeyCode.Alpha4) && hasShotgun)
            {
                weapon = Weapons.Shotgun;
                playerController.showMessage("Selected Shotgun");
            }
            if (!firing && ammo > 0 && Time.time >= nextShot)
            {
                if (weapon == Weapons.Pistol && Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(fire(pistol.maxDamage, pistol.maxDamage, pistol.RPM, pistol.spread, pistol.shotAmount, pistol.shotsFired));
                } else if (weapon == Weapons.AssaultRifle && Input.GetButton("Fire1"))
                {
                    StartCoroutine(fire(assaultRifle.maxDamage, assaultRifle.maxDamage, assaultRifle.RPM, assaultRifle.spread, assaultRifle.shotAmount, assaultRifle.shotsFired));
                } else if (weapon == Weapons.BurstRifle && Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(fire(burstRifle.maxDamage, burstRifle.maxDamage, burstRifle.RPM, burstRifle.spread, burstRifle.shotAmount, burstRifle.shotsFired));
                } else if (weapon == Weapons.Shotgun && Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(fire(shotgun.minDamage, shotgun.maxDamage, shotgun.RPM, shotgun.spread, shotgun.shotAmount, shotgun.shotsFired));
                }
            }
        }
        if (!hasPistol) hasPistol = true;
        if (weapon == Weapons.AssaultRifle && !hasAssaultRifle) weapon = Weapons.Pistol;
        if (weapon == Weapons.BurstRifle && !hasBurstRifle) weapon = Weapons.Pistol;
        if (weapon == Weapons.Shotgun && !hasShotgun) weapon = Weapons.Pistol;
        if (pistol.ammo > pistol.maxAmmo) pistol.ammo = pistol.maxAmmo;
        if (assaultRifle.ammo > assaultRifle.maxAmmo) assaultRifle.ammo = assaultRifle.maxAmmo;
        if (burstRifle.ammo > burstRifle.maxAmmo) burstRifle.ammo = burstRifle.maxAmmo;
        if (shotgun.ammo > shotgun.maxAmmo) shotgun.ammo = shotgun.maxAmmo;
        ammoText.text = "Ammo: " + ammo + "/" + maxAmmo;
    }

    IEnumerator fire(long minDamage, long maxDamage, float RPM, float spread, int shotAmount, int shotsFired)
    {
        if (!firing)
        {
            int c = 0;
            int shotsToFire = shotsFired;
            if (shotsToFire < 1) shotsToFire = 1;
            firing = true;
            while (c < shotsToFire)
            {
                if (Time.time >= nextShot)
                {
                    ++c;
                    if (weapon == Weapons.Pistol)
                    {
                        --pistol.ammo;
                    } else if (weapon == Weapons.AssaultRifle)
                    {
                        --assaultRifle.ammo;
                    } else if (weapon == Weapons.BurstRifle)
                    {
                        --burstRifle.ammo;
                    } else if (weapon == Weapons.Shotgun)
                    {
                        --shotgun.ammo;
                    }
                    nextShot = Time.time + 60 / RPM;
                    if (muzzleLight) muzzleLight.enabled = true;
                    if (audioSource && fireSound) audioSource.PlayOneShot(fireSound);
                    CancelInvoke("resetEffects");
                    Invoke("resetEffects", 0.075f);
                    for (int i = 0; i < shotAmount; i++)
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
                                if (minDamage == maxDamage)
                                {
                                    bulletHit.collider.GetComponent<EnemyHealth>().takeDamage(maxDamage);
                                } else
                                {
                                    bulletHit.collider.GetComponent<EnemyHealth>().takeDamage((long)Random.Range(minDamage, maxDamage));
                                }
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

    void resetEffects()
    {
        if (muzzleLight) muzzleLight.enabled = false;
    }
}
