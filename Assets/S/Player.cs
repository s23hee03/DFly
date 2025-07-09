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
            Instantiate(missilePrefabs[missIndex], spPostion.position, Quaternion.identity);
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
}
