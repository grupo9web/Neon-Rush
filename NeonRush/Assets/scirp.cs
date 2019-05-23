using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scirp : generalManager
{
    #region Variables

    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo
    private bool updateWorld = false;           // AL cambiar de modo actualizar valores

    public float speed;
    public float score = 0;

    //We tell the script more about canvas and text 
    public Text scoreTxt;

    private Vector3 direccion;

    private Camera cam;                         // Ref. a cámara
    private GameObject pivot;                   // Ref. a pivot que recibe la nueva posición a la que debe ir la cámara

    #endregion

    void Start()
    {
        if (mode.ContainsKey("horizontal"))
            stageMode = mode["horizontal"];

        direccion = stageMode.getDC()[0];

        cam = gameObject.GetComponentInChildren<Camera>();
        pivot = gameObject.transform.GetChild(0).gameObject;

        setWorldSpeed(1.0f);

        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void Update()
    {

        if (updateWorld)
        {
            Physics.gravity = stageMode.getGravity();
            direccion = stageMode.getDC()[0];
            pivot.transform.localPosition = stageMode.getPP();
            pivot.transform.LookAt(transform);

            updateWorld = false;
        }



        //if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown("e"))
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
        Debug.Log("El jauja del scirp");
        stageMode = mode[key];
        updateWorld = true;
    }

















    public void resolveNewDirection(string key)
    {
        stageMode = mode[key];
        updateWorld = true;
    }

}

