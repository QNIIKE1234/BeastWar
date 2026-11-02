using UnityEngine;

public class StartPoint2 : MonoBehaviour
{
	public GameObject player;

	private Transform stp2;

	private MeshRenderer objs;

	public string ntown;

	private void Start()
	{
		objs = GetComponent<MeshRenderer>();
		stp2 = GetComponent<Transform>();
		player = GameObject.FindWithTag("Player"); if (player == null) return;
		PlayerControl component = player.GetComponent<PlayerControl>();
		if (component.Ontown == "MainStreet")
		{
			component.Ontown = ntown;
			player.transform.position = stp2.transform.position;
			player.transform.rotation = stp2.transform.rotation;
		}
		if (component.Ontown == "CTSQ" && ntown == "MainStreet")
		{
			component.Ontown = ntown;
			player.transform.position = stp2.transform.position;
			player.transform.rotation = stp2.transform.rotation;
		}
		else if (component.Ontown == "CityLibary")
		{
			component.Ontown = ntown;
			player.transform.position = stp2.transform.position;
			player.transform.rotation = stp2.transform.rotation;
		}
		else if ((component.Ontown == "PlainColosseum" || component.Ontown == "SnowCamp" || component.Ontown == "Beach" || component.Ontown == "Eyp" || component.Ontown == "Cave" || component.Ontown == "Forest") && (ntown == "PlainColosseum" || ntown == "SnowCamp" || ntown == "Beach" || ntown == "Eyp" || ntown == "Cave" || ntown == "Forest") && component.Ontown != ntown)
		{
			component.Ontown = ntown;
			player.transform.position = stp2.transform.position;
			player.transform.rotation = stp2.transform.rotation;
		}
		Object.Destroy(objs);
	}
}
