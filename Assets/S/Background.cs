using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경이 이동하는 속도 (초당 픽셀)
    [SerializeField]
    private float moveSpeed = 1f;

    // 매 프레임마다 주물
    void Update()
    {
        // 배경 아래로 이동
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        // 만약 y < -10f면 배경을 위로 이동시키고, y축 값만 초기화
        if (transform.position.y < -10f)
        {
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
