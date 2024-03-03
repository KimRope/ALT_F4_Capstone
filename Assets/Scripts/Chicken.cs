using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    float currentTime;
    public float createTime;

    public GameObject eggFactory;
    Vector2 pos;
    public float delta_x = 7.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float delta_y = 0.0f; // ��(��)�� �̵������� (y)�ִ밪
    public float speed = 1.0f; //�̵��ӵ�
    // �ּҽð�
    float minTime = 1;
    // �ִ�ð�
    float maxTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        // �¾ �� Chicken Egg �����ð��� �����ϰ�
        createTime = UnityEngine.Random.Range(minTime, maxTime);

        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //Chicken �ݺ��
        Vector2 v = pos;
        v.x += delta_x * Mathf.Sin(Time.time * speed);
        v.y += delta_y * Mathf.Sin(Time.time * speed);
        transform.position = v;
        if (currentTime > createTime)
        {
            GameObject egg = Instantiate(eggFactory);
            egg.transform.position = transform.position;
            // Chicken Egg ������ �� �� �����ð��� �ٽ� �����Ѵ�.
            currentTime = 0;

        }


    }

}
