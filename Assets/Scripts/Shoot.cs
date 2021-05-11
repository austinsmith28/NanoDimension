using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    [SerializeField] Camera cam;
    [SerializeField] float damage;

    [SerializeField] public AudioSource FireWeapon;

    void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireWeapon.Play();
            ShootGun();
        
        }
        
    }
   void ShootGun()
   {
       if (cam != null){
           
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("hit "+ hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
                

            }
        }

   }

   
}
