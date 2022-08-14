using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private movement movement;
    public GameObject enemy;
    public int currentLevel = 1;
    public int killCount = 0;
    float timePassed = 0;
    List<GameObject> enemyList;
    int index = 0;
    float dmg;
    string weapon;
    int level;
    string type;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        movement = player.GetComponent<movement>();
        StartCoroutine(enemies());
    }
     void Update()
    {
        
        if(killCount > 5)
        {
            currentLevel++;
            killCount = 0;
        }
    }
    public void removeFromList(GameObject enem)
    {
        enemyList.Remove(enem);
    }
    IEnumerator enemies()
    {
        while (Application.isPlaying)
        {
            enemyList.Add(createEnemy());
            index++;
            yield return new WaitForSeconds(5);
        }
    }
    GameObject createEnemy()
    {
        Vector2 random = Random.insideUnitCircle.normalized * Random.Range(5, 15);
        Vector3 vector = player.transform.position;
        vector.x += random.x;
        vector.z += random.y;
        enemy.GetComponent<enemy>().level = currentLevel;

        return Instantiate(enemy, vector, Quaternion.identity);
    }
    
    // Update is called once per frame
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width / 4 - 50, 20, 100, 100), "stamina " + movement.stamina);
        GUI.Label(new Rect(Screen.width / 4 - 50, 40, 100, 100), "hp " + movement.hp);
        GUI.Label(new Rect(Screen.width / 2 - 50, 20, 100, 100), "mana " + movement.mana);
        GUI.Label(new Rect(Screen.width / 2 - 50, 100, 100, 100), "arrows " + movement.arrowCount);
        if (movement.mode == 0)
        {
            weapon = "sword";
            dmg = movement.getSword().dmg;
            type = movement.getSword().type;
            level = movement.getSword().level;
        }
        else if (movement.mode == 1)
        {
            weapon = "bow";
            dmg = movement.getBow().dmg;
            type= movement.getBow().type;
            level= movement.getBow().level;
        }
        else if(movement.mode == 2)
        {
            weapon = "staff";
            dmg = movement.getStaff().dmg;
            type = movement.getStaff().type;
            level = movement.getStaff().level;
        }
        int diff = 20;
        GUI.Label(new Rect(100, 100, 100, 100), "Relics");
        foreach (movement.relic relic in movement.getRelic())
        {
            GUI.Label(new Rect( 100, 100 + diff, 100, 100), relic.effect+" +%"+relic.effectType);
            diff += 10;
        }
        GUI.Label(new Rect(Screen.width / 3 - 50, 100, 100, 100), "level "+level+" " + type +" "+ weapon);

        GUI.Label(new Rect(Screen.width / 2, Screen.height/2, 100, 100), "-" );
        int x = 20;
        GUI.Label(new Rect(Screen.width - 100, 100 , 100, 100), "enemy hp " );
        foreach (GameObject enem in enemyList)
        {
            GUI.Label(new Rect(Screen.width -100, 100+x, 100, 100),enem.GetComponent<enemy>().hp.ToString());
            x += 10;
        }
    }
}
