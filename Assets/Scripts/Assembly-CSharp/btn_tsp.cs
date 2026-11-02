using UnityEngine;
using UnityEngine.EventSystems;

public class btn_tsp : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public string R_L;

	public GameObject snow;

	public GameObject beach;

	public GameObject eyp;

	public GameObject cave;

	public GameObject forest;

	public takeoff_tsp takeoff_btn;

	private void Start()
	{
		takeoff_btn = GameObject.Find("takeoff").GetComponent<takeoff_tsp>();
		snow.SetActive(value: false);
		beach.SetActive(value: false);
		eyp.SetActive(value: false);
		cave.SetActive(value: false);
		forest.SetActive(value: false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (R_L == "R")
		{
			takeoff_btn.numtown++;
		}
		else if (R_L == "L")
		{
			takeoff_btn.numtown--;
		}
		else if (R_L == "esc")
		{
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
			PilotMole_NPC component = GameObject.Find("PilotMole").GetComponent<PilotMole_NPC>();
			component.gamegui.SetActive(value: true);
			component.isclicked = false;
		}
		Debug.Log(takeoff_btn.numtown);
	}

	public void ticket_show()
	{
		if (takeoff_btn.numtown == 1)
		{
			snow.SetActive(value: false);
			beach.SetActive(value: false);
			eyp.SetActive(value: false);
			cave.SetActive(value: false);
			forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 2)
		{
			snow.SetActive(value: true);
			beach.SetActive(value: false);
			eyp.SetActive(value: false);
			cave.SetActive(value: false);
			forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 3)
		{
			snow.SetActive(value: false);
			beach.SetActive(value: true);
			eyp.SetActive(value: false);
			cave.SetActive(value: false);
			forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 4)
		{
			snow.SetActive(value: true);
			beach.SetActive(value: false);
			eyp.SetActive(value: true);
			cave.SetActive(value: false);
			forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 5)
		{
			snow.SetActive(value: true);
			beach.SetActive(value: false);
			eyp.SetActive(value: false);
			cave.SetActive(value: true);
			forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 6)
		{
			snow.SetActive(value: true);
			beach.SetActive(value: false);
			eyp.SetActive(value: false);
			cave.SetActive(value: false);
			forest.SetActive(value: true);
		}
	}
}
