using UnityEngine;

public class Spawner : MonoBehaviour
{
    float delayTime = 5f;
    float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > delayTime)
        {
            var MonsterPool = ObjectPoolManager.instance.objPool.Get();

            if(MonsterPool.GetComponent<Monster>().MType == Define.MonsterType.Eagle)
            {
                MonsterPool.transform.position = new Vector3(9f, 1f, 0);
            }
            else if(MonsterPool.GetComponent<Monster>().MType == Define.MonsterType.Cow)
            {
                MonsterPool.transform.position = new Vector3(9f, -3f, 0);
            }
            

            time = 0;
        }

    }
}
