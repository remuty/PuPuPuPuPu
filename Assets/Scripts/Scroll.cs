using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public GameObject player, base_point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 player_pos = player.transform.position;
        Vector2 base_pos = base_point.transform.position;
        if (player_pos.x > base_pos.x)
        {
            Vector3 pos = transform.position;
            pos.x += 0.07f;
            transform.position = pos;
        }
    }
}
