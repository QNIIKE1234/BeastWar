using UnityEngine;

public class StartPoint3 : MonoBehaviour
{
	public GameObject player;

	private Transform stp3;

	private MeshRenderer objs;

	public string ntown;

	private void Start()
	{
		objs = GetComponent<MeshRenderer>();
		stp3 = GetComponent<Transform>();
		player = GameObject.FindWithTag("Player"); if (player == null) return;
		PlayerControl component = player.GetComponent<PlayerControl>();
		if (component.Ontown == "NoGuild" && ntown == "GuildPlain")
		{
			component.Ontown = ntown;
			player.transform.position = stp3.transform.position;
			player.transform.rotation = stp3.transform.rotation;
		}
		else if (component.Ontown == "PlainColosseum")
		{
			component.Ontown = ntown;
			player.transform.position = stp3.transform.position;
			player.transform.rotation = stp3.transform.rotation;
		}
		else if (component.Ontown == "LightPark" && ntown == "CTSQ")
		{
			component.Ontown = ntown;
			player.transform.position = stp3.transform.position;
			player.transform.rotation = stp3.transform.rotation;
		}
		else if (component.Ontown == "LightPalace" && ntown == "LightPark")
		{
			component.Ontown = ntown;
			player.transform.position = stp3.transform.position;
			player.transform.rotation = stp3.transform.rotation;
		}
		Object.Destroy(objs);
	}
}
