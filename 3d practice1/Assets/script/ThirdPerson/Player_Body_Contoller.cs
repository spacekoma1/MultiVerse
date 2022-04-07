using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Body_Contoller : MonoBehaviour
{
    [SerializeField] private Transform Player_Follower;

    public float Player_WalkSpeed = 3.0f;
    public float Player_RunSpeed = 7.0f;

    private Animator player_animator;
    private CharacterController player_controller;
    private Vector3 MoveDir;

    void Awake() {
        player_animator = GetComponent<Animator>();
    }

    void Start() {
        player_controller = GetComponent<CharacterController>();
    }

    void Update() {
        //인풋처리
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        MoveDir = new Vector3(inputX, 0, inputZ);
        MoveDir = transform.TransformDirection(MoveDir); //로컬에서 월드로 변환
        if (Input.GetButton("Left Shift"))  {//뛰는지 걷는지 판단해서 각 스피드 곱하기
            MoveDir *= Player_RunSpeed;
            player_animator.SetBool("IsRun", true); //Run 스테이트로 넘어가기(애니메이션)
        }
        else {
            MoveDir *= Player_WalkSpeed;
            player_animator.SetBool("IsRun", false); //Run 스테이트 끝
        }

        //움직이는 키 입력시
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            //카메라 방향을 보기
            transform.localRotation = Player_Follower.localRotation;
            //Walk 스테이트로 넘어가기 (애니메이션)
            player_animator.SetBool("IsWalk", true);
            player_animator.SetFloat("WalkDir_Ver", inputZ);
        }
        else {
            player_animator.SetBool("IsWalk", false);
        }

        
    }

    void FixedUpdate()
    {
        player_controller.Move(MoveDir * Time.fixedDeltaTime);
    }
}
