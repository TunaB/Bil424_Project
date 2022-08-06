using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float dmg;
    public int type;//0 arrow, 1 magic
    public string altType;
    // Start is called before the first frame update
    void Start()
    {
        if (type == 1)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (type == 1 && !collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("weapon"))
        {
            Destroy(gameObject);
        }
        if (type == 0 && !collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("weapon"))
        {
            Debug.Log("hit");
            gameObject.GetComponent<Rigidbody>().useGravity = false;

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
