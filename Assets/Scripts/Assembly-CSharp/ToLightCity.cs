using UnityEngine;

public class ToLightCity : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter()
	{
		Object.Destroy(GameObject.Find("GameGui(Clone)"));
		Initiate.Fade("T51_MainStreet", Color.white, 0.5f);
	}
}
