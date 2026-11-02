using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private CharacterController characterController;

	public string family_name;

	public string character_name;

	public string Race;

	public int Rank;

	public float Speed = 6f;

	public float RotationSpeed = 1440f;

	private float Gravity = 20f;

	private Vector3 _moveDir = Vector3.zero;

	public Vector3 move;

	public bool Islobby;

	public string Ontown = "";

	public int cLv;

	public int Rp;

	public int Mrp;

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
		family_name = PlayerPrefs.GetString("fam_name").ToString();
		cLv = 1;
		Rank = 0;
		Rp = 0;
		Mrp = 99999;
	}

	public void FixedUpdate()
	{
		if (!Islobby)
		{
			playermovement();
		}
	}

	public void playermovement()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		Vector3 normalized = Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
		move = axis2 * normalized + axis * Camera.main.transform.right;
		if (move.magnitude > 1f)
		{
			move.Normalize();
		}
		move = base.transform.InverseTransformDirection(move);
		float num = Mathf.Atan2(move.x, move.z);
		base.transform.Rotate(0f, num * RotationSpeed * Time.deltaTime, 0f);
		if (characterController.isGrounded)
		{
			_moveDir = base.transform.forward * move.magnitude;
			_moveDir *= Speed;
		}
		_moveDir.y -= Gravity * Time.deltaTime;
		characterController.Move(_moveDir * Time.deltaTime);
	}
}
