using UnityEngine;

public class Cow : Monster
{
    public override void Init()
    {
        Type = Define.MonsterType.Cow;
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            Pool.Release(gameObject);
        }
    }
}

