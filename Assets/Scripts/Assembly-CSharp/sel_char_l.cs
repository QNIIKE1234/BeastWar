using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class sel_char_l : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage bot;

	public LobbyCamera lbc;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}

	private void Update()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Quaternion quaternion = Quaternion.AngleAxis(120f, Vector3.up);
		lbc._cameraOffset = quaternion * lbc._cameraOffset;
		lbc.nSelb--;
		if (lbc.nSelb < 1)
		{
			lbc.nSelb = 3;
		}
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
