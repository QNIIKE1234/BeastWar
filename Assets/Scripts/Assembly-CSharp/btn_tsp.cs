using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
		GameObject toObj = GameObject.Find("takeoff");
		if (toObj != null) takeoff_btn = toObj.GetComponent<takeoff_tsp>();

		if (snow != null) snow.SetActive(value: false);
		if (beach != null) beach.SetActive(value: false);
		if (eyp != null) eyp.SetActive(value: false);
		if (cave != null) cave.SetActive(value: false);
		if (forest != null) forest.SetActive(value: false);

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

	public void OnPointerDown(PointerEventData eventData)
	{
		if (takeoff_btn == null)
		{
			GameObject toObj = GameObject.Find("takeoff");
			if (toObj != null) takeoff_btn = toObj.GetComponent<takeoff_tsp>();
		}

		if (takeoff_btn != null)
		{
			if (R_L == "R")
			{
				takeoff_btn.numtown++;
			}
			else if (R_L == "L")
			{
				takeoff_btn.numtown--;
			}
		}

		if (R_L == "esc")
		{
			GameObject tspClone = GameObject.Find("TransportGui(Clone)");
			if (tspClone != null) Object.Destroy(tspClone);

			GameObject pmObj = GameObject.Find("PilotMole");
			if (pmObj != null)
			{
				PilotMole_NPC component = pmObj.GetComponent<PilotMole_NPC>();
				if (component != null)
				{
					if (component.gamegui != null) component.gamegui.SetActive(value: true);
					component.isclicked = false;
				}
			}
		}

		if (takeoff_btn != null)
		{
			Debug.Log(takeoff_btn.numtown);
		}
	}

	public void ticket_show()
	{
		if (takeoff_btn == null) return;

		if (takeoff_btn.numtown == 1)
		{
			if (snow != null) snow.SetActive(value: false);
			if (beach != null) beach.SetActive(value: false);
			if (eyp != null) eyp.SetActive(value: false);
			if (cave != null) cave.SetActive(value: false);
			if (forest != null) forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 2)
		{
			if (snow != null) snow.SetActive(value: true);
			if (beach != null) beach.SetActive(value: false);
			if (eyp != null) eyp.SetActive(value: false);
			if (cave != null) cave.SetActive(value: false);
			if (forest != null) forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 3)
		{
			if (snow != null) snow.SetActive(value: false);
			if (beach != null) beach.SetActive(value: true);
			if (eyp != null) eyp.SetActive(value: false);
			if (cave != null) cave.SetActive(value: false);
			if (forest != null) forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 4)
		{
			if (snow != null) snow.SetActive(value: true);
			if (beach != null) beach.SetActive(value: false);
			if (eyp != null) eyp.SetActive(value: true);
			if (cave != null) cave.SetActive(value: false);
			if (forest != null) forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 5)
		{
			if (snow != null) snow.SetActive(value: true);
			if (beach != null) beach.SetActive(value: false);
			if (eyp != null) eyp.SetActive(value: false);
			if (cave != null) cave.SetActive(value: true);
			if (forest != null) forest.SetActive(value: false);
		}
		else if (takeoff_btn.numtown == 6)
		{
			if (snow != null) snow.SetActive(value: true);
			if (beach != null) beach.SetActive(value: false);
			if (eyp != null) eyp.SetActive(value: false);
			if (cave != null) cave.SetActive(value: false);
			if (forest != null) forest.SetActive(value: true);
		}
	}
}
