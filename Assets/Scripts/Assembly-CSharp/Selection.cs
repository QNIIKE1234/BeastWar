using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public LobbyCamera lbc;

	public GameObject Character_player;

	public GameObject Weapon_Character;

	public GameObject Whale_Shield;

	public Texture2D pic;

	public string Character_type;

	public rd_face rd_face_bt;

	public Texture2D face0;

	public Texture2D face1;

	public Texture2D face2;

	public Texture2D face3;

	public Texture2D face4;

	public Texture2D face5;

	public Texture2D face6;

	public Texture2D face7;

	public Texture2D face8;

	public Texture2D face9;

	public Texture2D face10;

	public Texture2D face11;

	public Texture2D face12;

	public Texture2D face13;

	public Texture2D face14;

	public Texture2D face15;

	public Texture2D face16;

	public Texture2D face17;

	public Texture2D face18;

	public Texture2D face19;

	public Texture2D face20;

	public Texture2D face21;

	public Texture2D face22;

	private void Start()
	{
		lbc = GameObject.Find("LobbyCamera").GetComponent<LobbyCamera>();
		rd_face_bt = GameObject.Find("button_random_face").GetComponent<rd_face>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		CreateCharacter();
		lbc.Issel = true;
	}

	public void CreateCharacter()
	{
		if (lbc.nSelb == 1)
		{
			Object.Destroy(GameObject.Find("player_chr1"));
		}
		else if (lbc.nSelb == 2)
		{
			Object.Destroy(GameObject.Find("player_chr2"));
		}
		else if (lbc.nSelb == 3)
		{
			Object.Destroy(GameObject.Find("player_chr3"));
		}
		GameObject gameObject = Object.Instantiate(Character_player, Character_player.transform.position, Quaternion.identity);
		gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.mainTexture = rd_face_bt.AddWatermark(pic, face0, 0, 256);
		gameObject.tag = "Player";
		gameObject.AddComponent<PlayerControl>();
		PlayerControl component = gameObject.GetComponent<PlayerControl>();
		if (Character_type == "Wolf")
		{
			GameObject obj = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			GameObject gameObject2 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child = gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			Transform child2 = gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj.transform.parent = child.transform;
			gameObject2.transform.parent = child2.transform;
			obj.transform.position = child.position;
			obj.transform.rotation = child.rotation;
			gameObject2.transform.position = child2.position;
			gameObject2.transform.rotation = child2.rotation;
			component.Race = "Wolf";
		}
		else if (Character_type == "Cat")
		{
			GameObject obj2 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child3 = gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
				.GetChild(0)
				.GetChild(2)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj2.transform.parent = child3.transform;
			obj2.transform.position = child3.position;
			obj2.transform.rotation = child3.rotation;
			component.Race = "Cat";
		}
		else if (Character_type == "Penguin")
		{
			GameObject obj3 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child4 = gameObject.transform.GetChild(0).GetChild(2).GetChild(3)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(1);
			obj3.transform.parent = child4.transform;
			obj3.transform.position = child4.position;
			obj3.transform.rotation = child4.rotation;
			component.Race = "Penguin";
		}
		else if (Character_type == "Bat")
		{
			GameObject obj4 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child5 = gameObject.transform.GetChild(0).GetChild(0).GetChild(1)
				.GetChild(2)
				.GetChild(2)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj4.transform.parent = child5.transform;
			obj4.transform.position = child5.position;
			obj4.transform.rotation = child5.rotation;
			component.Race = "Bat";
		}
		else if (Character_type == "Mole")
		{
			GameObject obj5 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child6 = gameObject.transform.GetChild(0).GetChild(0).GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj5.transform.parent = child6.transform;
			obj5.transform.position = child6.position;
			obj5.transform.rotation = child6.rotation;
			component.Race = "Mole";
		}
		else if (Character_type == "Rabbit")
		{
			GameObject obj6 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child7 = gameObject.transform.GetChild(0).GetChild(0).GetChild(2)
				.GetChild(0)
				.GetChild(2)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj6.transform.parent = child7.transform;
			obj6.transform.position = child7.position;
			obj6.transform.rotation = child7.rotation;
			component.Race = "Rabbit";
		}
		else if (Character_type == "Sheep")
		{
			GameObject obj7 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child8 = gameObject.transform.GetChild(0).GetChild(0).GetChild(0)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(0)
				.GetChild(3);
			obj7.transform.parent = child8.transform;
			obj7.transform.position = child8.position;
			obj7.transform.rotation = child8.rotation;
			component.Race = "Sheep";
		}
		else if (Character_type == "Whale")
		{
			GameObject obj8 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			GameObject gameObject3 = Object.Instantiate(Whale_Shield, Whale_Shield.transform.position, Quaternion.identity);
			Transform child9 = gameObject.transform.GetChild(0).GetChild(0).GetChild(0)
				.GetChild(0)
				.GetChild(2)
				.GetChild(0)
				.GetChild(0)
				.GetChild(2);
			Transform child10 = gameObject.transform.GetChild(0).GetChild(0).GetChild(0)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(2);
			obj8.transform.parent = child9.transform;
			gameObject3.transform.parent = child10.transform;
			obj8.transform.position = child9.position;
			obj8.transform.rotation = child9.rotation;
			gameObject3.transform.position = child10.position;
			gameObject3.transform.rotation = child10.rotation;
			component.Race = "Whale";
		}
		else if (Character_type == "Panda")
		{
			GameObject obj9 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			GameObject gameObject4 = Object.Instantiate(Whale_Shield, Whale_Shield.transform.position, Quaternion.identity);
			Transform child11 = gameObject.transform.GetChild(0).GetChild(0).GetChild(3)
				.GetChild(0)
				.GetChild(2)
				.GetChild(0)
				.GetChild(1);
			Transform child12 = gameObject.transform.GetChild(0).GetChild(0).GetChild(3)
				.GetChild(0)
				.GetChild(3)
				.GetChild(0)
				.GetChild(1);
			obj9.transform.parent = child11.transform;
			gameObject4.transform.parent = child12.transform;
			obj9.transform.position = child11.position;
			obj9.transform.rotation = child11.rotation;
			gameObject4.transform.position = child12.position;
			gameObject4.transform.rotation = child12.rotation;
			component.Race = "Panda";
		}
		else if (Character_type == "Chameleon")
		{
			GameObject obj10 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			GameObject gameObject5 = Object.Instantiate(Whale_Shield, Whale_Shield.transform.position, Quaternion.identity);
			Transform child13 = gameObject.transform.GetChild(0).GetChild(2).GetChild(3)
				.GetChild(1)
				.GetChild(0)
				.GetChild(2)
				.GetChild(0)
				.GetChild(0)
				.GetChild(2);
			Transform child14 = gameObject.transform.GetChild(0).GetChild(2).GetChild(3)
				.GetChild(1)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(2);
			obj10.transform.parent = child13.transform;
			gameObject5.transform.parent = child14.transform;
			obj10.transform.position = child13.position;
			obj10.transform.rotation = child13.rotation;
			gameObject5.transform.position = child14.position;
			gameObject5.transform.rotation = child14.rotation;
			component.Race = "Chameleon";
		}
		else if (Character_type == "Monkey")
		{
			GameObject obj11 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child15 = gameObject.transform.GetChild(0).GetChild(2);
			obj11.transform.parent = child15.transform;
			obj11.transform.position = child15.position;
			obj11.transform.rotation = child15.rotation;
			component.Race = "Monkey";
		}
		else if (Character_type == "Bison")
		{
			GameObject obj12 = Object.Instantiate(Weapon_Character, Weapon_Character.transform.position, Quaternion.identity);
			Transform child16 = gameObject.transform.GetChild(0).GetChild(2).GetChild(1)
				.GetChild(0)
				.GetChild(1)
				.GetChild(0)
				.GetChild(0)
				.GetChild(0)
				.GetChild(0)
				.GetChild(4);
			obj12.transform.parent = child16.transform;
			obj12.transform.position = child16.position;
			obj12.transform.rotation = child16.rotation;
			component.Race = "Bison";
		}
		component.Islobby = true;
		if (lbc.nSelb == 1)
		{
			gameObject.name = "player_chr1";
			gameObject.transform.position = new Vector3(99.5f, 50f, 102.15f);
			gameObject.transform.rotation = Quaternion.Euler(0f, 340f, 0f);
		}
		else if (lbc.nSelb == 2)
		{
			gameObject.name = "player_chr2";
			gameObject.transform.position = new Vector3(98.8f, 50f, 98.8f);
			gameObject.transform.rotation = Quaternion.Euler(0f, 230f, 0f);
		}
		else if (lbc.nSelb == 3)
		{
			gameObject.name = "player_chr3";
			gameObject.transform.position = new Vector3(101.8f, 50f, 99.8f);
			gameObject.transform.rotation = Quaternion.Euler(0f, 105f, 0f);
		}
		rd_face_bt.spwedplayer = gameObject;
		rd_face_bt.suit = pic;
		rd_face_bt.face_rd_0 = face0;
		rd_face_bt.face_rd_5 = face5;
		rd_face_bt.face_rd_10 = face10;
		rd_face_bt.face_rd_15 = face15;
		rd_face_bt.face_rd_19 = face19;
		rd_face_bt.face_rd_1 = face1;
		rd_face_bt.face_rd_6 = face6;
		rd_face_bt.face_rd_11 = face11;
		rd_face_bt.face_rd_16 = face16;
		rd_face_bt.face_rd_20 = face20;
		rd_face_bt.face_rd_2 = face2;
		rd_face_bt.face_rd_7 = face7;
		rd_face_bt.face_rd_12 = face12;
		rd_face_bt.face_rd_17 = face17;
		rd_face_bt.face_rd_21 = face21;
		rd_face_bt.face_rd_3 = face3;
		rd_face_bt.face_rd_8 = face8;
		rd_face_bt.face_rd_13 = face13;
		rd_face_bt.face_rd_18 = face18;
		rd_face_bt.face_rd_22 = face22;
		rd_face_bt.face_rd_4 = face4;
		rd_face_bt.face_rd_9 = face9;
		rd_face_bt.face_rd_14 = face14;
		lbc.target = gameObject;
	}
}
