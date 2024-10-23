using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySignifier : MonoBehaviour
{
    public float scale_change = 1.1f;
    private Vector3 original_scale;
    public Material highlight_mat;
    private Material old_mat;
    private MeshRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        original_scale = transform.localScale;
        render = GetComponent<MeshRenderer>();
        old_mat = render.material;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight(){
        transform.localScale = original_scale * scale_change;
        render.material = highlight_mat;
    }

    public void Unhighlight(){
        transform.localScale = original_scale;
        render.material = old_mat;
    }
}
