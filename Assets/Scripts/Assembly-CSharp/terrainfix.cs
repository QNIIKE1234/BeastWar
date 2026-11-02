using UnityEngine;

public class terrainfix : MonoBehaviour
{
	public virtual void Awake()
	{
		float heightmapPixelError = 5f;
		float basemapDistance = 200f;
		float detailObjectDistance = 40f;
		Terrain.activeTerrain.heightmapPixelError = heightmapPixelError;
		Terrain.activeTerrain.basemapDistance = basemapDistance;
		Terrain.activeTerrain.detailObjectDistance = detailObjectDistance;
	}
}
