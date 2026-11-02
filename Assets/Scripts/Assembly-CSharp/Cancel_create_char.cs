using UnityEngine;
using UnityEngine.EventSystems;

public class Cancel_create_char : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public LobbyCamera lbc;

	public Transform lbctf;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		lbctf = GameObject.Find("LobbyCamera").GetComponent<Transform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (lbc.nSelb == 1)
		{
			GameObject obj = GameObject.Find("player_chr1");
			PlayerPrefs.SetString("Character_1", "");
			Object.Destroy(obj);
		}
		else if (lbc.nSelb == 2)
		{
			GameObject obj2 = GameObject.Find("player_chr2");
			PlayerPrefs.SetString("Character_2", "");
			Object.Destroy(obj2);
		}
		else if (lbc.nSelb == 3)
		{
			GameObject obj3 = GameObject.Find("player_chr3");
			PlayerPrefs.SetString("Character_3", "");
			Object.Destroy(obj3);
		}
		lbc.Issel = false;
		lbctf.position = lbc.ogpos;
	}
}
