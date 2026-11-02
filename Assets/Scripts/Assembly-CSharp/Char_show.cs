using UnityEngine;

public class Char_show : MonoBehaviour
{
	public GameObject wolf;

	public GameObject sheep;

	public GameObject bat;

	public GameObject cat;

	public GameObject rabbit;

	public GameObject mole;

	public GameObject bison;

	public GameObject panda;

	public GameObject chameleon;

	public GameObject whale;

	public GameObject monkey;

	public GameObject penguin;

	public PlayerControl pct;

	private void Start()
	{
		pct = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
		if (pct.Race == "Wolf")
		{
			wolf.SetActive(value: true);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Cat")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: true);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Penguin")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: true);
		}
		else if (pct.Race == "Bat")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: true);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Mole")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: true);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Rabbit")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: true);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Sheep")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: true);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Whale")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: true);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Panda")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: true);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Chameleon")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: true);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Monkey")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: false);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: true);
			penguin.SetActive(value: false);
		}
		else if (pct.Race == "Bison")
		{
			wolf.SetActive(value: false);
			sheep.SetActive(value: false);
			bat.SetActive(value: false);
			cat.SetActive(value: false);
			rabbit.SetActive(value: false);
			mole.SetActive(value: false);
			bison.SetActive(value: true);
			panda.SetActive(value: false);
			chameleon.SetActive(value: false);
			whale.SetActive(value: false);
			monkey.SetActive(value: false);
			penguin.SetActive(value: false);
		}
	}
}
