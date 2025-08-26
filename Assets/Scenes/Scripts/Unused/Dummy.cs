using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    public int enemyFloor = 2;
    public int playerFloor = 2;
    public GameObject enemy;
    public GameObject gameData;
    public GameObject player;
    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyFloor = enemy.GetComponent<TargetController>().floor;
        playerFloor = gameData.GetComponent<GameData>().floor;
       // Debug.Log(enemyFloor == playerFloor);
        if (enemyFloor == playerFloor) {tf.position = player.GetComponent<Transform>().position; /*Debug.Log("on same floor");*/ }
    }
}
