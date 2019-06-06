using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;



public class TileManager : generalManager
{
    public GameObject[] tilePrefabList;
    public GameObject tilePrefab;               // Ref. MasterTile prefab
    public GameObject currentTile;              // Ref. Clones de MasterTile 
    //public GameObject powerUpSalto;
    public GameObject[] listaPowerUps;

    public bool isFirst = true;
    public bool coca = true;
    public bool coca2 = true;

    [SerializeField]
    private Vector3 gravedad = Physics.gravity;

    [SerializeField]
    Queue<GameObject> tilesQue = new Queue<GameObject>();



    public int index = 0;                       // Esto era para ver el número de bloque y renombrarlo, da un poco igual
    [SerializeField]
    public int counter = 0;                     // Cada doce se genera un camChanger

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo

    private tileGenerator tileGen;              // Ref. al tileGenerator
    private CoroutineWithData coroutineGO;      // Ref. a la clase anexa de corrutinas para acceder al valor devuelto

    public ConcurrentQueue<GameObject> colaTilesActivos;

    bool semaforoCutre = false;


    // Start is called before the first frame update
    void Start()
    {
        colaTilesActivos = new ConcurrentQueue<GameObject>();
        tileGen = gameObject.GetComponent<tileGenerator>();

        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        for(int i = 0; i < 5; i++) Spawner();

        semaforoCutre = false;

        isFirst = false;

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

        //gravedad = stageMode.getGravity();
        gravedad = Physics.gravity;
    }


    public void reSpawnTiles()
    {
        isFirst = true;
        Spawner();
        currentTile.transform.GetComponent<TileScript>().setLandState(true);
        //currentTile.transform.GetComponent<TileScript>().setLandTile(true);
        isFirst = false;
    }



    public void Spawner(){

        while (semaforoCutre == true);

        semaforoCutre = true;

        int rndPrefab = Random.Range(0, tilePrefabList.Length);
        
        //Probamos a meter una pila intermedia que contenga todos los GameObjects que se deben ir spawneando
        //  -La diferencia ahora es que el Instantiate pilla el primero de la cola así no se turbia con las posiciones al mezclar con el salto.
        //  -Sigue fallando xd
        //  -Para revertir cambios únicamente cambiar tilesQue.Dequeue() por tilePrefabList[rndPrefab]
        
        tilesQue.Enqueue(tilePrefabList[rndPrefab]);

        index++;
        counter++;

        GameObject aux = currentTile;

        int randomBuilder = Random.Range(0,5);

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

        if (counter < 8)
        {

            //Metemos los tiles que vamos creando en una lista para saber cual es el ultimo hijo para teletransportar al jugador alli con el powerup
            //aux.transform.SetParent(GameObject.Find("ListaHijos").transform);
            

            int rnd = Random.Range(0, 3);
            //int rndTex = Random.Range(0, 6);

            Vector3 posOrigin = aux.transform.GetChild(rnd).position; //+ new Vector3(0.0f, 4.0f, 0.0f); ////////MOVIDA QUITAR
            Vector3 posOriginInsta = aux.transform.GetChild(rnd).position;


            // AL generar varias de golpe obviamos la caída y las generamos en su sitio directamente
            if (isFirst)
            {
                /*coroutineGO = new CoroutineWithData(this, tileGen.BuildS(posOriginInsta, stageMode));
                currentTile = (GameObject)coroutineGO.result;
                */
                currentTile = tileGen.MasterBuilder(posOriginInsta, stageMode, randomBuilder);
                //currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOriginInsta, Quaternion.Euler(stageMode.getBO()));
            }
            else
            {
                /*coroutineGO = new CoroutineWithData(this, tileGen.BuildS(posOriginInsta, stageMode));
                currentTile = (GameObject)coroutineGO.result;
                */
                currentTile = tileGen.MasterBuilder(posOriginInsta, stageMode, randomBuilder);

                //currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getBO()));
            }

            //
            currentTile.name = "Tile " + index;

            //Debug.Log("Pieza existe? " + currentTile.name + ", " + stageMode.getNameAndKey());

            // Marcamos el modo de juego y establecemos tanto el apdre como el punto de anclaje que será el destino de la nueva ficha
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);


             
            //Con un 10% de probabilidad spawneamos el power up y nunca en el bloque en el que caemos
            if (Random.Range(0.0f, 1.0f) <= 0.1f && !currentTile.GetComponent<TileScript>().getLandTile())
            {
                int aleatorio = Random.Range(0, 3);
                //aleatorio = 0;
                if (aleatorio == 0) //PowerUp Salto
                {
                    GameObject powerUP = Instantiate(listaPowerUps[0], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);
                    //powerUP.transform.SetParent(currentTile.transform);
                    //Asociamos el game object de la pieza en la que esta el powerup
                    powerUP.GetComponent<saltoPowerUp>().tileAsociado = currentTile;
                }
                else if (aleatorio == 1) //PowerUp disminuir velocidad
                {
                    if (GameObject.Find("Player").GetComponent<scirp>().velocidadReducida == false)
                        Instantiate(listaPowerUps[1], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);
                }
                else
                {
                    if (GameObject.Find("Player").GetComponent<scirp>().cegado == false)
                        Instantiate(listaPowerUps[2], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);
                }
            }
            
            
        } 
        else if (counter >= 8)
        {
            int attachPos;

            // Comprobamos que modos son compatibles con el actual
            if (stageMode.getCollindantModes().Count == 1)
            {
                // Esto implica que solo tiene conexión por su izquierda, de modo que el attach
                // a tener en cuenta es el 3 hijo
                attachPos = 3;
                Vector3 posOrigin = currentTile.transform.GetChild(attachPos).position /* + new Vector3(0.0f, 4.0f, 0.0f)*/;

                /*coroutineGO = new CoroutineWithData(this, tileGen.BuildL(posOrigin, stageMode.getCollindantModes()[0]));
                currentTile = (GameObject)coroutineGO.result;
                */
                currentTile = tileGen.MasterBuilder(posOrigin, stageMode.getCollindantModes()[0], randomBuilder);

                //currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());

            }
            else
            {
                // Al tener también la posibilidad de generarse al frente, se decide de form aleatoria
                attachPos = Random.Range(3, 6);
                Vector3 posOrigin = currentTile.transform.GetChild(attachPos).position /*+ new Vector3(0.0f, 4.0f, 0.0f)*/;


                if (attachPos == 3)
                {
                    /*coroutineGO = new CoroutineWithData(this, tileGen.BuildL(posOrigin, stageMode.getCollindantModes()[0]));
                    currentTile = (GameObject)coroutineGO.result;*/
                    currentTile = tileGen.MasterBuilder(posOrigin, stageMode.getCollindantModes()[0], randomBuilder);

                    //currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                    currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                    updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
                }
                else
                {
                    /*coroutineGO = new CoroutineWithData(this, tileGen.BuildL(posOrigin, stageMode.getCollindantModes()[1]));
                    currentTile = (GameObject)coroutineGO.result;
                    */
                    currentTile = tileGen.MasterBuilder(posOrigin, stageMode.getCollindantModes()[1], randomBuilder);

                    //currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[1].getBO()));
                    currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[1].getNameAndKey());
                    updateStageMode(stageMode.getCollindantModes()[1].getNameAndKey());
                }

            }


            currentTile.tag = "Changer";
            currentTile.name = "CamChanger Tile " + index;

            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(attachPos);

            counter = 0;
        }

        //currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);
        currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

        colaTilesActivos.Enqueue(currentTile);

        semaforoCutre = false;

    }

    public Vector3 getGrabity(){
        return stageMode.getGravity();
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
