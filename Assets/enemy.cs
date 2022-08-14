﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy : MonoBehaviour
{
    public int level;
    public float hp ;
    Transform goal;
     GameObject character;
    NavMeshAgent agent;
    movement movement;
    public GameObject equipment;
    GameManager gameManager;
    List<string> altTypes;
    List<string> effectTypes;
    float fireTime;
    float poisonTime;
    bool isFire=false;
    bool isPoison = false;
    float fireDmg;
    float poisonDmg;
    int poisonCounter;
    int fireCounter;
    public bool boss=false;

    // Start is called before the first frame update
    void Start()
    {
        altTypes = new List<string>();
        altTypes.Add("basic");
        altTypes.Add("fire");
        altTypes.Add("multi");
        altTypes.Add("heavy");
        altTypes.Add("posion");

        effectTypes = new List<string>();
        effectTypes.Add("armor");
        effectTypes.Add("speed");
        effectTypes.Add("damage");
        effectTypes.Add("jump");
        effectTypes.Add("hp");
        

        character = GameObject.Find("character");
        movement = character.GetComponent<movement>();
        gameManager = GameObject.Find("hud").GetComponent<GameManager>();
        goal =character.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        hp = (level  * Random.Range(10, 20))*(boss?3:1);
    }

IEnumerator poison()
    {
        while (isPoison)
        {
            
            hp -= poisonDmg;
            poisonCounter--;
            if (poisonCounter == 0)
            {
                isPoison = false;
            }
            yield return new WaitForSeconds(1);

        }

    }
    IEnumerator fire()
    {
        while (isFire)
        {
            hp -= fireDmg;
            fireCounter--;
            if (fireCounter == 0)
            {
                isFire = false;
            }
            yield return new WaitForSeconds(1);

        }
    }
    void enterFire(float dmg)
    {
        fireCounter = 3;

        fireDmg = dmg / 8;
        isFire = true;
        fireTime = Time.time;
        StartCoroutine(fire());
    }
    void enterPoison(float dmg)
    {
        poisonCounter = 5;

        poisonDmg = dmg / 10;
        isPoison = true;
        poisonTime = Time.time;
        StartCoroutine(poison());

    }

    public void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag.Equals("weapon"))
        {
            float dmg = collision.gameObject.GetComponent<arrow>().dmg;
            string type= collision.gameObject.GetComponent<arrow>().altType;
            Debug.Log(type);
            switch (type)
            {
                case "heavy":
                    dmg = (float)(dmg * 1.5);
                    break;
                case "fire":
                    Debug.Log("saddsa");
                    enterFire(dmg);
                    break;
                case "poison":
                    enterPoison(dmg);
                    break;

            }


            hp -= dmg;
        }
    }
    void dropLoot()
    {
        var index = Random.Range(0, altTypes.Count);
        var randAltType = altTypes[index];
        equipmentValues equipmentValues = equipment.GetComponent<equipmentValues>();
        equipmentValues.level = level;
        equipmentValues.dmg = Random.Range(8, 12) * level;
        equipmentValues.altType = randAltType;
        int type = Random.Range(0, 10);
        if (type < 3)
        {
            equipmentValues.type = "sword";
        }
        else if (type < 6)
        {
            equipmentValues.type = "bow";
        }
        else if (type < 9)
        {
            equipmentValues.type = "staff";
        }
        else
        {
            equipmentValues.type = "relic";
            index = Random.Range(0, effectTypes.Count);
            var randEffectType = effectTypes[index];
            equipmentValues.effectType = randEffectType;
            equipmentValues.effect= (float)(Random.Range(0, 500))/100.0f;
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

        GameObject eq =Instantiate(equipment, gameObject.transform.position, Quaternion.identity);
        Destroy(eq, 20);

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
