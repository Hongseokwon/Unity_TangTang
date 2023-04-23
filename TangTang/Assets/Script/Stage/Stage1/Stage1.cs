using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public Stage1()
    {
        Tile_X = 10f;
        Tile_Y = 7.63f;
    }

    public override void Stage_Load()
    {
        Character_Load();
        Phase1();
    }

    public override void Character_Load()
    {
        Character_Manager.Instance.Character_Load();
        Character_Manager.Instance.Bullet_Load();
    }

    public override void Phase1()
    {
        Monster_Manager.Instance.Monster_Prefab_Create(GameManager.MONSTER_TYPE.MONSTER_CAT, 30);
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_CAT, 10);
        Monster_Manager.Instance.Set_Monster_Info(GameManager.MONSTER_TYPE.MONSTER_CAT, new GameManager.MONSTER_INFO(new GameManager.OBJ_INFO(30, 10, 0)));
        Monster_Manager.Instance.Set_Monster_Dis(6f);
    }

    public override void Phase2()
    {
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_CAT, 30);
        Monster_Manager.Instance.Set_Monster_Info(GameManager.MONSTER_TYPE.MONSTER_CAT, new GameManager.MONSTER_INFO(new GameManager.OBJ_INFO(50, 15, 0)));
    }

    public override void Phase3()
    {
        Monster_Manager.Instance.Monster_Prefab_Create(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, 30);
        Bullet_Manager.Instance.Bullet_Prefab_Create(GameManager.BULLET_TYPE.MONSTER_BULLET_NORMAL, 300);
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, 10);
        Monster_Manager.Instance.Set_Monster_Info(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, new GameManager.MONSTER_INFO(new GameManager.OBJ_INFO(20, 5, 0)));
        Monster_Manager.Instance.Set_Monster_Dis(7f);
    }
    public override void Phase4()
    {
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, 30);
        Monster_Manager.Instance.Set_Monster_Info(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, new GameManager.MONSTER_INFO(new GameManager.OBJ_INFO(30, 10, 0)));
    }

    public override void Phase5()
    {
        Stage_Manager.Instance.Boss_Stage_Active = true;
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_CAT, 0);
        Monster_Manager.Instance.Set_Monster_Cnt(GameManager.MONSTER_TYPE.MONSTER_SOLDIER, 0);
        Monster_Manager.Instance.Monster_All_Die();
        Monster_Manager.Instance.Boss_Monster_Create(GameManager.MONSTER_TYPE.MONSTER_BOSS1, new GameManager.MONSTER_INFO(new GameManager.OBJ_INFO(1000, 30, 10)));
        Character_Manager.Instance.Get_Character().GetComponent<Character>().Set_Boss_Start();
    }
}
