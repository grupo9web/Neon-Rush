using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEmision : MonoBehaviour
{
    // Start is called before the first frame update

    public Material material;

    void Awake()
    {
        material.EnableKeyword ("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
