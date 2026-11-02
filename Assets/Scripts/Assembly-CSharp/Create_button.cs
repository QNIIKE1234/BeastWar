using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Create_button : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public LobbyCamera lbc;
	public GameObject sel_menu;
	public GameObject sel_char;
	public GameObject cre_char;
	public GameObject create_menu;
	public GameObject sb1;
	public GameObject sb2;
	public GameObject sb3;

	[Header("UI - TMPro and Legacy")]
	public Text Character_name;
	public TMP_InputField tmpInputField;
	public TextMeshProUGUI tmpCharacterName;

	public PlayerControl pct;
	public Transform lbctf;

	private void Start()
	{
		GameObject camObj = GameObject.Find("LobbyCamera");
		if (camObj != null)
		{
			lbc = camObj.GetComponent<LobbyCamera>();
			lbctf = camObj.GetComponent<Transform>();
		}

		if (tmpInputField == null)
		{
			tmpInputField = FindObjectOfType<TMP_InputField>();
		}
		if (tmpCharacterName == null && tmpInputField != null)
		{
			tmpCharacterName = tmpInputField.textComponent as TextMeshProUGUI;
		}

		// Self-heal button component for standard Unity UI
		Button btn = GetComponent<Button>();
		if (btn != null)
		{
			btn.onClick.RemoveListener(ExecuteCreate);
			btn.onClick.AddListener(ExecuteCreate);
		}
	}

	public string GetCharacterName()
	{
		if (tmpInputField != null && !string.IsNullOrEmpty(tmpInputField.text))
			return tmpInputField.text.Trim();

		if (tmpCharacterName != null && !string.IsNullOrEmpty(tmpCharacterName.text))
			return tmpCharacterName.text.Trim();

		if (Character_name != null && !string.IsNullOrEmpty(Character_name.text))
			return Character_name.text.Trim();

		return "Hero";
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		ExecuteCreate();
	}

	public void ExecuteCreate()
	{
		string finalName = GetCharacterName();
		Createfunc(finalName);

		if (lbc != null)
		{
			lbc.Issel = false;
			if (lbctf != null) lbctf.position = lbc.ogpos;
		}
	}

	private void Createfunc(string charName = "Hero")
	{
		if (lbc == null) return;

		string targetChr = "player_chr1";
		if (lbc.nSelb == 1)
		{
			targetChr = "player_chr1";
			lbc.nChar_1 = true;
		}
		else if (lbc.nSelb == 2)
		{
			targetChr = "player_chr2";
			lbc.nChar_2 = true;
		}
		else if (lbc.nSelb == 3)
		{
			targetChr = "player_chr3";
			lbc.nChar_3 = true;
		}

		GameObject pObj = GameObject.Find(targetChr);
		if (pObj != null)
		{
			pct = pObj.GetComponent<PlayerControl>();
			if (pct != null)
			{
				pct.character_name = charName;
			}
		}

		if (sel_menu != null) sel_menu.SetActive(true);
		if (sel_char != null) sel_char.SetActive(true);
		if (cre_char != null) cre_char.SetActive(false);
		if (create_menu != null) create_menu.SetActive(false);
		if (sb1 != null) sb1.SetActive(true);
		if (sb2 != null) sb2.SetActive(true);
		if (sb3 != null) sb3.SetActive(true);
	}
}
