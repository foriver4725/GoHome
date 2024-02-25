using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シーンカメラは何も見てはいけない
public class IsAnimalLookable : MonoBehaviour
{
    public bool isLookable { get; set; } = false; // カメラに映っているかどうか

    // このAnimalがゲームカメラに映っている時（障害物関係無し）だけ，フラグをオンにする。
    private void OnBecameVisible()
    {
        isLookable = true;
    }
    private void OnBecameInvisible()
    {
        isLookable = false;
    }
}
