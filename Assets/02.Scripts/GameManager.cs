using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//게임 오버 상태를 표현하고 게임 점수와 UI를 관리하는 매니저
//씬에는 단 하난의 게임 매니저만 존재할 수 있음
public class GameManager : MonoBehaviour
{
    public static GameManager instance;//싱글턴을 할당할 전역변수
    public bool isGameover = false;//게임오버 상태
    public Text scoreText;//점수를 출력할 UI 텍스트
    public GameObject gameoverUI;//게임오버시활성화할 UI 오브젝트
    private int score = 0;//게임 점수

    public GameObject menuPanel;

    public int hpCount = 3; //사용자 생명력
    public Text hpText; //사용자에게 보여질 Text

    //게임 시작과 동시에 싱글턴을 구성
    private void Awake()
    {
        //싱글턴 변수 instance가 비어 있나요?
        if(instance == null)
        {
            instance = this; //인스턴스가 비어 있다면 그곳에 내 자신을 할당
        }
        else
        {
            //인스턴스에 이미 다른 GameManager 오브젝트가 존재한다는 의미....
            //싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.Log("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //사용자에게 보여질 생명력을 실제 생명력으로 등록
        hpText.text = hpCount.ToString(); //ToString 문자로 형변환
    }

    // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
    void Update()
    {
        //게임오버 상태에서 마우스 왼쪽 버튼을 클릭 한다면...
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            //SceneManager.LoadScene(0);
            //SceneManager.LoadScene("Main"); ->HardCoding
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//현재 활성화된 씬의 이름을 가져와라. ->SoftCoding
        }
    }

    //점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        //게임오버가 아니라면
        if(!isGameover)
        {
            //점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score;
        }
        /*
         * if(isGameover)
         *    return
         *  score += newScore;
            scoreText.text = "Score : " + score;
        */
    }

    //플레이어 캐릭터가 사망 시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        //현재 상태를 게임오버 상태로 변경
        isGameover = true;
        gameoverUI.SetActive(true);//게임오버시 활성화할 UI 오브젝트
    }
    /*
    public void OnMenu()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffMenu()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//현재 활성화된 씬의 이름을 가져와라. ->SoftCoding
        Time.timeScale = 1f;
    }
    */

    public void MenuPanelControl(bool isActive)
    {
        menuPanel.SetActive(isActive);
    }
    
    public void UIControl(string type)
    {
        switch(type)
        {
            case "menuon":
                menuPanel.SetActive(true);
                Time.timeScale = 0f;
                break;
            case "menuoff":
                menuPanel.SetActive(false);
                Time.timeScale = 1f;
                PlayerController.playerRigidbody.velocity = Vector2.zero;//속도를 제로(0,0)로 변경
                break;
            case "exit":
                Application.Quit();
                break;
            case "restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);//현재 활성화된 씬의 이름을 가져와라. ->SoftCoding
                Time.timeScale = 1f;
                break;
        }
    }

    public bool Crash()
    {
        //hpCount--;
        //hpText.text = hpCount.ToString();
        hpText.text = "" + --hpCount; //"" + --hpCount : 자동 형변환
        if (hpCount <= 0) return true;//조건문 아래있으면 반환되지 않음
        return false;
    }
}
