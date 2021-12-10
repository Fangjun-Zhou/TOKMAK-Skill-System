using FinTOKMAK.SkillSystem;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestSkillInvoke : MonoBehaviour
    {
        public SkillManager manager;
        public string eventName;
        
        [Button]
        public void InvokeEvent()
        {
            manager.SkillEventsInvoke(eventName);
        }
    }
}