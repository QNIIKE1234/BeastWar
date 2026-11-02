using UnityEngine;

public class OptionMenu : MonoBehaviour
{
	public GameObject bar;

	private bool ishw;

	private void Start()
	{
		bar.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (ishw)
			{
				bar.SetActive(value: false);
				ishw = false;
			}
			else
			{
				bar.SetActive(value: true);
				ishw = true;
			}
		}
	}
}
