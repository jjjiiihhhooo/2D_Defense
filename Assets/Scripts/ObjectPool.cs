using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;


    private Dictionary<string, Queue<GameObject>> pool_Dic;

    private int count;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        pool_Dic = new Dictionary<string, Queue<GameObject>>();

        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            pool_Dic.Add(objectPrefabs[i].name, new Queue<GameObject>());

        }

        Enqueue();
    }



    public GameObject Dequeue(string name)
    {
        if (pool_Dic[name].Count <= 0)
            Enqueue();


        return pool_Dic[name].Dequeue();
    }

    public void Enqueue()
    {
        for (int i = 0; i < pool_Dic.Count; i++)
        {
            if (pool_Dic[objectPrefabs[i].name].Count <= 0)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject temp = Instantiate(objectPrefabs[i], this.transform);

                    temp.SetActive(false);

                    pool_Dic[objectPrefabs[i].name].Enqueue(temp);
                }
            }
        }
    }

    public void Return(GameObject temp)
    {
        temp.SetActive(false);
        temp.transform.parent = this.gameObject.transform;
        string name = temp.name;

        string replaceName = name.Replace("(Clone)", "");

        pool_Dic[replaceName].Enqueue(temp);
    }
}
