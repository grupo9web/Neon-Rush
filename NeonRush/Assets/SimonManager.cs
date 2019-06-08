using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : generalManager
{

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

    List<GameObject> leftWRoute = new List<GameObject>();
    List<GameObject> frontWRoute = new List<GameObject>();

    Queue<GameObject> deleteRoute = new Queue<GameObject>();
    private Queue<int> colores = new Queue<int>();
    private Queue<int> auxColor = new Queue<int>();

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        tileGen = gameObject.GetComponent<tileGenerator>();

        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        for (int i = 0; i < 5; i++) Spawnerv4();
            //conSpawner(parentIsT, null);
            //StartCoroutine(concurrentSpawner(parentIsT));
        //Spawner();


        isFirst = false;

    }


    // Update is called once per frame
    void Update()
    {
        gravedad = Physics.gravity;

        if (Input.GetKeyDown("space"))
            currentTile.GetComponent<TileScript>().setSimonBifurcado(true);
    }


    public void reSpawnTiles()
    {
        isFirst = true;
        //StartCoroutine(concurrentSpawner(parentIsT));
        //Spawner();
        //conSpawner(parentIsT, null);

        Spawnerv4();

        currentTile.transform.GetComponent<TileScript>().setLandState(true);
        isFirst = false;
    }




    // 4 intento

    public void Spawnerv4()
    {

        Debug.Log("Concurrent llamadas que me joden lo bailado");
        GameObject aux = currentTile;

        for (int i = 0; i < 4; i++)
            colores.Enqueue(Random.Range(0,5));


        // Pieza aleatoria para construir (inlcuye la T)
        int randomBuilder = UnityEngine.Random.Range(0, 5);

        int rndAttach = UnityEngine.Random.Range(0, 3);

        Debug.Log("Metodo tocho de generacion de piezas " + colores.Count);


        coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(rndAttach).position, stageMode, randomBuilder, (index++), colores));
        currentTile = (GameObject)coroutineGO.result;

        Debug.Log("Y lo que viene a ser dentro? " + currentTile.GetComponent<TileScript>().getColores().Count);


        currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

        currentTile.GetComponent<TileScript>().setTile(aux);

        if (randomBuilder == 4)
        {
            parentIsT = true;
            foreach (Transform tileBT in GameObject.Find("ListaHijos").transform)
            {
                tileBT.gameObject.GetComponent<TileScript>().setSimonBifurcado(true);
            }

            currentTile.name = "Esta es una puta T " + (index);
            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);


            //currentTile.GetComponent<TileScript>().setCOlores(colores);

            int randomWay = Random.Range(0, 2);

            Debug.Log("Antes de bifurcarse ha perdido " + currentTile.GetComponent<TileScript>().getColores().Count);
            genBifurcacion(currentTile, randomWay);
        }
        else
        {
            currentTile.name = "Tile " + (index);
            //currentTile.GetComponent<TileScript>().setCOlores(colores);
            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);
        }


    }


    public void genBifurcacion(GameObject originTile, int correctOne)
    {

        frontWRoute.Clear();
        leftWRoute.Clear();

        originTile.GetComponent<TileScript>().setSoyUnaT(true);

        currentTile = originTile;

        for (int i = 0; i < 3; i++)
        {

            if (i == 0 && correctOne == 0)
            {
                foreach (int m in originTile.GetComponent<TileScript>().getColores())
                    auxColor.Enqueue(m);
            }
            else
            {
                for (int x = 0; x < 4; x++)
                    auxColor.Enqueue(Random.Range(0, 5));
            }

            branchLeft = currentTile;
            int randomBuilder = UnityEngine.Random.Range(0, 4);

            int rnd = Random.Range(0, 1);

            Vector3 posOriginInsta = branchLeft.transform.GetChild(rnd).position;


            Debug.Log("genBifurcacion leftBranch " + auxColor.Count);
            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
            currentTile = (GameObject)coroutineGO.result;

            currentTile.name = "Tile " + index;
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
            //currentTile.GetComponent<TileScript>().setColores(auxColor);
            currentTile.GetComponent<TileScript>().setTile(branchLeft);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

            leftWRoute.Add(currentTile);
        }

        branchLeft = currentTile;

        currentTile = originTile;

        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && correctOne == 1)
            {
                foreach (int m in originTile.GetComponent<TileScript>().getColores())
                    auxColor.Enqueue(m);
            }
            else
            {
                for (int x = 0; x < 4; x++)
                    auxColor.Enqueue(Random.Range(0, 5));
            }

            branchFront = currentTile;
            int randomBuilder = UnityEngine.Random.Range(0, 4);

            int rnd = Random.Range(1, 3);

            Vector3 posOriginInsta = branchFront.transform.GetChild(rnd).position;

            Debug.Log("genBifurcacion frontBranch " + auxColor.Count);
            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(posOriginInsta, stageMode, randomBuilder, (index++), auxColor));
            currentTile = (GameObject)coroutineGO.result;

            currentTile.name = "Tile " + index;
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
            //currentTile.GetComponent<TileScript>().setColores(auxColor);
            currentTile.GetComponent<TileScript>().setTile(branchFront);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);


            frontWRoute.Add(currentTile);
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
        for (int i = 0; i < 3; i++)
        {        
            GameObject aux = deleteRoute.Dequeue();
            Destroy(aux);
        }
    }




    // Toda esta mierda comentada no tocarla. en principio es apra borrar, pero de momento que se quede ahi
    

   /* public void conSpawner(bool bifurcacion, GameObject cloroformo)
    {

        GameObject aux;

        if (cloroformo == null)
            aux = currentTile;
        else
            aux = cloroformo;

        // SI bifurcacion es false, se genera el mundo normal con posibilidad de generar una T
        if (!bifurcacion)
        {
            // Pieza aleatoria para construir (inlcuye la T)
            int randomBuilder = UnityEngine.Random.Range(0, 5);
            int rndAttach = UnityEngine.Random.Range(0, 3);

            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(rndAttach).position, stageMode, randomBuilder, (index++)));
            currentTile = (GameObject)coroutineGO.result;

            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);

            if (randomBuilder == 4)
            {
                parentIsT = true;
                currentTile.name = "Esta es una puta T " + (index);
            }
            else
                currentTile.name = "Tile " + (index);

        }
        else
        {

            // Elegimos al azar dos attaches desde donde crecerán las ramas
            /*for (int j = 0; attachPoints.Count < 2; j++)
            {
                int rndAttach = Random.Range(0, 3);
                if (!attachPoints.Contains(rndAttach) && !attachPoints.Contains(rndAttach + 2) && !attachPoints.Contains(rndAttach + 3) && !attachPoints.Contains(rndAttach - 2) && !attachPoints.Contains(rndAttach - 3))
                    attachPoints.Add(rndAttach);
            }*/
            /*
            attachPoints.Add(0);
            attachPoints.Add(1);


            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(attachPoints[0]).position, stageMode, UnityEngine.Random.Range(0, 4), index++));
            branchLeft = (GameObject)coroutineGO.result;
            branchLeft.name = "Tile Left " + index;

            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(attachPoints[1]).position, stageMode, UnityEngine.Random.Range(0, 4), index++));
            branchFront = (GameObject)coroutineGO.result;
            branchFront.name = "Tile Front " + index;


            branchLeft.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
            branchFront.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            branchLeft.GetComponent<TileScript>().setTile(aux);
            branchFront.GetComponent<TileScript>().setTile(aux);
            */
  /*}


        attachPoints.Clear();
        parentIsT = false;

        // Generamos los dos caminos de forma simultánea
        for (int i = 0; i < 4; i++)
        {

            // Pieza aleatoria para construir (sin T hasta que se hayan generado adecuadamente)
            //int randomBuilder = Random.Range(0, 4);

        }

        currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);

    }


    /*public void generandooooooooooooo()
    {

        Debug.Log("Menuda T más guapa, vamos a generar los dos caminos. Ah que no sé");

        // Tendria q ser tan facil como generar el iquierdo y luego el derecho ahi pimpam fium fium

        GameObject jeje = currentTile;
        GameObject aux;

        for (int i = 0; i < 4; i++)
        {

            aux = currentTile;

            int rnd = Random.Range(0, 1);

            Vector3 posOriginInsta = aux.transform.GetChild(rnd).position;

            currentTile = (GameObject)Instantiate(prefab, posOriginInsta, Quaternion.Euler(stageMode.getBO()));

            currentTile.name = "Tile " + index;
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);
        }

        aux = jeje;

        for (int i = 0; i < 4; i++)
        {


            int rnd = Random.Range(1, 3);

            Vector3 posOriginInsta = aux.transform.GetChild(rnd).position;

            currentTile = (GameObject)Instantiate(prefab, posOriginInsta, Quaternion.Euler(stageMode.getBO()));

            currentTile.name = "Tile " + index;
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);


            aux = currentTile;

        }



    }
    */
    
    // El spawner de Don Simon genera dos caminos al mismo tiempo

   /* public IEnumerator concurrentSpawner(bool bifurcacion)
    {

        GameObject aux = currentTile;

        // SI bifurcacion es false, se genera el mundo normal con posibilidad de generar una T
        if (!bifurcacion)
        {
            // Pieza aleatoria para construir (inlcuye la T)
            int randomBuilder = Random.Range(0, 5);
            int rndAttach = Random.Range(0, 3);

            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(rndAttach).position, stageMode, randomBuilder, (index++)));
            currentTile = (GameObject)coroutineGO.result;

            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);

            if (randomBuilder == 4)
            {
                parentIsT = true;
                currentTile.name = "Esta es una puta T " + (index);
            }
            else
                currentTile.name = "Tile " + (index);

        }
        else
        {

            // Elegimos al azar dos attaches desde donde crecerán las ramas
            /*for (int j = 0; attachPoints.Count < 2; j++)
            {
                int rndAttach = Random.Range(0, 3);
                if (!attachPoints.Contains(rndAttach) && !attachPoints.Contains(rndAttach + 2) && !attachPoints.Contains(rndAttach + 3) && !attachPoints.Contains(rndAttach - 2) && !attachPoints.Contains(rndAttach - 3))
                    attachPoints.Add(rndAttach);
            }*/

        /*
            attachPoints.Add(0);
            attachPoints.Add(1);


            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(attachPoints[0]).position, stageMode, Random.Range(0, 4), index++));
            branchLeft = (GameObject)coroutineGO.result;
            branchLeft.name = "Tile Left " + index;

            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(attachPoints[1]).position, stageMode, Random.Range(0, 4), index++));
            branchFront = (GameObject)coroutineGO.result;
            branchFront.name = "Tile Front " + index;


            branchLeft.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());
            branchFront.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            branchLeft.GetComponent<TileScript>().setTile(aux);
            branchFront.GetComponent<TileScript>().setTile(aux);



            /*
            foreach (int k in attachPoints)
            {

                if (k >= 3)
                {

                    Debug.Log("Aqui no entras ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");



                    if (k == 3)
                        coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(k).position, stageMode.getCollindantModes()[0], Random.Range(0, 4), index));
                    else
                        coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(k).position, stageMode.getCollindantModes()[1], Random.Range(0, 4), index));

                    currentTile = (GameObject)coroutineGO.result;

                    if (k == 3)
                    {
                        currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[0].getNameAndKey());
                        updateStageMode(stageMode.getCollindantModes()[0].getNameAndKey());
                    }
                    else
                    {
                        currentTile.GetComponent<TileScript>().setMode(stageMode.getCollindantModes()[1].getNameAndKey());
                        updateStageMode(stageMode.getCollindantModes()[1].getNameAndKey());
                    }



                    currentTile.tag = "Changer";
                    currentTile.name = "CamChanger Tile " + (index++);

                    currentTile.GetComponent<TileScript>().setTile(aux);
                }
                else
                {

                    Debug.Log("Deberias entrar dos veces");

                    coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(k).position, stageMode, Random.Range(0, 4), index++));
                    currentTile = (GameObject)coroutineGO.result;

                    currentTile.name = "Tile " + index;

                    currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

                    currentTile.GetComponent<TileScript>().setTile(aux);
                }

            }*/

        /*
            attachPoints.Clear();
            parentIsT = false;

            // Generamos los dos caminos de forma simultánea
            for (int i = 0; i < 4; i++)
            {

                // Pieza aleatoria para construir (sin T hasta que se hayan generado adecuadamente)
                //int randomBuilder = Random.Range(0, 4);









            }

        }



        currentTile.transform.SetParent(GameObject.Find("ListaHijos").transform);



        /*
        for (int i = 0; attachPoints.Count < 2; i++)
        {
            int rndAttach = Random.Range(0, 3);
            if (!attachPoints.Contains(rndAttach) && !attachPoints.Contains(rndAttach + 2) && !attachPoints.Contains(rndAttach + 3) && !attachPoints.Contains(rndAttach - 2) && !attachPoints.Contains(rndAttach - 3))
                attachPoints.Add(rndAttach);
        }


        if (currentTile.GetComponent<TileScript>().getSimonBifurcado())
        {
            foreach (int i in attachPoints)
            {
                coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(i).position, stageMode, randomBuilder, index));
                currentTile = (GameObject)coroutineGO.result;

                currentTile.name = "Tile " + index;
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

                currentTile.GetComponent<TileScript>().setTile(aux);

            }

        }
        else
        {
            coroutineGO = new CoroutineWithData(this, tileGen.tileBuilder(aux.transform.GetChild(0).position, stageMode, randomBuilder, index));
            currentTile = (GameObject)coroutineGO.result;

            currentTile.name = "Tile " + index;
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode.getNameAndKey());

            currentTile.GetComponent<TileScript>().setTile(aux);
        }
        */


        /*

        yield return null;

    }*/





    public Vector3 getGrabity()
    {
        return stageMode.getGravity();
    }


    public void updateStageMode(string key)
    {
        stageMode = mode[key];
    }


}
