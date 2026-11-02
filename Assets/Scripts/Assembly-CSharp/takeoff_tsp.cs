using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class takeoff_tsp : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public string thistown;

	public int numtown = 1;

	private void Start()
	{
		GameObject playerObj = GameObject.FindWithTag("Player");
		if (playerObj != null)
		{
			PlayerControl component = playerObj.GetComponent<PlayerControl>();
			if (component != null)
			{
				thistown = component.Ontown;
			}
		}

		Button btn = GetComponent<Button>();
		if (btn != null)
		{
			btn.onClick.RemoveListener(ExecuteClick);
			btn.onClick.AddListener(ExecuteClick);
		}
	}

	public void ExecuteClick()
	{
		OnPointerDown(null);
	}

	private void FixedUpdate()
	{
		if (numtown >= 7)
		{
			numtown = 1;
		}
		else if (numtown <= 0)
		{
			numtown = 6;
		}

		GameObject rObj = GameObject.Find("R");
		if (rObj != null)
		{
			btn_tsp btnTsp = rObj.GetComponent<btn_tsp>();
			if (btnTsp != null)
			{
				btnTsp.ticket_show();
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (string.IsNullOrEmpty(thistown))
		{
			GameObject playerObj = GameObject.FindWithTag("Player");
			if (playerObj != null)
			{
				PlayerControl component = playerObj.GetComponent<PlayerControl>();
				if (component != null) thistown = component.Ontown;
			}
		}

		if (numtown == 1 && thistown != "PlainColosseum")
		{
			Initiate.Fade("T56_PlainColosseum", Color.white, 0.5f);
			DestroyTransportGui();
		}
		else if (numtown == 2 && thistown != "SnowCamp")
		{
			Initiate.Fade("G32_SnowCamp", Color.white, 0.5f);
			DestroyTransportGui();
		}
		else if (numtown == 3 && thistown != "Beach")
		{
			Initiate.Fade("G33_CrystalBeach", Color.white, 0.5f);
			DestroyTransportGui();
		}
		else if (numtown == 4 && thistown != "Eyp")
		{
			Initiate.Fade("G34_ExcavationCamp", Color.white, 0.5f);
			DestroyTransportGui();
		}
		else if (numtown == 5 && thistown != "Cave")
		{
			Initiate.Fade("G35_UndergroundCamp", Color.white, 0.5f);
			DestroyTransportGui();
		}
		else if (numtown == 6 && thistown != "Forest")
		{
			Initiate.Fade("G36_ForestCamp", Color.white, 0.5f);
			DestroyTransportGui();
		}
	}

	private void DestroyTransportGui()
	{
		GameObject tspClone = GameObject.Find("TransportGui(Clone)");
		if (tspClone != null) Object.Destroy(tspClone);
	}
}
