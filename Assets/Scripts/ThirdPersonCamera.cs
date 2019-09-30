using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    float TTime;
    bool Resetting;
    WaitForSeconds Wait;
    Vector3 Rotation, Velocity;

    public float Smooth;
    private bool UseCursor;
    public Vector2 ClampX;
    public Vector3 Offset;
    private GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        Resetting = false;
        Smooth = (Smooth == 0f) ? 0.3f : Smooth;
        Wait = new WaitForSeconds(Time.deltaTime);
        Cursor.visible = UseCursor ? false : true;
        Offset = (Offset == Vector3.zero) ? new Vector3(0, 1, -5) : Offset;
        Cursor.lockState = UseCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void ControllerCheck()
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            if (Input.GetJoystickNames()[0] == "Wireless Controller")
            {
                UseCursor = false;
            }
            else
            {
                UseCursor = true;
            }
        }
        else
        {
            UseCursor = true;
        }


        Cursor.visible = UseCursor ? false : true;
    }

    // Update is called once per frame
    void Update()
    {

        ControllerCheck();
        Quaternion Prev = transform.rotation;
        transform.rotation = CheckInputRotation();
        transform.position = Target.transform.position + Quaternion.Euler(Rotation) * Offset;

        if (Prev == transform.rotation) TTime += Time.deltaTime;
        //if (TTime > 2f && !Resetting) StartCoroutine("ResetToBack");
    }

    bool CheckInput()
    {
        bool temp = false;
        if (!UseCursor)
        {
            if (Mathf.Abs(Input.GetAxis("Second X")) > 0.2)
                temp = true;
            if (Mathf.Abs(Input.GetAxis("Second Y")) > 0.2)
                temp = true;
        }
        else
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.2)
                temp = true;
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.2)
                temp = true;
        }
        return temp;
    }

    IEnumerator ResetToBack()
    {
        float Dist;
        yield return Resetting = true;

        while (Resetting)
        {
            Vector3 TargetRotation = Quaternion.LookRotation(Target.transform.forward, transform.up).eulerAngles;

            if (TargetRotation.y >= 180f) yield return TargetRotation.y = 360f - TargetRotation.y;
            else if (TargetRotation.y <= -180f) yield return TargetRotation.y = -360f + TargetRotation.y;

            Dist = Vector3.Distance(Rotation, TargetRotation);

            if (Dist < 5f || CheckInput())
            {
                yield return TTime = 0f;
                yield return Resetting = false;
            }

            Vector3 NextRotation = Vector3.SmoothDamp(Rotation, TargetRotation, ref Velocity, Smooth);
            yield return NextRotation.x = Rotation.x;
            yield return Rotation = NextRotation;
        }
    }

    Quaternion CheckInputRotation()
    {
        if (!UseCursor)
        {
            Rotation.y += Input.GetAxis("Second X");
            Rotation.x += Input.GetAxis("Second Y");
        }

        else
        {
            Rotation.y += Input.GetAxis("Mouse X");
            Rotation.x -= Input.GetAxis("Mouse Y");
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && !UseCursor)
        {
            Rotation.y += Input.GetAxis("Horizontal");
        }

        Rotation.z = 0f;
        Rotation.y = (Rotation.y > 360 || Rotation.y < -360) ? 0f : Rotation.y;
        Rotation.x = (ClampX == Vector2.zero) ? Mathf.Clamp(Rotation.x, -10f, 20f) : Mathf.Clamp(Rotation.x, ClampX.x, ClampX.y);

        return Quaternion.Euler(Rotation);
    }
}
