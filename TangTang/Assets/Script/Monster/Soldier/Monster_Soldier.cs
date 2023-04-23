using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Soldier : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        Fire_Ready = false;
        Fire_Delay_Time = 2f;
        Fire_Delay_Count = 0f;
        Attack_Dis = 3f;
        Bullet_Dis = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
        {
            if (Fire_Ready)
                Fire_Delay_Check();
            else
                Monster_Move();
        }
    }

    protected override void Monster_Move()
    {
        Target_Pos = Target_Obj.transform.position;
        Move_Vector = Target_Pos - transform.position;

        if (Move_Vector.magnitude < Attack_Dis)
        {
            Fire_Ready = true;
        }
        else
        {
            Move_Vector.Normalize();

            transform.position += Move_Vector * Speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Fire_Delay_Check()
    {
        Fire_Delay_Count += Time.deltaTime;
        if (Fire_Delay_Count > Fire_Delay_Time)
        {
            Bullet_Fire();
            Fire_Ready = false;
            Fire_Delay_Count = 0f;
        }
    }

    private void Bullet_Fire()
    {
        Bullet_Dir = Target_Obj.transform.position - transform.position;
        Bullet_Dir.Normalize();
        Bullet_Pos = transform.position + (Bullet_Dir * Bullet_Dis);
        Bullet_Manager.Instance.Monster_Bullet_Normal_Fire(new GameManager.BULLET_INFO(Bullet_Pos, Bullet_Dir, Monster_Info.Obj_Info.Att));
    }

    public override void Monster_Dead()
    {
        gameObject.SetActive(false);

        int x = Random.Range(0, 4);

        if (x == 0)
        {
            Item_Manager.Instance.Item_Create(GameManager.ITEM_TYPE.ITEM_EXP, new Item_Manager.ITEM_INFO(transform.position));
        }
        else
        {
            Item_Manager.Instance.Item_Create(GameManager.ITEM_TYPE.ITEM_EXP2, new Item_Manager.ITEM_INFO(transform.position));
        }
    }

    private bool Fire_Ready;
    private float Fire_Delay_Time;
    private float Fire_Delay_Count;
    private float Attack_Dis;
    private float Bullet_Dis;
    private Vector3 Bullet_Dir;
    private Vector3 Bullet_Pos;
}
