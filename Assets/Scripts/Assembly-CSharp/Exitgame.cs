using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exitgame : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage exitg_bot;

	private void Start()
	{
		exitg_bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Application.Quit();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		exitg_bot.CrossFadeAlpha(1f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		exitg_bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}
}
