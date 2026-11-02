using UnityEngine;

public class Rabbit : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_13";

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "run_4";
			name_2 = "root_13";
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
