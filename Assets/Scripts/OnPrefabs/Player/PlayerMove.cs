using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// プレイヤーの大きさに関わらず，カメラの相対位置が固定（特に，鳥が小さくなる）
// ゴールしたら，カメラを写真の角度に向けたい
// 6,7が，移動した後特定の方向を向く
// 走りながら回転すると，レンダリングがぶれる
// ジャンプの頂点で，「速度が0」判定になってしまわないか？
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpPower = 5;
    [SerializeField] bool isFryable = false;
    [SerializeField] bool isPlayer4 = false;
    [SerializeField] bool isPlayer5 = false;
    [SerializeField] float player5SpeedMul = 3;
    [SerializeField] float rotX = 0; // 向きを変える際の，オイラー角xの固定値
    [SerializeField] bool isRotYAdjust = false; // 向きを変える際の，オイラー角yを+90するかどうか
    // スタート位置に強制的に戻す，境界の座標(x,y,z : 最小，最大)
    Vector2[] limit = { new Vector2(-60, 80), new Vector2(-10, 100), new Vector2(-65,65) };
    Rigidbody rb;
    int jumpCount = 0;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // limitの判定
        Vector3 p = transform.position;
        if (p.x < limit[0].x || limit[0].y < p.x)
        {
            transform.position = GameManager.Instance.StartPos;
        }
        else if(p.y < limit[1].x || limit[1].y < p.y)
        {
            transform.position = GameManager.Instance.StartPos;
        }
        else if (p.z < limit[2].x || limit[2].y < p.z)
        {
            transform.position = GameManager.Instance.StartPos;
        }

        // ワープボタンの判定
        if (Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.Return) && !GameManager.Instance.IsTaking)
            {
                transform.position = GameManager.Instance.GoalPos;
            }
            else if (Input.GetKeyDown(KeyCode.Backspace) && !GameManager.Instance.IsTaking)
            {
                transform.position = GameManager.Instance.StartPos;
            }
        }

        // 2段ジャンプのカウントをリセット
        if (jumpCount >= 2 && Mathf.Abs(rb.velocity.y) <= 0.00001f)
        {
            jumpCount = 0;
        }

        // 飛べる場合と飛べない場合で判定を分ける
        if (!isFryable && !GameManager.Instance.IsTaking)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            Vector3 mov = new Vector3(inputX, 0, inputZ);

            if (mov != Vector3.zero)
            {
                // 向きを変える
                foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                {
                    Vector3 cameraAngles = Camera.main.transform.localEulerAngles;
                    cameraAngles.x = rotX;
                    if (isPlayer5 && Input.GetKey(KeyCode.LeftShift)) { cameraAngles.x += 90; } // 腹ばいモードになる
                    if (isRotYAdjust) { cameraAngles.y += 90; }
                    playerBody.transform.localEulerAngles = cameraAngles;
                }
            }
            else if (isPlayer5)
            {
                // 向きを変える
                foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                {
                    Vector3 playerAngles = playerBody.transform.localEulerAngles;
                    playerAngles.x = rotX;
                    if (Input.GetKey(KeyCode.LeftShift)) { playerAngles.x += 90; } // 腹ばいモードになる
                    playerBody.transform.localEulerAngles = playerAngles;
                }
            }

            Vector3 pos = transform.position;
            Vector3 MOV = Camera.main.transform.localRotation * mov;
            MOV.y = 0;
            MOV = MOV.normalized * speed;
            if (isPlayer5 && Input.GetKey(KeyCode.LeftShift)) { MOV *= player5SpeedMul; }
            rb.MovePosition(pos + MOV * Time.deltaTime);

            if (isPlayer4)
            {
                // 鶏は2段ジャンプできる
                if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
                {
                    jumpCount += 1;
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 0.00001f)
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
            }
        }
        else if (isFryable && !GameManager.Instance.IsTaking)
        {
            float inputX = 0;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { inputX += 1; }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { inputX -= 1; }

            float inputZ = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { inputZ += 1; }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { inputZ -= 1; }

            Vector3 mov = new Vector3(inputX, 0, inputZ);

            if (mov != Vector3.zero)
            {
                // 向きを変える
                foreach (GameObject playerBody in GameObject.FindGameObjectsWithTag("PlayerBody"))
                {
                    Vector3 cameraAngles = Camera.main.transform.localEulerAngles;
                    cameraAngles.x = rotX;
                    if (isRotYAdjust) { cameraAngles.y += 90; }
                    playerBody.transform.localEulerAngles = cameraAngles;
                }
            }

            Vector3 pos = transform.position;
            Vector3 MOV = Camera.main.transform.localRotation * mov;
            MOV.y = 0;
            MOV = MOV.normalized * speed;

            float inputY = 0;
            if (Input.GetKey(KeyCode.Space)) { inputY += 1; }
            if (Input.GetKey(KeyCode.LeftShift)) { inputY -= 1; }

            rb.MovePosition(pos + MOV * Time.deltaTime + Vector3.up * inputY * (jumpPower * Time.deltaTime));
        }
    }
}