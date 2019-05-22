using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : generalManager
{
    public GameObject[] tilePrefabList;
    public GameObject tilePrefab;               // Ref. MasterTile prefab
    public GameObject currentTile;              // Ref. Clones de MasterTile 

    public bool isFirst = true;
    public bool coca = true;


    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo


    public enum platType
    {
        classicY,
        classicX,
        classicZ,
        camChanger,
    }

    public platType tipo;

    public Vector3 gravitiy = new Vector3( 0, 9.8f, 0);

    public Texture[] cosmicTex;


    // Start is called before the first frame update
    void Start()
    {
        if (mode.ContainsKey("horizontal"))
            stageMode = mode["horizontal"];


        for (int i = 0; i < 5; i++) Spawner();
        isFirst = false;


        tipo = platType.classicY;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            coca = false;
    }

    public void Spawner(){


        int rndPrefab = Random.Range(0, tilePrefabList.Length);

        if (coca)
        {
            GameObject aux = currentTile;

            int rnd = Random.Range(0, 2);
            int rndTex = Random.Range(0, 6);

            if (isFirst)
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], currentTile.transform.GetChild(rnd).transform.position, Quaternion.identity);

                currentTile.GetComponent<TileScript>().setPos(currentTile.transform.position);

            }
            else
            {
                Vector3 parentPos = currentTile.transform.position;
                Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);
                Vector3 posDestiny = currentTile.transform.GetChild(rnd).position;

                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

                //currentTile.GetComponent<Renderer>().material.SetTexture("_MainTex", cosmicTex[rndTex]);

                if (rnd == 0)
                {
                    //currentTile.GetComponent<Renderer>().material.color = Color.blue;
                    currentTile.GetComponent<TileScript>().setType(tipo);
                }
                else
                {
                    //currentTile.GetComponent<Renderer>().material.color = Color.red;
                    currentTile.GetComponent<TileScript>().setType(tipo);

                    //currentTile.transform.Rotate( -90, 0, 0);
                }

                currentTile.GetComponent<TileScript>().setPos(posDestiny);
                currentTile.GetComponent<TileScript>().setParent(parentPos);

                currentTile.GetComponent<TileScript>().setTile(aux);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            }
        }
        else
        {
            int rnd = Random.Range(3, 6);

            GameObject aux = currentTile;

            Vector3 parentPos = currentTile.transform.position;
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);
            Vector3 posDestiny = currentTile.transform.GetChild(rnd).position;


            currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setType(platType.camChanger);

            //currentTile.GetComponent<TileScript>().setPos(posDestiny);
            //currentTile.GetComponent<TileScript>().setParent(parentPos);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);


            tipo = platType.classicX;

            coca = true; ;
        }

       

    }

}
