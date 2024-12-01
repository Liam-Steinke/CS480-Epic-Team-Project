using UnityEngine;

public class Pistol : MonoBehaviour

{
    // The point where shots come from.
    public Transform shootPoint;
    public GameObject projectile;
    public GameObject shootSound;
    public ParticleLight muzzleFlash;
    public Tracer tracer;

    public float damage = 1f;

    public int ammo = 1200;
    public float range = 100f;
    public float inaccuracy = 0f;

    // "virtual" allows the method to be overridden.
    public virtual void shoot() {
        if (ammo > 0) {
            ammo -= 1;
            shootSound.GetComponent<AudioSource>().Play();
            createShot();
            muzzleFlash.Activate();
        }
    }


    // Uses 3D Projectile
    // private void createShot() {
    //     GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
    //     currentBullet.transform.forward = shootPoint.forward;
    //     currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    // }

    protected virtual void createShot() {
        createShot(new Vector3());
    }

    protected virtual void createShot(Vector3 offset) {
        RaycastHit hit;
        Tracer trace = Instantiate(tracer, new Vector3(0, 0, 0), Quaternion.identity);

        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward + offset, out hit, range)) {
            Debug.Log(hit.transform.name);
            trace.AimAt(shootPoint.transform.position, hit.point);
            

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
            }
        } else {
            trace.AimAt(shootPoint.transform.position, shootPoint.transform.position + (shootPoint.transform.forward * range));
        }
    }
}
