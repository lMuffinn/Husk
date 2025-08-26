using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject GameData;
    Transform position;
    // Start is called before the first frame update
    void Start()
    {
        GameData = GameObject.FindGameObjectWithTag("GameData");
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        int floor = GameData.GetComponent<GameData>().floor;
        position.position = new Vector3(0, (floor-2)*40, -10);
    }
}
