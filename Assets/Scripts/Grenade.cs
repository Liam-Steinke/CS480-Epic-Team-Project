using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Material active_mat;
    public GameObject Trigger;
    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Nade;
    public GameObject Timer_Text;
    public bool timer_start = false;
    public float timer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_start)
        {
            Timer_Text.GetComponent<TextMeshProUGUI>().text = timer.ToString("+#.#;-#.#");
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer_start= false;
            Timer_Text.SetActive(false);
            Detonate();
        }
    }

    public void Active()
    {
        Trigger.GetComponent<MeshRenderer> ().material = active_mat;
        Line1.GetComponent<MeshRenderer> ().material = active_mat;
        Line2.GetComponent<MeshRenderer> ().material = active_mat;
        Line3.GetComponent<MeshRenderer> ().material = active_mat;
        timer_start = true;
        Timer_Text.SetActive(true);
    }

    void Detonate()
    {
        Destroy(Nade);
    }
}
