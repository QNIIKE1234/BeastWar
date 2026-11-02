using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_h : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
	public RawImage bot;

	public GameObject bar;

	public bool ishw;

	private void Start()
	{
		bot.CrossFadeAlpha(0f, 0f, ignoreTimeScale: false);
		bar.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if (ishw)
			{
				bar.SetActive(value: false);
				ishw = false;
			}
			else
			{
				bar.SetActive(value: true);
				ishw = true;
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (ishw)
		{
			bar.SetActive(value: false);
			ishw = false;
		}
		else
		{
			bar.SetActive(value: true);
			ishw = true;
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
