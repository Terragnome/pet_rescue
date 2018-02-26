using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int PetCount;
    public List<GameObject> PetPrefabs;

    private const float kSpawnCooldown = 1.0f;
    private float mSpawnTimer = 0.0f;

    private float mMinX;
    private float mMaxX;
    private float mMinZ;
    private float mMaxZ;
    private float mY;

	void Start()
    {
        Collider collider = GetComponent<Collider>();
        Bounds bounds = collider.bounds;
        mMinX = bounds.min.x;
        mMinZ = bounds.min.z;
        mMaxX = bounds.max.x;
        mMaxZ = bounds.max.z;
        mY = bounds.max.y;
	}
	
	void FixedUpdate()
    {
        mSpawnTimer += Time.deltaTime;
        if (mSpawnTimer < kSpawnCooldown)
        {
            return;
        }

        GameObject[] pets = GameObject.FindGameObjectsWithTag("Pet");
        if (pets.Length < PetCount)
        {
            int index = Random.Range(0, PetPrefabs.Count);
            GameObject prefab = PetPrefabs[index];

            float x = Random.Range(mMinX, mMaxX);
            float z = Random.Range(mMinZ, mMaxZ);
            Vector3 position = new Vector3(x, mY, z);

            GameObject.Instantiate(prefab, position, Quaternion.identity);

            mSpawnTimer = 0.0f;
        }
	}
}
