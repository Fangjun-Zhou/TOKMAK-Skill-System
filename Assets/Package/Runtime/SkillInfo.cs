using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FinTOKMAK.SkillSystem
{
    [CreateAssetMenu(fileName = "Skill Info Config", menuName = "FinTOKMAK/SkillSystem/Create Skill Info Config",
        order = 0)]
    public class SkillInfo:ScriptableObject
    {
        public string id;
        public string name;
        public string describe;
        public string triggerEventName;
        public string prepareEventName;
        public List<string> cancelEventName;
        public float cd;
        [HideInInspector]
        public float cdEndTime;
        public int activeCount;
        [HideInInspector]
        public int remainingActiveCount;
        public bool isAcitve;
        public List<string> needActiceSkillID;
        public TriggerType triggerType;
    }

    public enum TriggerType
    { 
        Instance,
        Prepared
    
    }
    
}
