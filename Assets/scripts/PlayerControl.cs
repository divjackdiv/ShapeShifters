using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour
{
	public float walkSpeed = 5.0f;
	public float sprintSpeed = 7.0f;

	public float turnSmoothing = 3.0f;
	public float aimTurnSmoothing = 15.0f;
	public float speedDampTime = 0.1f;

	public  bool attacking;
	private float speed;

	private bool jumpBool;
	private GameObject cam;
    public GameObject cameraPointer;
    private float h;
	private float v;
	private bool aim;
	private bool sprint;

	private bool isMoving;

	void Start(){
		if (!isLocalPlayer)
		{
		    return;
		}
		
	}

	void Update(){

        if (!isLocalPlayer)
		{
		    return;
		}
        if (cam == null)
        {
            cam = (GameObject)Instantiate(Resources.Load("camera"));
            cam.GetComponent<ThirdPersonOrbitCam>().player = transform;
            cam.GetComponent<ThirdPersonOrbitCam>().enabled = true;
            cameraPointer = cam;
            //GetComponent<Shooter>().cam = cam;
            //GetComponent<Shooter>().enabled = true;
        }

		aim = Input.GetButton("Aim");
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		sprint = Input.GetButton ("Sprint");
		isMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;

		MovementManagement (h, v, sprint);
	}

	void MovementManagement(float horizontal, float vertical, bool sprinting)
	{	
		Rotating (horizontal, vertical);
		if(isMoving)
		{
			if(sprinting)
			{
				speed = sprintSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
		}
		else
		{
			speed = 0f;
		}
		float step = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * step);
    }

	Vector3 Rotating(float horizontal, float vertical)
	{

		Vector3 forward = cam.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		forward.y = 0;

		Vector3 right = cam.transform.TransformDirection(Vector3.right);
		right = right.normalized;
		right.y = 0;

		Vector3 targetDirection;

		float finalTurnSmoothing;

		if(IsAiming())
		{
            
			finalTurnSmoothing = aimTurnSmoothing;
		}
		else
		{			
			finalTurnSmoothing = turnSmoothing;
		}
        targetDirection = forward * vertical + right * horizontal;
        if (isMoving && targetDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation (targetDirection);
            Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, finalTurnSmoothing * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation (newRotation);
		}
		return targetDirection;
	}	
	
	public bool IsAiming()
	{
		return aim;
	}

	public bool isSprinting()
	{
		return sprint && !aim && (isMoving);
	}
	
}
