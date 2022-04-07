using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Body_Contoller : MonoBehaviour
{
    [SerializeField] private Transform Player_Follower;

    public float Player_Speed = 3.0f;
    
    private CharacterController controller;
    private Vector3 MoveDir;


    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //인풋처리
        MoveDir = transform.TransformDirection(MoveDir); //로컬에서 월드로 변환
        MoveDir *= Player_Speed; //스피드만큼 움직이기

        //움직일 때는 카메라 방향을 보고 움직이기
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            transform.localRotation = Player_Follower.localRotation;
        }

        
    }

    void FixedUpdate()
    {
        controller.Move(MoveDir * Time.fixedDeltaTime);
    }
}
