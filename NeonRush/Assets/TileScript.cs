using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : generalManager
{
    TileManager instanceOfB;
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


    // Start is called before the first frame update
    void Start()
    {
        instanceOfB = GameObject.Find("TileManager").GetComponent<TileManager>();
        generalMang = GameObject.Find("TileManager").GetComponent<generalManager>();
        instanceOfC = GameObject.Find("Player").GetComponent<scirp>();


        this.landedAxis = 1;                    // Se parará en el eje Y

        landedPos = parentTile.transform.GetChild(attachIndex).transform.position;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!landed && Vector3.Distance(transform.position, landedPos) > 0.001f)
            transform.position = Vector3.MoveTowards(transform.position, landedPos, (8 * getWS()) * Time.fixedDeltaTime);

        if (parentTile != null)
        {
            if (!landed && transform.position[landedAxis] != parentTile.transform.GetChild(attachIndex).transform.position[landedAxis])
                transform.position = Vector3.MoveTowards(transform.position, parentTile.transform.GetChild(attachIndex).transform.position, (18 * getWS()) * Time.fixedDeltaTime);
        }

        if (transform.position[landedAxis] == landedPos[landedAxis])
            landed = true;
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
        instanceOfB.Spawner();
        //Actualizamos la puntuación
        instanceOfC.ScoreUpdate();
    }


    public void GravityControl()
    {

        //Physics.gravity = stageMode.getGravity();

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        GameObject tileQuitado;

        GameObject.Find("TileManager").GetComponent<TileManager>().colaTilesActivos.TryDequeue(out tileQuitado);



        Destroy(this.gameObject, 1);
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
}
