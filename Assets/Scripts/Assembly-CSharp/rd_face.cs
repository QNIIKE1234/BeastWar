using UnityEngine;
using UnityEngine.EventSystems;

public class rd_face : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public GameObject spwedplayer;

	public Texture2D suit;

	public Texture2D face;

	public Selection selection;

	private int numF;

	public Texture2D face_rd_0;

	public Texture2D face_rd_5;

	public Texture2D face_rd_10;

	public Texture2D face_rd_14;

	public Texture2D face_rd_18;

	public Texture2D face_rd_1;

	public Texture2D face_rd_6;

	public Texture2D face_rd_11;

	public Texture2D face_rd_15;

	public Texture2D face_rd_19;

	public Texture2D face_rd_2;

	public Texture2D face_rd_7;

	public Texture2D face_rd_12;

	public Texture2D face_rd_16;

	public Texture2D face_rd_20;

	public Texture2D face_rd_3;

	public Texture2D face_rd_8;

	public Texture2D face_rd_13;

	public Texture2D face_rd_17;

	public Texture2D face_rd_21;

	public Texture2D face_rd_4;

	public Texture2D face_rd_9;

	public Texture2D face_rd_22;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		numF = Random.Range(0, 22);
		numTotx();
		spwedplayer.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.mainTexture = AddWatermark(suit, face, 0, 256);
	}

	public Texture2D AddWatermark(Texture2D background, Texture2D watermark, int startX, int startY)
	{
		Texture2D texture2D = new Texture2D(background.width, background.height, background.format, mipChain: false);
		for (int i = 0; i < background.width; i++)
		{
			for (int j = 0; j < background.height; j++)
			{
				if (i >= startX && j >= startY && i <= 256)
				{
					Color pixel = background.GetPixel(i, j);
					Color pixel2 = watermark.GetPixel(i - startX, j - startY);
					Color color = Color.Lerp(pixel, pixel2, pixel2.a / 1f);
					texture2D.SetPixel(i, j, color);
				}
				else
				{
					texture2D.SetPixel(i, j, background.GetPixel(i, j));
				}
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	public void numTotx()
	{
		if (numF == 0)
		{
			face = face_rd_0;
		}
		else if (numF == 1)
		{
			face = face_rd_1;
		}
		else if (numF == 2)
		{
			face = face_rd_2;
		}
		else if (numF == 3)
		{
			face = face_rd_3;
		}
		else if (numF == 4)
		{
			face = face_rd_4;
		}
		else if (numF == 5)
		{
			face = face_rd_5;
		}
		else if (numF == 6)
		{
			face = face_rd_6;
		}
		else if (numF == 7)
		{
			face = face_rd_7;
		}
		else if (numF == 8)
		{
			face = face_rd_8;
		}
		else if (numF == 9)
		{
			face = face_rd_9;
		}
		else if (numF == 10)
		{
			face = face_rd_10;
		}
		else if (numF == 11)
		{
			face = face_rd_11;
		}
		else if (numF == 12)
		{
			face = face_rd_12;
		}
		else if (numF == 13)
		{
			face = face_rd_13;
		}
		else if (numF == 14)
		{
			face = face_rd_14;
		}
		else if (numF == 15)
		{
			face = face_rd_15;
		}
		else if (numF == 16)
		{
			face = face_rd_16;
		}
		else if (numF == 17)
		{
			face = face_rd_17;
		}
		else if (numF == 18)
		{
			face = face_rd_18;
		}
		else if (numF == 19)
		{
			face = face_rd_19;
		}
		else if (numF == 20)
		{
			face = face_rd_20;
		}
		else if (numF == 21)
		{
			face = face_rd_21;
		}
		else if (numF == 22)
		{
			face = face_rd_22;
		}
	}
}
