using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Cat : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
        {
            Monster_Move();
        }
    }

    protected override void Monster_Move()
    {
        Target_Pos = Target_Obj.transform.position;
        Move_Vector = Target_Pos - transform.position;
        Move_Vector.Normalize();

        transform.position += Move_Vector * Speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public override void Monster_Dead()
    {
        gameObject.SetActive(false);

        int x = Random.Range(0, 4);

        if(x==0)
        {
            Item_Manager.Instance.Item_Create(GameManager.ITEM_TYPE.ITEM_EXP2, new Item_Manager.ITEM_INFO(transform.position));
        }
        else
        {
            Item_Manager.Instance.Item_Create(GameManager.ITEM_TYPE.ITEM_EXP, new Item_Manager.ITEM_INFO(transform.position));
        }
    }
}

