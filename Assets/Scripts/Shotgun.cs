using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private bool canShoot = true;
    private bool pumped = false;
    private double pumpLimit = 0.5;

    public Transform shootPoint;
    public GameObject shootSound;
    public GameObject projectile;
    public GameObject pump;

    private int ammo = 12;


    // Update is called once per frame
    void Update()
    {
        // Check if pump is pulled back
        if (pump.transform.localPosition.z <= pumpLimit) {
            pumped = true;
        }

        // Check if pump is pushed forward (after pulling back)
        if (pumped && pump.transform.localPosition.z >= 0.9f) {
            pumped = false;
            canShoot = true;
        }
        Debug.Log(pump.transform.localPosition);
    }

    public void shoot() {
        if (ammo > 0 && canShoot) {
            ammo -= 1;
            canShoot = false;
            shootSound.GetComponent<AudioSource>().Play();
            createShot();
        }
    }

    private void createShot() {
        GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        currentBullet.transform.forward = shootPoint.forward;
        currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    }
}
