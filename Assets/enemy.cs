using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy : MonoBehaviour
{
    float hp = 100;
    Transform goal;
     GameObject character;
    NavMeshAgent agent;
    movement movement;
    public GameObject equipment;
    // Start is called before the first frame update
    void Start()
    {
        character= GameObject.Find("character");
        movement = character.GetComponent<movement>();
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
            Destroy(gameObject);
        }
        //gameObject.transform.position
    }
}
