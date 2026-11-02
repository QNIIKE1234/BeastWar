using UnityEngine;
using UnityEngine.UI;

public class Rank_icon : MonoBehaviour
{
	public GameObject ric;

	public RawImage Rim;

	public Texture2D l0;

	public Texture2D l5;

	public Texture2D l10;

	public Texture2D l15;

	public Texture2D l20;

	public Texture2D l25;

	public Texture2D l30;

	public Texture2D l35;

	public Texture2D l40;

	public Texture2D l45;

	public Texture2D l50;

	public Texture2D l55;

	public Texture2D l60;

	public Texture2D l65;

	public Texture2D l70;

	public Texture2D l75;

	public Texture2D l80;

	public Texture2D l85;

	public Texture2D l90;

	public Texture2D l95;

	public Texture2D l100;

	public int p__l_rank;

	public GameObject player;

	public CameraControl cmr;

	public PlayerControl pct;

	private void Start()
	{
		cmr = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
		player = cmr.target;
		pct = player.GetComponent<PlayerControl>();
		Rim = ric.GetComponent<RawImage>();
	}

	private void Update()
	{
		p__l_rank = pct.Rank;
		if (pct.family_name == "Admin1")
		{
			Rim.texture = l100;
		}
		else
		{
			Seticon();
		}
		if (pct.family_name == "Admin2")
		{
			Rim.texture = l95;
		}
	}

	private void Seticon()
	{
		if (p__l_rank <= 4 && p__l_rank >= 0)
		{
			Rim.texture = l0;
		}
		else if (p__l_rank <= 9 && p__l_rank >= 5)
		{
			Rim.texture = l5;
		}
		else if (p__l_rank <= 14 && p__l_rank >= 10)
		{
			Rim.texture = l10;
		}
		else if (p__l_rank <= 19 && p__l_rank >= 15)
		{
			Rim.texture = l15;
		}
		else if (p__l_rank <= 24 && p__l_rank >= 20)
		{
			Rim.texture = l20;
		}
		else if (p__l_rank <= 29 && p__l_rank >= 25)
		{
			Rim.texture = l25;
		}
		else if (p__l_rank <= 34 && p__l_rank >= 30)
		{
			Rim.texture = l30;
		}
		else if (p__l_rank <= 39 && p__l_rank >= 35)
		{
			Rim.texture = l35;
		}
		else if (p__l_rank <= 44 && p__l_rank >= 40)
		{
			Rim.texture = l40;
		}
		else if (p__l_rank <= 49 && p__l_rank >= 45)
		{
			Rim.texture = l45;
		}
		else if (p__l_rank <= 54 && p__l_rank >= 50)
		{
			Rim.texture = l50;
		}
		else if (p__l_rank <= 59 && p__l_rank >= 55)
		{
			Rim.texture = l55;
		}
		else if (p__l_rank <= 64 && p__l_rank >= 60)
		{
			Rim.texture = l60;
		}
		else if (p__l_rank <= 69 && p__l_rank >= 65)
		{
			Rim.texture = l65;
		}
		else if (p__l_rank <= 74 && p__l_rank >= 70)
		{
			Rim.texture = l70;
		}
		else if (p__l_rank <= 79 && p__l_rank >= 75)
		{
			Rim.texture = l75;
		}
		else if (p__l_rank <= 84 && p__l_rank >= 80)
		{
			Rim.texture = l80;
		}
		else if (p__l_rank <= 89 && p__l_rank >= 85)
		{
			Rim.texture = l85;
		}
		else if (p__l_rank <= 94 && p__l_rank >= 90)
		{
			Rim.texture = l90;
		}
	}
}
