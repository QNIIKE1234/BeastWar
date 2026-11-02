using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_hold : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage bot;

	private bool ishw;

	private void Start()
	{
		bot = GetComponent<RawImage>();
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
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
