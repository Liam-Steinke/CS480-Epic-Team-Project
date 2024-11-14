using UnityEngine;

public class Pistol : MonoBehaviour

{
    public Transform shootPoint;
    public GameObject projectile;

    private int ammo = 12;

    public void shoot() {
        if (ammo > 0) {
            ammo -= 1;
            createShot();
        }
    }

    private void createShot() {
        GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        currentBullet.transform.forward = shootPoint.forward;
        currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    }
}
