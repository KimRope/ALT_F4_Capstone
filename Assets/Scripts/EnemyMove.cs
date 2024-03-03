using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Vector2 pos;
    public float delta_x = 2.0f; // 좌(우)로 이동가능한 (x)최대값
    public float delta_y = 0.0f; // 상(하)로 이동가능한 (y)최대값
    public float speed = 3.0f; //이동속도
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy 반복운동
        Vector2 v = pos;
        v.x += delta_x * Mathf.Sin(Time.time * speed);
        v.y += delta_y * Mathf.Sin(Time.time * speed);
        transform.position = v;
        
    }
    
}
