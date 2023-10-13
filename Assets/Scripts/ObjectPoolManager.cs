using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;

    public IObjectPool<GameObject> objPool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        Init();
    }

    private void Init()
    {
        objPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        // 미리 오브젝트 생성 해놓기
        for (int i = 0; i < defaultCapacity; i++)
        {
            Monster monster = CreatePooledItem().GetComponent<Monster>();
            monster.Pool.Release(monster.gameObject);
            Debug.Log("몬스터생성");
        }
    }

    // 생성
    private GameObject CreatePooledItem()
    {
        int i = Random.Range(1, 3);
        Debug.Log((Define.MonsterType)i);
        GameObject poolGo = Instantiate(Resources.Load("Prefabs/Obstacle/" + (Define.MonsterType)i) as GameObject);
        if((Define.MonsterType)i == Define.MonsterType.Eagle)
        {
            poolGo.GetComponent<Monster>().MType = Define.MonsterType.Eagle;
        }
        else 
        {
            poolGo.GetComponent<Monster>().MType = Define.MonsterType.Cow;
        }
        poolGo.GetComponent<Monster>().Pool = objPool;
        Debug.Log("1");
        return poolGo;
    }

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}




