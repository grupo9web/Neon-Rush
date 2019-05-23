using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalManager : MonoBehaviour
{

    /**
     * Creamos un diccionario que almacene los datos de
     * - Gravedad
     * - Rotación de los bloques 
     * - Dirección de los controles al hacer click
     * - Rotación y posición de cámara
     * 
     * En el propio TileManager y el controlador del jugador 
     * se tendrán en cuenta estos para alterar el mundo
     * 
     **/

    protected Dictionary<string, tileManagerMode> mode = new Dictionary<string, tileManagerMode>();

    // Referencias a cada uno de los elementos estáticos del mundo
    private TileManager tileManager;
    private scirp player;

    // Una velocidad global nos permite marcar ciertas pausas en todo el universo
    private float worldSpeed = 1.0f;
    public void setWorldSpeed(float f) { this.worldSpeed = f; }
    public float getWS() { return this.worldSpeed; }


    // Start is called before the first frame update
    void Awake()
    {

        mode.Add("horizontal", new tileManagerMode( new Vector3(0, -9.8f, 0),    
                                                    Vector3.zero,    
                                                    Vector3.forward, Vector3.left, 
                                                    Vector3.up, 
                                                    new Vector3( 0, 5, -12)));

        mode.Add("verticalZ" , new tileManagerMode( new Vector3( -9.8f, 0, 0), 
                                                    new Vector3( 0, 0, -90), 
                                                    Vector3.forward, Vector3.up, 
                                                    Vector3.right, 
                                                    new Vector3( 5, 0, -12)));

        mode.Add("verticalX" , new tileManagerMode( new Vector3(0, 0,  9.8f), 
                                                    new Vector3( -90, 0, 0), 
                                                    Vector3.up, Vector3.left, 
                                                    Vector3.back, 
                                                    new Vector3( 0, -12,  -5)));

        mode.Add("horizInv" , new tileManagerMode( new Vector3(0, 9.8f, 0),
                                                   Vector3.zero,
                                                   Vector3.back, Vector3.right,
                                                   Vector3.down,
                                                   new Vector3(0, -5, 12)));


        // Definimos a posteriori las dependencias entre modos de juego
        //mode["horizontal"].setCollindantModes


        tileManager = gameObject.GetComponent<TileManager>();
        player = GameObject.Find("Player").GetComponent<scirp>();

    }



    public void changeStageMode(string key)
    {
        tileManager.updateStageMode(key);
        player.updateStageMode(key);
    }



}
