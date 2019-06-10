using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : generalManager
{

    #region Data

    public bool isFirst = true;


    [SerializeField]
    private Vector3 gravedad = Physics.gravity;

    public GameObject currentTile;              // Ref. Clones de MasterTile 

    private GameObject branchLeft;
    private GameObject branchFront;

    public int index = 0;                       // Esto era para ver el número de bloque y renombrarlo, da un poco igual
    [SerializeField]
    public int counter = 0;                     // Cada doce se genera un camChanger

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo

    private tileGenerator tileGen;              // Ref. al tileGenerator
    private CoroutineWithData coroutineGO;      // Ref. a la clase anexa de corrutinas para acceder al valor devuelto

    private List<int> attachPoints = new List<int>();
    private bool parentIsT = false;

    private List<GameObject> leftWRoute = new List<GameObject>();
    private List<GameObject> frontWRoute = new List<GameObject>();

    private Queue<GameObject> deleteRoute = new Queue<GameObject>();
    private Queue<int> colores = new Queue<int>();
    private Queue<int> auxColor = new Queue<int>();

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        tileGen = gameObject.GetComponent<tileGenerator>();

        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        for (int i = 0; i < 5; i++) Spawnerv4();

        isFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        gravedad = Physics.gravity;
    }


    #region Generación de tiles

    public void Spawnerv4()
    {

        GameObject aux = currentTile;

        for (int i = 0; i < 5; i++)
            colores.Enqueue(Random.Range(0,5));

        // Pieza aleatoria para construir (inlcuye la T cuando isFirst = true)
        int randomBuilder;
        if (isFirst)
            randomBuilder = UnityEngine.Random.Range(0, 4);
        else
            randomBuilder = UnityEngine.Random.Range(0, 5);

        int rndAttach = UnityEngine.Random.Range(0, 3);


        coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(rndAttach).position, stageMode, randomBuilder, (index++), colores));
        currentTile = (GameObject)coroutineGO.result;

        currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
        currentTile.GetComponent<TileScript>().setTile(aux);

        if (randomBuilder == 4)
        {
            parentIsT = true;
            foreach (Transform tileBT in GameObject.Find("ListaHijos").transform)
            {
                tileBT.gameObject.GetComponent<TileScript>().setSimonBifurcado(true);
            }

            currentTile.name = "T tile Bif. " + (index);
            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

            int randomWay = Random.Range(0, 2);

            genBifurcacion(currentTile, randomWay);
        }
        else
        {
            currentTile.name = "Tile " + (index);
            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);
        }


    }


    public void genBifurcacion(GameObject originTile, int correctOne)
    {

        // Recogemos todos los colores anteriores (de la T hasta otras tres piezas anteriores)
        Queue<int> colorList = new Queue<int>();

        IEnumerator listaHijos = GameObject.Find("ListaHijos").transform.GetEnumerator();

        // El don simon comenzará de forma aleatoria en la propia T o en alguna de las 4 piezas anteriores
        for (int rndSimonStart = Random.Range(3, 5); rndSimonStart > 0; rndSimonStart--)
            listaHijos.MoveNext();
        

        Transform simonStart = (Transform)listaHijos.Current;
        simonStart.gameObject.GetComponent<TileScript>().inicioSimon();

        do
        {
            Transform aux = (Transform)listaHijos.Current;

            foreach (int i in aux.gameObject.GetComponent<TileScript>().getColoresPadre())
            {
                colorList.Enqueue(i);
            }

        } while (listaHijos.MoveNext());



        Debug.Log("Se me ha quedado una lista de colores de " + colorList.Count);

        frontWRoute.Clear();
        leftWRoute.Clear();

        originTile.GetComponent<TileScript>().setSoyUnaT(true);
        // Añadimos la info necesaria al tile T bifurcado
        originTile.GetComponent<TileScript>().setAuxColor(colorList);

        currentTile = originTile;

        // Rama izquierda

        for (int i = 0; i < 5; i++)
        {

            if (correctOne == 0)
            {
                branchLeft = currentTile;
                int randomBuilder = UnityEngine.Random.Range(0, 4);

                int rnd = Random.Range(0, 1);       // Attach izquierdo

                Vector3 posOriginInsta = branchLeft.transform.GetChild(rnd).position;

                if (originTile.GetComponent<TileScript>().getColorAuxSize() != 0)
                {
                    coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), originTile.GetComponent<TileScript>().getColorAux() ));
                }
                else
                {
                    for (int x = 0; x < 4; x++)
                        auxColor.Enqueue(Random.Range(0, 5));

                    coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
                }

                currentTile = (GameObject)coroutineGO.result;

                currentTile.name = "Tile " + index;
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
                currentTile.GetComponent<TileScript>().setTile(branchLeft);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

                currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

                //
                if (i == 0)
                    currentTile.GetComponent<TileScript>().inicioSimon();

                leftWRoute.Add(currentTile);
            }
            else
            {

                for (int x = 0; x < 4; x++)
                    auxColor.Enqueue(Random.Range(0, 5));

                branchLeft = currentTile;
                int randomBuilder = UnityEngine.Random.Range(0, 4);

                int rnd = Random.Range(0, 1);       // Attach izquierdo

                Vector3 posOriginInsta = branchLeft.transform.GetChild(rnd).position;

                coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
                currentTile = (GameObject)coroutineGO.result;

                currentTile.name = "Tile " + index;
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
                currentTile.GetComponent<TileScript>().setTile(branchLeft);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);


                //
                if (i == 0)
                    currentTile.GetComponent<TileScript>().inicioSimon();

                leftWRoute.Add(currentTile);

            }


        }

        branchLeft = currentTile;

        currentTile = originTile;

        // Rama delantera

        for (int i = 0; i < 5; i++)
        {

            if (correctOne == 1)
            {
                branchFront = currentTile;
                int randomBuilder = UnityEngine.Random.Range(0, 4);

                int rnd = Random.Range(1, 3);  // Front attaches

                Vector3 posOriginInsta = branchFront.transform.GetChild(rnd).position;

                if (originTile.GetComponent<TileScript>().getColorAuxSize() != 0)
                {
                    coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), originTile.GetComponent<TileScript>().getColorAux()));
                }
                else
                {
                    for (int x = 0; x < 4; x++)
                        auxColor.Enqueue(Random.Range(0, 5));

                    coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
                }

                currentTile = (GameObject)coroutineGO.result;

                currentTile.name = "Tile " + index;
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
                currentTile.GetComponent<TileScript>().setTile(branchFront);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

                currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

                //
                if (i == 0)
                    currentTile.GetComponent<TileScript>().inicioSimon();

                frontWRoute.Add(currentTile);
            }
            else
            {

                for (int x = 0; x < 4; x++)
                    auxColor.Enqueue(Random.Range(0, 5));

                branchFront = currentTile;
                int randomBuilder = UnityEngine.Random.Range(0, 4);

                int rnd = Random.Range(1, 3);

                Vector3 posOriginInsta = branchFront.transform.GetChild(rnd).position;

                coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
                currentTile = (GameObject)coroutineGO.result;

                currentTile.name = "Tile " + index;
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
                currentTile.GetComponent<TileScript>().setTile(branchFront);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);


                //
                if (i == 0)
                    currentTile.GetComponent<TileScript>().inicioSimon();


                frontWRoute.Add(currentTile);
            }

        }


        branchFront = currentTile;


        if (correctOne == 0)
        {
            currentTile = branchLeft;

            foreach (GameObject go in frontWRoute)
                deleteRoute.Enqueue(go);
        }
        else
        {
            currentTile = branchFront;

            foreach (GameObject go in leftWRoute)
                deleteRoute.Enqueue(go);
        }

        
    }

    public void RemoveWrongWay()
    {
        for (int i = 0; i < 5; i++)
        {        
            GameObject aux = deleteRoute.Dequeue();

            // Caida
            aux.GetComponent<Rigidbody>().useGravity = true;
            aux.GetComponent<Rigidbody>().isKinematic = false;

            Destroy(aux, 1);
        }
    }

    #endregion



    public void updateStageMode(string key)
    {
        stageMode = mode[key];
    }


}
