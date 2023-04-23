using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    private static Item_Manager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Item_Manager Instance
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

    public struct ITEM_INFO
    {
        public ITEM_INFO(Vector3 _Pos)
        {
            Pos = _Pos;
        }

        public Vector3 Pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        Item_Dic = new Dictionary<GameManager.ITEM_TYPE, List<GameObject>>();
        Item_Index = new Dictionary<GameManager.ITEM_TYPE, LinkedList<int>>();
        Item_Magnet_Dis = 0.5f;
        Item_Magnet_Per = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Item_Create(GameManager.ITEM_TYPE _Type, ITEM_INFO _Info)
    {
        int n;
        if (Item_Dic.TryAdd(_Type, new List<GameObject>()))
        {
            n = Item_Dic[_Type].Count;
            Item_Dic[_Type].Add(Prefab_Manager.Instance.Item_Create(_Type, _Info, Parent_Obj, n));
        }
        else
        {
            if (Item_Index.TryAdd(_Type, new LinkedList<int>()))
            {
                n = Item_Dic[_Type].Count;
                Item_Dic[_Type].Add(Prefab_Manager.Instance.Item_Create(_Type, _Info, Parent_Obj, n));
            }
            else if (Item_Index[_Type].Count == 0)
            {
                n = Item_Dic[_Type].Count;
                Item_Dic[_Type].Add(Prefab_Manager.Instance.Item_Create(_Type, _Info, Parent_Obj, n));
            }
            else
            {
                int Index_n = Item_Index[_Type].First.Value;
                Item_Index[_Type].RemoveFirst();
                Item_Dic[_Type][Index_n].GetComponent<Item>().Set_Item_Info(_Info);
                Item_Dic[_Type][Index_n].SetActive(true);
            }
        }
    }

    public void Item_Inactive(GameManager.ITEM_TYPE _Type, int _Index)
    {
        Item_Index.TryAdd(_Type, new LinkedList<int>());
        
        Item_Index[_Type].AddLast(_Index);
    }

    public void Magnet_Dis_Increase()
    {
        Item_Magnet_Per += 0.5f;
    }

    public float Get_Magnet_Dis()
    {
        return Item_Magnet_Per * Item_Magnet_Dis;
    }

    public void Item_Obj_Move(Vector3 _Vec)
    {
        foreach (KeyValuePair<GameManager.ITEM_TYPE, List<GameObject>> _Dic in Item_Dic)
        {
            foreach (GameObject _Obj in _Dic.Value)
            {
                if (_Obj.activeSelf)
                    _Obj.transform.position += _Vec;
            }
        }
    }

    private Dictionary<GameManager.ITEM_TYPE, List<GameObject>> Item_Dic;
    private Dictionary<GameManager.ITEM_TYPE, LinkedList<int>> Item_Index;

    public GameObject Parent_Obj;

    public float Item_Magnet_Dis;
    public float Item_Magnet_Per;
}
