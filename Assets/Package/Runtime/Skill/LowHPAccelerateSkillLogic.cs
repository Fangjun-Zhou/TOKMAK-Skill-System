using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinTOKMAK.SkillSystem;
using System;
[CreateAssetMenu(fileName = "LowHP Accelerate Skill", menuName = "FinTOKMAK/SkillSystem/Skill/LowHP Accelerate Skill",
      order = 0)]
public class LowHPAccelerateSkillLogic : SkillLogic
{

    public float speed;
    private float oldspeed;
   // PlayerPlatformerController Controller;

    public override void OnAdd(SkillLogicManager target, SkillLogic self)
    {
        Debug.Log("Spedd+" + speed);
       // Controller= target.GetComponent<PlayerPlatformerController>();
      //  oldspeed = Controller.maxSpeed;
      //  Controller.maxSpeed = speed;
    }

    public override void OnContinue()
    {
        
    }

    public override void OnRemove()
    {
     //   Controller.maxSpeed = oldspeed;
    }

}
