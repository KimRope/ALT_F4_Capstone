using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    float currentTime;
    public float createTime;

    public GameObject eggFactory;
    Vector2 pos;
    public float delta_x = 7.0f; // 좌(우)로 이동가능한 (x)최대값
    public float delta_y = 0.0f; // 상(하)로 이동가능한 (y)최대값
    public float speed = 1.0f; //이동속도
    // 최소시간
    float minTime = 1;
    // 최대시간
    float maxTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 Chicken Egg 생성시간을 설정하고
        createTime = UnityEngine.Random.Range(minTime, maxTime);

        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //Chicken 반복운동
        Vector2 v = pos;
        v.x += delta_x * Mathf.Sin(Time.time * speed);
        v.y += delta_y * Mathf.Sin(Time.time * speed);
        transform.position = v;
        if (currentTime > createTime)
        {
            GameObject egg = Instantiate(eggFactory);
            egg.transform.position = transform.position;
            // Chicken Egg 생성한 후 적 생성시간을 다시 설정한다.
            currentTime = 0;

        }


    }

}
