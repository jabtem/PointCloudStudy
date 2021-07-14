using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using System.IO;
using System;


public class ExcavatorScript : MonoBehaviour {
    //Animator
    public Animator anim;
	float rotSpeed = 30f;
	public float driveSpeed = 2f;
	//Door
	bool opened = false;
    public Transform Sphere00;
    public Transform Sphere01;
    public Camera mainCamera;
    public Transform Sphere02;
    public Transform Sphere03;
    public Transform Sphere04;
    public Transform Sphere05;
    public bool InDriveMode = true;
	//Animate UV'S
	public float scrollSpeed = 0.5f;

	float offsetL;
	float offsetR;
    public int a = 1;
    public bool U = false;
	public bool V = true;
    Vector3 vector3;
    DirectoryInfo dir;

    private Material matL;
	private Material matR;

	//Treads
	public GameObject TreadsL;
	public GameObject TreadsR;

	//Weight Points - determines the rotation and movement axis of the Excavator
	public GameObject leftTread;
	public GameObject rightTread;

	//Big Wheels
	public GameObject WheelFrontLeft;
	public GameObject WheelFrontRight;

	public GameObject WheelBackLeft;
	public GameObject WheelBackRight;


	void Start()
	{
		// Materials for the Treads
		matL = TreadsL.GetComponent<Renderer> ().material;
		matR = TreadsR.GetComponent<Renderer> ().material;

		//set the bigarm to a non colliding position
		anim.SetFloat("BigArmSpeed",10f);
		//anim.Play("BigOpen", 0 , (1/30)*5);
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Sphere00 = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(9);
        Sphere01 = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1);
        Sphere02 = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1);
        Sphere03 = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1);
        Sphere04 = transform.GetChild(2).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(1);
    }

	void Update() 
	{
		anim.SetFloat("BigArmSpeed", 1f);

		if (!InDriveMode)
		{
			//-------------------------------------------------BIG ARM-----------------------------------------------------------------
			if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) /*&& anim.GetInteger("BigArmPosition")!=2*/)
			{
				//anim.SetInteger("BigArmPosition",1);
				//anim.SetFloat("BigArmSpeed",1f);
			}
			else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) /*&& anim.GetInteger("BigArmPosition")!=0*/)
			{
				//anim.SetInteger("BigArmPosition",1);
				anim.SetFloat("BigArmSpeed", -1f);
			}
			else
			{
				anim.SetFloat("BigArmSpeed", 0);
			}

			//-------------------------------------------------------SMALL ARM-------------------------------------------------------------
			if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("SmallArmPosition")!=2)
			{
				anim.SetInteger("SmallArmPosition",1);
				anim.SetFloat("SmallArmSpeed",1f);
			}
			else if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("SmallArmPosition")!=0)
			{
				anim.SetInteger("SmallArmPosition",1);
				anim.SetFloat("SmallArmSpeed", -1f);
			}
			else
			{
				anim.SetFloat("SmallArmSpeed", 0);
			}

			//----------------------------------------------------------SHOVEL-----------------------------------------------------------------
			if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)  && anim.GetInteger("ShovelPosition")!=2)
			{
				anim.SetInteger("ShovelPosition",1);
				anim.SetFloat("ShovelSpeed", 1f);
			}
			else if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)  && anim.GetInteger("ShovelPosition")!=0)
			{
				anim.SetInteger("ShovelPosition",1);
				anim.SetFloat("ShovelSpeed", -1f);
			}
			else
			{
				anim.SetFloat("ShovelSpeed", 0);
			}

			//---------------------------------------------------------ROTATE BODY----------------------------------------------------------
			if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				anim.SetFloat("RotateSpeed", 0.5f);
			}
			else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
			{
				anim.SetFloat("RotateSpeed", -0.5f);
			}
			else
			{
				anim.SetFloat("RotateSpeed", 0f);
			}
		
		}

		//---------------------------------------------------------DRIVE MODE--------------------------------------------------------------
		if (InDriveMode)
		{
			//ANIMATE RIGHT TREAD
			if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.A))
			{
				transform.RotateAround(leftTread.transform.position, -Vector3.up, Time.deltaTime * rotSpeed);
				offsetR = Time.time * scrollSpeed % 1;
				WheelFrontRight.transform.Rotate(Vector3.forward * Time.deltaTime *rotSpeed *4);
				WheelBackRight.transform.Rotate(Vector3.forward * Time.deltaTime *rotSpeed *4);

			}

			if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.A))
			{
				transform.RotateAround(leftTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
				offsetR = Time.time * -scrollSpeed % 1;
				WheelFrontRight.transform.Rotate(-Vector3.forward * Time.deltaTime *rotSpeed *4);
				WheelBackRight.transform.Rotate(-Vector3.forward * Time.deltaTime *rotSpeed *4);
			}

			//ANIMATE LEFT TREAD
			if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.D))
			{
				transform.RotateAround(rightTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
				offsetL = Time.time * scrollSpeed % 1;
				WheelFrontLeft.transform.Rotate(-Vector3.forward * Time.deltaTime *rotSpeed *4);
				WheelBackLeft.transform.Rotate(-Vector3.forward * Time.deltaTime *rotSpeed *4);
			}

			if (!Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.D))
			{
				transform.RotateAround(rightTread.transform.position, -Vector3.up, Time.deltaTime * rotSpeed);
				offsetL = Time.time * -scrollSpeed % 1;
				WheelFrontLeft.transform.Rotate(Vector3.forward * Time.deltaTime *rotSpeed *4);
				WheelBackLeft.transform.Rotate(Vector3.forward * Time.deltaTime *rotSpeed *4);
			}
		}

		//------------------------------------------------------DOOR OPEN / CLOSE-----------------------------------------------------
		if (Input.GetKeyDown(KeyCode.F))
		{
			opened = !opened;
			anim.SetBool("DoorOpen", opened);
		}

		//-----------------------------------------------Switch Drive Mode/ Work Mode-------------------------------------------------
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			InDriveMode = !InDriveMode;
		}

		//--------------------------------------------------------------Animate UV's---------------------------------------------------
		if(U && V)
		{
			matL.mainTextureOffset = new Vector2(offsetL,offsetL);
			matR.mainTextureOffset = new Vector2(offsetR,offsetR);
		}
		else if(U)
		{
			matL.mainTextureOffset = new Vector2(offsetL,0);
			matR.mainTextureOffset = new Vector2(offsetR,0);
		}
		else if(V)
		{
			matL.mainTextureOffset = new Vector2(0,offsetL);
			matR.mainTextureOffset = new Vector2(0,offsetR);
		}

        if (Input.GetKeyDown(KeyCode.Return))
        {

            System.IO.File.AppendAllText(@"output.txt", a + ";" +
                Sphere00.position.x + ";" + Sphere00.position.y + ";" + Sphere00.position.z + ";" +
                Sphere01.position.x + ";" + Sphere01.position.y + ";" + Sphere01.position.z + ";" +
                Sphere02.position.x + ";" + Sphere02.position.y + ";" + Sphere02.position.z + ";" +
                Sphere03.position.x + ";" + Sphere03.position.y + ";" + Sphere03.position.z + ";" +
                Sphere04.position.x + ";" + Sphere04.position.y + ";" + Sphere04.position.z + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n");


            Vector3 viewPose0 = mainCamera.WorldToViewportPoint(Sphere00.position);
            Vector3 viewPose1 = mainCamera.WorldToViewportPoint(Sphere01.position);
            Vector3 viewPose2 = mainCamera.WorldToViewportPoint(Sphere02.position);
            Vector3 viewPose3 = mainCamera.WorldToViewportPoint(Sphere03.position);
            Vector3 viewPose4 = mainCamera.WorldToViewportPoint(Sphere04.position);

            System.IO.File.AppendAllText(@"output2.txt", a + ";" +
               viewPose0.x + ";" + viewPose0.y + ";" +
               viewPose1.x + ";" + viewPose1.y + ";" +
               viewPose2.x + ";" + viewPose2.y + ";" +
               viewPose3.x + ";" + viewPose3.y + ";" +
               viewPose4.x + ";" + viewPose4.y + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n");





            a = a + 1;

                                                                 }
    }
}