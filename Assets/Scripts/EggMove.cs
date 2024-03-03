using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMove : MonoBehaviour
{
    //�ʿ�Ӽ� : �̵��ӵ�
    public float speed = 2;

    void Update()
    {
        // 1. ������ ���Ѵ�.
        Vector3 dir = Vector3.down;
        // 2. �̵��ϰ� �ʹ�. ���� P = P0 + vt
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


