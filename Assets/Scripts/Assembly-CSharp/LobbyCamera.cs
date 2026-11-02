using System;
using UnityEngine;

public class LobbyCamera : MonoBehaviour
{
	public Transform PlayerTransform;

	public Vector3 _cameraOffset;

	[Range(0.01f, 1f)]
	public float SmoothFactor = 0.5f;

	public bool LookAtPlayer;

	public bool RotateAroundPlayer = true;

	public int nSelb;

	public bool nChar_1;

	public bool nChar_2;

	public bool nChar_3;

	public GameObject sel;

	public GameObject cre;

	public Vector3 ogpos;

	public bool Issel;

	public GameObject target;

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

	private Camera camera;

	private void Start()
	{
		_cameraOffset = base.transform.position - PlayerTransform.position;
		if (RotateAroundPlayer)
		{
			Quaternion quaternion = Quaternion.AngleAxis(60f, Vector3.up);
			_cameraOffset = quaternion * _cameraOffset;
		}
	}

	private void FixedUpdate()
	{
		if (nSelb == 1)
		{
			if (nChar_1)
			{
				sel.SetActive(value: true);
				cre.SetActive(value: false);
			}
			else
			{
				sel.SetActive(value: false);
				cre.SetActive(value: true);
			}
		}
		else if (nSelb == 2)
		{
			if (nChar_2)
			{
				sel.SetActive(value: true);
				cre.SetActive(value: false);
			}
			else
			{
				sel.SetActive(value: false);
				cre.SetActive(value: true);
			}
		}
		else if (nSelb == 3)
		{
			if (nChar_3)
			{
				sel.SetActive(value: true);
				cre.SetActive(value: false);
			}
			else
			{
				sel.SetActive(value: false);
				cre.SetActive(value: true);
			}
		}
		if (Issel)
		{
			CameraCT();
		}
		Vector3 b = PlayerTransform.position + _cameraOffset;
		base.transform.position = Vector3.Slerp(base.transform.position, b, SmoothFactor);
		if (LookAtPlayer || RotateAroundPlayer)
		{
			base.transform.LookAt(PlayerTransform);
		}
	}

	public virtual void Awake()
	{
		FDp39w4vAww = Vector3.zero;
		mP239moOrjG = 0f;
		AYu39jreQlg = Vector3.zero;
		Ayg39lhO5oR = 0f;
		MCamera = GetComponent<Transform>();
		camera = GetComponent<Camera>();
		camera.farClipPlane = Mathf.Clamp(clipPlaneMultiplier, 0.5f, 2f) * 500f;
		lowAngle = true;
	}

	public virtual void CameraCT()
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
			distance = 0f;
			int num = 4;
			if (lowAngle)
			{
				num = 20;
			}
			Quaternion b = Quaternion.LookRotation(target.transform.position - MCamera.position);
			MCamera.rotation = Quaternion.Slerp(MCamera.rotation, b, Time.deltaTime * 3f);
			float x = Mathf.SmoothDampAngle(MCamera.eulerAngles.x, 4f * distance - (float)num, ref mP239moOrjG, 0.3f);
			Vector3 eulerAngles = MCamera.eulerAngles;
			eulerAngles.x = x;
			MCamera.eulerAngles = eulerAngles;
			rotation = 0f;
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
