using UnityEngine;

public class Crocodile_NPC : MonoBehaviour
{
	private Animation anim;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	private void FixedUpdate()
	{
		anim.Play("root_259");
	}
}
