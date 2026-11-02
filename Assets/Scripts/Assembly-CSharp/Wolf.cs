using UnityEngine;

public class Wolf : MonoBehaviour
{
	public PlayerControl pct;

	private Animation animator;

	private string name_2 = "root_52";

	public int Atk;

	public int Def;

	public int Agi;

	public int Vit;

	public int Mag;

	public int Cha;

	public int Tal;

	public int Lck;

	public int pAtk;

	public int pDef;

	public int pAgi;

	public int pVit;

	public int pMag;

	public int pCha;

	public int pTal;

	public int pLck;

	public int Hp;

	public int Mhp;

	public int Mp;

	public int Mmp;

	public int Sp;

	public int Msp;

	public int Hate;

	public int Mhate;

	public int nPow;

	private void Start()
	{
		animator = GetComponent<Animation>();
		pct = GetComponent<PlayerControl>();
	}

	private void FixedUpdate()
	{
		if (pct.move != Vector3.zero)
		{
			string animation = "walk_0";
			name_2 = "root_52";
			if (pct.Speed > 5.5f)
			{
				animation = "run_10";
			}
			animator.CrossFade(animation);
		}
		else
		{
			if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.B))
			{
				name_2 = "battle_3";
			}
			animator.CrossFade(name_2);
		}
	}
}
