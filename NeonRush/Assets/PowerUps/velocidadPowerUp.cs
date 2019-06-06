using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocidadPowerUp : MonoBehaviour
{

    GameObject player;
    scirp ScriptPersonaje;
    bool activo = false;
    float tiempo;

    //public float velocidadAAplicar = 2.0f;
    public float tiempoDuracion = 5.0f;

    private float velocidadNormalPersonaje;


    //Efectos de sonido
    public AudioClip efectoSonidoVelocidad;
    AudioSource audioSourcePowerUP;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ScriptPersonaje = player.GetComponent<scirp>();
        tiempo = 0;

        audioSourcePowerUP = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up, 10.0f);


        if (activo)
        {
            ScriptPersonaje.speed = velocidadNormalPersonaje - 1;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            tiempo += Time.deltaTime;

            GameObject.Find("Player").GetComponent<scirp>().velocidadReducida = true;
            //Duracion total del powerup
            if (tiempo >= tiempoDuracion)
            {
                activo = false;
                ScriptPersonaje.speed = velocidadNormalPersonaje;
                GameObject.Find("Player").GetComponent<scirp>().velocidadReducida = false;
                GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Debugito");

        //Si el jugador choca con el PowerUp
        if (other.gameObject.name == "Player")
        {

            //Lanzamos el sonido de salto
            audioSourcePowerUP.clip = efectoSonidoVelocidad;
            audioSourcePowerUP.Play();


            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ SLOW DOWN !";

            gameObject.GetComponent<Renderer>().enabled = false;
            velocidadNormalPersonaje = ScriptPersonaje.speedAux;
            activo = true;


        }
    }


}

