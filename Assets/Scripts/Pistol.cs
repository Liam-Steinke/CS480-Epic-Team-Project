using UnityEngine;

public class Pistol : MonoBehaviour

{
    public Transform shootPoint;
    public GameObject projectile;
    public GameObject shootSound;

    public float damage = 1f;

    private int ammo = 1200;
    private float range = 100f;

    public void shoot() {
        if (ammo > 0) {
            ammo -= 1;
            shootSound.GetComponent<AudioSource>().Play();
            createShot();
        }
    }


    // Uses 3D Projectile
    // private void createShot() {
    //     GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
    //     currentBullet.transform.forward = shootPoint.forward;
    //     currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    // }

    private void createShot() {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
            }
        }
    }
}
