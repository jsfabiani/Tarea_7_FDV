using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour
{
    public CinemachineBrain brain;
    public CinemachineBlendDefinition blendStyle = new CinemachineBlendDefinition (CinemachineBlendDefinition.Style.EaseInOut, 1.0f);
    private CinemachineVirtualCamera vcam;
    public float nearZoom = 2.0f;
    public float farZoom = 4.0f;
    private float resetTime = 0.0f;
    private float resetTimeCounter = 0.0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        //Assign active camera as vcam. This should be the default player camera, and should have highest priority in the scene.
        vcam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;

        // Change Default Blend
        brain.m_DefaultBlend = blendStyle;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer control for resetting gamespeed changes.
        if (resetTime != 0.0f)
        {
            resetTimeCounter += Time.deltaTime;
            if (resetTimeCounter >= resetTime)
            {
                GameSpeed(1.0f, 0.0f);
            }
        }

        // Zoom In and Out
        float verticalInput = Input.GetAxis("Vertical");
        if(verticalInput > 0)
        {
            vcam.m_Lens.OrthographicSize = nearZoom;
        }
        if(verticalInput < 0)
        {
            vcam.m_Lens.OrthographicSize = farZoom;
        }

        // Disable and reenable assigned virtual camera
        if(Input.GetKeyDown("c"))
        {
            if (vcam.enabled)
            {
                vcam.enabled = false;
            }
            else
            {
                vcam.enabled = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameSpeed(0.5f, 2.0f);          
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            GameSpeed(1.5f, 1.0f);
        }

        if (other.gameObject.tag == "SwitchPlayerCamera")
        {
            int priority = vcam.Priority;
            vcam.Priority -= 5;
            CinemachineVirtualCamera fixedCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            fixedCamera.Priority = priority;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SwitchPlayerCamera")
        {
            CinemachineVirtualCamera fixedCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            int priority = fixedCamera.Priority;
            vcam.Priority = priority;
            fixedCamera.Priority -= 5;
        }
    }

    // Change the timeScale to tscale, with the change lasting for delay in real time seconds.
    void GameSpeed(float tscale, float delay)
    {
        Time.timeScale = tscale;
        resetTime = Mathf.Abs(delay*tscale);
        resetTimeCounter = 0.0f;
    }
}
