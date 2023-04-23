using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Exp2 : Item
{
    // Start is called before the first frame update
    void Start()
    {
        Exp_Point = 50f;
        Speed = 4f;
        My_Type = GameManager.ITEM_TYPE.ITEM_EXP2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer_Manager.Instance.Active)
            Item_Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Character_Manager.Instance.Get_Character().GetComponent<Character>().Exp_Gain(Exp_Point);

            Item_Manager.Instance.Item_Inactive(My_Type, Index_Num);
            gameObject.SetActive(false);
        }
    }

    private float Exp_Point;
}
