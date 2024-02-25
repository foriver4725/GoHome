using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 照準がAnimalの足元になる
// 障害物に隠れているAnimalも，カメラに映っている判定になる
public class PlayerCamera : MonoBehaviour
{
    float cameraMaxDistance = 10; // カメラからどこまで離れたオブジェクトを判定するか 
    List<GameObject> playerBodyList;
    int changeNum = 0;

    private void Start()
    {
        playerBodyList = new List<GameObject>();
    }

    void Update()
    {
        // マウス右クリックでカメラを覗く
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.IsTaking = true;
            foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
            {
                playerBodyList.Add(playerBody);
                playerBody.SetActive(false);
            }
            GetComponentInChildren<CinemachineUserInputZoom>()._scrollDelta = 100;
            GameManager.Instance.CameraUI.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            GameManager.Instance.IsTaking = false;
            foreach (GameObject playerBody in playerBodyList)
            {
                playerBody.SetActive(true);
            }
            playerBodyList = new List<GameObject>();
            GetComponentInChildren<CinemachineUserInputZoom>()._scrollDelta = -100;
            GameManager.Instance.CameraUI.SetActive(false);
        }

        // カメラを覗いている間
        if (GameManager.Instance.IsTaking)
        {
            // 最近接のAnimalを探査(カメラに映っているもの/*，かつそのAnimalとメインカメラの間に障害物がないとき*/だけ)
            GameObject nearestAnimal = null;
            float distance = 10000;
            foreach (GameObject animal in GameManager.Instance.AnimalList)
            {
                if (animal.GetComponent<IsAnimalLookable>().isLookable)
                {
                    float distance_ = (animal.transform.position - transform.position).magnitude;
                    if (distance_ < distance)
                    {
                        nearestAnimal = animal;
                        distance = distance_;
                    }

                    /*
                    RaycastHit[] _raycastHits = new RaycastHit[100];
                    Vector3 positionDiff = animal.transform.position - transform.position; // 自身とプレイヤーの座標差分を計算
                    float d = positionDiff.magnitude; // プレイヤーとの距離を計算
                    Vector3 direction = positionDiff.normalized; // プレイヤーへの方向ベクトルを正規化
                    // _raycastHitsに、ヒットしたColliderや座標情報などが格納される
                    int hitCount = Physics.RaycastNonAlloc(animal.transform.position, direction, _raycastHits, d);

                    if (hitCount == 1)
                    {
                        float distance_ = (animal.transform.position - transform.position).magnitude;
                        if (distance_ < distance)
                        {
                            nearestAnimal = animal;
                            distance = distance_;
                        }
                    }
                    */
                }
            }

            if (distance <= cameraMaxDistance)
            {
                // 親UIのRectTransformを保持
                RectTransform parentUI = GameManager.Instance.Mark.transform.parent.GetComponent<RectTransform>();

                // オブジェクトのワールド座標→スクリーン座標変換
                Vector3 screenPos = Camera.main.WorldToScreenPoint(nearestAnimal.transform.position);

                // スクリーン座標変換→UIローカル座標変換
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentUI, screenPos, null, out Vector2 uiLocalPos);

                // RectTransformのローカル座標を更新
                GameManager.Instance.Mark.enabled = true;
                GameManager.Instance.Mark.transform.localPosition = uiLocalPos;

                // カメラを覗いている間，左クリックが押されたら，プレイヤーチェンジのフラグを立てる
                if (Input.GetMouseButtonDown(0))
                {
                    if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal1")
                    {
                        changeNum = 1;
                    }
                    else if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal2")
                    {
                        changeNum = 2;
                    }
                    else if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal3")
                    {
                        changeNum = 3;
                    }
                    else if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal4")
                    {
                        changeNum = 4;
                    }
                    else if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal5")
                    {
                        changeNum = 5;
                    }
                    else if (nearestAnimal.transform.GetChild(0).gameObject.tag == "Animal6")
                    {
                        changeNum = 6;
                    }
                    else
                    {
                        changeNum = 7;
                    }
                }
            }
            else
            {
                GameManager.Instance.Mark.enabled = false;
            }
        }
        else
        {
            GameManager.Instance.Mark.enabled = false;
        }

        // カメラの覗き込みがやめられた時，フラグが立っていればプレイヤーチェンジ
        if (!GameManager.Instance.IsTaking)
        {
            if (changeNum == 1)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(1);
            }
            else if (changeNum == 2)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(2);
            }
            else if (changeNum == 3)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(3);
            }
            else if (changeNum == 4)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(4);
            }
            else if (changeNum == 5)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(5);
            }
            else if (changeNum == 6)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(6);
            }
            else if (changeNum == 7)
            {
                changeNum = 0;
                ChangePlayer changePlayer = GameObject.FindGameObjectsWithTag("ChangePlayer")[0].GetComponent<ChangePlayer>();
                changePlayer.Change(7);
            }
        }
    }
}
