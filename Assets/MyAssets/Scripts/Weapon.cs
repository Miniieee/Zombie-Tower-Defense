using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Unity.Burst;

[BurstCompile]
public class Weapon : MonoBehaviour
{
    private float fireCountDown = 0f;

    [Header("Basic")]
    public Camera FPCamera;
    public float range = 50;
    public int damage = 50;
    public float fireRate = 15f;
    public int maxAmmo = 30;
    private int currentAmmo;
    public int ammo;

    [Header("Audios")]
    public AudioSource shootSound;
    public AudioManager audioManager;
    public string audioToPlay;

    [Header("Texts and Buttons")]
    public TextMeshProUGUI ammoText;
    public Color redColor;

    [Header("Particle effects")]
    public ParticleSystem muzzle;
    public ParticleSystem impactBody;
    public ParticleSystem impactGround;

    [Header("Animation")]
    public Animator throttleAnim;
    public Animator akAnim;
    private string shooting = "isShooting";
    private string akShoot = "shootAK47";
    private string isReload = "isReloadingPistol";
    private string isReloadAK = "isReloadingAK";

    ShootButton shootButton;

    private bool isReloading = false;

    void Start()
    {
        isReloading = false;
        currentAmmo = 0;
        shootButton = ShootButton.instace;
        AmmoCheck();
    }

    void FixedUpdate()
    {
        if (CanShoot && fireCountDown <= 0f && !isReloading)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
            else
            {
                audioManager.PlayOneShot("Empty");
                throttleAnim.SetBool(shooting, false);
                akAnim.SetBool(akShoot, false);
            }
        }
        if (!CanShoot)
        {
            muzzle.Stop();
            throttleAnim.SetBool(shooting, false);
            akAnim.SetBool(akShoot, false);
        }
        fireCountDown -= Time.deltaTime;
    }

    public bool CanShoot { get { return ShootButton.buttonPressed; } }

    public void Shoot()
    {
        if (!muzzle.isPlaying)
        {
            muzzle.Play();
        }

        ProcessRaycasting();
        throttleAnim.SetBool(shooting, true);
        akAnim.SetBool(akShoot, true);
        shootSound.Play();
        currentAmmo--;
        AmmoCheck();
    }

    public void Reload()
    {
        if (ammo > 0 && currentAmmo != maxAmmo)
        {
            isReloading = true;
            ReloadAnim();
            audioManager.Play(audioToPlay);
            int j = maxAmmo - currentAmmo;
            for (int i = 0; i < j; i++)
            {
                if (ammo > 0)
                {
                    ammo--;
                    currentAmmo++;
                }
            }
        }
    }

    private void ReloadAnim()
    {
        throttleAnim.SetBool(isReload, true);
        akAnim.SetBool(isReloadAK, true);
    }

    public void AnimationIdle()
    {
        throttleAnim.SetBool(isReload, false);
        akAnim.SetBool(isReloadAK, false);
        isReloading = false;
        AmmoCheck();
    }

    public void AmmoCheck()
    {
        isReloading = false;
        ammoText.text = currentAmmo.ToString() + " | " + ammo.ToString();
        //color of the weapon
        //color of the numbers
        if (currentAmmo == 0 && ammo == 0)
        {
            ammoText.color = redColor;
        }
        else
        {
            ammoText.color = Color.white;
        }

    }

    public void AddAmmo(int addAmmo)
    {
        ammo += addAmmo;
        AmmoCheck();
    }

    private void ProcessRaycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Effects(hit);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy == null)
            {
                return;
            }
            enemy.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void Effects(RaycastHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            Instantiate(impactBody, hit.point, Quaternion.LookRotation(hit.normal));

        }
        else if (hit.transform.tag == "Ground")
        {
            Instantiate(impactGround, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            return;
        }
    }
}
