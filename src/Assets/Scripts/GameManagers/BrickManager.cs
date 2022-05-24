using System.Collections.Generic;
using UnityEngine;
using Random = System.Random; // using Random = UnityEngine.Random;


public class BrickManager : MonoBehaviour
{
    /* Class that creates and manages brick creation
     */
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 5;
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private int seed = 10;
    public List<GameObject> brickPrefabs;

    private Random rnd;
    private List<GameObject> bricks = new List<GameObject>();

    private void Start()
    {
        rnd = new Random(seed); // Using seed, so that generated level always is the same
        InstantiateLevel();
    }

    public void InstantiateLevel()
    {
        /* This is the function responsible for generating the brick positioning for the level
         */
        foreach (GameObject brick in bricks)
        {
            Destroy(brick);
        }

        bricks.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 localScale = brickPrefabs[0].transform.localScale; // get a generic scaling
                Vector2 spawn = (Vector2) transform.position +
                                new Vector2(x * (localScale.x + spacing) / 5, -y * (localScale.y + spacing) / 10);

                int idx = rnd.Next(2, brickPrefabs.Count); // atm, this will always return 2 as there are only 3 types of bricks
                int chance = rnd.Next(0, 101); // random percentage

                if (chance < 11) // ensuring there is at most 10 percent of Permanent Bricks
                {
                    idx = 0;
                }

                if (chance > 10 && chance < 51) // there is at most 40 percent of Explosive bricks
                {
                    idx = 1;
                }

                GameObject brick = Instantiate(brickPrefabs[idx], spawn, transform.rotation);

                if (!brick.CompareTag("PermaBlock")) // don't put PermaBlock into the counter for simplicity
                {
                    bricks.Add(brick);
                }
            }
        }
    }

    public void RemoveBrick(GameObject brick)
    {
        /* Handles check for whether all possible bricks were destroyed
         */
        bricks.Remove(brick);
        if (bricks.Count == 0)
        {
            UIManager.Instance.DisplayWinScreen(); // calls the UIManager instance to display appropriate screen
        }
    }
}