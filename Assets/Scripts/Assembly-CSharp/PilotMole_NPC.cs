using UnityEngine;

public class PilotMole_NPC : MonoBehaviour
{
	private Animation anim;

	public float distance;

	public float min_dst = 3f;

	public GameObject ptf;

	private Transform PilotMole;

	public GameObject tps;

	public GameObject MCamera;

	public GameObject gamegui;

	public bool isclicked;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	private void FixedUpdate()
	{
		anim.Play("root_251");
	}

	public virtual void Awake()
	{
		PilotMole = GetComponent<Transform>();
		ptf = GameObject.FindWithTag("Player");
	}

	public virtual void Update()
	{
		distance = Vector3.Distance(ptf.transform.position, PilotMole.position);
	}

	public void OnMouseDown()
	{
		if (distance <= min_dst && !isclicked)
		{
			MCamera = GameObject.FindWithTag("MainCamera");
			Object.Instantiate(tps, base.transform.position, base.transform.rotation).transform.parent = MCamera.transform;
			gamegui = GameObject.Find("GameGui(Clone)");
			gamegui.SetActive(value: false);
			isclicked = true;
		}
	}
}
