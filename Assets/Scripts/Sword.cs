using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    
    public GameObject player;
    public GameObject enemy;
    
        public void SwordDamage(float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                Vector3 directionToTarget = player.transform.position - enemy.transform.position;
                float angle = Vector3.Angle(transform.forward, directionToTarget);
                float distance = directionToTarget.magnitude;

                if (Mathf.Abs(angle) < 90 && distance < 10)
                    hitCollider.SendMessage("AddDamage");
            }
        }
    
    
}
