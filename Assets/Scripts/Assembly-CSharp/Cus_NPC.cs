using UnityEngine;

public class Cus_NPC : MonoBehaviour
{
	private Animation anim;

	public new string name;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	private void FixedUpdate()
	{
		anim.Play(name);
	}
}
