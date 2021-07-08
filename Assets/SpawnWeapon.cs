using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    Transform barrel;

    private void Start()
    {
        GameObject spawnedweapon = Instantiate(weapon, barrel.position, barrel.rotation);
        spawnedweapon.GetComponent<Rigidbody>().isKinematic = true;
        spawnedweapon.GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag =="Hand")
        //{
        //    GameObject spawnedweapon = Instantiate(weapon, barrel.position, barrel.rotation);
        //    spawnedweapon.GetComponent<Rigidbody>().isKinematic = true;
        //    spawnedweapon.GetComponent<Rigidbody>().useGravity = false;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            GameObject spawnedweapon = Instantiate(weapon, barrel.position, barrel.rotation);
            spawnedweapon.GetComponent<Rigidbody>().isKinematic = true;
            spawnedweapon.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
