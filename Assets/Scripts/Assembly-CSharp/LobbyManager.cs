using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Modern, decoupled Lobby & Character Selection/Creation Manager for BeastWar / 12 Tails.
/// Fully self-healing at runtime and in editor: replaces legacy click swallowers with standard Buttons,
/// manages Character Select / Character Create menus, and supports offline player mock.
/// </summary>
public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }

    [Header("GUI Roots")]
    public GameObject charSelGui;
    public GameObject charCreateGui;

    [Header("Modern UI Elements")]
    public TMP_InputField characterNameInput;
    public TextMeshProUGUI statusText;

    [Header("Buttons")]
    public Button selectButton;
    public Button deleteButton;
    public Button arrowLeftButton;
    public Button arrowRightButton;
    public Button openCreateButton;
    public Button confirmCreateButton;
    public Button cancelCreateButton;
    public Button randomFaceButton;

    [Header("Settings")]
    public string nextSceneName = "G31_GuildPlain_n";
    public float fadeDuration = 0.5f;

    // Auto-create LobbyManager in L10_LobbyGuild scene if not present
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "L10_LobbyGuild" && Instance == null)
        {
            GameObject managerObj = GameObject.Find("LobbyManager");
            if (managerObj == null)
            {
                managerObj = new GameObject("LobbyManager");
                managerObj.AddComponent<LobbyManager>();
                Debug.Log("[LobbyManager] Auto-created LobbyManager in L10_LobbyGuild scene.");
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

        FindAndBindUIComponents();
    }

    private void Start()
    {
        EnsureDefaultOfflineCharacter();
    }

    private void Update()
    {
        // Keyboard navigation
        if (charCreateGui != null && charCreateGui.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                OnConfirmCreateClicked();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnCancelCreateClicked();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            {
                OnSelectCharacterClicked();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnArrowLeftClicked();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnArrowRightClicked();
            }
        }
    }

    public void FindAndBindUIComponents()
    {
        // 1. Find GUI Roots
        if (charSelGui == null) charSelGui = GameObject.Find("Char_sel_gui");
        if (charCreateGui == null) charCreateGui = GameObject.Find("Char_create_gui");

        // 2. Find TMPro InputField
        if (characterNameInput == null)
        {
            characterNameInput = FindObjectOfType<TMP_InputField>();
        }

        // 3. Ensure and bind Buttons
        selectButton = BindButton("sel_b", OnSelectCharacterClicked, selectButton);
        deleteButton = BindButton("del_b", OnDeleteCharacterClicked, deleteButton);
        arrowLeftButton = BindButton("b_arrow_l", OnArrowLeftClicked, arrowLeftButton);
        arrowRightButton = BindButton("b_arrow_r", OnArrowRightClicked, arrowRightButton);
        openCreateButton = BindButton("botton_create_char", OnOpenCreateClicked, openCreateButton);
        confirmCreateButton = BindButton("Create_button", OnConfirmCreateClicked, confirmCreateButton);
        cancelCreateButton = BindButton("Cancel_create_char", OnCancelCreateClicked, cancelCreateButton);
        randomFaceButton = BindButton("button_random_face", OnRandomFaceClicked, randomFaceButton);

        // 4. Ensure EventSystem input module
        EventSystem es = FindObjectOfType<EventSystem>();
        if (es != null)
        {
            BaseInputModule bim = es.GetComponent<BaseInputModule>();
            if (bim != null && !bim.enabled) bim.enabled = true;
        }
    }

    private Button BindButton(string objName, UnityEngine.Events.UnityAction action, Button existing)
    {
        if (existing != null)
        {
            existing.onClick.RemoveListener(action);
            existing.onClick.AddListener(action);
            return existing;
        }

        GameObject obj = GameObject.Find(objName);
        if (obj == null) return null;

        // Ensure Graphic raycasting
        Graphic g = obj.GetComponent<Graphic>();
        if (g != null) g.raycastTarget = true;

        Button btn = obj.GetComponent<Button>();
        if (btn == null)
        {
            btn = obj.AddComponent<Button>();
            ColorBlock cb = btn.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = new Color(0.9f, 0.95f, 1f, 1f);
            cb.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
            cb.selectedColor = Color.white;
            btn.colors = cb;
        }

        btn.onClick.RemoveListener(action);
        btn.onClick.AddListener(action);
        return btn;
    }

    public void OnSelectCharacterClicked()
    {
        LobbyCamera lbc = FindObjectOfType<LobbyCamera>();
        int selIndex = lbc != null ? lbc.nSelb : 1;

        string targetPlayerName = "player_chr" + selIndex;
        GameObject playerObj = GameObject.Find(targetPlayerName);

        // If character does not exist yet, spawn or activate fallback
        if (playerObj == null)
        {
            playerObj = GameObject.Find("player_chr1");
        }

        if (playerObj != null)
        {
            PlayerControl pct = playerObj.GetComponent<PlayerControl>();
            if (pct != null)
            {
                pct.Islobby = false;
                if (string.IsNullOrEmpty(pct.character_name))
                {
                    pct.character_name = "Hero";
                }
            }
            DontDestroyOnLoad(playerObj);
        }

        if (charSelGui != null) charSelGui.SetActive(false);

        Debug.Log($"[LobbyManager] Starting game -> loading {nextSceneName}");
        Initiate.Fade(nextSceneName, Color.white, fadeDuration);
    }

    public void OnOpenCreateClicked()
    {
        b_create_char bcc = FindObjectOfType<b_create_char>();
        if (bcc != null)
        {
            bcc.OnPointerDown(null);
        }
        else
        {
            if (charSelGui != null) charSelGui.SetActive(false);
            if (charCreateGui != null) charCreateGui.SetActive(true);
        }
    }

    public void OnConfirmCreateClicked()
    {
        Create_button cb = FindObjectOfType<Create_button>();
        if (cb != null)
        {
            cb.ExecuteCreate();
        }
        else
        {
            if (charCreateGui != null) charCreateGui.SetActive(false);
            if (charSelGui != null) charSelGui.SetActive(true);
        }
    }

    public void OnCancelCreateClicked()
    {
        Cancel_create_char ccc = FindObjectOfType<Cancel_create_char>();
        if (ccc != null)
        {
            ccc.OnPointerDown(null);
        }
        else
        {
            if (charCreateGui != null) charCreateGui.SetActive(false);
            if (charSelGui != null) charSelGui.SetActive(true);
        }
    }

    public void OnArrowLeftClicked()
    {
        sel_char_l scl = FindObjectOfType<sel_char_l>();
        if (scl != null)
        {
            scl.OnPointerDown(null);
        }
    }

    public void OnArrowRightClicked()
    {
        sel_char_r scr = FindObjectOfType<sel_char_r>();
        if (scr != null)
        {
            scr.OnPointerDown(null);
        }
    }

    public void OnDeleteCharacterClicked()
    {
        del_b_h dbh = FindObjectOfType<del_b_h>();
        if (dbh != null)
        {
            dbh.OnPointerDown(null);
        }
    }

    public void OnRandomFaceClicked()
    {
        rd_face rdf = FindObjectOfType<rd_face>();
        if (rdf != null)
        {
            rdf.OnPointerDown(null);
        }
    }

    private void EnsureDefaultOfflineCharacter()
    {
        // Check if at least one character exists in the lobby
        GameObject p1 = GameObject.Find("player_chr1");
        if (p1 != null)
        {
            PlayerControl pct = p1.GetComponent<PlayerControl>();
            if (pct != null && string.IsNullOrEmpty(pct.character_name))
            {
                pct.character_name = "Hero";
            }
        }
    }
}

