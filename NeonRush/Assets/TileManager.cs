using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : generalManager
{
    public GameObject[] tilePrefabList;
    public GameObject tilePrefab;               // Ref. MasterTile prefab
    public GameObject currentTile;              // Ref. Clones de MasterTile 
    public GameObject powerUpSalto;

    public bool isFirst = true;
    public bool coca = true;
    public bool coca2 = true;

    public int index = 0;
    private int guarrada = 0;

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo


    public enum platType
    {
        classicY,
        classicX,
        classicZ,
        camChanger,
    }

    public platType tipo;

    public Texture[] cosmicTex;


    // Start is called before the first frame update
    void Start()
    {
        if (mode.ContainsKey("horizontal"))
            stageMode = mode["horizontal"];

        reSpawnTiles();

        tipo = platType.classicY;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            coca = false;
        if (Input.GetKeyDown("v"))
            coca2 = false;

    }


    public void reSpawnTiles()
    {
        isFirst = true;
        for (int i = 0; i < 5; i++) Spawner();
        isFirst = false;
    }



    public void Spawner(){


        int rndPrefab = Random.Range(0, tilePrefabList.Length);

        index++;
        Debug.Log("Llamas a spawner pero no entras al otro lao?"); 

        if (coca && coca2)
        {
            GameObject aux = currentTile;

            //Metemos los tiles que vamos creando en una lista para saber cual es el ultimo hijo para teletransportar al jugador alli con el powerup
            aux.transform.SetParent(GameObject.Find("ListaHijos").transform);

            int rnd = Random.Range(0, 3);
            //int rndTex = Random.Range(0, 6);


            Vector3 posOrigin = aux.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);
            Vector3 posOriginInsta = aux.transform.GetChild(rnd).position;


            // AL generar varias de golpe obviamos la caída y las generamos en su sitio directamente
            if (isFirst)
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOriginInsta, Quaternion.Euler(stageMode.getBO()));
            else
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getBO()));


            //
            currentTile.name = "Tile " + index;

            // Marcamos el modo de juego y establecemos tanto el apdre como el punto de anclaje que será el destino de la nueva ficha
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);



            Debug.Log("Hermano del rey");

            //Con un 10% de probabilidad spawneamos el power up y nunca en el bloque en el que caemos
            if (Random.Range(0.0f, 1.0f) <= 0.1f && !currentTile.GetComponent<TileScript>().getLandTile())
            {
                Instantiate(powerUpSalto, currentTile.transform.GetChild(9).transform.position - new Vector3(0, 4.0f, 0), Quaternion.identity);

            }
         
        }
        else if (!coca)
        {
            // Attach al que se une la nueva pieza
            int rnd = Random.Range(3, 6);

            GameObject aux = currentTile;
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);

            currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            Debug.Log(rnd + " en fin " + aux.transform.GetChild(rnd).name);

            // Al marcarlo como camChanger provoca el cambio de modo
            currentTile.GetComponent<TileScript>().setType(platType.camChanger);

            // En caso de que sea 3 se generará a la izquierda, si es 4 o 5 por delante
            if (rnd == 3)
            {
                tipo = platType.classicZ;
                updateStageMode("verticalZ");
            }
            else
            {
                tipo = platType.classicX;
                updateStageMode("verticalX");
            }


            coca = true; 
        }
        else if (!coca2)
        {

            // Attach al que se une la nueva pieza
            //int rnd = Random.Range(3, 6);
            int rnd = 4;
            GameObject aux = currentTile;
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);

            currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            Debug.Log(rnd + " en fin " + aux.transform.GetChild(rnd).name);

            // Al marcarlo como camChanger provoca el cambio de modo
            currentTile.GetComponent<TileScript>().setType(platType.camChanger);

            // En caso de que sea 3 se generará a la izquierda, si es 4 o 5 por delante
            if (rnd == 3)
                tipo = platType.classicZ;
            else
                tipo = platType.classicX;
        }

        //currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

        guarrada++;

    }



    public void updateStageMode(string key)
    {
        Debug.Log("El jauja de TileManager");
        stageMode = mode[key];
    }
}
