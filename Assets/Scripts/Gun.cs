using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   [SerializeField] float damage = 10f;
   [SerializeField] float range = 100f;
   [SerializeField] float impactForce = 30f;

   [SerializeField] Camera playerCamera;
   [SerializeField] ParticleSystem muzzleFlash;
   [SerializeField] GameObject impactEffect;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.parent = hit.transform;
            Destroy(impactGO, 2f);
        }
    }
}
