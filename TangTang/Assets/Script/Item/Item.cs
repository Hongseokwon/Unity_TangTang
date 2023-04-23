using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Item_Info(Item_Manager.ITEM_INFO _Info)
    {
        transform.position = _Info.Pos;
    }

    protected void Item_Move()
    {
        Vector3 Vec = Character_Manager.Instance.Get_Character().transform.position - transform.position;
        float Dis = Vector3.Magnitude(Vec);

        if(Dis<Item_Manager.Instance.Get_Magnet_Dis())
        {
            transform.position += Vec.normalized * Speed * Time.deltaTime;
        }
    }

    public int Index_Num;

    protected Item_Manager.ITEM_INFO Item_Info;

    protected GameManager.ITEM_TYPE My_Type;

    protected float Speed;
}
