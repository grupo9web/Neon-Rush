using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scirp : generalManager
{
    #region Variables

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo
    private bool updateWorld = false;           // AL cambiar de modo actualizar valores

    public float speed;
    public float speedAux;
    public float score = 0;
    public float incrementoVelocidad = 0.001f;

    private saltoPowerUp auxSaltoScript;

    //We tell the script more about canvas and text 
    public Text scoreTxt;

    private Vector3 direccion;

    [SerializeField]
    private Vector3 gravedad = Physics.gravity;

    private Camera cam;                         // Ref. a cámara
    private GameObject pivot;                   // Ref. a pivot que recibe la nueva posición a la que debe ir la cámara


    public bool velocidadReducida = false;

   


    #endregion

    void Start()
    {
        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        direccion = stageMode.getDC()[0];

        cam = gameObject.GetComponentInChildren<Camera>();
        pivot = gameObject.transform.GetChild(0).gameObject;

        setWorldSpeed(1.0f);

        

        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void Update()
    {
        gravedad = Physics.gravity;

        Vector3 playerHeightPos = this.gameObject.transform.position;
        Vector3 referencePosition = GameObject.Find("ListaHijos").transform.GetChild(1).transform.position;

        speed = speed + incrementoVelocidad;


        if (!velocidadReducida)
        {
            speedAux = speed;
        }

        /*/
        if (gravedad.x == -9.8f || gravedad.y == -9.8f || gravedad.z == -9.8f || gravedad.y == -9.81f){
            //Debug.Log("Willyrex");
            if (gravedad.x != 0){
                Debug.Log("Willyrex1"); //Funciona
                if(Vector3.Distance(playerHeightPos, referencePosition) < f)
                    SceneManager.LoadScene("SampleScene");
            }else if (gravedad.y != 0) {  //Gravedad y = -9.8
                Debug.Log("Willyrex2"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y -7, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else{
                Debug.Log("Willyrex3");
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y, playerHeightPos.z - 5)) < 7f)
                    SceneManager.LoadScene("SampleScene");
                }
        }else {                    
            if (gravedad.x != 0){
                Debug.Log("Willyrex4"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x - 7, playerHeightPos.y, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else if (gravedad.y != 0){
                Debug.Log("Willyrex5"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y + 7, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else{
                Debug.Log("Willyrex6");
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y, playerHeightPos.z + 7)) < 7f)
                    SceneManager.LoadScene("SampleScene");
                }
        } 
        */

        
        
        //Soy retrasado y se podia condensar en esto:
         if(Vector3.Distance(playerHeightPos, referencePosition) > 15f) {
            SceneManager.LoadScene("SampleScene");
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
        
        



        if (updateWorld)
        {
            Physics.gravity = stageMode.getGravity();
            direccion = stageMode.getDC()[0];
            pivot.transform.localPosition = stageMode.getPP();
            pivot.transform.LookAt(transform);
            setWorldSpeed(0.2f);

            updateWorld = false;
        }



        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("e"))
        {
            if (direccion == stageMode.getDC()[0])
            {
                direccion = stageMode.getDC()[1];
                pivot.transform.RotateAround(transform.position, stageMode.getCA(), -90.0f);
            }
            else if (direccion == stageMode.getDC()[1])
            {
                direccion = stageMode.getDC()[0];
                pivot.transform.RotateAround(transform.position, stageMode.getCA(), 90.0f);
            } 

        }



        // Al actualizar la posición del pivot, movemos la cámara
        if (Vector3.Distance(pivot.transform.localPosition, cam.transform.localPosition) > 0.01f)
        {
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, pivot.transform.localPosition, (75 * getWS()) * Time.deltaTime);
            cam.transform.LookAt(transform, stageMode.getCA());
        }
        else if (Vector3.Distance(pivot.transform.localPosition, cam.transform.localPosition) < 0.1f)
            setWorldSpeed(1.0f);

        //Debug.Log(speed * getWS() + ", " + getWS());

        float autoMove = (speed * getWS()) * Time.deltaTime;

        transform.Translate(direccion * autoMove);

    }

    public void ScoreUpdate()
    {
        score += 10 * speed;

        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void setScoretxt()
    {
        scoreTxt.text = "Puntuación: " + score.ToString();
    }




    public void updateStageMode(string key) {
        //Debug.Log("El jauja del scirp");
        stageMode = mode[key];
        updateWorld = true;
    }



    public Vector3 getDirection()
    {
        return this.direccion;
    }

}

