using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Bullet_Normal : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        Speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
            Bullet_Move();
    }

    public override void Bullet_Move()
    {
        transform.position += Move_Vector * Speed * Time.deltaTime;
    }

    public override void Fire(GameManager.BULLET_INFO _Info)
    {
        transform.position = _Info.Pos;
        Att = _Info.Att;
        Move_Vector = _Info.Dir;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * (Mathf.Atan2(Move_Vector.y, Move_Vector.x)));

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Character_Manager.Instance.Get_Character().GetComponent<Character>().Hp_Down(Att);
            gameObject.SetActive(false);
        }
    }
}
