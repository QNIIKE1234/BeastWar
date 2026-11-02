using UnityEngine;

public class Cat : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_35";

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "walk";
			name_2 = "root_35";
			if (pct.Speed > 5.5f)
			{
				animation = "run_8";
			}
			animator.Play(animation);
		}
		else
		{
			if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.B))
			{
				name_2 = "battle_3";
			}
			animator.Play(name_2);
		}
	}
}
