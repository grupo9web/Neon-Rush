using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    TileManager instanceOfB;
    scirp instanceOfC;

    private Vector3 parentPos;                                              // Indica la posición final de su antecesor
    private Vector3 landedPos = new Vector3(0, 0, 5);                         // Indica la posición de la plataforma final (init en 1 plat pos)

    private bool landed = false;                                            // Marca cuando está la plat en su posición

    private TileManager.platType type;

    // Start is called before the first frame update
    void Start()
    {
        instanceOfB = GameObject.Find("TileManager").GetComponent<TileManager>();
        instanceOfC = GameObject.Find("Player").GetComponent<scirp>();



    }

    // Update is called once per frame
    void Update()
    {

        switch (type)
        {
            case TileManager.platType.classic:

                Vector3 posDest = Physics.gravity;

                // Colocación de la plataforma
                if (Vector3.Distance(transform.position, landedPos) > 0.001f && !landed)
                    
                    if (posDest.y != 0) transform.position = Vector3.MoveTowards(transform.position, landedPos, 5 * Time.deltaTime);
                    else                transform.position = Vector3.MoveTowards(transform.position, landedPos, 5 * Time.deltaTime);

                if (transform.position.y != 0 && !landed)
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(landedPos.x, 0, landedPos.z), 10 * Time.deltaTime);

                if (transform.position.y == 0)
                    landed = true;

                break;

            case TileManager.platType.camChanger:
                // Colocación de la plataforma
                if (Vector3.Distance(transform.position, landedPos) > 1.001f && !landed)
                    transform.position = Vector3.MoveTowards(transform.position, landedPos, 5 * Time.deltaTime);

                if (transform.position.y != 1 && !landed)
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(landedPos.x, 1, landedPos.z), 10 * Time.deltaTime);

                if (transform.position.y == 1)
                    landed = true;

                break;

            default:
                break;
        }

        Vector3 graviy =  Physics.gravity;
        Debug.Log("GRAVEDAAAAAAAAAD: " + graviy);

      
    }

    void OnTriggerExit(Collider other){

        if (other.gameObject.name == "Player"){
            GameControl();
            GravityControl();                
        }

    }


    void GameControl(){
        //Generamos una nueva plataforma
        instanceOfB.Spawner();
        //Actualizamos la puntuación
        instanceOfC.ScoreUpdate();        
    }

    void GravityControl(){

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        Destroy(this.gameObject, 1);
    }



    // Getters & Setters
    public void setParent(Vector3 pos) { this.parentPos = pos; }
    public void setPos(Vector3 pos) { this.landedPos = pos; }
    public void setType(TileManager.platType jauja) {type =  jauja; }

}
