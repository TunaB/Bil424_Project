using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy : MonoBehaviour
{
    public int level=1;
    public float hp = 100;
    Transform goal;
     GameObject character;
    NavMeshAgent agent;
    movement movement;
    public GameObject equipment;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        character= GameObject.Find("character");
        movement = character.GetComponent<movement>();
        gameManager = GameObject.Find("hud").GetComponent<GameManager>();
        goal =character.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("weapon"))
        {
            hp -= 40;
        }
    }
    void dropLoot()
    {
        equipmentValues equipmentValues = equipment.GetComponent<equipmentValues>();
        equipmentValues.level = level;
        equipmentValues.dmg = Random.Range(1, 2) * level;
        int type = Random.Range(0, 10);
        if (type < 3)
        {
            equipmentValues.type = "sword";
            equipmentValues.altType = "basic";
        }
        else if (type < 6)
        {
            equipmentValues.type = "bow";
            equipmentValues.altType = "basic";
        }
        else if (type < 9)
        {
            equipmentValues.type = "staff";
            equipmentValues.altType = "basic";
        }
        else
        {
            equipmentValues.type = "relic";
            int perm=Random.Range(0, 100);
            if (perm > 90)
            {
                equipmentValues.permanent = true;
            }
            else
            {
                equipmentValues.permanent = false;
            }
            equipmentValues.effectType = "armor";
            equipmentValues.effect = 15;
        }

        Instantiate(equipment, gameObject.transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
        agent.destination = character.transform.position;
        if (hp <= 0)
        {
            dropLoot();
            movement.enemyKilled();
            gameManager.killCount++;
            gameManager.removeFromList(gameObject);
            Destroy(gameObject);
        }
        //gameObject.transform.position
    }
}
