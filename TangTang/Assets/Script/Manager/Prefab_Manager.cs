using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Manager : MonoBehaviour
{
    private static Prefab_Manager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Prefab_Manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Character_Create(GameManager.CHARACTER_TYPE _Type)
    {
        Character_Temp = Get_Character_Type_Obj(_Type);

        return Instantiate(Character_Temp);
    }

    private GameObject Get_Character_Type_Obj(GameManager.CHARACTER_TYPE _Type)
    {
        switch (_Type)
        {
            case GameManager.CHARACTER_TYPE.CHARACTER_CAT:
                return Character_Cat;
        }

        return null;
    }

    public GameObject Monster_Create(GameManager.MONSTER_TYPE _Type, GameObject _Parent)
    {
        Monster_Temp = Get_Monster_Type_Obj(_Type);

        return Instantiate(Monster_Temp, _Parent.transform);
    }

    public GameObject Boss_Create(GameManager.MONSTER_TYPE _Type , Vector3 _Pos)
    {
        Monster_Temp = Get_Monster_Type_Obj(_Type);

        return Instantiate(Monster_Temp, _Pos, Quaternion.Euler(0f, 0f, 0f));
    }

    private GameObject Get_Monster_Type_Obj(GameManager.MONSTER_TYPE _Type)
    {
        switch (_Type)
        {
            case GameManager.MONSTER_TYPE.MONSTER_CAT:
                return Monster_Cat;
            case GameManager.MONSTER_TYPE.MONSTER_SOLDIER:
                return Monster_Soldier;
            case GameManager.MONSTER_TYPE.MONSTER_BOSS1:
                return Monster_Boss1;
        }

        return null;
    }

    public GameObject Bullet_Create(GameManager.BULLET_TYPE _Type, GameObject _Parent)
    {
        Bullet_Temp = Get_Bullet_Type_Obj(_Type);

        return Instantiate(Bullet_Temp, _Parent.transform);
    }

    private GameObject Get_Bullet_Type_Obj(GameManager.BULLET_TYPE _Type)
    {
        switch (_Type)
        {
            case GameManager.BULLET_TYPE.BULLET_NORMAL:
                return Bullet_Normal;
            case GameManager.BULLET_TYPE.BULLET_CHASE:
                return Bullet_Chase;
            case GameManager.BULLET_TYPE.MONSTER_BULLET_NORMAL:
                return Monster_Bullet_Normal;
        }

        return null;
    }

    public GameObject Item_Create(GameManager.ITEM_TYPE _Type , Item_Manager.ITEM_INFO _Info ,GameObject _Parent, int _Index_Num)
    {
        Item_Temp = Get_Item_Type_Obj(_Type);
        Item_Temp.GetComponent<Item>().Index_Num = _Index_Num;
        Item_Temp.GetComponent<Item>().Set_Item_Info(_Info);

        return Instantiate(Item_Temp, _Parent.transform);
    }

    private GameObject Get_Item_Type_Obj(GameManager.ITEM_TYPE _Type)
    {
        switch (_Type)
        {
            case GameManager.ITEM_TYPE.ITEM_EXP:
                return Item_Exp;
            case GameManager.ITEM_TYPE.ITEM_EXP2:
                return Item_Exp2;
        }

        return null;
    }


    private GameObject Character_Temp;
    private GameObject Monster_Temp;
    private GameObject Bullet_Temp;
    private GameObject Item_Temp;

    public GameObject Character_Cat;

    public GameObject Monster_Cat;
    public GameObject Monster_Soldier;
    public GameObject Monster_Boss1;

    public GameObject Bullet_Normal;
    public GameObject Bullet_Chase;

    public GameObject Monster_Bullet_Normal;

    public GameObject Item_Exp;
    public GameObject Item_Exp2;
}
