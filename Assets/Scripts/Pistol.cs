using UnityEngine;

public class Pistol : MonoBehaviour

{
    public Transform shootPoint;
    public GameObject projectile;

    public void shoot() {
        GameObject currentBullet = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        currentBullet.transform.forward = shootPoint.forward;
        currentBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 20, ForceMode.Impulse);
    }
}
