using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Dedicated Game Scene Manager for BeastWar / 12 Tails.
/// Handles offline player spawning, modern TextMeshPro HUD updates,
/// Escape Menu, and seamless scene navigation across all town & adventure maps.
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("UI References (TMPro)")]
    public TextMeshProUGUI familyNameText;
    public TextMeshProUGUI characterNameLvText;
    public TextMeshProUGUI charNameBoxText;
    public TextMeshProUGUI hpText;

    [Header("In-Game Menu")]
    public GameObject optionWindow;
    public Button resumeButton;
    public Button lobbyButton;
    public Button quitButton;

    [Header("Player Reference")]
    public GameObject activePlayer;

    private bool isOptionOpen = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // Only run in gameplay scenes (not Login and not Lobby)
        if (currentScene.name != "A01_SecureLogin" && currentScene.name != "L10_LobbyGuild" && Instance == null)
        {
            GameObject managerObj = GameObject.Find("GameSceneManager");
            if (managerObj == null)
            {
                managerObj = new GameObject("GameSceneManager");
                managerObj.AddComponent<GameSceneManager>();
                Debug.Log($"[GameSceneManager] Auto-initialized in gameplay scene: {currentScene.name}");
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        EnsurePlayerExists();
        SetupHudAndMenu();
        SetupEventSystem();
    }

    private void Update()
    {
        // Toggle Escape Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }

        // Live HUD update
        UpdateHudStats();
    }

    public void EnsurePlayerExists()
    {
        activePlayer = GameObject.FindWithTag("Player");
        if (activePlayer == null)
        {
            // Search by common character names
            activePlayer = GameObject.Find("player_chr1") ?? GameObject.Find("player_chr2") ?? GameObject.Find("player_chr3");
        }

        // If still null, check if any StartPoint can spawn or spawn offline hero
        if (activePlayer == null)
        {
            StartPoint1 sp1 = FindObjectOfType<StartPoint1>();
            if (sp1 != null)
            {
                sp1.SpawnOfflinePlayerIfNeeded();
                activePlayer = GameObject.FindWithTag("Player");
            }
        }

        // Setup camera target
        CameraControl cct = FindObjectOfType<CameraControl>();
        if (cct != null && activePlayer != null)
        {
            cct.target = activePlayer;
        }

        TownCamera tc = FindObjectOfType<TownCamera>();
        if (tc != null && activePlayer != null)
        {
            tc.target = activePlayer.transform;
        }

        Camera_town ct = FindObjectOfType<Camera_town>();
        if (ct != null && activePlayer != null)
        {
            ct.target = activePlayer.transform;
        }
    }

    public void SetupHudAndMenu()
    {
        // Find TMPro HUD elements
        GameObject famObj = GameObject.Find("FamilyName");
        if (famObj != null) familyNameText = famObj.GetComponent<TextMeshProUGUI>();

        GameObject charLvObj = GameObject.Find("CharacterNameLv");
        if (charLvObj != null) characterNameLvText = charLvObj.GetComponent<TextMeshProUGUI>();

        GameObject charBoxObj = GameObject.Find("char_name_box");
        if (charBoxObj != null) charNameBoxText = charBoxObj.GetComponent<TextMeshProUGUI>();

        GameObject hpObj = GameObject.Find("HP_Text");
        if (hpObj != null) hpText = hpObj.GetComponent<TextMeshProUGUI>();

        // Find Option Window
        optionWindow = GameObject.Find("Optionwindow");
        if (optionWindow != null)
        {
            optionWindow.SetActive(false);
            isOptionOpen = false;
        }

        // Setup Quit Button
        GameObject quitObj = GameObject.Find("Quit Game");
        if (quitObj != null)
        {
            quitButton = quitObj.GetComponent<Button>();
            if (quitButton == null) quitButton = quitObj.AddComponent<Button>();
            quitButton.onClick.RemoveListener(QuitToLobby);
            quitButton.onClick.AddListener(QuitToLobby);
        }
    }

    public void SetupEventSystem()
    {
        EventSystem es = FindObjectOfType<EventSystem>();
        if (es != null)
        {
            BaseInputModule bim = es.GetComponent<BaseInputModule>();
            if (bim != null && !bim.enabled) bim.enabled = true;
        }
    }

    public void ToggleOptionMenu()
    {
        if (optionWindow == null) optionWindow = GameObject.Find("Optionwindow");
        if (optionWindow != null)
        {
            isOptionOpen = !isOptionOpen;
            optionWindow.SetActive(isOptionOpen);
        }
    }

    public void ResumeGame()
    {
        if (optionWindow != null)
        {
            optionWindow.SetActive(false);
            isOptionOpen = false;
        }
    }

    public void QuitToLobby()
    {
        Initiate.Fade("L10_LobbyGuild", Color.white, 0.5f);
    }

    public void QuitToLogin()
    {
        Initiate.Fade("A01_SecureLogin", Color.white, 0.5f);
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateHudStats()
    {
        if (activePlayer == null) return;

        PlayerControl pct = activePlayer.GetComponent<PlayerControl>();
        if (pct == null) return;

        string charName = string.IsNullOrEmpty(pct.character_name) ? "Hero" : pct.character_name;
        string famName = string.IsNullOrEmpty(pct.family_name) ? PlayerPrefs.GetString("fam_name", "12TailsOffline") : pct.family_name;

        if (familyNameText != null && familyNameText.text != famName)
        {
            familyNameText.SetText(famName);
        }

        if (characterNameLvText != null)
        {
            string displayLv = $"{charName} Lv.{pct.cLv}";
            if (characterNameLvText.text != displayLv)
            {
                characterNameLvText.SetText(displayLv);
            }
        }

        if (charNameBoxText != null && charNameBoxText.text != charName)
        {
            charNameBoxText.SetText(charName);
        }

        if (hpText != null)
        {
            string hpDisplay = $"{pct.cHp} / {pct.cMaxhp}";
            if (hpText.text != hpDisplay)
            {
                hpText.SetText(hpDisplay);
            }
        }
    }
}
