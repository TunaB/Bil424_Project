using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [HideInInspector]
    public bool isBasic;
    public bool isPoison;
    public bool isFlame;
    public bool isSlowing;
    public bool isStrong;

    public void explode(float radius)
    {
        if (transform.position.y == 0)
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius - (radius / 2) * System.Convert.ToSingle(isStrong));
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.SendMessage("AddDamage", 50);
            }
            


        }
    }
     void Update(){
        if (transform.position.y == 0)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag.Equals("enemy"))
                    Debug.Log("BOMB");
                
            }
            Destroy(this);
        }
    }
}
