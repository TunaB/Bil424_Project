using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> inventory;
    void Start()
    {
        inventory = new List<GameObject>();
        player.transform.position = new Vector3(400,1,400);
        SpawnEnemies.setCount(5);
    }

    
    void Update()
    {
        
    }

    void addItem(GameObject item)
    {
        inventory.Add(item);
    }
}
