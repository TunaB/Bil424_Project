using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private movement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = player.GetComponent<movement>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width / 4 - 50, 20, 100, 100), "" + movement.stamina);
        GUI.Label(new Rect(Screen.width / 3 - 50, 20, 100, 100), "" + movement.mana);
        GUI.Label(new Rect(Screen.width / 2, Screen.height/2, 100, 100), "-" );
    }
}
