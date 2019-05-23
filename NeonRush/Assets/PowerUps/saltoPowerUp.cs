using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltoPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.name == "Player")
        {
            /*
            col.gameObject.GetComponent<Actions>().increaseSpeed(speedGain);

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(gameObject, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            */

            print("Funciona LOCOOOOOOOO");
            Destroy(gameObject);

        }
    }
}
