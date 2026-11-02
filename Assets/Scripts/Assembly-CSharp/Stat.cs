using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
	public GameObject Race;

	public GameObject Rp;

	public GameObject Mrp;

	public CameraControl cct;

	public GameObject player;

	private void FixedUpdate()
	{
		GameObject gameObject = GameObject.FindWithTag("MainCamera");
		cct = gameObject.GetComponent<CameraControl>();
		player = cct.target;
		PlayerControl component = player.GetComponent<PlayerControl>();
		Race.GetComponent<TextMeshProUGUI>().SetText(component.Race);
		Rp.GetComponent<TextMeshProUGUI>().SetText(string.Concat(component.Rp));
		Mrp.GetComponent<TextMeshProUGUI>().SetText("/" + component.Mrp);
	}
}
