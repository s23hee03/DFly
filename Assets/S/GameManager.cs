using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    // 현재 코인 개수
    public int coin=0;

    // 코인 개수를 표시할 텍스트
    public TextMeshProUGUI textMeshProCoin;

    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // 싱글톤 인스턴스가 없으면 자신을 할당
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 중복 파괴
        }
    }

    // 게임 오버 UI 등 추가 가능
    public void GameOver()
    {
        Debug.Log("게임 오버!");
        // 게임 오버 UI 활성화 등 추가 구현 가능
        Time.timeScale = 0f; // 게임 정지
    }

    // 게임 클리어 UI 등 추가 가능
    public void GameClear()
    {
        Debug.Log("게임 클리어!");
        // 게임 클리어 UI 활성화 등 추가 구현 가능
        Time.timeScale = 0f; // 게임 정지
    }

    // 코인 개수를 증가시키고 UI에 표시, 2개마다 미사일 업그레이드, 10개마다 3발 2초 업그레이드
    public void ShowCoinCount()
    {
        coin++;
        textMeshProCoin.SetText(coin.ToString()); // 코인 개수 UI 갱신

        // 코인이 2의 배수일 때마다 미사일 업그레이드
        if (coin % 2 == 0)
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.MissileUp(); // 2개마다 미사일 업그레이드
            }
        }

        // 코인이 10의 배수일 때마다 3발 미사일 2초간 발사
        if (coin % 10 == 0)
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.StartCoroutine(player.TripleMissileForSeconds(2f));
            }
        }

        // 코인 100개를 먹었을 때 게임 클리어
        if (coin >= 100)
        {
            GameClear();
        }
    }
}
