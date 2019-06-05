using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalManager : MonoBehaviour
{

    /**
     * Creamos un diccionario que almacene los datos de
     * - Nombre = key
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

        mode.Add("dirZpositiva", new tileManagerMode(  "dirZpositiva",
                                                       new Vector3(0, -9.8f, 0),
                                                       Vector3.zero,
                                                       Vector3.forward, Vector3.left,
                                                       Vector3.up,
                                                       new Vector3(0, 5, -12)));

        mode.Add("dirYpositiva", new tileManagerMode(  "dirYpositiva",
                                                       new Vector3(0, 0, 9.8f),
                                                       new Vector3(-90, 0, 0),
                                                       Vector3.up, Vector3.left,
                                                       Vector3.back,
                                                       new Vector3(0, -12, -5)));

        mode.Add("dirZnegativa", new tileManagerMode(  "dirZnegativa",
                                                       new Vector3(0, 9.8f, 0),
                                                       new Vector3(180, 0, 0),
                                                       Vector3.back, Vector3.left,
                                                       Vector3.down,
                                                       new Vector3(0, -5, 12)));

        mode.Add("dirYnegativa", new tileManagerMode(  "dirYnegativa",
                                                       new Vector3(0, 0, -9.8f),
                                                       new Vector3(90, 0, 0),
                                                       Vector3.down, Vector3.left,
                                                       Vector3.forward,
                                                       new Vector3(0, 12, 5)));

        mode.Add("Zpositiva1", new tileManagerMode(    "Zpositiva1",
                                                       new Vector3(-9.8f, 0, 0),
                                                       new Vector3(0, 0, -90),
                                                       Vector3.forward, Vector3.up,
                                                       Vector3.right,
                                                       new Vector3(5, 0, -12)));

        mode.Add("Zpositiva2", new tileManagerMode(    "Zpositiva2",
                                                       new Vector3(0, 9.8f, 0),
                                                       new Vector3(0, 0, -180),
                                                       Vector3.forward, Vector3.right,
                                                       Vector3.down,
                                                       new Vector3(0, -5, -12)));

        mode.Add("Zpositiva3", new tileManagerMode(    "Zpositiva3",
                                                       new Vector3(9.8f, 0, 0),
                                                       new Vector3(0, 0, -270),
                                                       Vector3.forward, Vector3.down,
                                                       Vector3.left,
                                                       new Vector3(-5, 0, -12)));

        
        mode.Add("Ypositiva1", new tileManagerMode(    "Ypositiva1",
                                                       new Vector3( -9.8f, 0, 0),
                                                       new Vector3(-90, 0, -90),
                                                       Vector3.up, Vector3.back,
                                                       Vector3.right,
                                                       new Vector3(5, -12, 0)));

        mode.Add("Ypositiva2", new tileManagerMode(    "Ypositiva2",
                                                       new Vector3(0, 0, -9.8f),
                                                       new Vector3(-90, 0, -180),
                                                       Vector3.up, Vector3.right,
                                                       Vector3.forward,
                                                       new Vector3(0, -12, 5)));

        mode.Add("Ypositiva3", new tileManagerMode(    "Ypositiva3",
                                                       new Vector3(9.8f, 0, 0),
                                                       new Vector3(-90, 0, -270),
                                                       Vector3.up, Vector3.forward,
                                                       Vector3.left,
                                                       new Vector3(-5, -12, 0)));

        mode.Add("Znegativa1", new tileManagerMode(    "Znegativa1",
                                                       new Vector3(-9.8f, 0, 0),
                                                       new Vector3(-270, 0, -90),
                                                       Vector3.down, Vector3.forward,
                                                       Vector3.right,
                                                       new Vector3(5, 12, 0)));
        
        mode.Add("Znegativa2", new tileManagerMode(    "Znegativa2",
                                                       new Vector3(0, 0, 9.8f),
                                                       new Vector3(-180, -90, -270),
                                                       Vector3.right, Vector3.up,
                                                       Vector3.back,
                                                       new Vector3(-12, 0, -5)));
        
        mode.Add("Znegativa3", new tileManagerMode(    "Znegativa3",
                                                       new Vector3(0, 9.8f, 0),
                                                       new Vector3(-180, -90, 0),
                                                       Vector3.right, Vector3.back,
                                                       Vector3.down,
                                                       new Vector3(-12, -5, 0)));

        mode.Add("Ynegativa1", new tileManagerMode(    "Ynegativa1",
                                                       new Vector3( -9.8f, 0, 0),
                                                       new Vector3(-270, 0, -90),
                                                       Vector3.down, Vector3.forward,
                                                       Vector3.right,
                                                       new Vector3(5, 12, 0)));
        
        mode.Add("Ynegativa2", new tileManagerMode(    "Ynegativa2",
                                                       new Vector3(0, 0, 9.8f),
                                                       new Vector3(-270, -90, 90),
                                                       Vector3.down, Vector3.right,
                                                       Vector3.back,
                                                       new Vector3(0, 12, -5)));
        
        mode.Add("Ynegativa3", new tileManagerMode(    "Ynegativa3",
                                                       new Vector3( 9.8f, 0, 0),
                                                       new Vector3(-270, -90, 0),
                                                       Vector3.down, Vector3.back,
                                                       Vector3.left,
                                                       new Vector3(-5, 12, 0)));
                                                       

        // Definimos a posteriori las dependencias entre modos de juego


        // De +z pasamos a +y // De +y pasamos a -z // De -z pasamos a -y // De -y pasamos a +z
        // En cada uno el Attach de la izquierda nos lleva a la izquierda de cada dirección

        mode["dirZpositiva"].setCollindantModes(mode["Zpositiva1"]);
        mode["dirZpositiva"].setCollindantModes(mode["dirYpositiva"]);

        mode["dirYpositiva"].setCollindantModes(mode["Ypositiva1"]);
        mode["dirYpositiva"].setCollindantModes(mode["dirZnegativa"]);

        mode["dirZnegativa"].setCollindantModes(mode["Znegativa1"]);
        mode["dirZnegativa"].setCollindantModes(mode["dirYnegativa"]);

        mode["dirYnegativa"].setCollindantModes(mode["Ynegativa1"]);
        mode["dirYnegativa"].setCollindantModes(mode["dirZpositiva"]);



        // Correlación entre z positivas

        mode["Zpositiva1"].setCollindantModes(mode["Zpositiva2"]);      // bien
        mode["Zpositiva1"].setCollindantModes(mode["dirYpositiva"]);    // bien

        mode["Zpositiva2"].setCollindantModes(mode["Zpositiva3"]);      // bien
        //mode["Zpositiva2"].setCollindantModes(mode["dirYnegativa"]);    // mal

        mode["Zpositiva3"].setCollindantModes(mode["dirZpositiva"]);    // bien
        mode["Zpositiva3"].setCollindantModes(mode["dirYpositiva"]);    // bien



        // Correlación entre y positivas

        mode["Ypositiva1"].setCollindantModes(mode["Ypositiva2"]);      // bien
        mode["Ypositiva1"].setCollindantModes(mode["dirZnegativa"]);    // bien

        mode["Ypositiva2"].setCollindantModes(mode["Ypositiva3"]);      // bien
        //mode["Ypositiva2"].setCollindantModes(mode["dirZnegativa"]);    // mal

        mode["Ypositiva3"].setCollindantModes(mode["dirYpositiva"]);    // bien
        mode["Ypositiva3"].setCollindantModes(mode["dirZnegativa"]);    // bien



        // Correlación entre z negativas

        mode["Znegativa1"].setCollindantModes(mode["Znegativa2"]);      // bien
        mode["Znegativa1"].setCollindantModes(mode["dirZpositiva"]);    // bien

        mode["Znegativa2"].setCollindantModes(mode["Znegativa3"]);      // bien
        //mode["Znegativa2"].setCollindantModes(mode["dirYpositiva"]);    // mal

        mode["Znegativa3"].setCollindantModes(mode["dirYnegativa"]);    // bien
        //mode["Znegativa3"].setCollindantModes(mode["dirYpositiva"]);    // bien



        // Correlación entre y negativas

        mode["Ynegativa1"].setCollindantModes(mode["Ynegativa2"]);      // bien
        mode["Ynegativa1"].setCollindantModes(mode["dirZpositiva"]);    // bien

        mode["Ynegativa2"].setCollindantModes(mode["Ynegativa3"]);      // bien
        //mode["Ynegativa2"].setCollindantModes(mode["dirYnegativa"]);    // mal

        mode["Ynegativa3"].setCollindantModes(mode["dirYnegativa"]);    // bien
        mode["Ynegativa3"].setCollindantModes(mode["dirZpositiva"]);    // bien






        tileManager = gameObject.GetComponent<TileManager>();
        
        print("Debugito");
    }


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<scirp>();
    }


    public void changeStageMode(string key)
    {
        //player = GameObject.Find("Player").GetComponent<scirp>();
        //tileManager.updateStageMode(key);
        player.updateStageMode(key);
    }



}
