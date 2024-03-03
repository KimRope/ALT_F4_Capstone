using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMove : MonoBehaviour
{
    //필요속성 : 이동속도
    public float speed = 2;

    void Update()
    {
        // 1. 방향을 구한다.
        Vector3 dir = Vector3.down;
        // 2. 이동하고 싶다. 공식 P = P0 + vt
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EggBroken")
        {
            Destroy(gameObject);
        }
    }
}


