using UnityEngine;

public class GeneratorWorld : MonoBehaviour
{
    [SerializeField] private GameObject[] floorPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] wallPrefabs;
    [SerializeField] private GameObject[] spawnerPrefabs;

    [SerializeField] private Vector2Int sizeGeneration;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < sizeGeneration.x; x++)
        {
            for (int y = 0; y < sizeGeneration.y; y++)
            {
                if (x == 0 || x == sizeGeneration.x - 1 || y == 0 || y == sizeGeneration.y - 1)
                {
                    GenerateWall(x, y);
                }
                else if ((x == 1 || x == sizeGeneration.x - 2 || y == 1 || y == sizeGeneration.y - 2) && Random.value < 0.1f)
                {
                    GenerateSpawner(x, y);
                }
                else
                {
                    GenerateFloor(x, y);
                }
            }
        }

    }

    private void GenerateFloor(int x, int y)
    {
        var index = Random.Range(0, floorPrefabs.Length);
        var floor = Instantiate(floorPrefabs[index]);
        floor.transform.parent = transform;
        floor.transform.localPosition = new Vector3(x, 0, y);
    }

    private void GenerateWall(int x, int y)
    {
        var index = Random.Range(0, wallPrefabs.Length);
        var wall = Instantiate(wallPrefabs[index]);
        wall.transform.parent = transform;
        wall.transform.localPosition = new Vector3(x, 0, y);
    }

    private void GenerateSpawner(int x, int y)
    {
        var index = Random.Range(0, spawnerPrefabs.Length);
        var spawner = Instantiate(spawnerPrefabs[index]);
        spawner.transform.parent = transform;
        spawner.transform.localPosition = new Vector3(x, 0, y);
    }

}
