using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FinTOKMAK.SkillSystem;
namespace FinTOKMAK.SkillSystem
{
    [CreateAssetMenu(fileName = "Skill", menuName = "FinTOKMAK/SkillSystem/Create Skill Config",
      order = 0)]
    public class Skill:ScriptableObject
    {

        public SkillLogic logic;
        public SkillInfo info;

        public void PrepareAction()
        { 
        
        }

    }
}