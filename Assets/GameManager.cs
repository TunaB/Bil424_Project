using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private movement movement;
    public GameObject enemy;

    float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
        movement = player.GetComponent<movement>();
        StartCoroutine(enemies());
    }
    IEnumerator enemies()
    {
        while (Application.isPlaying)
        {
            createEnemy();
            yield return new WaitForSeconds(3);
        }
    }
    void createEnemy()
    {
        Vector2 random = Random.insideUnitCircle.normalized * Random.Range(5, 15);
        Vector3 vector = player.transform.position;
        vector.x += random.x;
        vector.z += random.y;

        Instantiate(enemy, vector, Quaternion.identity);
    }
    
    // Update is called once per frame
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width / 4 - 50, 20, 100, 100), "" + movement.stamina);
        GUI.Label(new Rect(Screen.width / 2 - 50, 20, 100, 100), "" + movement.mana);
        GUI.Label(new Rect(Screen.width / 2 - 50, 100, 100, 100), "" + movement.arrowCount);

        GUI.Label(new Rect(Screen.width / 2, Screen.height/2, 100, 100), "-" );
    }
}
