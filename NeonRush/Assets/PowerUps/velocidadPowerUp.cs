using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocidadPowerUp : MonoBehaviour
{

    GameObject player;
    scirp ScriptPersonaje;
    bool activo = false;
    float tiempo;

    public float velocidadAAplicar = 2.0f;
    public float tiempoDuracion = 5.0f;

    private float velocidadNormalPersonaje;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ScriptPersonaje = player.GetComponent<scirp>();
        tiempo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (activo)
        {
            ScriptPersonaje.speed = velocidadAAplicar;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            tiempo += Time.deltaTime;

            //Duracion total del powerup
            if(tiempo >= tiempoDuracion)
            {
                activo = false;
                ScriptPersonaje.speed = velocidadNormalPersonaje;
                GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(false);
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
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ SLOW DOWN !";

            gameObject.GetComponent<Renderer>().enabled = false;
            velocidadNormalPersonaje = ScriptPersonaje.speedBase;
            activo = true;
        }
    }
}

