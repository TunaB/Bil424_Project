using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private static int spawn_count;
    public GameObject player;
    public GameObject enemy;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (spawn_count > 0)
        {
            Instantiate(enemy,new Vector3(Random.Range(player.transform.position.x+1, player.transform.position.x + 100),1, Random.Range(player.transform.position.z + 1, player.transform.position.z + 100)),Quaternion.identity);
            spawn_count--;
        }
    }

    public static void setCount(int i)
    {
        spawn_count = i;
    }

    
}
