using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Modern, decoupled Login Manager for BeastWar / 12 Tails.
/// Fully self-healing at runtime and in editor: upgrades legacy Login_h to standard Button,
/// bridges InputField / TMP_InputField, and ensures EventSystem responds to clicks.
/// </summary>
public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance { get; private set; }

    [Header("UI References")]
    public InputField legacyInputField;
    public Text legacyText;
    public TMP_InputField tmpInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI tmpText;

    [Header("Buttons")]
    public Button loginButton;
    public Button guestLoginButton;
    public Button quitButton;

    [Header("Settings")]
    public string defaultFamilyName = "12TailsOffline";
    public string nextSceneName = "L10_LobbyGuild";
    public float fadeDuration = 0.4f;

    [Header("Audio (Optional)")]
    public AudioSource audioSource;
    public AudioClip clickSfx;

    // Automatically inject LoginManager into A01_SecureLogin if not already in the scene!
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "A01_SecureLogin" && Instance == null)
        {
            GameObject managerObj = GameObject.Find("LoginManager");
            if (managerObj == null)
            {
                managerObj = new GameObject("LoginManager");
                managerObj.AddComponent<LoginManager>();
                Debug.Log("[LoginManager] Auto-created LoginManager in A01_SecureLogin scene.");
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

        SetupModernUIComponents();
    }

    private void Start()
    {
        // Auto-detect InputField references if not manually assigned
        if (legacyInputField == null) legacyInputField = FindObjectOfType<InputField>();
        if (tmpInputField == null) tmpInputField = FindObjectOfType<TMP_InputField>();

        // Load saved family name
        string savedName = PlayerPrefs.GetString("fam_name", defaultFamilyName);
        SetInputFieldText(savedName);

        // Bind buttons
        if (loginButton != null)
        {
            loginButton.onClick.RemoveListener(Login);
            loginButton.onClick.AddListener(Login);
        }

        if (guestLoginButton != null)
        {
            guestLoginButton.onClick.RemoveListener(LoginAsGuest);
            guestLoginButton.onClick.AddListener(LoginAsGuest);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    private void Update()
    {
        // Keyboard shortcuts (Enter, Keypad Enter, Space)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Login();
        }
    }

    /// <summary>
    /// Upgrades legacy button objects to modern standard Button with proper raycasting and event handling.
    /// </summary>
    private void SetupModernUIComponents()
    {
        // Find Login_h button
        GameObject loginObj = GameObject.Find("Login_h");
        if (loginObj != null)
        {
            // Disable or remove legacy button_hold which was intercepting and swallowing clicks
            button_hold legacyHold = loginObj.GetComponent<button_hold>();
            if (legacyHold != null)
            {
                legacyHold.enabled = false;
            }

            // Ensure a standard UnityEngine.UI.Button component exists
            Button btn = loginObj.GetComponent<Button>();
            if (btn == null)
            {
                btn = loginObj.AddComponent<Button>();
                ColorBlock colors = btn.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = new Color(0.9f, 0.9f, 1f, 1f);
                colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
                colors.selectedColor = Color.white;
                btn.colors = colors;
            }

            btn.onClick.RemoveListener(Login);
            btn.onClick.AddListener(Login);
            loginButton = btn;

            // Ensure graphic raycaster and raycastTarget are active on the graphic
            Graphic graphic = loginObj.GetComponent<Graphic>();
            if (graphic != null)
            {
                graphic.raycastTarget = true;
            }
        }

        // Ensure EventSystem has a responsive input module in Unity 6
        EventSystem es = FindObjectOfType<EventSystem>();
        if (es != null)
        {
            // If InputSystemUIInputModule is available, ensure it's functional
            BaseInputModule bim = es.GetComponent<BaseInputModule>();
            if (bim != null && !bim.enabled)
            {
                bim.enabled = true;
            }
        }
    }

    public void Login()
    {
        string enteredName = GetEnteredName();
        if (string.IsNullOrEmpty(enteredName))
        {
            enteredName = defaultFamilyName;
        }

        PlayClickSound();

        PlayerPrefs.SetString("fam_name", enteredName);
        PlayerPrefs.Save();

        Debug.Log($"[LoginManager] Logged in successfully as: {enteredName} -> Loading {nextSceneName}");
        Initiate.Fade(nextSceneName, Color.white, fadeDuration);
    }

    public void LoginAsGuest()
    {
        string guestName = "Guest_" + Random.Range(100, 999);
        SetInputFieldText(guestName);
        Login();
    }

    public void QuitGame()
    {
        PlayClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private string GetEnteredName()
    {
        if (tmpInputField != null && !string.IsNullOrEmpty(tmpInputField.text))
            return tmpInputField.text.Trim();

        if (legacyInputField != null && !string.IsNullOrEmpty(legacyInputField.text))
            return legacyInputField.text.Trim();

        if (legacyText != null && !string.IsNullOrEmpty(legacyText.text))
            return legacyText.text.Trim();

        if (tmpText != null && !string.IsNullOrEmpty(tmpText.text))
            return tmpText.text.Trim();

        return string.Empty;
    }

    private void SetInputFieldText(string val)
    {
        if (tmpInputField != null) tmpInputField.text = val;
        if (legacyInputField != null) legacyInputField.text = val;
        if (legacyText != null && string.IsNullOrEmpty(legacyText.text)) legacyText.text = val;
        if (tmpText != null && string.IsNullOrEmpty(tmpText.text)) tmpText.text = val;
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSfx != null)
        {
            audioSource.PlayOneShot(clickSfx);
        }
    }
}
