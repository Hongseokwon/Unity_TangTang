using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Manager : MonoBehaviour
{
    private static Monster_Manager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            Monster_Dic = new Dictionary<int, MONSTER_MANAGER_INFO>();
            Monster_Cnt = new Dictionary<int, int>();
            Monster_Info = new Dictionary<int, GameManager.MONSTER_INFO>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Monster_Manager Instance
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

    public struct MONSTER_MANAGER_INFO
    {
        public MONSTER_MANAGER_INFO(int _Monster_Max)
        {
            Monster_List = new List<GameObject>();
            Monster_Max = _Monster_Max;
        }

        public List<GameObject> Monster_List;

        public int Monster_Max;
    }

    // Start is called before the first frame update
    void Start()
    {
        Monster_Respawn_Time = 3f;
        Monster_Respawn_Delay = 0f;
    }

    public void Monster_Init()
    {
        Monster_Dic.Clear();
    }
    void Update()
    {
        Monster_Respawn_Check();
    }

    public void Monster_Prefab_Create(GameManager.MONSTER_TYPE _Type ,int _Monster_Max)
    {
        Monster_Parent_Obj = GameObject.Find("Monster");

        if(Monster_Dic.TryAdd((int)_Type, new MONSTER_MANAGER_INFO(_Monster_Max)))
        {
            Monster_Cnt.TryAdd((int)_Type, 0);
            for(int i=0; i<_Monster_Max;++i)
            {
                Monster_Dic[(int)_Type].Monster_List.Add(Prefab_Manager.Instance.Monster_Create(_Type, Monster_Parent_Obj));
            }
        }
    }

    private void Monster_Respawn_Check()
    {
        if (Monster_Respawn_Time > Monster_Respawn_Delay)
        {
            Monster_Respawn();
            Monster_Respawn_Time = 0f;
        }
        else
            Monster_Respawn_Time += Time.deltaTime;
    }

    private void Monster_Respawn()
    {
        foreach(KeyValuePair<int,MONSTER_MANAGER_INFO> M_Info in Monster_Dic)
        {
            for (int i = 0; i < Monster_Cnt[M_Info.Key]; ++i)
            {
                if(!M_Info.Value.Monster_List[i].activeSelf)
                {
                    M_Info.Value.Monster_List[i].GetComponent<Monster>().Set_Monster_Info(Monster_Info[M_Info.Key], GameManager.Instance.Get_Random_Monster_Pos(Monster_Dis));
                    M_Info.Value.Monster_List[i].SetActive(true);
                }
            }
        }
    }

    public void Set_Monster_Cnt(GameManager.MONSTER_TYPE _Type, int _Cnt)
    {
        Monster_Cnt[(int)_Type] = _Cnt;
    }

    public void Set_Monster_Info(GameManager.MONSTER_TYPE _Type, GameManager.MONSTER_INFO _Monster_Info)
    {
        if(!Monster_Info.TryAdd((int)_Type, _Monster_Info))
        {
            Monster_Info[(int)_Type] = _Monster_Info;
        }
    }

    public void Set_Monster_Dis(float _Dis)
    {
        Monster_Dis = _Dis;
    }

    public GameObject Get_Close_Monster(Vector3 _Pos)
    {
        float Dis = 10000f;
        GameObject Obj_Temp = null;
        Vector3 Vec_Temp;
        foreach (KeyValuePair<int, MONSTER_MANAGER_INFO> _Monster_Dic in Monster_Dic)
        {
            for(int i=0; i<_Monster_Dic.Value.Monster_Max;++i)
            {
                if(_Monster_Dic.Value.Monster_List[i].activeSelf)
                {
                    Vec_Temp = _Pos - _Monster_Dic.Value.Monster_List[i].transform.position;
                    if (Vec_Temp.sqrMagnitude < Dis)
                    {
                        Dis = Vec_Temp.sqrMagnitude;
                        Obj_Temp = _Monster_Dic.Value.Monster_List[i];
                    }
                }
            }
        }

        return Obj_Temp;
    }

    public void Monster_All_Die()
    {
        foreach (KeyValuePair<int, MONSTER_MANAGER_INFO> _Dic in Monster_Dic)
        {
            foreach (GameObject _Obj in _Dic.Value.Monster_List)
            {
                if (_Obj.activeSelf)
                    _Obj.GetComponent<Monster>().Monster_Dead();
            }
        }
    }

    public void Monster_Obj_Move(Vector3 _Vec)
    {
        foreach (KeyValuePair<int, MONSTER_MANAGER_INFO> _Dic in Monster_Dic)
        {
            foreach (GameObject _Obj in _Dic.Value.Monster_List)
            {
                if (_Obj.activeSelf)
                    _Obj.transform.position += _Vec;
            }
        }
    }

    public void Boss_Monster_Create(GameManager.MONSTER_TYPE _Type , GameManager.MONSTER_INFO _Info)
    {
        Vector3 Temp_Pos = Character_Manager.Instance.Get_Character().transform.position;
        Temp_Pos.y += 2.3f;
        Prefab_Manager.Instance.Boss_Create(_Type, Temp_Pos);
    }


    private Dictionary<int, MONSTER_MANAGER_INFO> Monster_Dic;
    private Dictionary<int, int> Monster_Cnt;
    private Dictionary<int, GameManager.MONSTER_INFO> Monster_Info;

    private float Monster_Respawn_Time;
    private float Monster_Respawn_Delay;

    private GameObject Monster_Parent_Obj;

    private GameManager.MONSTER_INFO Temp_Monster_Info;

    private float Monster_Dis;
}
