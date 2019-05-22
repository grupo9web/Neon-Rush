using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : generalManager
{
    TileManager instanceOfB;
    scirp instanceOfC;

    private Vector3 parentPos;                                                  // Indica la posición final de su antecesor
    [SerializeField]
    private Vector3 landedPos = new Vector3(0,0,5);                             // Indica la posición de la plataforma final (init en 1 plat pos)
    
    private bool landed = false;                                                // Marca cuando está la plat en su posición

    private GameObject parentTile;                                              // Nodo padre
    private int attachIndex;                                                    // Índice que marca la posición del nodo


    private int landedAxis;
    private Vector3 axisFacing;

    private TileManager.platType type;
    private tileManagerMode stageMode;         



    // Start is called before the first frame update
    void Start()
    {
        instanceOfB = GameObject.Find("TileManager").GetComponent<TileManager>();
        instanceOfC = GameObject.Find("Player").GetComponent<scirp>();

        // Usando el enum marcamos el resto de valores de la plataforma
        switch (type)
        {
            case TileManager.platType.classicY:

                this.landedAxis = 1;                    // Se parará en el eje Y
                landedPos = parentTile.transform.GetChild(attachIndex).transform.position;
                transform.eulerAngles = stageMode.getBO();

                Debug.Log(landedPos);
                break;

            case TileManager.platType.classicX:

                this.landedAxis = 1;

                landedPos = parentTile.transform.GetChild(attachIndex).transform.position;          
                transform.eulerAngles = new Vector3(-90, 0, 0);
          

                break;

            case TileManager.platType.classicZ:

                this.landedAxis = 1;

                landedPos = parentTile.transform.GetChild(attachIndex).transform.position;
                transform.eulerAngles = stageMode.getBO();

                break;

            case TileManager.platType.camChanger:

                Debug.Log("Padre " + parentTile.transform.GetChild(1).transform.position + " hijo " + landedPos + " y el eje " + transform.position[landedAxis]);
                this.landedAxis = 1;
                landedPos = parentTile.transform.GetChild(attachIndex).transform.position;

                // Comprobamos la izquierda
                if (Vector3.Distance (parentTile.transform.GetChild(1).transform.position, landedPos ) <= 1)
                    transform.eulerAngles = new Vector3(-90, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 0, -90);

                break;

            default:
                break;
        }

        gameObject.name = type.ToString();

    }



    // Update is called once per frame
    void FixedUpdate()
    {


        if (!landed && Vector3.Distance(transform.position, landedPos) > 0.001f)
            transform.position = Vector3.MoveTowards(transform.position, landedPos, 8 * Time.fixedDeltaTime);

        if (!landed && transform.position[landedAxis] != parentTile.transform.GetChild(attachIndex).transform.position[landedAxis])
            transform.position = Vector3.MoveTowards(transform.position, parentTile.transform.GetChild(attachIndex).transform.position, 18 * Time.fixedDeltaTime);

        if (transform.position[landedAxis] == landedPos[landedAxis])
            landed = true;



        /*
        if (Vector3.Distance(transform.position, landedPos) > 0.001f && !landed)
            transform.position = Vector3.MoveTowards(transform.position, landedPos, 8 * Time.fixedDeltaTime);

        if (transform.position[landedAxis] != 0 && !landed)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(landedPos.x, 0, landedPos.z), 18 * Time.fixedDeltaTime);

        if (transform.position[landedAxis] == landedPos[landedAxis])
            landed = true;
            */

        /*
        switch (type)
        {
            case TileManager.platType.classic:

                // Colocación de la plataforma
                if (Vector3.Distance(transform.position, landedPos) > 0.001f && !landed)
                    transform.position = Vector3.MoveTowards(transform.position, landedPos, 5 * Time.deltaTime);

                if (transform.position.y != 0 && !landed)
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(landedPos.x, 0, landedPos.z), 10 * Time.deltaTime);

                if (transform.position.y == 0)
                    landed = true;

                break;

            case TileManager.platType.camChanger:
                Debug.Log("Jajaaaaaaaaaaaaaaaaaaa");
               
                break;

            default:
                break;
        }*/


    }



    void OnTriggerExit(Collider other){

        if (other.gameObject.name == "Player"){
            GameControl();
            GravityControl();                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && type == TileManager.platType.camChanger)
        {
            Debug.Log("Jajajajaja");
            instanceOfC.resolveNewDirection("verticalX");
        }
    }

    void GameControl(){
        //Generamos una nueva plataforma
        instanceOfB.Spawner();
        //Actualizamos la puntuación
        instanceOfC.ScoreUpdate();        
    }


    void GravityControl(){

        //Physics.gravity = stageMode.getGravity();

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        Destroy(this.gameObject, 1);
    }



    // Getters & Setters
    public void setParent(Vector3 pos) { this.parentPos = pos; }
    public void setPos(Vector3 pos) { this.landedPos = pos; }

    public void setType(TileManager.platType jauja) {type =  jauja; }

    public void setTile(GameObject tile) { this.parentTile = tile; }
    public void setAttachIndex(int index) { this.attachIndex = index; }

    public void setMode(tileManagerMode mode) { this.stageMode = mode; }


}
