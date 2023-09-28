using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint, gunHolder, ADSPoint;

    public string nameWeapon;
    public bool canAutoFire;
    public float rateFire;
    public float actualRateFire;
    public int sizeMagazine;
    public int maxAmmo;
    public int actualAmmoReserve;
    public int actualAmmoMagazine;
    public bool canShoot;
    public bool reload;
    private Vector3 gunStartPos;
    public ParticleSystem muzzleFlash;
    public float FOVWeapon = 50f;
    public float fireCounter;

    //Audio
    public AudioClip shootClip;
    public AudioClip noAmmo;
    public AudioClip reloadSound;
    private bool soundRecharge = false;

    public AudioSource weaponAudioSource;

    //Anim
    public Animator animator;
    readonly int idShootAnim = Animator.StringToHash("Shoot");
    readonly int idReloadAnim = Animator.StringToHash("Reload");

    private void Awake() 
    {
        muzzleFlash.Stop();
        gunStartPos = gunHolder.localPosition;
        weaponAudioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        UpdateWeaponInfoUI();
    }

    private void Update() 
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void ShootWeapon()
    {
        if(reload)
        {
            return;
        }

        //Shoot
        if(Input.GetMouseButtonDown(0) && !canAutoFire)
        {
            RaycastHit hit;

            if(actualAmmoMagazine >= 1)
            {
                animator.SetTrigger(idShootAnim);

                if (Physics.Raycast(PlayerController.Instance.camTrans.position, PlayerController.Instance.camTrans.forward, out hit, 50f))
                {
                    if (Vector3.Distance(PlayerController.Instance.camTrans.position, hit.point) > 2f)
                    {
                        firePoint.LookAt(hit.point);
                    }
                }
                else
                {
                    firePoint.LookAt(PlayerController.Instance.camTrans.position + (PlayerController.Instance.camTrans.forward * 30f));
                }
            } else {
                if(!soundRecharge)
                {
                    soundRecharge = true;
                    StartCoroutine(SoundRecharge());
                }
            }
        }

        //Automatic Weapon
        else if(Input.GetMouseButton(0) && canAutoFire)
        {
            RaycastHit hit;

            if(fireCounter <= 0)
            {
                if(actualAmmoMagazine >= 1)
                {
                    animator.SetTrigger(idShootAnim);

                    if (Physics.Raycast(PlayerController.Instance.camTrans.position, PlayerController.Instance.camTrans.forward, out hit, 50f))
                    {
                        if (Vector3.Distance(PlayerController.Instance.camTrans.position, hit.point) > 2f)
                        {
                            firePoint.LookAt(hit.point);
                        }
                    }
                    else
                    {
                        firePoint.LookAt(PlayerController.Instance.camTrans.position + (PlayerController.Instance.camTrans.forward * 30f));
                    }
                    fireCounter = rateFire;
                } else {
                    if(!soundRecharge)
                    {
                        soundRecharge = true;
                        StartCoroutine(SoundRecharge());
                    }
                }
            }

        }

    }

    
    private IEnumerator SoundRecharge()
    {
        weaponAudioSource.PlayOneShot(noAmmo, 1);
        yield return new WaitForSeconds(2f);
        soundRecharge = false;
    }

    public void AimWeapon()
    {
        //Start Cam FOV
        if (Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(FOVWeapon);
        }

        if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, ADSPoint.position, 2f * Time.deltaTime);
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, 2f * Time.deltaTime);
        }

        //End Cam FOV
        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();
        }

    }

    public void ReloadWeapon()
    {
        if(actualAmmoMagazine == sizeMagazine)
        {
            return;
        }

        if(actualAmmoReserve >= 1)
        {
            reload = true;
            animator.SetTrigger(idReloadAnim);
            weaponAudioSource.PlayOneShot(reloadSound, 1);

            actualAmmoReserve += actualAmmoMagazine;

            if(actualAmmoReserve >= sizeMagazine)
            {
                actualAmmoMagazine = sizeMagazine;
                actualAmmoReserve -= sizeMagazine;
            } else
            {
                actualAmmoMagazine = actualAmmoReserve;
                actualAmmoReserve = 0;
            }
        }
    }
    

    public void FinishReloadWeapon()
    {
        //Animation Event
        UpdateWeaponInfoUI();
        reload = false;
    }

    public void BulletShoot()
    {
        //Animation Event
        if(actualAmmoReserve >= 1)
        {
            weaponAudioSource.PlayOneShot(shootClip, 1);
            muzzleFlash.Play();
            Instantiate(bullet, firePoint.position,firePoint.rotation);
            actualAmmoMagazine--;
            UpdateWeaponInfoUI();
            canShoot = false;
        }
    }

    public void UpdateWeaponInfoUI()
    {
        UIManager.Instance.ChangeTextWeapon(nameWeapon, actualAmmoMagazine, actualAmmoReserve);
    }


    


}
