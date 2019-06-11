using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManagerMode
{

    private string key;                                                                 // Nombre del modo = key para acceder al diccionario
    private Vector3 gravity;                                                            // Gravedad que actúa sobre el mundo
    private Vector3 blockOrientation;                                                   // Rotación que debe aplicarse a los bloques
    private Vector3[] directionControl = new Vector3[2];                                // Pareja de direcciones 
    private Vector3 cameraDirection;                                                    // Eje de giro de cámara
    private Vector3 pivotPos;                                                           // Posición del pívot quesigue la cámara
    private List<tileManagerMode> collindantModes = new List<tileManagerMode>();        // Modos que están entrelazods directamente entre ellos


    public tileManagerMode(string key, Vector3 g, Vector3 euler, Vector3 basic, Vector3 alternative, Vector3 camAxis, Vector3 pivotPos)
    {
        this.key = key;
        this.gravity = g;
        this.blockOrientation = euler;
        this.directionControl[0] = basic;
        this.directionControl[1] = alternative;
        this.cameraDirection = camAxis;
        this.pivotPos = pivotPos;
    }


    //Getters
    public void setNameAndKey(string key) { this.key = key; }
    public void setGravity(Vector3 g) { this.gravity = g; }
    public void setBO(Vector3 euler) { this.blockOrientation = euler; }
    public void setDC(Vector3 baseC, Vector3 alternativeC) { this.directionControl[0] = baseC; this.directionControl[1] = alternativeC; }
    public void setCamAxis(Vector3 camAxis) { this.cameraDirection = camAxis; }
    public void setPivotPos(Vector3 pivot) { this.pivotPos = pivot; }
    public void setCollindantModes(tileManagerMode mode) { this.collindantModes.Add(mode); }


    // Setters (no deberían hacer falta)
    public string getNameAndKey() { return this.key; }
    public Vector3 getGravity() { return this.gravity; }
    public Vector3 getBO() { return this.blockOrientation; }
    public Vector3[] getDC() { return this.directionControl; }
    public Vector3 getCA() { return this.cameraDirection; }
    public Vector3 getPP() { return this.pivotPos; }
    public List<tileManagerMode> getCollindantModes() { return this.collindantModes; }

}
