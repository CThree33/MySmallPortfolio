using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    private float nextTimeToFire;
    private InputAction shoot;

    public float impactForce = 150;
    public float range = 20;
    public int fireRate = 10;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject FX;

    void Start()
    {
        shoot = new InputAction("Shoot", binding:"<mouse>/leftButton");
        shoot.Enable();
    }

    void Update()
    {
        bool isShooting = shoot.ReadValue<float>() == 1;

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

        }

        FX = Instantiate(FX, muzzleFlash.transform.position, transform.rotation);
        FX.transform.parent = gameObject.transform;

        Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
        GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
        impact.transform.parent = hit.transform;
        Destroy(impact, 5);
    }
}
