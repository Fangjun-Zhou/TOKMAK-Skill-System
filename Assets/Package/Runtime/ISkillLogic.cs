using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FinTOKMAK.SkillSystem
{ 
    /// <summary>
    /// ����Ч���ӿ�
    /// </summary>
   
    public class SkillLogic:ScriptableObject
    {
        /// <summary>
        /// ���ܵ�ID
        /// </summary>
        [HideInInspector]
        public string id;
        /// <summary>
        /// ���ܵĳ���ʱ��(��)
        /// </summary>
        public float continueTime;

        /// <summary>
        /// ���ܵ�ֹͣʱ��
        /// </summary>
        [HideInInspector]
        public float continueStopTime;

        /// <summary>
        /// ����Ч������
        /// </summary>
        public SkillEffectType effectType;

        /// <summary>
        /// ����ֹͣʱ���Ƿ�ʹ�ø���ģʽ
        /// </summary>
        public bool continueStopTimeOverlay;

        /// <summary>s
        /// �����ڳ���ģʽ�µ�ִ�м��(��)
        /// </summary>
        public float continueDeltaTime;

        /// <summary>
        /// �����ڳ���ģʽ�µ���һ�μ��ִ��ʱ��
        /// </summary>
        [HideInInspector]
        public float continueDeltaTimeNext;

        /// <summary>
        /// ���ܱ����ʱִ�еķ���
        /// </summary>
        /// <param name="targer">��Ӽ��ܵ�manager</param>
        /// <param name="self">���ܴ��ڵļ��ܣ������������Ϊ��</param>
        public virtual void OnAdd(SkillLogicManager targer, SkillLogic self)
        { 
        

        }

        /// <summary>
        /// ���ܱ��Ƴ�ʱִ�еķ���
        /// </summary>
        public virtual void OnRemove()
        { 
        

        }

        /// <summary>
        /// ���ܳ�������ʱִ�еķ���
        /// </summary>
        public virtual void OnContinue()
        { 
        
        }

    }
    /// <summary>
    /// ����Ч������
    /// </summary>
    public enum SkillEffectType
    { 
        /// <summary>
        /// ��ӡ��Ƴ�ʱ��Ч
        /// </summary>
        ARModel,
        /// <summary>
        /// ������Ч
        /// </summary>
        Continue,
        /// <summary>
        /// ��ӡ��Ƴ���������Ч
        /// </summary>
        ARModelAndContinue,

    }
}