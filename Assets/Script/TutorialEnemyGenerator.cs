using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieType
{
    Basic,
    Strong
}


public class TutorialEnemyGenerator : MonoBehaviour
{
    public static TutorialEnemyGenerator Instance;


    public ZombieType zombietype;
    public GameObject zombiePref;
    public GameObject strongZombiePref;
    [SerializeField]
    private Vector3 spawnPos;
    private float spawnDelay;
    private int strongMaxCount = 2;
    private int basicMaxCount = 2;
    private Queue<GameObject> basicList = new Queue<GameObject>();
    private Queue<GameObject> strongList = new Queue<GameObject>();
    private int currentZombieCount;
    private int currentStrongZombieCount;

    private float minx;
    private float miny;
    private float maxx;
    private float maxy;

    public GameObject ground;
    public Bounds groundbound;

    private void Awake()
    {
        Instance = this;
        groundbound = ground.GetComponent<MeshRenderer>().bounds;
        minx = groundbound.min.x;
        miny = groundbound.min.z;
        maxx = groundbound.max.x;
        maxy = groundbound.max.z;
        spawnDelay = 3;
        StartCoroutine(GenerateZombie());
    }

    private IEnumerator GenerateZombie()
    {
        int count = 0;
        while (true)
        {
            count++;
            if(currentZombieCount < basicMaxCount)
            {
                GenerateZombie(zombiePref, ZombieType.Basic);
            }
            if(currentStrongZombieCount < strongMaxCount)
            {
                GenerateZombie(strongZombiePref, ZombieType.Strong);
            }
        yield return new WaitForSeconds(1);
        }

    }

    private void GenerateZombie(GameObject pref, ZombieType type)
    {
        Queue<GameObject> queue = null;
        switch(type)
        {
            case ZombieType.Basic:
                currentZombieCount++;
                queue = basicList;
                break;
            case ZombieType.Strong:
                currentStrongZombieCount++;
                queue = strongList;
                break;
            default:
                queue = new Queue<GameObject>();
                break;

        }

        float xpos = Random.Range(minx + 1, maxx - 1);
        float ypos = Random.Range(miny + 1, maxy - 1);

        Vector3 spawnPos = new Vector3(xpos, 1, ypos);
        float rotation = Random.Range(0, 360);

        if (queue.Count == 0)
        {
            GameObject zombie = Instantiate(pref, spawnPos,Quaternion.Euler(0,rotation,0));
        }
        else
        {
            GameObject zombie = queue.Dequeue();
            zombie.transform.position = spawnPos;
            zombie.transform.rotation = Quaternion.Euler(0, rotation, 0);
            zombie.SetActive(true);
        }

    }

    public void Zombiess(ZombieType type, GameObject pref)
    {
        Queue<GameObject> queue = null;
        switch (type)
        {
            case ZombieType.Basic:
                Instance.currentZombieCount--;
                queue = Instance.basicList;
                break;
            case ZombieType.Strong:
                Instance.currentStrongZombieCount--;
                queue = Instance.strongList;
                break;
            default:
                queue = new Queue<GameObject>();
                break;
        }

        queue.Enqueue(pref);
    }
}
