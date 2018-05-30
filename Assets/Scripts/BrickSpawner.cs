using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BlockTier
{
    public float Row;
    public Color Color;
    public int Points;
}

/// <summary>
/// Spawns a grid of bricks based on sprite renderer bounds
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class BrickSpawner : MonoBehaviour {




    [SerializeField]
    private BlockTier[] _tiers;
    [SerializeField]
    private GameObject _brickPrefab;
    [SerializeField]
    private float _horizontalSpacing;
    [SerializeField]
    private float _vertSpacing;

    private int _bricksSpawned;
    
    // Grid is spawned in Awake
	void Awake () {
        SpawnBlocks();

        EventMessenger.Instance.OnBrickDestroyed.AddListener(BrickDestroyed);
        EventMessenger.Instance.OnLevelFinished.AddListener(SpawnBlocks);
    }

   

    void BrickDestroyed(int points)
    {
        _bricksSpawned -= 1;
        if(_bricksSpawned == 0)
        {
            EventMessenger.Instance.OnLevelFinished.Invoke();
        }
    }

    void SpawnBlocks()
    {
        // get bounds of us and bounds of children
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        Bounds brickBounds = _brickPrefab.GetComponent<SpriteRenderer>().bounds;

        int currentTier = 0;
        int rowCount = 0;
        // iterate through each row 
        for (float y = 0; y < bounds.size.y; y += brickBounds.size.y + _vertSpacing, rowCount++)
        {
            if(currentTier + 1 < _tiers.Length && _tiers[currentTier + 1].Row <= rowCount)
            {
                currentTier = currentTier + 1;
            }
            // use a parent for each row to make centering easier
            GameObject rowParent = new GameObject("Row " + rowCount);

            // store the length of our row 
            float finalLength = 0;
            // iterate through row spawning bricks
            for (float x = 0; x < bounds.size.x; x += brickBounds.size.x + _horizontalSpacing)
            {
                GameObject spawnedBrick = Instantiate(_brickPrefab, new Vector2(x - bounds.size.x / 2, 0), Quaternion.identity, rowParent.transform);
                spawnedBrick.GetComponent<Brick>().SetTier(_tiers[currentTier]);
                _bricksSpawned += 1;
                finalLength = x;
            }
            // our rows won't be centered so calculate the amount off we are from being centered and offset our x by half of that to center
            float xOffset = (bounds.size.x - finalLength) / 2;

            // parent rows and set row position
            rowParent.transform.parent = transform;
            // we aren't vertically centered, but its much harder to tell, so I didn't bother

            rowParent.transform.position = rowParent.transform.position + new Vector3(xOffset, bounds.size.y - y, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, GetComponent<SpriteRenderer>().bounds.size);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
