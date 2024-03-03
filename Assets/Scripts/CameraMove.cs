using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; //플레이어 translate 값
    public float speed;
    public Vector3 offset;
    Vector3 vel = Vector3.zero;


    [HideInInspector]
    public float height;
    public float width;

    public Vector2 center;
    public Vector2 size = new Vector2(25f, 50f);

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos() //카메라 최대각도
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);

    }


    void LateUpdate() // Update 후 호출되는 LastUpdate
    {
        offset = transform.position - target.position;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel,0.5f);// 카메라를 target으로 이동 (멀면 빠름 & 가까우면 느려짐)
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

        
    }
}
