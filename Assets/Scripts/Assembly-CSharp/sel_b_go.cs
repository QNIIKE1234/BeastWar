using UnityEngine;
using UnityEngine.EventSystems;

public class sel_b_go : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public LobbyCamera lbc;

	public PlayerControl pct;

	private GameObject player_c1;

	private GameObject player_c2;

	private GameObject player_c3;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
	}

	private void FixedUpdate()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (lbc.nChar_1)
		{
			player_c1 = GameObject.Find("player_chr1");
			if (lbc.nSelb != 1)
			{
				if (lbc.nSelb == 2)
				{
					player_c1.SetActive(value: false);
				}
				else if (lbc.nSelb == 3)
				{
					player_c1.SetActive(value: false);
				}
			}
			Object.DontDestroyOnLoad(player_c1);
		}
		if (lbc.nChar_2)
		{
			player_c2 = GameObject.Find("player_chr2");
			if (lbc.nSelb == 1)
			{
				player_c2.SetActive(value: false);
			}
			else if (lbc.nSelb != 2 && lbc.nSelb == 3)
			{
				player_c2.SetActive(value: false);
			}
			Object.DontDestroyOnLoad(player_c2);
		}
		if (lbc.nChar_3)
		{
			player_c3 = GameObject.Find("player_chr3");
			if (lbc.nSelb == 1)
			{
				player_c3.SetActive(value: false);
			}
			else if (lbc.nSelb == 2)
			{
				player_c3.SetActive(value: false);
			}
			else
			{
				_ = lbc.nSelb;
				_ = 3;
			}
			Object.DontDestroyOnLoad(player_c3);
		}
		if (lbc.nSelb == 1)
		{
			pct = GameObject.Find("player_chr1").GetComponent<PlayerControl>();
		}
		else if (lbc.nSelb == 2)
		{
			pct = GameObject.Find("player_chr2").GetComponent<PlayerControl>();
		}
		else if (lbc.nSelb == 3)
		{
			pct = GameObject.Find("player_chr3").GetComponent<PlayerControl>();
		}
		pct.Islobby = false;
		GameObject.Find("Char_sel_gui").SetActive(value: false);
		Initiate.Fade("G31_GuildPlain_n", Color.white, 0.5f);
	}
}
