using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.PostProcessing;

public class scirp : generalManager
{
    #region Variables


    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo
    private bool updateWorld = false;           // AL cambiar de modo actualizar valores

    public float speed;
    public float speedAux;
    public float score = 0;
    public float incrementoVelocidad = 0.001f;
    float timeLeft = 2.0f;

    private saltoPowerUp auxSaltoScript;

    //We tell the script more about canvas and text 
    public TMPro.TextMeshProUGUI scoreTxt;

    private Vector3 direccion;

    [SerializeField]
    private Vector3 gravedad = Physics.gravity;

    private Camera cam;                         // Ref. a cámara
    private GameObject pivot;                   // Ref. a pivot que recibe la nueva posición a la que debe ir la cámara

    //Efectos de sonido
    public AudioClip efectoSonidoMuerte;
    public AudioClip efectoSonidoCambioCamara;
    AudioSource audioSourceJugador;


    public bool velocidadReducida = false;
    public bool cegado = false;

    float scoreTxtCount = 5.0f;

    bool noReproducido = true;


    private int language;
    private string preScore;
    private bool scoreSent;

    private PostProcessingProfile profileFinal;

    #endregion

    void Start()
    {
        language = PlayerPrefs.GetInt("LANGUAGE");
        scoreSent = false;
        profileFinal = gameObject.GetComponentInChildren<PostProcessingBehaviour>().profile;
        profileFinal.vignette.Reset();

        SetLanguage(language);

        if (mode.ContainsKey("dirZpositiva"))
            stageMode = mode["dirZpositiva"];

        direccion = stageMode.getDC()[0];

        cam = gameObject.GetComponentInChildren<Camera>();
        pivot = gameObject.transform.GetChild(0).gameObject;

        setWorldSpeed(1.0f);


        audioSourceJugador = GetComponent<AudioSource>();


        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void Update()
    {
        gravedad = Physics.gravity;

        Vector3 playerHeightPos = this.gameObject.transform.position;
        Vector3 referencePosition = GameObject.Find("ListaHijos").transform.GetChild(1).transform.position;

        speed = speed + incrementoVelocidad;

        //Debug.Log("speed: " + speed);

   


        /*/
        if (speed > 4.02f  && scoreTxtCount == 0) {
            Debug.Log("chispitas rompe el saque y 40 iguales");
            //Activa el texto de salto
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Speed LVL: " + Mathf.Round(speed);
            StartCoroutine(Example());
            timeLeft = 2.0f;
            scoreTxtCount++;            
        }else if (speed > 4.08 && scoreTxtCount == 1){
            Debug.Log("warwinka");
            //Activa el texto de salto
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Speed LVL: " + Mathf.Round(speed);
            StartCoroutine(Example());
            timeLeft = 2.0f;
            scoreTxtCount++;         
        }else if (speed > 4.12 && scoreTxtCount == 2){
            Debug.Log("juan pedro comitea y el codigo perrea");
            //Activa el texto de salto
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Speed LVL: " + Mathf.Round(speed);
            StartCoroutine(Example());
            timeLeft = 2.0f;
            scoreTxtCount++;         
        }
        */
        if (Mathf.FloorToInt(speed) % scoreTxtCount == 0)
        {
            //Activa el texto de salto
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Speed LVL: " + Mathf.Round(speed);
            StartCoroutine(Example());
            timeLeft = 2.0f;
            scoreTxtCount++;
        }

        if (!velocidadReducida)
        {
            speedAux = speed;
        }

        /*/
        if (gravedad.x == -9.8f || gravedad.y == -9.8f || gravedad.z == -9.8f || gravedad.y == -9.81f){
            //Debug.Log("Willyrex");
            if (gravedad.x != 0){
                Debug.Log("Willyrex1"); //Funciona
                if(Vector3.Distance(playerHeightPos, referencePosition) < f)
                    SceneManager.LoadScene("SampleScene");
            }else if (gravedad.y != 0) {  //Gravedad y = -9.8
                Debug.Log("Willyrex2"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y -7, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else{
                Debug.Log("Willyrex3");
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y, playerHeightPos.z - 5)) < 7f)
                    SceneManager.LoadScene("SampleScene");
                }
        }else {                    
            if (gravedad.x != 0){
                Debug.Log("Willyrex4"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x - 7, playerHeightPos.y, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else if (gravedad.y != 0){
                Debug.Log("Willyrex5"); //Funciona
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y + 7, playerHeightPos.z)) < 7f)
                    SceneManager.LoadScene("SampleScene");
            }else{
                Debug.Log("Willyrex6");
                if(Vector3.Distance(playerHeightPos, new Vector3(playerHeightPos.x, playerHeightPos.y, playerHeightPos.z + 7)) < 7f)
                    SceneManager.LoadScene("SampleScene");
                }
        } 
        */


        if (Vector3.Distance(playerHeightPos, referencePosition) > 15f && noReproducido)
        {
            //Sonido muerte
            audioSourceJugador.clip = efectoSonidoMuerte;
            audioSourceJugador.Play();
            noReproducido = false;

            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "¡ GAME OVER !";

        }

        //Soy retrasado y se podia condensar en esto:
        if (Vector3.Distance(playerHeightPos, referencePosition) > 30f)
        {
            noReproducido = true;
            SceneManager.LoadScene("SampleScene");
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }





        if (Vector3.Distance(playerHeightPos, referencePosition) > 15f)
        {
            if (!scoreSent)
            {
                UIManagerInGame.Instance.OnDeath(score.ToString());
                string leaderBoard = PlayerPrefs.GetString("LEADERBOARD");
                if (leaderBoard != "")
                    leaderBoard += "|" + PlayerPrefs.GetString("USERNAME") + "%" + score.ToString();
                else
                    leaderBoard += PlayerPrefs.GetString("USERNAME") + "%" + score.ToString();
                PlayerPrefs.SetString("LEADERBOARD", leaderBoard);
                Debug.Log("La tabla esta así: " + leaderBoard);
                Debug.Log(PlayerPrefs.GetString("LEADERBOARD"));
                scoreSent = true;
            }
        }


        if (updateWorld)
        {
            Physics.gravity = stageMode.getGravity();
            direccion = stageMode.getDC()[0];
            pivot.transform.localPosition = stageMode.getPP();
            pivot.transform.LookAt(transform);
            setWorldSpeed(0.2f);

            updateWorld = false;
        }



        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("e"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (direccion == stageMode.getDC()[0])
            {
                direccion = stageMode.getDC()[1];
                pivot.transform.RotateAround(transform.position, stageMode.getCA(), -90.0f);
            }
            else if (direccion == stageMode.getDC()[1])
            {
                direccion = stageMode.getDC()[0];
                pivot.transform.RotateAround(transform.position, stageMode.getCA(), 90.0f);
            }

        }



        // Al actualizar la posición del pivot, movemos la cámara
        if (Vector3.Distance(pivot.transform.localPosition, cam.transform.localPosition) > 0.01f)
        {
            cam.transform.localPosition = Vector3.MoveTowards(cam.transform.localPosition, pivot.transform.localPosition, (75 * getWS()) * Time.deltaTime);
            cam.transform.LookAt(transform, stageMode.getCA());
        }
        else if (Vector3.Distance(pivot.transform.localPosition, cam.transform.localPosition) < 0.1f)
        {
            profileFinal.motionBlur.enabled = false;
            setWorldSpeed(1.0f);
        }

        //Debug.Log(speed * getWS() + ", " + getWS());

        float autoMove = (speed * getWS()) * Time.deltaTime;

        transform.Translate(direccion * autoMove);

    }

    private void SetLanguage(int lang)
    {
        switch (lang)
        {
            case 0:
                preScore = "PUNTUACIÓN: ";
                break;
            case 1:
                preScore = "SCORE: ";
                break;
            case 2:
                preScore = "SCORE: ";
                break;
        }
    }

    public void ScoreUpdate()
    {
        score += 10;
        //Actualizamos el txt de la puntuación
        setScoretxt();
    }

    void setScoretxt()
    {
        scoreTxt.text = preScore + score.ToString();
    }



    public string getScoretxt()
    {
        return score.ToString();
    }

    IEnumerator Example()
    {
        //print(Time.time);
        yield return new WaitForSeconds(2);
        GameObject.Find("CanvasTextoSalto").transform.GetChild(0).gameObject.SetActive(false);
        //print(Time.time);
    }


    public void updateStageMode(string key)
    {
        //Efecto sonido cambio de camara
        audioSourceJugador.clip = efectoSonidoCambioCamara;
        audioSourceJugador.Play();

        // Motion blur + ralentizacion
        profileFinal.motionBlur.enabled = true;

        //Debug.Log("El jauja del scirp");
        stageMode = mode[key];
        print("Debugito");
        updateWorld = true;
    }



    public Vector3 getDirection()
    {
        return this.direccion;
    }

}

