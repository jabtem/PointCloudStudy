using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnimation : MonoBehaviour
{

    Animator anim;


    //애니메이션 재생여부
    bool bigStart;//맨처음 재생되어야됨
    bool smallStart;
    bool shovelStart;
    bool rotateStart;

    //애니메이션 역재생여부
    bool smallReverse;
    bool shovelReverse;
    bool rotateReverse;



    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Excavator2").GetComponent<Animator>();
        anim.SetFloat("BigArmSpeed", 1.0f);
        //첫애니메이션은 재생되어야함으로
        bigStart = true;
    }



    void Update()
    {
        if(bigStart)
        {
            //애니메이선 1번레이어(BigArm) 애니메이션 재생
            if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime < 0.5f)
            {
                anim.SetFloat("BigArmSpeed", 0.5f);
            }
            else if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.51f)
            {
                anim.SetFloat("BigArmSpeed", -0.5f);
            }
            else
            {
                anim.SetFloat("BigArmSpeed", 0.0f);
                bigStart = false;
                smallStart = true;
            }
        }




        //애니메이선 2번레이어(SmallArm) 애니메이션 재생
        if (smallStart)
        {
            if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime < 0.7f)
            {
                anim.SetFloat("SmallArmSpeed", 0.5f);
            }
            else if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.71f)
            {
                anim.SetFloat("SmallArmSpeed", -0.5f);
            }
            else
            {
                anim.SetFloat("SmallArmSpeed", 0.0f);
                smallStart = false;
                shovelStart = true;
            }
        }

        //애니메이선 3번레이어(Shovel) 애니메이션 재생
        if (shovelStart)
        {
            anim.SetFloat("ShovelSpeed", 1.0f);

            if(anim.GetCurrentAnimatorStateInfo(3).normalizedTime >= 1.0f)
            {
                anim.SetFloat("ShovelSpeed", 0.0f);
                shovelStart = false;
                rotateStart = true;
            }
        }
        //애니메이선 4번레이어(Rotate) 애니메이션 재생
        if (rotateStart)
        {
            if (anim.GetCurrentAnimatorStateInfo(4).normalizedTime < 0.15f)
            {
                anim.SetFloat("RotateSpeed", 0.5f);
            }
            else if (anim.GetCurrentAnimatorStateInfo(4).normalizedTime >= 0.16f)
            {
                anim.SetFloat("RotateSpeed", -0.5f);
            }
            else
            {
                anim.SetFloat("RotateSpeed", 0.0f);
                smallReverse = true;
                rotateStart = false;
            }
        }

        
        //애니메이션 역재생
        //SmallArm -> Shovel -> Rotate 순
        if(smallReverse)
        {
            if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime > 0.5f)
            {
                anim.SetFloat("SmallArmSpeed", -0.5f);
            }
            else
            {
                anim.SetFloat("SmallArmSpeed", 0.0f);
                smallReverse = false;
                shovelReverse = true; 
            }
        }

        if (shovelReverse)
        {
            anim.SetFloat("ShovelSpeed", -1.0f);

            if (anim.GetCurrentAnimatorStateInfo(3).normalizedTime <= 0.0f)
            {
                anim.SetFloat("ShovelSpeed", 0.0f);
                shovelReverse = false;
                rotateReverse = true;
            }
        }


        if(rotateReverse)
        {
            anim.SetFloat("RotateSpeed", -0.5f);
            if (anim.GetCurrentAnimatorStateInfo(4).normalizedTime <= 0.0f)
            {
                anim.SetFloat("ShovelSpeed", 0.0f);
                rotateReverse = false;

                //SmallArm 애니메이션부터 애니메이션 반복하도록 설정
                smallStart = true;
            }
        }

    }
}
