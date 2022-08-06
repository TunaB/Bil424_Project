using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicDestroy : MonoBehaviour
{
    Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<Collider>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        //particle
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
