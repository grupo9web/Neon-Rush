using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;



public class TileManager : generalManager
{
    public GameObject[] tilePrefabList;
    public GameObject tilePrefab;               // Ref. MasterTile prefab
    public GameObject currentTile;              // Ref. Clones de MasterTile 
    public GameObject[] listaPowerUps;
    public int numeroHijos;

    public bool isFirst = true;

    [SerializeField]
    private Vector3 gravedad = Physics.gravity;

    [SerializeField]
    Queue<GameObject> tilesQue = new Queue<GameObject>();



    public int index = 0;                       // Esto era para ver el número de bloque y renombrarlo, da un poco igual
    [SerializeField]
    public int counter = 0;                     // Cada doce se genera un camChanger

    int nextNumber = -1;

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo

    private tileGenerator tileGen;              // Ref. al tileGenerator

    public ConcurrentQueue<GameObject> colaTilesActivos;

    bool semaforoCutre = false;


    // Start is called before the first frame update
    void Start()
    {
        colaTilesActivos = new ConcurrentQueue<GameObject>();
        tileGen = gameObject.GetComponent<tileGenerator>();

        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        for (int i = 0; i < 5; i++) Spawner();

        semaforoCutre = false;

        isFirst = false;

    }


    // Update is called once per frame
    void Update()
    {    
        gravedad = Physics.gravity;
        numeroHijos = GameObject.Find("ListaHijos").transform.childCount;
    }


    public void reSpawnTiles()
    {
        isFirst = true;
        Spawner();
        currentTile.transform.GetComponent<TileScript>().setLandState(true);
        isFirst = false;
    }



    public void Spawner()
    {

        while (semaforoCutre == true) ;

        semaforoCutre = true;

        int rndPrefab = Random.Range(0, tilePrefabList.Length);

        tilesQue.Enqueue(tilePrefabList[rndPrefab]);

        index++;
        counter++;

        GameObject aux = currentTile;

        int randomBuilder = Random.Range(0, 5);
    

        if (counter < 8)
        {

            int rnd = Random.Range(0, 3);

            Vector3 posOrigin = aux.transform.GetChild(rnd).position; 

            currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getBO()));
            currentTile.name = "Tile " + index;

            // Marcamos el modo de juego y establecemos tanto el apdre como el punto de anclaje que será el destino de la nueva ficha
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);



            //Con un 10% de probabilidad spawneamos el power up y nunca en el bloque en el que caemos
            if (Random.Range(0.0f, 1.0f) <= 0.2f && !currentTile.GetComponent<TileScript>().getLandTile())
            {
                if (nextNumber == -1) //Primera ejecucion
                {
                    int aleatorio = Random.Range(0, 3);
                    if (aleatorio == 0) //PowerUp Salto
                    {
                        GameObject powerUP = Instantiate(listaPowerUps[0], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);
                        powerUP.GetComponent<saltoPowerUp>().tileAsociado = currentTile;

                        //Generamos un número aleatorio entre 1 y 2 para seleccionar cual será el próximo power up
                        nextNumber = Random.Range(1, 3);
                    }
                    else if (aleatorio == 1) //PowerUp disminuir velocidad
                    {
                        if (GameObject.Find("Player").GetComponent<scirp>().velocidadReducida == false)
                            Instantiate(listaPowerUps[1], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);

                        nextNumber = Random.Range(1, 3);
                        if (nextNumber == 1)
                            nextNumber = 0;
                    }
                    else if (aleatorio == 2)
                    {

                        if (GameObject.Find("Player").GetComponent<scirp>().cegado == false)
                            Instantiate(listaPowerUps[2], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);

                        nextNumber = Random.Range(0, 2);
                    }

                }
                else
                {
                    int aleatorio = Random.Range(0, 3);
                    if (aleatorio == 0 && nextNumber == 0) //PowerUp Salto
                    {
                        GameObject powerUP = Instantiate(listaPowerUps[0], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);
                        //Asociamos el game object de la pieza en la que esta el powerup
                        powerUP.GetComponent<saltoPowerUp>().tileAsociado = currentTile;

                        //Generamos un número aleatorio entre 1 y 2 para seleccionar cual será el próximo power up
                        nextNumber = Random.Range(1, 3);
                    }
                    else if (aleatorio == 1 && nextNumber == 1) //PowerUp disminuir velocidad
                    {
                        if (GameObject.Find("Player").GetComponent<scirp>().velocidadReducida == false)
                            Instantiate(listaPowerUps[1], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);

                        nextNumber = Random.Range(1, 3);
                        if (nextNumber == 1)
                            nextNumber = 0;
                    }
                    else if (aleatorio == 2 && nextNumber == 2)
                    {

                        if (GameObject.Find("Player").GetComponent<scirp>().cegado == false)
                            Instantiate(listaPowerUps[2], currentTile.transform.GetChild(9).transform.position, currentTile.transform.GetChild(9).transform.rotation);

                        nextNumber = Random.Range(0, 2);
                    }
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

                currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
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
                    currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[0].getBO()));
                    currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                    updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
                }
                else
                {
                    currentTile = (GameObject)Instantiate(tilesQue.Dequeue(), posOrigin, Quaternion.Euler(stageMode.getCollindantModes()[1].getBO()));
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


    public void updateStageMode(string key)
    {
        stageMode = mode[key];
    }

}
