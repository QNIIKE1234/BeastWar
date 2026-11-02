using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class LoginSceneUpgrader
{
    private const string ScenePath = "Assets/Scene/Scene/A01_SecureLogin.unity";

    static LoginSceneUpgrader()
    {
        EditorApplication.delayCall += AutoUpgradeIfNeeded;
    }

    [InitializeOnLoadMethod]
    private static void OnEditorLoad()
    {
        EditorApplication.delayCall += AutoUpgradeIfNeeded;
    }

    private static void AutoUpgradeIfNeeded()
    {
        Scene currentScene = EditorSceneManager.GetActiveScene();
        if (currentScene.IsValid() && (currentScene.path == ScenePath || currentScene.name == "A01_SecureLogin"))
        {
            GameObject userNameObj = GameObject.Find("User_Name");
            if (userNameObj != null)
            {
                bool hasLegacy = userNameObj.GetComponent<InputField>() != null;
                Transform t = userNameObj.transform.Find("Text");
                if (t != null && t.GetComponent<Text>() != null) hasLegacy = true;

                if (hasLegacy)
                {
                    Debug.Log("[LoginSceneUpgrader] Legacy UI components detected in A01_SecureLogin. Auto-upgrading to TextMeshPro...");
                    ExecuteUpgrade(false);
                }
            }
        }
    }

    [MenuItem("12Tails/Upgrade Login Scene to Modern UI (TMPro)")]
    public static void UpgradeLoginSceneManual()
    {
        ExecuteUpgrade(true);
    }

    public static void ExecuteUpgrade(bool showDialog)
    {
        Scene currentScene = EditorSceneManager.GetActiveScene();
        if (currentScene.path != ScenePath && currentScene.name != "A01_SecureLogin")
        {
            currentScene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            if (!currentScene.IsValid())
            {
                Debug.LogError($"[LoginSceneUpgrader] Cannot open scene at: {ScenePath}");
                return;
            }
        }

        // Find LiberationSans SDF font or fallback
        TMP_FontAsset fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Resources/fonts & materials/LiberationSans SDF.asset");
        if (fontAsset == null)
        {
            fontAsset = TMP_Settings.defaultFontAsset;
        }

        // 1. Ensure LoginManager exists
        GameObject loginManagerObj = GameObject.Find("LoginManager");
        if (loginManagerObj == null)
        {
            loginManagerObj = new GameObject("LoginManager");
            Undo.RegisterCreatedObjectUndo(loginManagerObj, "Create LoginManager");
        }
        LoginManager loginManager = loginManagerObj.GetComponent<LoginManager>();
        if (loginManager == null)
        {
            loginManager = loginManagerObj.AddComponent<LoginManager>();
        }

        // 2. Upgrade Login_h Button
        GameObject loginButtonObj = GameObject.Find("Login_h");
        if (loginButtonObj != null)
        {
            button_hold legacyHold = loginButtonObj.GetComponent<button_hold>();
            if (legacyHold != null)
            {
                Object.DestroyImmediate(legacyHold);
            }

            Button btn = loginButtonObj.GetComponent<Button>();
            if (btn == null)
            {
                btn = loginButtonObj.AddComponent<Button>();
            }

            ColorBlock colors = btn.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.9f, 0.95f, 1f, 1f);
            colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
            colors.selectedColor = Color.white;
            btn.colors = colors;

            Graphic graphic = loginButtonObj.GetComponent<Graphic>();
            if (graphic != null)
            {
                graphic.raycastTarget = true;
            }

            loginManager.loginButton = btn;
        }

        // 3. Upgrade User_Name to TMPro
        GameObject userNameObj = GameObject.Find("User_Name");
        TMP_InputField userTMP = UpgradeInputToTMPro(userNameObj, fontAsset, "User_Name", false);
        if (userTMP != null)
        {
            loginManager.tmpInputField = userTMP;
            loginManager.legacyInputField = null;
        }

        // 4. Upgrade Password to TMPro
        GameObject passwordObj = GameObject.Find("Password");
        TMP_InputField passTMP = UpgradeInputToTMPro(passwordObj, fontAsset, "Password", true);
        if (passTMP != null)
        {
            loginManager.passwordInputField = passTMP;
        }

        // 5. Upgrade EventSystem for Unity 6 (Input System)
        EventSystem es = Object.FindObjectOfType<EventSystem>();
        if (es != null)
        {
            InputSystemUIInputModule modernModule = es.GetComponent<InputSystemUIInputModule>();
            if (modernModule == null)
            {
                StandaloneInputModule legacyModule = es.GetComponent<StandaloneInputModule>();
                if (legacyModule != null)
                {
                    Object.DestroyImmediate(legacyModule);
                }
                es.gameObject.AddComponent<InputSystemUIInputModule>();
            }
        }

        // Mark scene dirty and save
        EditorSceneManager.MarkSceneDirty(currentScene);
        EditorSceneManager.SaveScene(currentScene);

        Debug.Log("[LoginSceneUpgrader] ALL UI Elements in A01_SecureLogin successfully upgraded to TMPro and saved!");
        if (showDialog)
        {
            EditorUtility.DisplayDialog("12 Tails Modern UI",
                "Login Scene upgraded to Modern TextMeshPro!\n\n" +
                "- User_Name: Converted to TMP_InputField & TextMeshProUGUI\n" +
                "- Password: Converted to TMP_InputField & TextMeshProUGUI\n" +
                "- Login_h: Converted to standard Button\n" +
                "- EventSystem: Updated to InputSystemUIInputModule\n" +
                "- LoginManager: Bound to TMPro components\n" +
                "- Scene saved successfully!", "OK");
        }
    }

    private static TMP_InputField UpgradeInputToTMPro(GameObject inputRoot, TMP_FontAsset fontAsset, string defaultPlaceholder, bool isPassword)
    {
        if (inputRoot == null) return null;

        Undo.RegisterCompleteObjectUndo(inputRoot, "Upgrade Input to TMPro");

        // 1. Read legacy InputField properties before destroying
        InputField legacyInput = inputRoot.GetComponent<InputField>();
        string existingText = "";
        int charLimit = 0;
        if (legacyInput != null)
        {
            existingText = legacyInput.text;
            charLimit = legacyInput.characterLimit;
            Object.DestroyImmediate(legacyInput);
        }

        // 2. Handle Text child
        Transform textChild = inputRoot.transform.Find("Text");
        TextMeshProUGUI tmpText = null;
        if (textChild != null)
        {
            Undo.RegisterCompleteObjectUndo(textChild.gameObject, "Upgrade Text child to TMPro");
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

            tmpText = textChild.GetComponent<TextMeshProUGUI>();
            if (tmpText == null)
            {
                tmpText = textChild.gameObject.AddComponent<TextMeshProUGUI>();
            }
            if (fontAsset != null) tmpText.font = fontAsset;
            tmpText.fontSize = fontSize;
            tmpText.color = col;
            tmpText.text = txt;
            tmpText.alignment = TextAlignmentOptions.MidlineLeft;
            tmpText.raycastTarget = false;
        }

        // 3. Handle Placeholder child
        Transform placeholderChild = inputRoot.transform.Find("Placeholder");
        TextMeshProUGUI tmpPlaceholder = null;
        if (placeholderChild != null)
        {
            Undo.RegisterCompleteObjectUndo(placeholderChild.gameObject, "Upgrade Placeholder child to TMPro");
            Text legacyPlaceholder = placeholderChild.GetComponent<Text>();
            string pText = defaultPlaceholder;
            int fontSize = 14;
            Color col = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            if (legacyPlaceholder != null)
            {
                if (!string.IsNullOrEmpty(legacyPlaceholder.text))
                    pText = legacyPlaceholder.text.Trim();
                fontSize = legacyPlaceholder.fontSize > 0 ? legacyPlaceholder.fontSize : 14;
                col = legacyPlaceholder.color;
                Object.DestroyImmediate(legacyPlaceholder);
            }

            tmpPlaceholder = placeholderChild.GetComponent<TextMeshProUGUI>();
            if (tmpPlaceholder == null)
            {
                tmpPlaceholder = placeholderChild.gameObject.AddComponent<TextMeshProUGUI>();
            }
            if (fontAsset != null) tmpPlaceholder.font = fontAsset;
            tmpPlaceholder.fontSize = fontSize;
            tmpPlaceholder.color = col;
            tmpPlaceholder.text = pText;
            tmpPlaceholder.fontStyle = FontStyles.Italic;
            tmpPlaceholder.alignment = TextAlignmentOptions.MidlineLeft;
            tmpPlaceholder.raycastTarget = false;
        }

        // 4. Add or configure TMP_InputField
        TMP_InputField tmpInputField = inputRoot.GetComponent<TMP_InputField>();
        if (tmpInputField == null)
        {
            tmpInputField = inputRoot.AddComponent<TMP_InputField>();
        }

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
        if (graphic != null)
        {
            graphic.raycastTarget = true;
        }

        return tmpInputField;
    }
}
