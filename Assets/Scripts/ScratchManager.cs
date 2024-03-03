using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchManager : MonoBehaviour
{
    public Transform Player;

    SpriteRenderer spr;

    public bool foundScratch_1;
    public bool foundScratch_2;
    public bool foundScratch_3;
    public bool foundScratch_4;
    public bool foundScratch_5;

    public GameObject Scratch_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(foundScratch_1&& foundScratch_2 && foundScratch_3 && foundScratch_4 && foundScratch_5)
        {
            Scratch_.SetActive(true);
            this.gameObject.SetActive(false);
            Invoke("Scratch_true", 1f);
        }
    }
    void Scratch_true()
    {
        Scratch_.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player.GetComponent<PlayerManager>().Scratch_get != 0)//플레이어 닿음 + 플레이어 스크래치 잡은상태
        {
            switch (Player.GetComponent<PlayerManager>().Scratch_get)
            {
                case 1:
                    transform.GetChild(0).gameObject.GetComponent<SpriteOutline>().outlineSize = 0;
                    spr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
                    spr.color = new Color32(255, 255, 255, 255);
                    foundScratch_1 = true;
                    Player.GetComponent<PlayerManager>().Scratch_get = 0;
                    break;
                case 2:
                    transform.GetChild(1).gameObject.GetComponent<SpriteOutline>().outlineSize = 0;
                    spr = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
                    spr.color = new Color32(255, 255, 255, 255);
                    foundScratch_2 = true;
                    Player.GetComponent<PlayerManager>().Scratch_get = 0;
                    break;
                case 3:
                    transform.GetChild(2).gameObject.GetComponent<SpriteOutline>().outlineSize = 0;
                    spr = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
                    spr.color = new Color32(255, 255, 255, 255);
                    foundScratch_3 = true;
                    Player.GetComponent<PlayerManager>().Scratch_get = 0;
                    break;
                case 4:
                    transform.GetChild(3).gameObject.GetComponent<SpriteOutline>().outlineSize = 0;
                    spr = transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();
                    spr.color = new Color32(255, 255, 255, 255);
                    foundScratch_4 = true;
                    Player.GetComponent<PlayerManager>().Scratch_get = 0;
                    break;
                case 5:
                    transform.GetChild(4).gameObject.GetComponent<SpriteOutline>().outlineSize = 0;
                    spr = transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>();
                    spr.color = new Color32(255, 255, 255, 255);
                    foundScratch_5 = true;
                    Player.GetComponent<PlayerManager>().Scratch_get = 0;
                    break;
                default://??? 이건 무슨 버그세요? (절대 다른 수치가 나올 수 없음)
                    break;
            }
        }
    }
}
