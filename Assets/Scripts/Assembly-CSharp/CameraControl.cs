using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public GameObject target;

	public GameObject lockedTarget;

	public bool isLocked;

	public bool lowAngle;

	public float distance;

	public float rotation;

	public float zoom;

	public float clipPlaneMultiplier;

	private CharacterController character;

	private Vector3 FDp39w4vAww;

	private float mP239moOrjG;

	private Vector3 AYu39jreQlg;

	private float Ayg39lhO5oR;

	private Transform MCamera;

	public Camera camera;

	public virtual void Awake()
	{
		FDp39w4vAww = Vector3.zero;
		mP239moOrjG = 0f;
		AYu39jreQlg = Vector3.zero;
		Ayg39lhO5oR = 0f;
		MCamera = GetComponent<Transform>();
		camera = GetComponent<Camera>();
		camera.farClipPlane = Mathf.Clamp(clipPlaneMultiplier, 0.5f, 2f) * 500f;
		target = GameObject.FindWithTag("Player");
		lockedTarget = target;
		isLocked = true;
	}

	public virtual void FixedUpdate()
	{
		if ((bool)target)
		{
			character = target.GetComponent<CharacterController>();
			Vector3 center = character.bounds.center;
			Ayg39lhO5oR = character.height - 1.5f;
			if (Ayg39lhO5oR < 0f)
			{
				Ayg39lhO5oR = 0f;
			}
			FDp39w4vAww.y = 0.2f * character.height;
			zoom += Input.mouseScrollDelta.y * 0.4f;
			distance += zoom;
			if (distance < 3f)
			{
				distance = 3f;
			}
			if (distance > 9f)
			{
				distance = 9f;
			}
			zoom = 0f;
			int num = 4;
			if (lowAngle)
			{
				num = 25;
			}
			if ((bool)lockedTarget && isLocked)
			{
				Quaternion b = Quaternion.LookRotation(lockedTarget.transform.position - MCamera.position);
				MCamera.rotation = Quaternion.Slerp(MCamera.rotation, b, Time.deltaTime * 3f);
				float x = Mathf.SmoothDampAngle(MCamera.eulerAngles.x, 4f * distance - (float)num, ref mP239moOrjG, 0.3f);
				Vector3 eulerAngles = MCamera.eulerAngles;
				eulerAngles.x = x;
				MCamera.eulerAngles = eulerAngles;
				if (Input.GetMouseButton(1))
				{
					rotation = Input.GetAxis("Mouse X");
				}
				else
				{
					rotation = 0f;
				}
				MCamera.RotateAround(target.transform.position, Vector3.up, rotation * 5f);
				float x2 = Mathf.SmoothDampAngle(MCamera.eulerAngles.x, 4f * distance - (float)num, ref mP239moOrjG, 0.3f);
				Vector3 eulerAngles2 = MCamera.eulerAngles;
				eulerAngles2.x = x2;
				Vector3 vector2 = (MCamera.eulerAngles = eulerAngles2);
				int num2 = 0;
				Vector3 eulerAngles3 = MCamera.eulerAngles;
				eulerAngles3.z = num2;
				vector2 = (MCamera.eulerAngles = eulerAngles3);
				ApplyPositionDamping(center + FDp39w4vAww);
			}
		}
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
		{
			if (lowAngle)
			{
				lowAngle = false;
			}
			else
			{
				lowAngle = true;
			}
		}
	}

	public virtual void ApplyPositionDamping(Vector3 targetCenter)
	{
		float num = Mathf.Clamp(distance, 2f, 8f);
		Vector3 position = MCamera.position;
		float f = MCamera.eulerAngles.x * ((float)Math.PI / 180f);
		float f2 = MCamera.eulerAngles.y * ((float)Math.PI / 180f);
		float num2 = Mathf.Cos(f) * (num + Ayg39lhO5oR);
		float num3 = Mathf.Sin(f) * (num + Ayg39lhO5oR);
		Vector3 vector = Vector3.Lerp(b: new Vector3
		{
			x = -1f * Mathf.Sin(f2) * num2 + targetCenter.x,
			y = num3 + targetCenter.y,
			z = -1f * Mathf.Cos(f2) * num2 + targetCenter.z
		}, a: position - AYu39jreQlg, t: Time.deltaTime * 4f);
		MCamera.position = vector + AYu39jreQlg;
		camera.fieldOfView = Mathf.Clamp(45f + (targetCenter - vector).magnitude + 5f * Ayg39lhO5oR, 15f, 90f);
	}
}
