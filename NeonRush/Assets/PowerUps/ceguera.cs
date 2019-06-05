using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ceguera : MonoBehaviour
{

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

        tiempo = 0;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up, 10.0f);

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
            else if (intensidad <= 0.0f)
            {
                intensidad = 0.0f;
                finalProfile.vignette.Reset();
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        print("Debugito");

        //Si el jugador choca con el PowerUp
        if (col.gameObject.name == "Player")
        {

            intensidad = 1.0f;

           // sonido


            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ T E  J O D E S !";

            gameObject.GetComponent<Renderer>().enabled = false;

            finalProfile.vignette.setIntensity(intensidad);

            activo = true;
        }
    }

}
