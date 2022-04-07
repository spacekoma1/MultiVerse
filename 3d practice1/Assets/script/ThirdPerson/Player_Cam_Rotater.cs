using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player_Cam_Rotater
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -80F;
    public float MaximumX = 80F;
    public bool smooth = false;
    public float smoothTime = 5f;
    public bool lockCursor = true;


    private Quaternion m_SubCoordinateTargetRot;
    private Quaternion m_CameraTargetRot;
    private bool m_cursorIsLocked = true;

    public void Init(Transform subcoordinate, Transform camera) {
        m_SubCoordinateTargetRot = subcoordinate.localRotation;
        m_CameraTargetRot = camera.localRotation;
    }


    public void LookRotation(Transform subcoordinate, Transform camera) {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_SubCoordinateTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        if (smooth) {
            subcoordinate.localRotation = Quaternion.Slerp(subcoordinate.localRotation, m_SubCoordinateTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else {
            subcoordinate.localRotation = m_SubCoordinateTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value) {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock() {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()  {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        //어차피 x축회전만 하므로 x값은 sin(theta/2)
        //q.x / q.w = tan(theta/2)
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x / q.w);

        //float angleX = Mathf.Acos(q.w) * 2.0f * Mathf.Rad2Deg; // -값 이 안나오고 절댓값으로 나옴 -> 폐기

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        //x축 회전만 하므로 y = z = 0;
        q = Quaternion.Euler(angleX, 0f, 0f);

        return q;
    }
}
