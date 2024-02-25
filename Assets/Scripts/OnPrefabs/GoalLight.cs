using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalLight : MonoBehaviour
{
    Material mt;
    int firstA = 100; // 最初のA値
    float showRange = 20; // ゴールの光見せ始める距離
    float goalRange = 2; // ゴール判定になる，ゴールとの距離

    private void Start()
    {
        mt = GetComponent<Renderer>().material;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        float distance = (transform.position - player.transform.position).magnitude;
        if (distance <= goalRange) // プレイヤーがゴールについた
        {
            GameManager.Instance.IsClear = true;
            GameObject.Find("ClearText").GetComponent<TextMeshProUGUI>().enabled = true;
            Destroy(gameObject);
        }
        else if (distance <= showRange) // プレイヤーとの距離に応じて透明度を変化
        {
            float ratio = (distance - showRange) / (goalRange - showRange);
            float a = firstA / 256f * ratio;
            mt.color = new Color(0, 0, 0, a);
        }
        else // プレイヤーと離れすぎているなら非表示
        {
            mt.color = new Color(0, 0, 0, 0);
        }
    }
}
