using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class SceneUIUpgrader
{
    private const string UpgradeKey = "BeastWar_AllScenesUpgraded_V3";

    static SceneUIUpgrader()
    {
        EditorApplication.delayCall += TriggerAutoUpgradeOnce;
    }

    [InitializeOnLoadMethod]
    private static void OnEditorLoad()
    {
        EditorApplication.delayCall += TriggerAutoUpgradeOnce;
    }

    private static void TriggerAutoUpgradeOnce()
    {
        if (!EditorPrefs.GetBool(UpgradeKey, false))
        {
            EditorPrefs.SetBool(UpgradeKey, true);
            Debug.Log("[SceneUIUpgrader] Automatically upgrading all scenes to modern UI & Managers...");
            UpgradeAllScenes();
        }
    }

    [MenuItem("12Tails/Upgrade All Scenes to Modern UI (TMPro)")]
    public static void UpgradeAllScenes()
    {
        string originalScenePath = EditorSceneManager.GetActiveScene().path;

        UpgradeLoginScene(false);
        UpgradeLobbyScene(false);
        UpgradeGameplayScenes();

        // Reopen original or Login scene
        string returnScene = !string.IsNullOrEmpty(originalScenePath) ? originalScenePath : "Assets/Scene/Scene/A01_SecureLogin.unity";
        EditorSceneManager.OpenScene(returnScene, OpenSceneMode.Single);

        Debug.Log("[SceneUIUpgrader] All 15 scenes successfully upgraded and saved!");
    }

    [MenuItem("12Tails/Upgrade Login Scene (A01_SecureLogin)")]
    public static void UpgradeLoginSceneManual()
    {
        UpgradeLoginScene(true);
    }

    [MenuItem("12Tails/Upgrade Lobby Scene (L10_LobbyGuild)")]
    public static void UpgradeLobbySceneManual()
    {
        UpgradeLobbyScene(true);
    }

    public static void UpgradeLoginScene(bool showDialog)
    {
        string path = "Assets/Scene/Scene/A01_SecureLogin.unity";
        Scene currentScene = EditorSceneManager.GetActiveScene();
        if (currentScene.name != "A01_SecureLogin" && currentScene.path != path)
        {
            currentScene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }

        TMP_FontAsset fontAsset = GetDefaultFont();

        // 1. LoginManager
        GameObject loginManagerObj = GameObject.Find("LoginManager");
        if (loginManagerObj == null)
        {
            loginManagerObj = new GameObject("LoginManager");
            Undo.RegisterCreatedObjectUndo(loginManagerObj, "Create LoginManager");
        }
        LoginManager loginManager = loginManagerObj.GetComponent<LoginManager>() ?? loginManagerObj.AddComponent<LoginManager>();

        // 2. Login_h Button
        UpgradeButton("Login_h");
        Button loginBtn = GameObject.Find("Login_h")?.GetComponent<Button>();
        if (loginBtn != null) loginManager.loginButton = loginBtn;

        // 3. User_Name and Password
        TMP_InputField userTMP = UpgradeInputFieldToTMPro("User_Name", fontAsset, "User_Name", false);
        if (userTMP != null)
        {
            loginManager.tmpInputField = userTMP;
            loginManager.legacyInputField = null;
        }

        TMP_InputField passTMP = UpgradeInputFieldToTMPro("Password", fontAsset, "Password", true);
        if (passTMP != null)
        {
            loginManager.passwordInputField = passTMP;
        }

        // 4. EventSystem
        UpgradeEventSystem();

        EditorUtility.SetDirty(loginManagerObj);
        EditorSceneManager.MarkSceneDirty(currentScene);
        EditorSceneManager.SaveScene(currentScene);

        Debug.Log("[SceneUIUpgrader] A01_SecureLogin upgraded and saved.");
        if (showDialog)
        {
            EditorUtility.DisplayDialog("12 Tails Modern UI", "A01_SecureLogin upgraded to TMPro successfully!", "OK");
        }
    }

    public static void UpgradeLobbyScene(bool showDialog)
    {
        string path = "Assets/Scene/Scene/L10_LobbyGuild.unity";
        Scene currentScene = EditorSceneManager.GetActiveScene();
        if (currentScene.name != "L10_LobbyGuild" && currentScene.path != path)
        {
            currentScene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }

        TMP_FontAsset fontAsset = GetDefaultFont();

        // 1. LobbyManager
        GameObject lobbyManagerObj = GameObject.Find("LobbyManager");
        if (lobbyManagerObj == null)
        {
            lobbyManagerObj = new GameObject("LobbyManager");
            Undo.RegisterCreatedObjectUndo(lobbyManagerObj, "Create LobbyManager");
        }
        LobbyManager lobbyManager = lobbyManagerObj.GetComponent<LobbyManager>() ?? lobbyManagerObj.AddComponent<LobbyManager>();

        // 2. Character_Name InputField
        TMP_InputField charNameTMP = UpgradeInputFieldToTMPro("Character_Name", fontAsset, "Enter Hero Name...", false);
        if (charNameTMP != null)
        {
            lobbyManager.characterNameInput = charNameTMP;
        }

        // 3. Upgrade all interactive buttons
        UpgradeButton("sel_b");
        UpgradeButton("del_b");
        UpgradeButton("b_arrow_l");
        UpgradeButton("b_arrow_r");
        UpgradeButton("botton_create_char");
        UpgradeButton("Create_button");
        UpgradeButton("Cancel_create_char");
        UpgradeButton("button_random_face");

        // 4. Update Create_button reference
        Create_button cb = Object.FindObjectOfType<Create_button>();
        if (cb != null && charNameTMP != null)
        {
            cb.tmpInputField = charNameTMP;
            cb.tmpCharacterName = charNameTMP.textComponent as TextMeshProUGUI;
            EditorUtility.SetDirty(cb.gameObject);
        }

        // 5. Update EventSystem
        UpgradeEventSystem();

        // Wire references in LobbyManager
        lobbyManager.FindAndBindUIComponents();

        EditorUtility.SetDirty(lobbyManagerObj);
        EditorSceneManager.MarkSceneDirty(currentScene);
        EditorSceneManager.SaveScene(currentScene);

        Debug.Log("[SceneUIUpgrader] L10_LobbyGuild upgraded and saved.");
        if (showDialog)
        {
            EditorUtility.DisplayDialog("12 Tails Modern UI", "L10_LobbyGuild upgraded to TMPro & LobbyManager successfully!", "OK");
        }
    }

    public static void UpgradeGameplayScenes()
    {
        string[] scenes = new string[]
        {
            "Assets/Scene/Scene/G30_NoGuild.unity",
            "Assets/Scene/Scene/G31_GuildPlain_n.unity",
            "Assets/Scene/Scene/G32_SnowCamp.unity",
            "Assets/Scene/Scene/G33_CrystalBeach.unity",
            "Assets/Scene/Scene/G34_ExcavationCamp.unity",
            "Assets/Scene/Scene/G35_UndergroundCamp.unity",
            "Assets/Scene/Scene/G36_ForestCamp.unity",
            "Assets/Scene/Scene/T51_MainStreet.unity",
            "Assets/Scene/Scene/T52_CentralSquare.unity",
            "Assets/Scene/Scene/T53_CityLibrary.unity",
            "Assets/Scene/Scene/T54_LightPark.unity",
            "Assets/Scene/Scene/T55_LightPalace.unity",
            "Assets/Scene/Scene/T56_PlainColosseum.unity"
        };

        foreach (string scenePath in scenes)
        {
            try
            {
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                if (!scene.IsValid()) continue;

                // Ensure GameSceneManager
                GameObject managerObj = GameObject.Find("GameSceneManager");
                if (managerObj == null)
                {
                    managerObj = new GameObject("GameSceneManager");
                    Undo.RegisterCreatedObjectUndo(managerObj, "Create GameSceneManager");
                    managerObj.AddComponent<GameSceneManager>();
                }

                // Ensure EventSystem
                UpgradeEventSystem();

                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
                Debug.Log($"[SceneUIUpgrader] Upgraded scene: {scene.name}");
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[SceneUIUpgrader] Skipping {scenePath}: {ex.Message}");
            }
        }
    }

    private static TMP_InputField UpgradeInputFieldToTMPro(string objName, TMP_FontAsset fontAsset, string defaultPlaceholder, bool isPassword)
    {
        GameObject inputRoot = GameObject.Find(objName);
        if (inputRoot == null) return null;

        Undo.RegisterCompleteObjectUndo(inputRoot, "Upgrade Input to TMPro");

        // 1. Read legacy InputField
        InputField legacyInput = inputRoot.GetComponent<InputField>();
        string existingText = "";
        int charLimit = 0;
        if (legacyInput != null)
        {
            existingText = legacyInput.text;
            charLimit = legacyInput.characterLimit;
            Object.DestroyImmediate(legacyInput);
        }

        // 2. Text child
        Transform textChild = inputRoot.transform.Find("Text");
        TextMeshProUGUI tmpText = null;
        if (textChild != null)
        {
            Undo.RegisterCompleteObjectUndo(textChild.gameObject, "Upgrade Text child");
            Text legacyText = textChild.GetComponent<Text>();
            string txt = "";
            int fontSize = 14;
            Color col = new Color(0.2f, 0.2f, 0.2f, 1f);
            if (legacyText != null)
            {
                txt = legacyText.text;
                fontSize = legacyText.fontSize > 0 ? legacyText.fontSize : 14;
                col = legacyText.color;
                Object.DestroyImmediate(legacyText);
            }

            tmpText = textChild.GetComponent<TextMeshProUGUI>() ?? textChild.gameObject.AddComponent<TextMeshProUGUI>();
            if (fontAsset != null) tmpText.font = fontAsset;
            tmpText.fontSize = fontSize;
            tmpText.color = col;
            tmpText.text = txt;
            tmpText.alignment = TextAlignmentOptions.MidlineLeft;
            tmpText.raycastTarget = false;
            EditorUtility.SetDirty(textChild.gameObject);
        }

        // 3. Placeholder child
        Transform placeholderChild = inputRoot.transform.Find("Placeholder");
        TextMeshProUGUI tmpPlaceholder = null;
        if (placeholderChild != null)
        {
            Undo.RegisterCompleteObjectUndo(placeholderChild.gameObject, "Upgrade Placeholder child");
            Text legacyPlaceholder = placeholderChild.GetComponent<Text>();
            string pText = defaultPlaceholder;
            int fontSize = 14;
            Color col = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            if (legacyPlaceholder != null)
            {
                if (!string.IsNullOrEmpty(legacyPlaceholder.text)) pText = legacyPlaceholder.text.Trim();
                fontSize = legacyPlaceholder.fontSize > 0 ? legacyPlaceholder.fontSize : 14;
                col = legacyPlaceholder.color;
                Object.DestroyImmediate(legacyPlaceholder);
            }

            tmpPlaceholder = placeholderChild.GetComponent<TextMeshProUGUI>() ?? placeholderChild.gameObject.AddComponent<TextMeshProUGUI>();
            if (fontAsset != null) tmpPlaceholder.font = fontAsset;
            tmpPlaceholder.fontSize = fontSize;
            tmpPlaceholder.color = col;
            tmpPlaceholder.text = pText;
            tmpPlaceholder.fontStyle = FontStyles.Italic;
            tmpPlaceholder.alignment = TextAlignmentOptions.MidlineLeft;
            tmpPlaceholder.raycastTarget = false;
            EditorUtility.SetDirty(placeholderChild.gameObject);
        }

        // 4. TMP_InputField
        TMP_InputField tmpInputField = inputRoot.GetComponent<TMP_InputField>() ?? inputRoot.AddComponent<TMP_InputField>();
        if (fontAsset != null) tmpInputField.fontAsset = fontAsset;
        tmpInputField.textViewport = inputRoot.GetComponent<RectTransform>();
        if (tmpText != null) tmpInputField.textComponent = tmpText;
        if (tmpPlaceholder != null) tmpInputField.placeholder = tmpPlaceholder;
        tmpInputField.contentType = isPassword ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
        if (charLimit > 0) tmpInputField.characterLimit = charLimit;
        if (!string.IsNullOrEmpty(existingText)) tmpInputField.text = existingText;

        ColorBlock cb = tmpInputField.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.95f, 0.95f, 1f, 1f);
        cb.selectedColor = Color.white;
        tmpInputField.colors = cb;

        Graphic graphic = inputRoot.GetComponent<Graphic>();
        if (graphic != null) graphic.raycastTarget = true;

        EditorUtility.SetDirty(inputRoot);
        return tmpInputField;
    }

    private static void UpgradeButton(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        if (obj == null) return;

        button_hold legacyHold = obj.GetComponent<button_hold>();
        if (legacyHold != null) Object.DestroyImmediate(legacyHold);

        Button btn = obj.GetComponent<Button>() ?? obj.AddComponent<Button>();
        ColorBlock colors = btn.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = new Color(0.9f, 0.95f, 1f, 1f);
        colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        colors.selectedColor = Color.white;
        btn.colors = colors;

        Graphic graphic = obj.GetComponent<Graphic>();
        if (graphic != null) graphic.raycastTarget = true;

        EditorUtility.SetDirty(obj);
    }

    private static void UpgradeEventSystem()
    {
        EventSystem es = Object.FindObjectOfType<EventSystem>();
        if (es != null)
        {
            InputSystemUIInputModule modernModule = es.GetComponent<InputSystemUIInputModule>();
            if (modernModule == null)
            {
                StandaloneInputModule legacyModule = es.GetComponent<StandaloneInputModule>();
                if (legacyModule != null) Object.DestroyImmediate(legacyModule);
                es.gameObject.AddComponent<InputSystemUIInputModule>();
            }
            EditorUtility.SetDirty(es.gameObject);
        }
    }

    private static TMP_FontAsset GetDefaultFont()
    {
        TMP_FontAsset font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Resources/fonts & materials/LiberationSans SDF.asset");
        if (font == null) font = TMP_Settings.defaultFontAsset;
        return font;
    }
}
