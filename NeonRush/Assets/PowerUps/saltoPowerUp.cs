using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltoPowerUp : MonoBehaviour
{

    public int numeroSaltos = 3;
    bool saltando = false;
    Vector3 posicionSalto;

    //float t;

    Vector3 startPosition;
    //Vector3 posicionSaltoArriba;
    //Vector3 ultimaPosicionSubida;

    GameObject player;
    private TileManager tileMang;

    protected float Animation;
    public float velocidadSalto = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        tileMang = GameObject.Find("TileManager").GetComponent<TileManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //Si nos hemos saltado un powerup, se destruye
        if (player.transform.position.z >= transform.position.z + 1.0f && saltando == false)
            Destroy(gameObject);

        if (saltando)
        {
            /* FORMA CUTRE

            t += Time.deltaTime / 1;

            if (t <= 0.5f) {
                player.transform.position = Vector3.Lerp(startPosition, posicionSaltoArriba, t);
            }
            else
            {
                player.transform.position = Vector3.Lerp(startPosition, posicionSalto, t);
            }
            
            if (player.transform.position == posicionSalto)
            {
                saltando = false;
                Destroy(gameObject);
            }

            */

            Animation += Time.deltaTime;

            Animation = Animation % velocidadSalto;


            player.transform.position = MathParabola.Parabola(startPosition, posicionSalto, 3f, Animation/velocidadSalto);

            if( Vector3.Distance(player.transform.position,posicionSalto) <= 0.3f)// Si hemos llegado a la posicion que queremos
            {
                saltando = false;
                Destroy(gameObject);
            }
        }
    }



    void OnCollisionEnter(Collision col)
    {
       

        //Si el jugador choca con el PowerUp
        if (col.gameObject.name == "Player")
        {
            //t = 0;

            startPosition = player.transform.position;

            GameObject listaBloques = GameObject.Find("ListaHijos"); //Lista de bloques activos

            //Transform ultimobloque = listaBloques.transform.GetChild(transform.childCount - 1);


            Transform ultimobloque = listaBloques.transform.GetChild(listaBloques.transform.childCount - 1); //Saltamos al ukltimo bloque disponible

            // Marcamos el bloque como la pista de aterrizaje
            ultimobloque.GetComponent<TileScript>().setLandTile(true);

            posicionSalto = new Vector3(ultimobloque.position.x, 0.6f, ultimobloque.position.z);

            saltando = true;

            //posicionSaltoArriba = new Vector3(posicionSalto.x, posicionSalto.y += 1, posicionSalto.z);


            //Teletransportamos al jugador a la posicion del bloque elegido con la altura correcta para seguir jugando

            //player.transform.position = ultimobloque.position;
            //player.transform.position = new Vector3(player.transform.position.x, 0.6f, player.transform.position.z);

            int numero = listaBloques.transform.childCount - 1;

            //Hace la llamada a gravity control para cada uno de los bloques que hemos saltado

            for (int i = 1; i< numero; i++)
            {
                TileScript scriptBloque = listaBloques.transform.GetChild(i).GetComponent<TileScript>();
                //scriptBloque.GameControl();
                scriptBloque.GravityControl();
                
            }

            tileMang.reSpawnTiles();

            
            //Destruye el PowerUp
            //Destroy(gameObject);

        }
    }
}
