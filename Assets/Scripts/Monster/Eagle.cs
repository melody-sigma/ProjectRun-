using UnityEngine;

public class Eagle : Monster
{
    public override void Init()
    {
        Type = Define.MonsterType.Eagle;
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            Pool.Release(gameObject);
        }
    }


}