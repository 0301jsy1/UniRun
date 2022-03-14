using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발판으로로서 필요한 동작을 담는 스크립트
public class Platform : MonoBehaviour
{
    //장애물 오브젝트들을 담는 배열
    public GameObject[] obstacles;
    //플레이어 캐릭터가 밟았는지
    private bool stepped = false;
    //새로운 유니티 이벤트 메서드를 확인
    private void OnEnable()
    {
        //Awake()나 Start()와 같은 유니티 이벤트 메서드
        //Start() 메서드 처럼 컴포넌트가 활성화될 때 자동으로 한번 실행되는 메서드
        //처음 한번만 실행되는 Start() 메서드와 달리
        //OnEnable() 메서드는 컴포넌트가 활성화될 때 마다 매번 다시 실행되는 메서드라서
        //컴포넌트를 끄고 다시 켜는 방식으로 재실행할 수 있는 메서드

        //발판을 리셋하는 처리
        //밞힌 상태를 리셋
        stepped = false;
        //장애물의 수만큼 루프
        for(int i = 0; i<obstacles.Length; i++) //Length : 배열의 크기를 가져오는 메소드
        {
            //현재 순번의 장애물을 1/3의 확률로 활성화
            if(Random.Range(0,3)==0) //조건 연산자로 표현하자 => "obstacles[i].SetActive(Random.Range(0, 3) == 0 ? true : false);"
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }

          
        }
    }

    //플레이어 캐릭터가 자신을 밟았을 때 점수를 추가하는 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 상대방의 태그가 Player이고
        //이전에 플레이어 캐릭터가 밟지 않았다면
        if(collision.collider.tag=="Player" && !stepped)
        {
            //점수를 추가하고 밞힌 상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }

}
