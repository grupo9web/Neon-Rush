using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private string playerName;
    private int score;

    public Player(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getScore()
    {
        return score;
    }
}

public class UIManager : MonoBehaviour
{
    public GameObject setNameMenu, confirmAnon, mainMenu, settingsMenu, leaderboardDefaultMenu, leaderboardDonSimonMenu, creditsMenu, gameModeMenu;
    public InputField nameInputField;
    public Button confirmNameButton;
    public Slider volumeSlider;

    private int languageIndex, maxLanguageIndex = 2;
    private string[] languages = { "ESPAÑOL", "ENGLISH", "FRANÇAIS" };
    private float mainVolume;

    private List<Player> playersListDefault = new List<Player>();
    private List<Player> playersListDonSimon = new List<Player>();

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        SetVolumeStart();
        SetLanguageStart();
        SetUserNameStart();
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
    private void SetLanguageStart()
    {
        // SI ES LA PRIMERA VEZ QUE SE ABRE EL JUEGO
        if (PlayerPrefs.GetInt("LANGUAGE", 100) == 100)
        {
            languageIndex = 0;
            PlayerPrefs.SetInt("LANGUAGE", 0);
            SetLanguage(0);
        }
        else
        {
            languageIndex = PlayerPrefs.GetInt("LANGUAGE");
            SetLanguage(languageIndex);
        }
        
        settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = languages[languageIndex];
    }
    private void SetUserNameStart()
    {
        // SI EL USUARIO NO HA INTRODUCIDO NOMBRE O LO ABRE POR PRIMERA VEZ
        if (PlayerPrefs.GetString("USERNAME").StartsWith("Hulio-") || (PlayerPrefs.GetString("USERNAME", "USERNAME_IS_EMPTY") == "USERNAME_IS_EMPTY"))
        {
            setNameMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }
    
    private void SetLanguage(int language)
    {
        switch (languageIndex)
        {
            case 0: //ESPAÑOL

                // Set Name Menu
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "¡Bienvenid@ a Neon Rush!";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "INTRODUCE TU NOMBRE";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "*Si no pones nada se generará un nombre aleatorio*";
                setNameMenu.GetComponentsInChildren<Text>()[0].text = "Tu nombre aquí...";

                // Confirm Anon Menu
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "¿Seguro que no quieres usar un nombre?";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLVER";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CONTINUAR";

                // Main Menu
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "NUEVA PARTIDA";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "AJUSTES";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "MARCADORES \n MODO NORMAL";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "MARCADORES \n MODO DON SIMÓN";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CRÉDITOS";

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "AJUSTES";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "IDIOMA";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUMEN";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CAMBIAR NOMBRE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "VOLVER";

                // Leaderboard
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "MARCADORES";
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLVER";
                // Leaderboard
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "MARCADORES";
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLVER";
                
                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CRÉDITOS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "VOLVER";

                // Gamemode Menu
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "ELIGE EL MODO DE JUEGO";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "NORMAL";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "DON SIMÓN";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLVER";

                break;
            case 1: //INGLÉS

                // Set Name Menu
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Welcome to Neon Rush!";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "ENTER YOUR NAME";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "*If you do not put anything, a random name will be generated*";
                setNameMenu.GetComponentsInChildren<Text>()[0].text = "Your name here...";

                // Confirm Anon Menu
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Are you sure you do not want to use a name?";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "BACK";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CONTINUE";

                // Main Menu
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "NEW GAME";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "SETTINGS";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "LEADERBOARD \n DEFAULT MODE";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "LEADERBOARD \n SIR SIMON MODE";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CREDITS";

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "SETTINGS";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "LANGUAGE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CHANGE NAME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "BACK";

                // Leaderboard
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "LEADERBOARD";
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "BACK";
                // Leaderboard
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "LEADERBOARD";
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "BACK";
                
                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CREDITS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "BACK";

                // Gamemode Menu
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CHOOSE GAMEMODE";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "DEFAULT";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "SIR SIMON";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "BACK";

                break;
            case 2: //FRANCÉS

                // Set Name Menu
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Bienvenue dans Neon Rush!";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "ENTREZ VOTRE NOM";
                setNameMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "*Si vous ne mettez rien, un nom aléatoire sera généré*";
                setNameMenu.GetComponentsInChildren<Text>()[0].text = "Votre nom ici...";

                // Confirm Anon Menu
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Êtes-vous sûr de ne pas vouloir utiliser un nom?";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "REVENIR";
                confirmAnon.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CONTINUER";

                // Main Menu
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "NOUVEAU JEU";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "AJUSTEMENTS";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CLASSEMENT";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "CRÉDITS";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "CLASSEMENT \n NORMAL MODE";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "CLASSEMENT \n DON SIMON MODE";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CRÉDITS";

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "AJUSTEMENTS";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "LANGAGE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "REBAPTISER";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "REVENIR";

                // Leaderboard Default
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CLASSEMENT";
                leaderboardDefaultMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "REVENIR";
                // Leaderboard Don Simon
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CLASSEMENT";
                leaderboardDonSimonMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "REVENIR";

                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CRÉDITS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "REVENIR";

                // Gamemode Menu
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CHOISISSEZ LE MODE DE JEU";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "NORMAL";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "DON SIMON";
                gameModeMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "REVENIR";
                break;
            default:
                break;
        }
    }

    public void OnConfirmNamePress()
    {
        if(nameInputField.text == "")
        {
            confirmAnon.SetActive(true);
            nameInputField.interactable = false;
            confirmNameButton.interactable = false;
        }
        else
        {
            PlayerPrefs.SetString("USERNAME", nameInputField.text);
            setNameMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void OnBackNamePress()
    {
        confirmAnon.SetActive(false);
        setNameMenu.SetActive(true);
        nameInputField.interactable = true;
        confirmNameButton.interactable = true;
    }

    public void OnContinueNamePress()
    {
        // Poner nombre aleatorio
        if (!PlayerPrefs.GetString("USERNAME").StartsWith("Hulio-"))
        {
            string userName = "Hulio-" + System.DateTime.Now.ToString("hh-mm-ss");
            PlayerPrefs.SetString("USERNAME", userName);
        }

        confirmAnon.SetActive(false);
        nameInputField.interactable = true;
        confirmNameButton.interactable = true;
        setNameMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnNewGameClick()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        // Panel de modos de Juego
        gameModeMenu.SetActive(true);

        // Deshabilitar botones del menu principal
        mainMenu.GetComponentsInChildren<Button>()[0].interactable = false;
        mainMenu.GetComponentsInChildren<Button>()[1].interactable = false;
        mainMenu.GetComponentsInChildren<Button>()[2].interactable = false;
        mainMenu.GetComponentsInChildren<Button>()[3].interactable = false;
    }

    public void OnMainMenuButtonsClick(int clickType)
    {
        mainMenu.SetActive(false);
        switch (clickType)
        {
            case 1:
                settingsMenu.SetActive(true);
                break;
            case 2:
                UpdateLeaderBoardDefault();
                RefreshLeaderboardDefault();
                leaderboardDefaultMenu.SetActive(true);
                break;
            case 3:
                creditsMenu.SetActive(true);
                break;
            case 4:
                settingsMenu.SetActive(false);
                setNameMenu.SetActive(true);
                break;
            case 5:
                // Modo Normal
                mainMenu.GetComponentsInChildren<Button>()[0].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[1].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[2].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[3].interactable = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
                break;
            case 6:
                // Modo Don Simon
                mainMenu.GetComponentsInChildren<Button>()[0].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[1].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[2].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[3].interactable = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene("DonSimon");
                break;
            case 7:
                UpdateLeaderBoardDonSimon();
                RefreshLeaderboardDonSimon();
                leaderboardDonSimonMenu.SetActive(true);
                break;
        }
    }

    public void OnBackButtonsClick(int clickType)
    {
        switch (clickType)
        {
            case 1:
                settingsMenu.SetActive(false);
                break;
            case 2:
                leaderboardDefaultMenu.SetActive(false);
                break;
            case 3:
                creditsMenu.SetActive(false);
                break;
            case 4:
                gameModeMenu.SetActive(false);
                mainMenu.GetComponentsInChildren<Button>()[0].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[1].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[2].interactable = true;
                mainMenu.GetComponentsInChildren<Button>()[3].interactable = true;
                break;
            case 5:
                leaderboardDonSimonMenu.SetActive(false);
                break;
        }

        mainMenu.SetActive(true);
    }

    public void OnChangeLanguageClick(bool nextLanguage)
    {
        if (nextLanguage)
        {
            if (languageIndex == maxLanguageIndex)
                languageIndex = 0;
            else
                languageIndex++;
        }
        else
        {
            if (languageIndex == 0)
                languageIndex = maxLanguageIndex;
            else
                languageIndex--;
        }

        settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = languages[languageIndex];
        PlayerPrefs.SetInt("LANGUAGE", languageIndex);
        SetLanguage(languageIndex);
    }

    public void OnGithubButtonClick(int githubNum)
    {
        switch (githubNum)
        {
            case 0: Application.OpenURL("https://github.com/jpguirado");
                break;
            case 1: Application.OpenURL("https://github.com/ularrarte"); 
                break;
            case 2: Application.OpenURL("https://github.com/JorgeMS05");
                break;
            case 3: Application.OpenURL("https://github.com/jmorenomorales");
                break;
        }
    }

    private void RefreshLeaderboardDefault()
    {
        if(PlayerPrefs.GetString("LEADERBOARD", "NOLEADERBOARDAVAILABLE") != "NOLEADERBOARDAVAILABLE")
        {
            string[] leaders = PlayerPrefs.GetString("LEADERBOARD").Split('|');
            for(int i = 0; i < leaders.Length; i++)
            {
                string[] currentLeader = leaders[i].Split('%');
                leaderboardDefaultMenu.GetComponentsInChildren<Text>()[i * 3 + 1].text = currentLeader[0];
                leaderboardDefaultMenu.GetComponentsInChildren<Text>()[i * 3 + 2].text = currentLeader[1];
            }
        }
    }

    private void UpdateLeaderBoardDefault()
    {
        string[] leadersArray = PlayerPrefs.GetString("LEADERBOARD").Split('|');

        string finalLeaders = "";

        // Recorremos el array de jugadores en la lista de marcadores y los metemos en la lista de jugadores

        if (leadersArray[0] == "")
            return;

        for (int i = 0; i < leadersArray.Length; i++)
        {
            string[] currentLeader = leadersArray[i].Split('%');
            playersListDefault.Add(new Player(currentLeader[0], int.Parse(currentLeader[1])));
        }

        // Ordenamos la lista de jugadores de mayor a menor puntuación
        for (int i = 0; i < playersListDefault.Count; i++)
        {
            for (int j = i + 1; j < playersListDefault.Count; j++)
            {
                if(playersListDefault[j].getScore() > playersListDefault[i].getScore())
                {
                    Player playerHelper = playersListDefault[i];
                    playersListDefault[i] = playersListDefault[j];
                    playersListDefault[j] = playerHelper;
                }
            }
        }

        for (int i = 0; i < playersListDefault.Count; i++)
        {
            for(int j = i + 1; j < playersListDefault.Count; j++)
            {
                if(playersListDefault[i].getPlayerName() == playersListDefault[j].getPlayerName())
                {
                    playersListDefault.RemoveAt(j);
                    j--;
                }
            }
        }

        // Limitamos la lista a 10 elementos tras la ordenación
        while (playersListDefault.Count > 10)
        {
            playersListDefault.RemoveAt(playersListDefault.Count - 1);
        }

        // Recorremos la lista y metemos los jugadores en el string de PlayerPrefs
        for (int i = 0; i < playersListDefault.Count; i++)
        {
            // Último elemento de la lista
            if(i == playersListDefault.Count - 1)
            {
                finalLeaders += playersListDefault[i].getPlayerName() + "%" + playersListDefault[i].getScore().ToString();
            }
            else
            {
                finalLeaders += playersListDefault[i].getPlayerName() + "%" + playersListDefault[i].getScore().ToString() + "|";
            }
        }

        PlayerPrefs.SetString("LEADERBOARD", finalLeaders);
    }

    private void RefreshLeaderboardDonSimon()
    {
        if (PlayerPrefs.GetString("LEADERBOARDDONSIMON", "NOLEADERBOARDAVAILABLE") != "NOLEADERBOARDAVAILABLE")
        {
            string[] leaders = PlayerPrefs.GetString("LEADERBOARDDONSIMON").Split('|');
            for (int i = 0; i < leaders.Length; i++)
            {
                string[] currentLeader = leaders[i].Split('%');
                leaderboardDonSimonMenu.GetComponentsInChildren<Text>()[i * 3 + 1].text = currentLeader[0];
                leaderboardDonSimonMenu.GetComponentsInChildren<Text>()[i * 3 + 2].text = currentLeader[1];
            }
        }
    }

    private void UpdateLeaderBoardDonSimon()
    {
        string[] leadersArray = PlayerPrefs.GetString("LEADERBOARDDONSIMON").Split('|');

        string finalLeaders = "";

        // Recorremos el array de jugadores en la lista de marcadores y los metemos en la lista de jugadores

        if (leadersArray[0] == "")
            return;

        for (int i = 0; i < leadersArray.Length; i++)
        {
            string[] currentLeader = leadersArray[i].Split('%');
            playersListDonSimon.Add(new Player(currentLeader[0], int.Parse(currentLeader[1])));
        }

        // Ordenamos la lista de jugadores de mayor a menor puntuación
        for (int i = 0; i < playersListDonSimon.Count; i++)
        {
            for (int j = i + 1; j < playersListDonSimon.Count; j++)
            {
                if (playersListDonSimon[j].getScore() > playersListDonSimon[i].getScore())
                {
                    Player playerHelper = playersListDonSimon[i];
                    playersListDonSimon[i] = playersListDonSimon[j];
                    playersListDonSimon[j] = playerHelper;
                }
            }
        }

        for (int i = 0; i < playersListDonSimon.Count; i++)
        {
            for (int j = i + 1; j < playersListDonSimon.Count; j++)
            {
                if (playersListDonSimon[i].getPlayerName() == playersListDonSimon[j].getPlayerName())
                {
                    playersListDonSimon.RemoveAt(j);
                    j--;
                }
            }
        }

        // Limitamos la lista a 10 elementos tras la ordenación
        while (playersListDonSimon.Count > 10)
        {
            playersListDonSimon.RemoveAt(playersListDonSimon.Count - 1);
        }

        // Recorremos la lista y metemos los jugadores en el string de PlayerPrefs
        for (int i = 0; i < playersListDonSimon.Count; i++)
        {
            // Último elemento de la lista
            if (i == playersListDonSimon.Count - 1)
            {
                finalLeaders += playersListDonSimon[i].getPlayerName() + "%" + playersListDonSimon[i].getScore().ToString();
            }
            else
            {
                finalLeaders += playersListDonSimon[i].getPlayerName() + "%" + playersListDonSimon[i].getScore().ToString() + "|";
            }
        }

        PlayerPrefs.SetString("LEADERBOARDDONSIMON", finalLeaders);
    }
}