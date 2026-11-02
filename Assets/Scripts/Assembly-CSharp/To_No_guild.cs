using UnityEngine;

public class To_No_guild : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter()
	{
		Object.Destroy(GameObject.Find("GameGui(Clone)"));
		Initiate.Fade("G30_NoGuild", Color.white, 0.5f);
	}
}
