using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Renderer towerRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeTransparent(bool transparent)
    {
        Color color = towerRenderer.material.color;
        if (transparent)
            color.a = 0.5f;
        else
            color.a = 1f;

        towerRenderer.material.color = color;
    }
}
