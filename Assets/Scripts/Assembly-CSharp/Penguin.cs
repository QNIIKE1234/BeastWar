using UnityEngine;

public class Penguin : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_42";

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "run_9";
			name_2 = "root_42";
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
