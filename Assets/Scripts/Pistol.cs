using UnityEngine;

public class Pistol : MonoBehaviour

{
    public Transform shootPoint;
    public GameObject projectile;
    public GameObject shootSound;

    private int ammo = 1200;

    public void shoot() {
        if (ammo > 0) {
            ammo -= 1;
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
