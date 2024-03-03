using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    Vector2 pos;
    public float delta_x = 2.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float delta_y = 0.0f; // ��(��)�� �̵������� (y)�ִ밪
    public float speed = 3.0f; //�̵��ӵ�
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy �ݺ��
        Vector2 v = pos;
        v.x += delta_x * Mathf.Sin(Time.time * speed);
        v.y += delta_y * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("FeetPos"))
        {
            collision.transform.parent.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("FeetPos"))
        {
            collision.transform.parent.SetParent(null);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
