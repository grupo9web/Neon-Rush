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
    public GameObject setNameMenu, confirmAnon, mainMenu, settingsMenu, leaderboardMenu, creditsMenu;
    public InputField nameInputField;
    public Button confirmNameButton;

    private int languageIndex, maxLanguageIndex = 2;
    private string[] languages = { "ESPAÑOL", "ENGLISH", "FRANÇAIS" };

    private List<Player> playersList = new List<Player>();
    
    void Start()
    {
        setLanguageStart();
        setUserNameStart();
    }
    
    void Update()
    {
    }
    
    private void setLanguageStart()
    {
        // SI ES LA PRIMERA VEZ QUE SE ABRE EL JUEGO
        if (PlayerPrefs.GetInt("LANGUAGE", 100) == 100)
        {
            languageIndex = 0;
            PlayerPrefs.SetInt("LANGUAGE", 0);
        }
        else
        {
            languageIndex = PlayerPrefs.GetInt("LANGUAGE");
            setLanguage(languageIndex);
        }
        
        settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = languages[languageIndex];
    }
    private void setUserNameStart()
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

    private void setLanguage(int language)
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
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "MARCADORES";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "CRÉDITOS";

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "AJUSTES";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "IDIOMA";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUMEN";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CAMBIAR NOMBRE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "VOLVER";

                // Leaderboard
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "MARCADORES";
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "VOLVER";

                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CRÉDITOS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "VOLVER";

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
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = "LEADERBOARD";
                mainMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "CREDITS";

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "SETTINGS";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "LANGUAGE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "CHANGE NAME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "BACK";

                // Leaderboard
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "LEADERBOARD";
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "BACK";

                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CREDITS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "BACK";

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

                // Settings Menu
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "AJUSTEMENTS";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "LANGAGE";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[3].text = "VOLUME";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[4].text = "REBAPTISER";
                settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "REVENIR";

                // Leaderboard
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CLASSEMENT";
                leaderboardMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "REVENIR";

                // Credits Menu
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "CRÉDITS";
                creditsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[5].text = "REVENIR";
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
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
                UpdateLeaderBoard();
                RefreshLeaderboard();
                leaderboardMenu.SetActive(true);
                break;
            case 3:
                creditsMenu.SetActive(true);
                break;
            case 4:
                settingsMenu.SetActive(false);
                setNameMenu.SetActive(true);
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
                leaderboardMenu.SetActive(false);
                break;
            case 3:
                creditsMenu.SetActive(false);
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

        Debug.Log("El índice de lenguaje es: " + languageIndex);

        settingsMenu.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[2].text = languages[languageIndex];
        PlayerPrefs.SetInt("LANGUAGE", languageIndex);
        setLanguage(languageIndex);
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

    private void RefreshLeaderboard()
    {
        if(PlayerPrefs.GetString("LEADERBOARD", "NOLEADERBOARDAVAILABLE") != "NOLEADERBOARDAVAILABLE")
        {
            string[] leaders = PlayerPrefs.GetString("LEADERBOARD").Split('|');
            for(int i = 0; i < leaders.Length; i++)
            {
                string[] currentLeader = leaders[i].Split('%');
                leaderboardMenu.GetComponentsInChildren<Text>()[i * 3 + 1].text = currentLeader[0];
                leaderboardMenu.GetComponentsInChildren<Text>()[i * 3 + 2].text = currentLeader[1];
            }
        }
    }

    private void UpdateLeaderBoard()
    {
        Debug.Log("Entro en UpdateLeaderBoard");
        //string pruebaLeader = "Kokebr%450|Nantorz%403|JoderMacho%53241|PepeLui%536|PepeLui%536|Maricarmen%132|JoderMacho%53242|PutoAmo%3154|Peasd%123|qwdqw%2356|fweg%4536|PepeLui%5384|gw4ef%345";
        string[] leadersArray = PlayerPrefs.GetString("LEADERBOARD").Split('|');

        string finalLeaders = "";

        // Recorremos el array de jugadores en la lista de marcadores y los metemos en la lista de jugadores
        for(int i = 0; i < leadersArray.Length; i++)
        {
            string[] currentLeader = leadersArray[i].Split('%');
            playersList.Add(new Player(currentLeader[0], int.Parse(currentLeader[1])));
        }
        
        Debug.Log("El nombre del primer jugador es: " + playersList[0].getPlayerName());

        // Ordenamos la lista de jugadores de mayor a menor puntuación
        for (int i = 0; i < playersList.Count; i++)
        {
            for (int j = i + 1; j < playersList.Count; j++)
            {
                if(playersList[j].getScore() > playersList[i].getScore())
                {
                    Player playerHelper = playersList[i];
                    playersList[i] = playersList[j];
                    playersList[j] = playerHelper;
                }
            }
        }

        for (int i = 0; i < playersList.Count; i++)
        {
            for(int j = i + 1; j < playersList.Count; j++)
            {
                if(playersList[i].getPlayerName() == playersList[j].getPlayerName())
                {
                    playersList.RemoveAt(j);
                    j--;
                }
            }
        }

        // Limitamos la lista a 10 elementos tras la ordenación
        while (playersList.Count > 10)
        {
            playersList.RemoveAt(playersList.Count - 1);
        }

        // Recorremos la lista y metemos los jugadores en el string de PlayerPrefs
        for (int i = 0; i < playersList.Count; i++)
        {
            // Último elemento de la lista
            if(i == playersList.Count - 1)
            {
                finalLeaders += playersList[i].getPlayerName() + "%" + playersList[i].getScore().ToString();
            }
            else
            {
                finalLeaders += playersList[i].getPlayerName() + "%" + playersList[i].getScore().ToString() + "|";
            }
        }

        Debug.Log(finalLeaders);

        PlayerPrefs.SetString("LEADERBOARD", finalLeaders);
    }
}
