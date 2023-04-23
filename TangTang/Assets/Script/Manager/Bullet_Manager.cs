using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{
    private static Bullet_Manager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            Bullet_Dic = new Dictionary<GameManager.BULLET_TYPE, BULLET_MANAGER_INFO>();
            Bullet_Cnt = new Dictionary<GameManager.BULLET_TYPE, int>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Bullet_Manager Instance
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

    public struct BULLET_MANAGER_INFO
    {
        public BULLET_MANAGER_INFO(int _Bullet_Max)
        {
            Bullet_List = new List<GameObject>();
            Bullet_Max = _Bullet_Max;
        }

        public List<GameObject> Bullet_List;

        public int Bullet_Max;
    }



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bullet_Init()
    {
        Bullet_Dic.Clear();
    }

    public void Bullet_Prefab_Create(GameManager.BULLET_TYPE _Type, int _Bullet_Max)
    {
        Bullet_Parent_Obj = GameObject.Find("Bullet");

        if (Bullet_Dic.TryAdd(_Type, new BULLET_MANAGER_INFO(_Bullet_Max)))
        {
            Bullet_Cnt.TryAdd(_Type, 0);
            for (int i = 0; i < _Bullet_Max; ++i)
            {
                Bullet_Dic[_Type].Bullet_List.Add(Prefab_Manager.Instance.Bullet_Create(_Type,Bullet_Parent_Obj));
            }
        }
    }

    public void Bullet_Normal_Fire(GameManager.BULLET_INFO _Info)
    {
        GameManager.BULLET_TYPE Type = GameManager.BULLET_TYPE.BULLET_NORMAL;

        Bullet_Dic[Type].Bullet_List[Bullet_Cnt[Type]].GetComponent<Bullet>().Fire(_Info);


        Bullet_Cnt[Type]++;
        if (Bullet_Cnt[Type] == Bullet_Dic[Type].Bullet_Max)
            Bullet_Cnt[Type] = 0;
    }

    public void Bullet_Chase_Fire(GameManager.BULLET_INFO _Info)
    {
        GameManager.BULLET_TYPE Type = GameManager.BULLET_TYPE.BULLET_CHASE;

        Bullet_Dic[Type].Bullet_List[Bullet_Cnt[Type]].GetComponent<Bullet>().Fire(_Info);

        Bullet_Cnt[Type]++;
        if (Bullet_Cnt[Type] == Bullet_Dic[Type].Bullet_Max)
            Bullet_Cnt[Type] = 0;
    }
    
    public void Monster_Bullet_Normal_Fire(GameManager.BULLET_INFO _Info)
    {
        GameManager.BULLET_TYPE Type = GameManager.BULLET_TYPE.MONSTER_BULLET_NORMAL;

        Bullet_Dic[Type].Bullet_List[Bullet_Cnt[Type]].GetComponent<Bullet>().Fire(_Info);

        Bullet_Cnt[Type]++;
        if (Bullet_Cnt[Type] == Bullet_Dic[Type].Bullet_Max)
            Bullet_Cnt[Type] = 0;
    }

    public void Bullet_Obj_Move(Vector3 _Vec)
    {
        Debug.Log(_Vec);
        foreach(KeyValuePair<GameManager.BULLET_TYPE,BULLET_MANAGER_INFO> _Dic in Bullet_Dic)
        {
            foreach (GameObject _Obj in _Dic.Value.Bullet_List)
            {
                if (_Obj.activeSelf)
                    _Obj.transform.position += _Vec;
            }
        }
    }


    private Dictionary<GameManager.BULLET_TYPE, BULLET_MANAGER_INFO> Bullet_Dic;
    private Dictionary<GameManager.BULLET_TYPE, int> Bullet_Cnt;

    private GameObject Bullet_Parent_Obj;
}
