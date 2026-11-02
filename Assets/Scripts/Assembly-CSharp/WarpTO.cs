using UnityEngine;

public class WarpTO : MonoBehaviour
{
	public string Name;

	private void Start()
	{
	}

	private void OnTriggerEnter()
	{
		Object.Destroy(GameObject.Find("GameGui(Clone)"));
		Initiate.Fade(Name, Color.white, 0.5f);
	}
}
