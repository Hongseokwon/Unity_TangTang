using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Bullet_Move() { }

    public virtual void Fire(GameManager.BULLET_INFO _Info) { }

    protected int Att;
    protected Vector3 Move_Vector;
    protected float Speed;
}
