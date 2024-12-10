using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public float healValue = 4.0f;
    private Player player;
    public GameObject[] glowParts;
    public Material usedMaterial;


    bool healed = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Heal() {
        if (!healed) {
            player.Heal(healValue);
            Debug.Log("I HEALED DA GUY");
            foreach (GameObject thing in glowParts) {
                thing.GetComponent<MeshRenderer>().material = usedMaterial;
            }
            healed = true;
        }
    }

}
