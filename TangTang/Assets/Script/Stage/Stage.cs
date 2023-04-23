using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public virtual void Stage_Load() { }
    public virtual void Character_Load() { }

    public virtual void Phase1() { }
    public virtual void Phase2() { }
    public virtual void Phase3() { }
    public virtual void Phase4() { }
    public virtual void Phase5() { }

    public float Get_Tile_X() { return Tile_X; }
    public float Get_Tile_Y() { return Tile_Y; }

    protected GameManager.MONSTER_INFO Monster_Info_Temp;
    protected GameManager.OBJ_INFO Obj_Info_Temp;
    protected Vector3 Pos_Temp;

    protected float Tile_X;
    protected float Tile_Y;
}
