using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] animalPrfbs;
    [Header("スポーンxzの限界\n(X,Z : 最小，最大)")] [SerializeField] Vector2[] spawnXZLimit;
    [Header("スポーンy")][SerializeField] float spawnY;
    int spawnNum = 10; // 1種類当たり何体スポーンさせるか

    void Start()
    {
        // 生成し，配列に格納

        GameManager.Instance.AnimalList = new List<GameObject>();

        foreach (GameObject animal in animalPrfbs)
        {
            for (int i = 0; i < spawnNum; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(spawnXZLimit[0].x, spawnXZLimit[0].y), spawnY, Random.Range(spawnXZLimit[1].x, spawnXZLimit[1].y));
                GameManager.Instance.AnimalList.Add(Instantiate(animal, spawnPos, Quaternion.identity));
            }
        }
    }
}
