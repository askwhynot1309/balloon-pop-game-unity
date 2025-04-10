﻿using UnityEngine;
using UnityEngine.Events;

public class AstraInputController : MonoBehaviour
{
    public System.Action<bool, Vector3> onDetectBody;
    public UnityEvent OnClickEvent = new UnityEvent();

    private Astra.Body[] _bodies = new Astra.Body[Astra.BodyFrame.MaxBodies];

    private float clickThreshold = 0.1f;
    private float lastClickTime = 0f;
    private float clickCooldown = 1f;

    private bool isHandInitialized = false;
    private float baseZ = 0f;

    private float firstFrameTime = -1f;
    private float delayBeforeCaptureZ = 2f; 

    private Vector3 currentHandPos;

    public void OnNewFrame(Astra.BodyStream bodyStream, Astra.BodyFrame frame)
    {
        if (frame.Width == 0 || frame.Height == 0) return;

        frame.CopyBodyData(ref _bodies);

        if (_bodies != null && _bodies.Length > 0 && _bodies[0] != null && _bodies[0].Joints != null)
        {
            Vector3 newHandPos = GetJointWorldPos(_bodies[0].Joints[(int)Astra.JointType.RightHand]);
            currentHandPos = newHandPos;

            onDetectBody?.Invoke(true, newHandPos);

            // Lưu thời gian bắt đầu đọc frame đầu tiên
            if (firstFrameTime < 0f)
                firstFrameTime = Time.time;

            // Sau 2s thì mới khởi tạo vị trí tay gốc
            if (!isHandInitialized && Time.time - firstFrameTime >= delayBeforeCaptureZ)
            {
                baseZ = newHandPos.z;
                isHandInitialized = true;
                Debug.Log($"[AstraInit] baseZ đã được xác định sau 2 giây: {baseZ}");
            }

            // Nếu đã có baseZ thì bắt đầu kiểm tra click
            if (isHandInitialized)
            {
                float depthChange = baseZ - newHandPos.z;

                if (depthChange > clickThreshold && Time.time - lastClickTime > clickCooldown)
                {
                    Debug.Log("Click!");
                    OnClickEvent.Invoke();
                    lastClickTime = Time.time;
                }
            }
        }
        else
        {
            onDetectBody?.Invoke(false, currentHandPos);
        }
    }

    private Vector3 GetJointWorldPos(Astra.Joint joint)
    {
        return new Vector3(joint.WorldPosition.X / 1000f, joint.WorldPosition.Y / 1000f, joint.WorldPosition.Z / 1000f);
    }
}
