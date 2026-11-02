using UnityEngine;

public class QuestionIcon : MonoBehaviour
{
	private Animation anim;

	private void Start()
	{
		anim = GetComponent<Animation>();
	}

	private void FixedUpdate()
	{
		anim.Play("root_135");
	}
}
