using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region staticかつシングルトンにする
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject CameraUI; // カメラのUI
    public Image Mark; // 照準マーク
    public List<GameObject> AnimalList; // 生成したAnimalを格納
    public Vector3 StartPos = new Vector3(18, 3, -40.75f); // スタート座標
    public Vector3 GoalPos = new Vector3(9.3f, 2.3f, -8.1f); // ゴール座標
    public bool IsTaking = false; // カメラを覗いているかどうか
    public bool IsClear = false; // クリアしたかどうか

    // シーンを再ロード
    private void Update()
    {
        if (IsClear && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
