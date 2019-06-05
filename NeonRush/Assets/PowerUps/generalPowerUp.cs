using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalPowerUp : MonoBehaviour
{

    float speed = 10.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Jajaaaaaaaaaaaaaaaaaaaaaa");
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
