using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class X_bot : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage xbot;

	public GameObject bar;

	private void Start()
	{
		xbot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		bar.SetActive(value: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		xbot.CrossFadeAlpha(1f, 0f, ignoreTimeScale: false);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		xbot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
	}
}
