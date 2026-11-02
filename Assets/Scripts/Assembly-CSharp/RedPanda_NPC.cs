using UnityEngine;

public class RedPanda_NPC : MonoBehaviour
{
	private Animation anim;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	private void FixedUpdate()
	{
		anim.Play("sit_8");
	}
}
