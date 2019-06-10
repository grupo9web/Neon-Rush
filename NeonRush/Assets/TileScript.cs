using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : generalManager
{

    #region Variables

    TileManager instanceOfB;
    SimonManager simonMode;
    scirp instanceOfC;
    generalManager generalMang;

    private Vector3 parentPos;                                                  // Indica la posición final de su antecesor
    [SerializeField]
    private Vector3 landedPos = new Vector3(0, 0, 5);                             // Indica la posición de la plataforma final (init en 1 plat pos)

    [SerializeField]
    private bool landed = false;                                                // Marca cuando está la plat en su posición

    private GameObject parentTile;                                              // Nodo padre
    private int attachIndex;                                                    // Índice que marca la posición del nodo

    private int landedAxis;
    private Vector3 axisFacing;

    private tileManagerMode stageMode;

    [SerializeField]
    private string modeChanger = "";                                             // Cada tile camChanger determina en qué sentido se actualiza el mundo

    private bool landTile;                                                       // Marcamos si el bloque es donde aterriza la bola después powerUp del salto

    [SerializeField]
    private bool simonBifurcado = false;                                         // Sólo las piezas marcadas como simonBifurcado podrán generar dos caminos simultáneos
    private bool soyUnaT;

    private List<int> coloresDelPadre = new List<int>();
    private Queue<int> coloresAux = new Queue<int>();

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        simonMode = GameObject.Find("TileManager").GetComponent<SimonManager>();
        instanceOfB = GameObject.Find("TileManager").GetComponent<TileManager>();
        generalMang = GameObject.Find("TileManager").GetComponent<generalManager>();
        instanceOfC = GameObject.Find("Player").GetComponent<scirp>();


        this.landedAxis = 1;                    // Se parará en el eje Y

    } 



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GameControl();
            setWorldSpeed(1.0f);
            GravityControl();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && tag == "Changer")
            generalMang.changeStageMode(modeChanger);
    }

    public void GameControl()
    {
        //Generamos una nueva plataforma
        if (instanceOfB != null)
            instanceOfB.Spawner();
        else
        {
            // Hay que hacer una criba, y cuando hay dos caminos no generar más
            if (!simonBifurcado)     
                simonMode.Spawnerv4();
            
            if (soyUnaT)
                simonMode.RemoveWrongWay();

            //simonMode.conSpawner(isMyParentAFuckingT, null);
            //StartCoroutine(simonMode.concurrentSpawner(isMyParentAFuckingT));
        }
        //Actualizamos la puntuación
        instanceOfC.ScoreUpdate();
    }


    public void GravityControl()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        GameObject tileQuitado;

        if (instanceOfB != null)
            GameObject.Find("TileManager").GetComponent<TileManager>().colaTilesActivos.TryDequeue(out tileQuitado);



        Destroy(this.gameObject);
    }



    // Getters & Setters
    public void setParent(Vector3 pos) { this.parentPos = pos; }
    public void setPos(Vector3 pos) { this.landedPos = pos; }


    public void setTile(GameObject tile) { this.parentTile = tile; }
    public void setAttachIndex(int index) { this.attachIndex = index; }

    public void setLandTile(bool b) { this.landTile = b; }
    public bool getLandTile() { return this.landTile; }

    public string getMode() { return this.modeChanger; }
    public void setMode(string key) { this.modeChanger = key; }

    public void setLandState(bool a) { this.landed = a; }

    public bool getSimonBifurcado() { return this.simonBifurcado; }
    public void setSimonBifurcado(bool b) { this.simonBifurcado = b; }

    public bool getSoyUnaT() { return this.soyUnaT; }
    public void setSoyUnaT(bool b) { this.soyUnaT = b; }


    public List<int> getColoresPadre() { return this.coloresDelPadre; }

    public void setColoresPadre(Queue<int> b) {
        foreach (int i in b)
        {
            this.coloresDelPadre.Add(i);
        }
    }


    public int getColorAuxSize() { return this.coloresAux.Count; }

    public Queue<int> getColorAux() {

        Queue<int> returnColors = new Queue<int>();

        for (int i = 0; i < 4; i++)
        {
            returnColors.Enqueue(coloresAux.Dequeue());
        }
       
        return returnColors;
    }

    public void setAuxColor(Queue<int> b)
    {
        foreach (int i in b)
        {
            this.coloresAux.Enqueue(i);
        }
    }


    public void inicioSimon()
    {
        // Aqui la ide es darle feedback al jugador para que se empiece a pispar un poco más de los colores.
        // NO quiero poner carteles vaya. Hmmm a lo mejor habria que cambiar el color de las tiles para que sean 
        // más pim pam reconocibles. yo m entiendo. aunque son las seis d la mañana y deberia irme a sobar y mañana a saber q tal
        Debug.Log("El simon ha empezado en la ficha " + gameObject.name + " + 1 xd");


    }
}
