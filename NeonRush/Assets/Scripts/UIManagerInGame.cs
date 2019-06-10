using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerInGame : MonoBehaviour
{
    public GameObject pauseMenu, gameOverMenu;
    public static UIManagerInGame Instance { set; get; }
    public Slider volumeSlider;

    private static int language;
    private float mainVolume;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Time.timeScale = 1f;
        language = PlayerPrefs.GetInt("LANGUAGE");

        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        SetVolumeStart();
        SetLanguage(language);
    }

    public void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat("VOLUME", volumeSlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
    }

    private void SetVolumeStart()
    {
        if (PlayerPrefs.GetFloat("VOLUME", -1) == -1)
        {
            PlayerPrefs.SetFloat("VOLUME", 0.75f);
            volumeSlider.value = 0.75f;
            mainVolume = PlayerPrefs.GetFloat("VOLUME");
        }
        else
        {
            mainVolume = PlayerPrefs.GetFloat("VOLUME");
            volumeSlider.value = mainVolume;
        }
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
    }

    private void SetLanguage(int language)
    {
        switch (language)
        {
            case 0:
                //Pause Menu
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "PAUSA";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLÚMEN";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CONTINUAR";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "MENÚ PRINCIPAL";

                //Game Over Menu
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "FIN DEL JUEGO";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "PUNTUACIÓN : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "TOP MARCADORES : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "REINTENTAR";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "MENÚ PRINCIPAL";
                break;
            case 1:
                //Pause Menu
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "PAUSE";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLUME";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "RESUME";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "MAIN MENU";

                //Game Over Menu
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "GAME OVER";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "SCORE : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "TOP LEADERBOARD : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "RETRY";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "MAIN MENU";
                break;
            case 2:
                //Pause Menu
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "PAUSE";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLUME";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CONTINUER";
                pauseMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "MENU PRINCIPAL";

                //Game Over Menu
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "FIN DE JEU";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "SCORE : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "TOP CLASSEMENT : ";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "RÉESAYER";
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "MENU PRINCIPAL";
                break;
        }
    }

    public void OnDeath(string score)
    {
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text += score;
        if(SceneManager.GetActiveScene().name == "DonSimon")
        {
            if (PlayerPrefs.GetString("LEADERBOARDDONSIMON") != "")
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text += " " + PlayerPrefs.GetString("LEADERBOARDDONSIMON").Split('|')[0].Split('%')[0] + " - " +
                                                                                     PlayerPrefs.GetString("LEADERBOARDDONSIMON").Split('|')[0].Split('%')[1].ToString();
        }
        else
        {
            if (PlayerPrefs.GetString("LEADERBOARD") != "")
                gameOverMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text += " " + PlayerPrefs.GetString("LEADERBOARD").Split('|')[0].Split('%')[0] + " - " +
                                                                                     PlayerPrefs.GetString("LEADERBOARD").Split('|')[0].Split('%')[1].ToString();
        }
        

    }

    public void OnGameButtonsClick(int clickType)
    {
        switch (clickType)
        {
            case 0: //PauseButton
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                break;
            case 1: //ResumeButton
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                break;
            case 2: //MainMenuButton
                SceneManager.LoadScene("MainMenu");
                break;
            case 3: //RetryButton
                if(SceneManager.GetActiveScene().name == "DonSimon")
                    SceneManager.LoadScene("DonSimon");
                else
                    SceneManager.LoadScene("SampleScene");
                Physics.gravity = new Vector3(0, -9.8f, 0);
                break;
        }
    }
}