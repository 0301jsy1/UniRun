using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 오브젝트를 지속적으로 왼쪽으로 움직이는 스크립트
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; //이동속도      
    void Start()
    {
        Debug.Log("Chosy");
    }

    void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            //초당 speed의 속도로 왼쪽 방향으로 평행이동 구현
            transform.Translate(Vector3.left * speed * Time.deltaTime);  //transform.Translate : 위치에 따른 평행 이동 메서드 -> 방향 Vector3 값으로 받음
        }
        

                                                  
    }
}
