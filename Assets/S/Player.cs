using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    int missIndex = 0;
    public GameObject[] missilePrefabs;
    public Transform spPostion;

    [SerializeField]
    private float shootInverval = 0.05f; // 발사 간격
    private float lastshotTime = 0f; // 마지막 발사 시간
    private Animator animator;
    private bool tripleMissileMode = false; // 3발 모드 여부

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        if (horizontalInput < 0)
        {
            animator.Play("Left");
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right");
        }
        else
        {
            animator.Play("Idle");
        }
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time - lastshotTime > shootInverval)
        {
            if (tripleMissileMode)
            {
                // 3발 모드일 때: 플레이어 기준으로 나선(퍼지는) 방향으로 발사
                float[] angles = { 0f, 20f, -20f }; // 가운데, 왼쪽, 오른쪽 (각도 조절 가능)
                foreach (float angle in angles)
                {
                    Instantiate(
                        missilePrefabs[missIndex],
                        spPostion.position,
                        Quaternion.Euler(0, 0, angle)
                    );
                }
            }
            else
            {
                // 일반 발사
                Instantiate(missilePrefabs[missIndex], spPostion.position, Quaternion.identity);
            }
            lastshotTime = Time.time;
        }
    }

    public void MissileUp()
    {
        missIndex++;
        shootInverval = shootInverval - 0.1f;
        if (shootInverval <= 0.1f)
        {
            shootInverval = 0.1f;
        }
        if (missIndex >= missilePrefabs.Length)
        {
            missIndex = missilePrefabs.Length - 1;
        }
    }

    // Player.cs 내부에 추가
    public IEnumerator TripleMissileForSeconds(float seconds)
    {
        EnableTripleMissile(true); // 3발 모드 ON
        yield return new WaitForSeconds(seconds);
        EnableTripleMissile(false); // 3발 모드 OFF
    }

    private void EnableTripleMissile(bool enable)
    {
        tripleMissileMode = enable; // tripleMissileMode는 bool 타입 필드로 선언 필요
    }
}
