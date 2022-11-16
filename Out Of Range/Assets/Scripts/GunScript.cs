using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    public Animator animator;
    InputAction shoot;
    InputAction reload;
    public Transform fpsCamera;
    public float impactForce = 150;
    public float range = 20;
    public int fireRate = 10;
    float nextTimeToFire;

    public AudioClip sparo;
    public AudioClip ricarica;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject zombieImpactEffect;
    public GameObject fx;

    public LayerMask IgnoreMe;

    public int currentAmmo;
    public int maxAmmo;
    public int clipSize;
    public int maxClipSize;

    private float reloadTime = 2f;
    private bool isReloading;
    bool reloading = false;

    public float damageEnemy = 10f;

    public AudioClip passiErba;
    public float shootingTime = 1;
    public float timer;

    public PlayerMovement playerMovement1;

    public bool isShooting;
    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        reload = new InputAction("Reload", binding: "keyboard/r");
        shoot.Enable();
        reload.Enable();

        currentAmmo = maxAmmo;
        clipSize = maxClipSize;
    }
    void Update()
    {
        reloading = reload.ReadValue<float>() == 1;
        if (currentAmmo == 0 && clipSize == 0)
        {
            animator.SetBool("IsShooting", false);
            return;
        }
        if (isReloading)
        {
            return;
        }
        if (currentAmmo == 0 && isReloading == false || reloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        bool isShooting = shoot.ReadValue<float>() == 1;

        animator.SetBool("IsShooting", isShooting);


        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }
        RaycastHit hit;
        Physics.Raycast(fpsCamera.transform.position, fpsCamera.forward, out hit, int.MaxValue, ~IgnoreMe);
        if (hit.distance > 0.5f & hit.distance < 100)
            transform.LookAt(hit.point);
    }

    private void Fire()
    {
        isShooting = true;
        AudioSource.PlayClipAtPoint(sparo, transform.position, 0.5f);
        muzzleFlash.Play();
        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.forward, out hit, range, ~IgnoreMe))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.transform.tag == "Enemy")
            {
                Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                GameObject impact = Instantiate(zombieImpactEffect, hit.point, impactRotation);
                Destroy(impact, 0.1f);
                EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                enemyHealthScript.ReduceHealth(damageEnemy);
            }
            else
            {
                Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
                Destroy(impact, 5);
            }
        }

        var f = Instantiate(fx, muzzleFlash.transform.position, transform.rotation);
        f.transform.parent = gameObject.transform;
        if (hit.distance != 0)
        {
            Destroy(f, hit.distance * 0.018f);
        }
        else
        {
            Destroy(f, range * 0.018f);
        }
    }

    private IEnumerator Reload()
    {

        isReloading = true;
        animator.SetBool("IsReloading", true);
        AudioSource.PlayClipAtPoint(ricarica, transform.position, 0.5f);
        yield return new WaitForSeconds(reloadTime);
        if (maxAmmo - currentAmmo <= clipSize)
        {
            while (currentAmmo < maxAmmo)
            {
                currentAmmo++;
                clipSize--;
            }
        }
        else
        {
            while (clipSize != 0)
            {
                currentAmmo++;
                clipSize--;
            }
        }

        isReloading = false;
        animator.SetBool("IsReloading", false);
    }

    public void StepsSound()
    {
        AudioSource.PlayClipAtPoint(passiErba, transform.position, 0.5f);
    }

}