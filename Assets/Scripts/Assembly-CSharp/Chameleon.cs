using UnityEngine;

public class Chameleon : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_130";

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "run_46";
			name_2 = "root_130";
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
