using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Pistol
{
    private bool canShoot = true;
    private bool pumped = false;
    public GameObject pump;
    public Transform goalFront;
    public Transform goalBack;

    public GameObject pumpForwardSound;
    public GameObject pumpBackSound;


    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }
        // Check if pump is pulled back
        if ((!pumped) && (Vector3.Distance(pump.transform.position, goalBack.transform.position) < 0.02))
        {
            pumped = true;
            canShoot = false;
            Debug.Log("PUMPED BACK");
            pumpBackSound.GetComponent<AudioSource>().Play();
        }

        // Check if pump is pushed forward (after pulling back)
        if ((pumped) && (Vector3.Distance(pump.transform.position, goalFront.transform.position) < 0.02))
        {
            pumped = false;
            canShoot = true;
            Debug.Log("SHOT READY");
            pumpForwardSound.GetComponent<AudioSource>().Play();
        }
    }

    public override void shoot()
    {
        if ((ammo > 0) && (canShoot))
        {
            ammo -= 1;
            canShoot = false;
            shootSound.GetComponent<AudioSource>().Play();
            muzzleFlash.Activate();
            createShot();
            for (int i = 0; i < 8; i++) {
                createShot(new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy)));
            }
        }
    }

    // private void createShot()
    // {
    //     GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
    //     currentBullet.transform.forward = shootPoint.forward;
    //     currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    // }
}
