using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ceguera : MonoBehaviour
{

    public AudioClip efectoSonidoCeguera;
    AudioSource audioSourcePowerUP;
    
    GameObject player;
    scirp ScriptPersonaje;
    bool activo = false;
    float tiempo;
    float intensidad = 1.0f;

    private PostProcessingProfile finalProfile;

    public float tiempoDuracion = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ScriptPersonaje = player.GetComponent<scirp>();

        finalProfile = player.GetComponentInChildren<PostProcessingBehaviour>().profile;
        audioSourcePowerUP = GetComponent<AudioSource>();


        tiempo = 0;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up, 10.0f);

        //Comprobamos si nos hemos saltado el powerup para destruirlo
        checkDestruirPowerUp();


        if (activo)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;

            tiempo += Time.deltaTime;

            //Duracion total del powerup
            if (tiempo >= tiempoDuracion)
            {
                activo = false;
                GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(false);
            }

        }
        else if (tiempo >= tiempoDuracion)
        {

            if (intensidad > 0.005)
            {
                intensidad -= 0.005f;
                finalProfile.vignette.setIntensity(intensidad);
            }
            else if (intensidad <= 0.005f)
            {
                intensidad = 0.0f;
                finalProfile.vignette.Reset();
                ScriptPersonaje.cegado = false;
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        //Si el jugador choca con el PowerUp
        if (col.gameObject.name == "Player")
        {
            ScriptPersonaje.cegado = true;

            intensidad = 1.0f;

            // sonido
            audioSourcePowerUP.clip = efectoSonidoCeguera;
            audioSourcePowerUP.Play();


            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ CIEGO !";

            gameObject.GetComponent<Renderer>().enabled = false;

            finalProfile.vignette.setIntensity(intensidad);

            activo = true;
        }
    }


    public void checkDestruirPowerUp()
    {

        scirp ScriptPersonaje = player.GetComponent<scirp>();

        if (!activo && !ScriptPersonaje.cegado)
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
}
