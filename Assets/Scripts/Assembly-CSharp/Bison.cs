using UnityEngine;

public class Bison : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_72";

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "run_11";
			name_2 = "root_72";
			animator.Play(animation);
			return;
		}
		if (Input.GetKey(KeyCode.LeftAlt))
		{
			Input.GetKeyDown(KeyCode.B);
		}
		animator.Play(name_2);
	}
}
