using UnityEngine;

public class selbox1 : MonoBehaviour
{
	public LobbyCamera lbc;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		lbc.nSelb = 1;
	}

	private void OnMouseDown()
	{
		if (lbc.nSelb == 2)
		{
			Quaternion quaternion = Quaternion.AngleAxis(120f, Vector3.up);
			lbc._cameraOffset = quaternion * lbc._cameraOffset;
		}
		if (lbc.nSelb == 3)
		{
			Quaternion quaternion2 = Quaternion.AngleAxis(-120f, Vector3.up);
			lbc._cameraOffset = quaternion2 * lbc._cameraOffset;
		}
		lbc.nSelb = 1;
	}
}
