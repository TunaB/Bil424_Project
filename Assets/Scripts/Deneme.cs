using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public GameObject terr;
    public GameObject player;

    void Start()
    {
        Instantiate(terr, new Vector3(0, 0, 0), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = player.transform.position;
        Vector3 direction = Vector3.down;
        RaycastHit hit;
        RaycastHit hit2;

        if (Physics.Raycast(start, direction, out hit))
        {
            if (player.transform.position.x > hit.collider.gameObject.transform.position.x+900)
            {
                if (!Physics.Raycast(start+new Vector3(1000,0,0), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x + 1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z), Quaternion.identity);
            }
            if (player.transform.position.x < hit.collider.gameObject.transform.position.x + 100)
            {
                if (!Physics.Raycast(start + new Vector3(-1000, 0, 0), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x - 1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z), Quaternion.identity);
            }
            if (player.transform.position.z > hit.collider.gameObject.transform.position.z + 900)
            {
                if (!Physics.Raycast(start + new Vector3(0, 0, 1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x , hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z + 1000), Quaternion.identity);
            }
            if (player.transform.position.z < hit.collider.gameObject.transform.position.z + 100)
            {
                if (!Physics.Raycast(start + new Vector3(0, 0, -1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z - 1000), Quaternion.identity);
            }

            if (player.transform.position.x > hit.collider.gameObject.transform.position.x + 900 && player.transform.position.z > hit.collider.gameObject.transform.position.z + 900)
            {
                if (!Physics.Raycast(start + new Vector3(1000, 0, 1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x+1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z + 1000), Quaternion.identity);
            }
            if (player.transform.position.x > hit.collider.gameObject.transform.position.x + 900 && player.transform.position.z < hit.collider.gameObject.transform.position.z + 100)
            {
                if (!Physics.Raycast(start + new Vector3(1000, 0, -1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x+1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z - 1000), Quaternion.identity);
            }
            if (player.transform.position.x < hit.collider.gameObject.transform.position.x + 100 && player.transform.position.z > hit.collider.gameObject.transform.position.z + 900)
            {
                if (!Physics.Raycast(start + new Vector3(-1000, 0, 1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x-1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z + 1000), Quaternion.identity);
            }
            if (player.transform.position.x < hit.collider.gameObject.transform.position.x + 100 && player.transform.position.z < hit.collider.gameObject.transform.position.z + 100)
            {
                if (!Physics.Raycast(start + new Vector3(-1000, 0, -1000), direction, out hit2))
                    Instantiate(terr, new Vector3(hit.collider.gameObject.transform.position.x-1000, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z - 1000), Quaternion.identity);
            }
        }
    }
}
