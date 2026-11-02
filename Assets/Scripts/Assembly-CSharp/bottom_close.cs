using UnityEngine;
using UnityEngine.EventSystems;

public class bottom_close : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public GameObject sel_menu;

	public GameObject create_menu;

	public GameObject selb1;

	public GameObject selb2;

	public GameObject selb3;

	private void Start()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		sel_menu.SetActive(value: true);
		create_menu.SetActive(value: false);
		selb1.SetActive(value: true);
		selb2.SetActive(value: true);
		selb3.SetActive(value: true);
	}
}
