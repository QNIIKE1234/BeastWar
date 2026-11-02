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

	public Text Character_name;

	public PlayerControl pct;

	public Transform lbctf;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		lbctf = GameObject.Find("LobbyCamera").GetComponent<Transform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (Character_name != null && string.IsNullOrEmpty(Character_name.text))
		{
			Character_name.text = "Hero";
		}
		Createfunc();
		lbc.Issel = false;
		lbctf.position = lbc.ogpos;
	}

	private void Createfunc()
	{
		if (lbc.nSelb == 1)
		{
			pct = GameObject.Find("player_chr1").GetComponent<PlayerControl>();
			pct.character_name = Character_name.text.ToString();
			lbc.nChar_1 = true;
		}
		else if (lbc.nSelb == 2)
		{
			pct = GameObject.Find("player_chr2").GetComponent<PlayerControl>();
			pct.character_name = Character_name.text.ToString();
			lbc.nChar_2 = true;
		}
		else if (lbc.nSelb == 3)
		{
			pct = GameObject.Find("player_chr3").GetComponent<PlayerControl>();
			pct.character_name = Character_name.text.ToString();
			lbc.nChar_3 = true;
		}
		sel_menu.SetActive(value: true);
		sel_char.SetActive(value: true);
		cre_char.SetActive(value: false);
		create_menu.SetActive(value: false);
		sb1.SetActive(value: true);
		sb2.SetActive(value: true);
		sb3.SetActive(value: true);
	}

	private void note()
	{
		if (lbc.nSelb == 1)
		{
			if (lbc.nChar_1)
			{
				Createfunc();
			}
		}
		else if (lbc.nSelb == 2)
		{
			if (lbc.nChar_2)
			{
				Createfunc();
			}
		}
		else if (lbc.nSelb == 3 && lbc.nChar_3)
		{
			Createfunc();
		}
	}
}
