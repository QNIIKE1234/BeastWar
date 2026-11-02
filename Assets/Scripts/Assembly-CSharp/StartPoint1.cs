using UnityEngine;

public class StartPoint1 : MonoBehaviour
{
	public GameObject player;

	private Transform stp1;

	private MeshRenderer objs;

	public string ntown;

	private void Start()
	{
		objs = GetComponent<MeshRenderer>();
		stp1 = GetComponent<Transform>();
				player = GameObject.FindWithTag("Player");
		if (player == null)
		{
			player = new GameObject("OfflineTestPlayer");
			player.tag = "Player";
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
			CharacterController cc = player.AddComponent<CharacterController>();
			cc.center = new Vector3(0, 1, 0);
			cc.height = 2f;
			PlayerControl pc = player.AddComponent<PlayerControl>();
			pc.Speed = 6f;
			pc.Ontown = ntown;
			GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			visual.transform.SetParent(player.transform);
			visual.transform.localPosition = new Vector3(0, 1, 0);
			Collider col = visual.GetComponent<Collider>();
			if (col != null) Object.Destroy(col);
		}
		PlayerControl component = player.GetComponent<PlayerControl>();
		if (component.Ontown == "")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "NoGuild")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "GuildPlain")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "MainStreet" && ntown == "CTSQ")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "CTSQ" && ntown == "CityLibary")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "CTSQ" && ntown == "LightPark")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		else if (component.Ontown == "LightPark" && ntown == "LightPalace")
		{
			component.Ontown = ntown;
			player.transform.position = stp1.transform.position;
			player.transform.rotation = stp1.transform.rotation;
		}
		Object.Destroy(objs);
	}

	private void Update()
	{
	}
}
