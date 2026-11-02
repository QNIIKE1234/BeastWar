using UnityEngine;

public class WarpAltar_LP : MonoBehaviour
{
	public float distance;

	public float min_dst = 4.25f;

	public GameObject ptf;

	private Transform warpaltar;

	public string Name;

	public virtual void Awake()
	{
		warpaltar = GetComponent<Transform>();
		ptf = GameObject.FindWithTag("Player");
	}

	public virtual void Update()
	{
		distance = Vector3.Distance(ptf.transform.position, warpaltar.position);
	}

	public void OnMouseDown()
	{
		if (distance <= min_dst)
		{
			Object.Destroy(GameObject.Find("GameGui(Clone)"));
			Initiate.Fade(Name, Color.white, 0.5f);
		}
	}
}
