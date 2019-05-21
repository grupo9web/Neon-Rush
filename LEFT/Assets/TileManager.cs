using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour

{
    public GameObject tilePrefab;              // Ref. MasterTile prefab
    public GameObject currentTile;             // Ref. Clones de MasterTile 

    public bool isFirst = true;

    public enum platType
    {
        classic,
        camChanger,
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++) Spawner();
        isFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawner(){

        int rnd = Random.Range(0,2);

        if (isFirst)
        {
            currentTile = (GameObject)Instantiate(tilePrefab, currentTile.transform.GetChild(rnd).transform.position, Quaternion.identity);

            currentTile.GetComponent<TileScript>().setPos(currentTile.transform.position);

        }
        else
        {
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3( 0.0f, 4.0f, 0.0f);
            Vector3 posDestiny = currentTile.transform.GetChild(rnd).position;
      
            currentTile = (GameObject)Instantiate(tilePrefab, posOrigin, Quaternion.identity);



            if (GameObject.Find("Player").GetComponent<scirp>().score == 1000){
                currentTile.GetComponent<Renderer>().material.color = Color.green;
                currentTile.GetComponent<TileScript>().setType(platType.camChanger);
                currentTile.transform.Rotate(-90, 0, 0);
            }else{
                if (rnd == 0)
                {
                    currentTile.GetComponent<Renderer>().material.color = Color.blue;
                    currentTile.GetComponent<TileScript>().setType(platType.classic);
                }
                else
                {
                    currentTile.GetComponent<Renderer>().material.color = Color.red;
                    currentTile.GetComponent<TileScript>().setType(platType.classic);
                }
            }

            currentTile.GetComponent<TileScript>().setPos(posDestiny);

        }

    }

}
