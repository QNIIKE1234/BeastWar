using UnityEngine;

public class TownCamera : MonoBehaviour
{
	public GameObject PlayerTransform;

	private Vector3 _cameraOffset;

	[Range(0.01f, 1f)]
	public float SmoothFactor = 0.5f;

	public bool LookAtPlayer;

	public bool RotateAroundPlayer = true;

	public float RotationsSpeed = 5f;

	private void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player");
		if (PlayerTransform == null)
		{
			PlayerTransform = GameObject.FindWithTag("Enemy");
		}
		if (PlayerTransform != null)
		{
			_cameraOffset = base.transform.position - PlayerTransform.transform.position;
		}
	}

	private void FixedUpdate()
	{
		if (PlayerTransform == null)
		{
			PlayerTransform = GameObject.FindWithTag("Player");
			if (PlayerTransform == null) return;
			_cameraOffset = base.transform.position - PlayerTransform.transform.position;
		}

		if (Input.GetMouseButton(1) && RotateAroundPlayer)
		{
			Quaternion quaternion = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
			_cameraOffset = quaternion * _cameraOffset;
		}
		Vector3 b = PlayerTransform.transform.position + _cameraOffset;
		base.transform.position = Vector3.Slerp(base.transform.position, b, SmoothFactor);
		if (LookAtPlayer || RotateAroundPlayer)
		{
			base.transform.LookAt(PlayerTransform.transform);
		}
	}
}
