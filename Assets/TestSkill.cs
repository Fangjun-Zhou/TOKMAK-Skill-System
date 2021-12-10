using FinTOKMAK.SkillSystem;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Test Skill Logic", menuName = "FinTOKMAK/Skill System/Skill Logics/Test",
        order = 0)]
    public class TestSkill : Skill
    {
        public override void OnInitialization(SkillLogicManager manager)
        {
            base.OnInitialization(manager);
            
            Debug.Log("Initialized.");
        }

        public override void OnAdd(Skill self)
        {
            base.OnAdd(self);
            
            Debug.Log("Added.");
        }

        public override void OnContinue()
        {
            base.OnContinue();
            
            Debug.Log("Continued.");
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            Debug.Log("Removed.");
        }
    }
}