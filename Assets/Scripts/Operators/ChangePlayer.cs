using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チェンジ前のカメラの回転角が保持されない
public class ChangePlayer : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrfbs;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) && !GameManager.Instance.IsTaking) { Change(0); }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && !GameManager.Instance.IsTaking) { Change(1); }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && !GameManager.Instance.IsTaking) { Change(2); }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !GameManager.Instance.IsTaking) { Change(3); }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !GameManager.Instance.IsTaking) { Change(4); }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && !GameManager.Instance.IsTaking) { Change(5); }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && !GameManager.Instance.IsTaking) { Change(6); }
            else if (Input.GetKeyDown(KeyCode.Alpha7) && !GameManager.Instance.IsTaking) { Change(7); }
        }
    }

    // num番目のプレイヤーにチェンジ
    public void Change(int num)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Quaternion rot = Camera.main.transform.localRotation;

        Destroy(player);
        Instantiate(playerPrfbs[num], playerPos + Vector3.up, Quaternion.identity);
        Camera.main.transform.localRotation = rot;
    }
}