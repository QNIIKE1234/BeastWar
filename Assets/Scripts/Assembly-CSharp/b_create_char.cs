using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class b_create_char : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage bot;

	public GameObject sel_menu;

	public GameObject create_menu;

	public GameObject selb1;

	public GameObject selb2;

	public GameObject selb3;

	public LobbyCamera lbc;

	private Transform lbctf;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		lbctf = GameObject.Find("LobbyCamera").GetComponent<Transform>();
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
		sel_menu.SetActive(value: true);
		create_menu.SetActive(value: false);
	}

	private void Update()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		lbc.ogpos = lbctf.position;
		sel_menu.SetActive(value: false);
		create_menu.SetActive(value: true);
		selb1.SetActive(value: false);
		selb2.SetActive(value: false);
		selb3.SetActive(value: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		bot.CrossFadeAlpha(1f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}
}
