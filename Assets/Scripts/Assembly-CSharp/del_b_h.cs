using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class del_b_h : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage bot;

	private void Start()
	{
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}

	private void Update()
	{
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
