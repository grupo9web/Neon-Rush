using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scirp : MonoBehaviour
{
    #region Variables

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
        //Actualizamos el txt de la puntuación
        setScoretxt();

        direccion = Vector3.forward;

        cam = gameObject.GetComponentInChildren<Camera>();
        pivot = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (direccion == Vector3.forward)
            {
                direccion = Vector3.left;
                pivot.transform.RotateAround(transform.position, transform.up, -90.0f);
            }
            else
            {
                direccion = Vector3.forward;
                pivot.transform.RotateAround(transform.position, transform.up, 90.0f);
            }
        }

        // Al actualizar la posición del pivot, movemos la cámara
        if (Vector3.Distance(pivot.transform.localPosition, cam.transform.localPosition) > 0.01f)
        {
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, pivot.transform.localPosition, 75 * Time.deltaTime);
            //cam.transform.LookAt(transform, transform.right);
            cam.transform.LookAt(transform);
        }

        float autoMove = speed * Time.deltaTime;

        transform.Translate(direccion * autoMove);

    }

    public void ScoreUpdate()
    {
        score += 10 /* * speed*/;

        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void setScoretxt()
    {
        scoreTxt.text = "Puntuación: " + score.ToString();
    }

}

