using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 40f;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip AudioClip;

    private int TotalAmmo = 30;
    public void Fire()
    {
        if (TotalAmmo > 0)
        {
            GameObject spawnedbullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedbullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            //audioSource.PlayOneShot(AudioClip);
            Destroy(spawnedbullet, 2);
            TotalAmmo--;
        }
        else
            return;
        
    }
}
