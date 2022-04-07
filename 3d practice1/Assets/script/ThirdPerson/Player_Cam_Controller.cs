using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cam_Controller : MonoBehaviour
{
    [SerializeField] private Transform Player_Body;
    [SerializeField] private Camera Player_Cam;
    [SerializeField] private Transform Player_Follower;
    [SerializeField] private Player_Cam_Rotater m_Player_Cam_Rotater;

    void Start() {
        m_Player_Cam_Rotater.Init(Player_Follower, Player_Cam.transform);
    }

    void Update() {
        //3인칭 카메라 회전
        m_Player_Cam_Rotater.LookRotation(Player_Follower, Player_Cam.transform);
        //카메라가 플레이어 바디 따라다님
        Player_Follower.position = Player_Body.position + (Vector3.up * 2);
        
    }
}