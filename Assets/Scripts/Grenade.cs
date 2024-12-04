using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Grenade : MonoBehaviour
{
    public Animator grenadeAnim;
    public GameObject explosion;
    public bool timer_start = false;
    public float timer = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }
        if (timer_start)
        {
            //Timer_Text.GetComponent<TextMeshProUGUI>().text = timer.ToString("+#.#;-#.#");
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer_start = false;
            //Timer_Text.SetActive(false);
            Detonate();
        }
    }

    public void triggerGrenade() {
        timer_start = true;
        grenadeAnim.Play("GrenadeDetonate", -1, 0f);
    }

    // public void Active()
    // {
    //     Trigger.GetComponent<MeshRenderer>().material = active_mat;
    //     Line1.GetComponent<MeshRenderer>().material = active_mat;
    //     Line2.GetComponent<MeshRenderer>().material = active_mat;
    //     Line3.GetComponent<MeshRenderer>().material = active_mat;
    //     timer_start = true;
    //     Timer_Text.SetActive(true);
    // }

    void Detonate()
    {
        GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
