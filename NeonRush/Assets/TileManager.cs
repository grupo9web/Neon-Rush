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

    public int index = 0;                       // Esto era para ver el número de bloque y renombrarlo, da un poco igual
    public int counter = 0;                     // Cada doce se genera un camChanger

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
        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        for(int i = 0; i < 5; i++) Spawner();

        tipo = platType.classicY;
    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
            coca = false;
        if (Input.GetKeyDown("r"))
            coca2 = false;
      */

    }


    public void reSpawnTiles()
    {
        isFirst = true;
        Spawner();
        Debug.Log("Posiciooooooooooooooon: " + currentTile.transform.GetComponent<TileScript>().getLandTile());
        currentTile.transform.GetComponent<TileScript>().setLandState(true);
        //currentTile.transform.GetComponent<TileScript>().setLandTile(true);
        isFirst = false;
    }



    public void Spawner(){


        int rndPrefab = Random.Range(0, tilePrefabList.Length);

        index++;
        counter++;

        GameObject aux = currentTile;

        #region Debug
        // Esto es a modo de debug para generar la que queremos
        // Con r ponemos las de cambio de eje y con el espacio hacemos el tubo alrededor del eje
        /*
        if (coca && coca2)
        {

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



            //Con un 10% de probabilidad spawneamos el power up y nunca en el bloque en el que caemos
            if (Random.Range(0.0f, 1.0f) <= 0.1f && !currentTile.GetComponent<TileScript>().getLandTile())
            {
                //Instantiate(powerUpSalto, currentTile.transform.GetChild(9).transform.position - new Vector3(0, 4.0f, 0), Quaternion.identity);
            }
         
        }
        else if (!coca)
        {
            // Attach al que se une la nueva pieza
            int rnd = 3;

            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);


            if (rnd == 3)
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
            }
            else
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[1].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[1].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[1].getNameAndKey());
            }

            currentTile.GetComponent<TileScript>().setType(platType.camChanger);


            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            tipo = platType.classicZ;


            coca = true; 
        }
        else if (!coca2)
        {

            // Attach al que se une la nueva pieza
            //int rnd = Random.Range(3, 6);
            int rnd = 4;
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);


            if (rnd == 3)
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
            }
            else
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[1].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[1].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[1].getNameAndKey());
            }

            currentTile.GetComponent<TileScript>().setType(platType.camChanger);


            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            tipo = platType.classicZ;

            coca2 = true;

        }*/
        #endregion

        if (counter < 12)
        {

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
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);



            //Con un 10% de probabilidad spawneamos el power up y nunca en el bloque en el que caemos
            if (Random.Range(0.0f, 1.0f) <= 0.1f && !currentTile.GetComponent<TileScript>().getLandTile())
            {
                Instantiate(powerUpSalto, currentTile.transform.GetChild(9).transform.position /* - new Vector3(0, 4.0f, 0)*/, currentTile.transform.GetChild(9).transform.rotation);
                //currentTile.transform.GetChild(10).gameObject.SetActive(true);

            }

        } 
        else if (counter >= 12)
        {
            int attachPos;

            // Comprobamos que modos son compatibles con el actual
            if (stageMode.getCollindantModes().Count == 1)
            {
                // Esto implica que solo tiene conexión por su izquierda, de modo que el attach
                // a tener en cuenta es el 3 hijo
                attachPos = 3;
                Vector3 posOrigin = currentTile.transform.GetChild(attachPos).position + new Vector3(0.0f, 4.0f, 0.0f);

                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());

            }
            else
            {
                // Al tener también la posibilidad de generarse al frente, se decide de form aleatoria
                attachPos = Random.Range(3, 6);
                Vector3 posOrigin = currentTile.transform.GetChild(attachPos).position + new Vector3(0.0f, 4.0f, 0.0f);


                if (attachPos == 3)
                {
                    currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                    currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                    updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
                }
                else
                {
                    currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[1].getBO()));
                    currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[1].getNameAndKey());
                    updateStageMode(stageMode.getCollindantModes()[1].getNameAndKey());
                }

            }

            // Marcamos la nueva plataforma como cambio de cámara para que al pisarla el jugador se produzca un efecto
            currentTile.GetComponent<TileScript>().setType(platType.camChanger);
            currentTile.tag = "Changer";

            currentTile.name = "CamChanger Tile " + index;


            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(attachPos);

            // El resto de plataformas se genera de forma normal
            tipo = platType.classicZ;

            counter = 0;
        }

        //currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);


    }



    public void updateStageMode(string key)
    {
        stageMode = mode[key];
    }

    public bool getCoca()
    {
        return coca;
    }

}
