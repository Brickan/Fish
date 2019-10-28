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
    [SerializeField]
    private bool UseCursor;
    public Vector2 ClampX;
    public Vector3 Offset;
    private GameObject target;

    private float offsetZStart;

    [SerializeField]
    private float clampMinDistance;
    [SerializeField]
    private float smoother, distance, hitDistanceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Resetting = false;
        Smooth = (Smooth == 0f) ? 0.3f : Smooth;
        Wait = new WaitForSeconds(Time.deltaTime);
        Cursor.visible = false;
        Offset = (Offset == Vector3.zero) ? new Vector3(0, 1, -5) : Offset;
        offsetZStart = Offset.z;
        Cursor.lockState = CursorLockMode.Locked;

        smoother = (smoother == 0) ? smoother = 10 : smoother;
        hitDistanceMultiplier = (hitDistanceMultiplier == 0) ? hitDistanceMultiplier = 0.85f : hitDistanceMultiplier;
        clampMinDistance = (clampMinDistance == 0) ? clampMinDistance = 5 : clampMinDistance;
    }

    void CameraCollision()
    {
        RaycastHit hit;

        if (Physics.Linecast(target.transform.position, transform.position, out hit))
        {
            distance = Mathf.Clamp((hit.distance * hitDistanceMultiplier), clampMinDistance, -offsetZStart);
            Debug.Log("True");
           
        }
        else if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit))
        {
            distance = Mathf.Clamp((hit.distance * hitDistanceMultiplier) ,clampMinDistance, -offsetZStart);
        }
        
        else
        {

            distance = -offsetZStart;
            
            Debug.Log("False");
        }

        Offset.z = Mathf.Lerp(Offset.z, -distance, smoother * Time.deltaTime);

        Debug.DrawLine(target.transform.position, transform.position, Color.black);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back), Color.yellow);
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
    }

    // Update is called once per frame
    void Update()
    {

        ControllerCheck();
        CameraCollision();
        Quaternion Prev = transform.rotation;
        transform.rotation = CheckInputRotation();
        transform.position = target.transform.position + Quaternion.Euler(Rotation) * Offset;
        


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
            Vector3 TargetRotation = Quaternion.LookRotation(target.transform.forward, transform.up).eulerAngles;

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

    bool Raycast(float Distance) => Physics.Raycast(transform.position + transform.up, transform.TransformDirection(Vector3.back), Distance);
}
