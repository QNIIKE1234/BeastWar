using UnityEngine;
using UnityEngine.EventSystems;

public class takeoff_tsp : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public string thistown;

	public int numtown = 1;

	private void Start()
	{
		PlayerControl component = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		thistown = component.Ontown;
	}

	private void FixedUpdate()
	{
		if (numtown == 7)
		{
			numtown = 1;
		}
		else if (numtown == 0)
		{
			numtown = 6;
		}
		GameObject.Find("R").GetComponent<btn_tsp>().ticket_show();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (numtown == 1 && thistown != "PlainColosseum")
		{
			Initiate.Fade("T56_PlainColosseum", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
		else if (numtown == 2 && thistown != "SnowCamp")
		{
			Initiate.Fade("G32_SnowCamp", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
		else if (numtown == 3 && thistown != "Beach")
		{
			Initiate.Fade("G33_CrystalBeach", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
		else if (numtown == 4 && thistown != "Eyp")
		{
			Initiate.Fade("G34_ExcavationCamp", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
		else if (numtown == 5 && thistown != "Cave")
		{
			Initiate.Fade("G35_UndergroundCamp", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
		else if (numtown == 6 && thistown != "Forest")
		{
			Initiate.Fade("G36_ForestCamp", Color.white, 0.5f);
			Object.Destroy(GameObject.Find("TransportGui(Clone)"));
		}
	}
}
