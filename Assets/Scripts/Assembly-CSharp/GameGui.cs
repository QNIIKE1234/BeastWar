using TMPro;
using UnityEngine;

public class GameGui : MonoBehaviour
{
	public GameObject gamegui;

	public GameObject MCamera;

	public CameraControl cct;

	public GameObject player;

	public GameObject playerinfo;

	public GameObject cName_box;

	private void Start()
	{
		cct = GetComponent<CameraControl>();
		player = cct.target;
		PlayerControl component = player.GetComponent<PlayerControl>();
		MCamera = GameObject.FindWithTag("MainCamera");
		Object.Instantiate(gamegui, base.transform.position, base.transform.rotation).transform.parent = MCamera.transform;
		playerinfo = GameObject.Find("PlayerInfo");
		GameObject.Find("FamilyName").GetComponent<TextMeshProUGUI>().SetText(component.family_name);
	}

	private void FixedUpdate()
	{
		TextMeshProUGUI component = GameObject.Find("CharacterNameLv").GetComponent<TextMeshProUGUI>();
		PlayerControl component2 = player.GetComponent<PlayerControl>();
		component.SetText(component2.character_name + " Lv." + component2.cLv);
		GameObject.Find("char_name_box").GetComponent<TextMeshProUGUI>().SetText(component2.character_name);
		if (playerinfo != null)
		{
			Vector3 position = default(Vector3);
			position.x = player.transform.position.x;
			position.y = player.transform.position.y + 1.75f;
			position.z = player.transform.position.z;
			Vector3 position2 = cct.camera.WorldToScreenPoint(position);
			playerinfo.transform.position = position2;
		}
		else
		{
			Debug.Log("Can't find GameObject");
		}
	}
}
