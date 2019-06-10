using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltoPowerUp : MonoBehaviour
{

    public int numeroSaltos = 3;

    [SerializeField]
    bool saltando = false;

    Vector3 posicionSalto;

    int numeroHijos;


    //Efectos de sonido
    public AudioClip efectoSonidoSalto;
    AudioSource audioSourcePowerUP;

    //float t;

    Vector3 startPosition;
    //Vector3 posicionSaltoArriba;
    //Vector3 ultimaPosicionSubida;

    GameObject player;
    private TileManager tileMang;

    protected float Animation;
    public float velocidadSalto = 2.0f;

    [SerializeField]
    public GameObject tileAsociado;

    // Start is called before the first frame update
    void Start()
    {
        tileMang = GameObject.Find("TileManager").GetComponent<TileManager>();
        player = GameObject.Find("Player");
        audioSourcePowerUP = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {

        numeroHijos = GameObject.Find("ListaHijos").transform.childCount;

        transform.Rotate(Vector3.up, 10.0f);


        //Comprobamos si nos hemos saltado el powerup para destruirlo
        checkDestruirPowerUp();

        //Si hay mas hijos por delante de la pieza en la que esta este powerup

        if (tileAsociado != null)
        {
            if ((GameObject.Find("ListaHijos").transform.childCount - 1) > tileAsociado.transform.GetSiblingIndex())
            {
                //Mira si la pieza en la que esta puesta el powerup es justo la de detras de un cam changer y lo eliminamos para quitar problemas
                if (GameObject.Find("ListaHijos").transform.GetChild(tileAsociado.transform.GetSiblingIndex() + 1).tag == "Changer")
                {
                    Destroy(gameObject);
                }
            }
        }
        if (saltando)
        {
            Animation += Time.deltaTime;

            Animation = Animation % velocidadSalto;

            //Para que la animación del salto vaya bien, tendríamos que añadir en la parabola las coordenadas según el sistema de gravedad.
            player.transform.position = MathParabola.Parabola(startPosition, posicionSalto, 3f, Animation / velocidadSalto);

            if (Vector3.Distance(player.transform.position, posicionSalto) <= 0.25f)// Si hemos llegado a la posicion que queremos
            {
                saltando = false;
                GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        //Si el jugador choca con el PowerUp
        if (col.gameObject.name == "Player")
        {


            //Lanzamos el sonido de salto
            audioSourcePowerUP.clip = efectoSonidoSalto;
            audioSourcePowerUP.Play();


            //Activa el texto de salto
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ JUMPING !";



            Transform bloqueSalto;
            GameObject ListaHijos = GameObject.Find("ListaHijos");


            int indiceBloqueASaltar = 0;

            for (int i = 0; i < ListaHijos.transform.childCount; i++)
            {
                Debug.Log("Número de hijos: " + numeroHijos);
                if (ListaHijos.transform.GetChild(i).tag == "Changer" && i > 0)
                {
                    indiceBloqueASaltar = i - 1;
                    break;
                }
                else if (i == 5)
                {
                    indiceBloqueASaltar = i - 1;
                    break;
                }
                indiceBloqueASaltar = i - 1;
            }
            startPosition = player.transform.position;

            bloqueSalto = ListaHijos.transform.GetChild(indiceBloqueASaltar);

            bloqueSalto.GetComponent<TileScript>().setLandTile(true);

            posicionSalto = bloqueSalto.transform.GetChild(9).position;




            saltando = true;

            //Hace la llamada a gravity control para cada uno de los bloques que hemos saltado


            for (int i = 0; i < indiceBloqueASaltar; i++)
            {
                TileScript scriptBloque = ListaHijos.transform.GetChild(i).GetComponent<TileScript>();
                //scriptBloque.GameControl();
                scriptBloque.GravityControl();

                Debug.Log("Eeeeey creo dos loco: " + numeroHijos);

                tileMang.reSpawnTiles();
                

            }

        }
    }

    

    public void checkDestruirPowerUp()
    {

        scirp ScriptPersonaje = player.GetComponent<scirp>();

        if (!saltando)
        {
            if (ScriptPersonaje.getDirection().x == 1.0f)
            {
                if (player.transform.position.x >= transform.position.x + 1)
                    Destroy(gameObject);
            }
            else if (ScriptPersonaje.getDirection().y == 1.0f)
            {
                if (player.transform.position.y >= transform.position.y + 1)
                    Destroy(gameObject);
            }
            else if (ScriptPersonaje.getDirection().z == 1.0f)
            {
                if (player.transform.position.z >= transform.position.z + 1)
                    Destroy(gameObject);
            }
            else if (ScriptPersonaje.getDirection().x == -1.0f)
            {
                if (player.transform.position.x <= transform.position.x - 1)
                    Destroy(gameObject);
            }
            else if (ScriptPersonaje.getDirection().y == -1.0f)
            {
                if (player.transform.position.y <= transform.position.y - 1)
                    Destroy(gameObject);
            }
            else if (ScriptPersonaje.getDirection().z == -1.0f)
            {
                if (player.transform.position.z <= transform.position.z - 1)
                    Destroy(gameObject);
            }
        }
    }







    public bool getJumpState() { return this.saltando; }
}

