using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Manager : MonoBehaviour
{
    private static Stage_Manager instance = null;
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

    public static Stage_Manager Instance
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
        Round = 1;
        Boss_Stage_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stage_Load()
    {
        Stage_Level();

        Now_Stage.Stage_Load();
    }

    public void Stage_Level()
    {
        switch(Round)
        {
            case 1:
                Now_Stage = new Stage1();
                break;
        }
    }

    public void Phase2()
    {
        Now_Stage.Phase2();
    }

    public void Phase3()
    {
        Now_Stage.Phase3();
    }

    public void Phase4()
    {
        Now_Stage.Phase4();
    }

    public void Phase5()
    {
        Now_Stage.Phase5();
    }

    public float Get_Tile_X()
    {
        return Now_Stage.Get_Tile_X();
    }

    public float Get_Tile_Y()
    {
        return Now_Stage.Get_Tile_Y();
    }

    private Stage Now_Stage;

    private int Round;

    public bool Boss_Stage_Active;
}
