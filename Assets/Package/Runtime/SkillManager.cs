using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FinTOKMAK.SkillSystem;
using System;

namespace FinTOKMAK.SkillSystem
{
    [RequireComponent(typeof(SkillLogicManager))]
    public class SkillManager:MonoBehaviour
    {
        public float cdDetectionInterval = 0.1f;
        //���п��ü��ܵ�ʵ��
        public List<Skill> skills = new List<Skill>();

        //���еļ����¼�����
        public List<string> skillEventsName = new List<string>();

        //�߼�������(BUFF)ִ�о���ļ����߼�
        private SkillLogicManager manager;

        public Dictionary<string, Action> skillEvents=new Dictionary<string, Action>();

        void Awake()
        {
            manager = GetComponent<SkillLogicManager>();

            //��ȡ���еļ��ܣ���������Ӧ������ί��
            foreach (string name in skillEventsName)
            {
                skillEvents.Add(name, () => { });
            }

            //�������еļ��ܣ����ҽ�ִ���߼��Ĵ��������������Ӧ���¼�������
            foreach (Skill skill in skills)
            {
                skill.info.remainingActiveCount = skill.info.activeCount;
                skill.logic.id = skill.info.id;
                //�������Ϊ��������ģʽ
                if (skill.info.triggerType == TriggerType.Instance)
                {
                    //�������ܶ�Ӧ�Ĵ����¼��������¼�����ʱ�������ܼ���manager����ִ�ж�ӦonAdd
                    skillEvents[skill.info.triggerEventName] += () => {
                        if (skill.info.remainingActiveCount>0)
                        {
                            manager.Add(skill.logic);
                            skill.info.remainingActiveCount--;
                            skill.info.cdEndTime = Time.realtimeSinceStartup + skill.info.cd;
                        }
                        else
                        {
                            Debug.Log("������ȴ��");
                        }
                        
                    };
                }

                //�������Ϊ׼��ģʽ����
                else if (skill.info.triggerType == TriggerType.Prepared)
                {


                    //��ʼ��������׼���¼�
                    skillEvents[skill.info.prepareEventName] += skill.PrepareAction;
                    //PrepareActionӦ��ʵ�ֵ����ݣ�
                    //public void PrepareAction()
                    //{
                    //    skillEvents[skill.TriggerActionName] += () => {
                    //        manager.Add(skill.skillLogic);
                    //    };
                    //}
                    //��������ȡ���¼�
                    foreach (string CancelAction in skill.info.cancelEventName)
                    {
                        skillEvents[CancelAction] += () => {
                            skillEvents[skill.info.prepareEventName] -= skill.PrepareAction;
                        };
                    }
                }
            }
        }

        float time;
        private void Update()
        {
            time += Time.deltaTime;
            if (time < cdDetectionInterval)//���ܼ����
                return;
            time = 0;
            foreach (Skill skill in skills)//�������м��ܣ����CDʱ��
            {
                if (skill.info.cdEndTime < Time.realtimeSinceStartup && skill.info.remainingActiveCount < skill.info.activeCount)
                {
                    skill.info.cdEndTime = Time.realtimeSinceStartup + skill.info.cd;
                    skill.info.remainingActiveCount ++;
                }
            }
        }
        private void Start()
        {
            
        }

        /// <summary>
        /// ��Ӽ��ܵ����ü����б�
        /// </summary>
        /// <param name="logic">Ҫ��ӵļ�������</param>
        public void Add(Skill skill)
        {
            Debug.Log($"AddSKill:{skill.info.id}");
            var theSkillLogic = skills.FirstOrDefault(cus => cus.info.id == skill.info.id);//�õ���һ��ID��ͬ�ļ���
            if (theSkillLogic == null)
                skills.Add(skill);
            else
                Debug.Log($"�ü����Ѵ���:{skill.info.id}");
          
        }

       /// <summary>
       /// �����ܴӿ����б����Ƴ�
       /// </summary>
       /// <param name="ID">����ID</param>
        public void Remove(string ID)
        {
            var removeCount = skills.RemoveAll(cus => cus.info.id ==ID);//�õ���һ��ID��ͬ�ļ���
            if (removeCount >= 1)
                Debug.Log($"�ü������Ƴ�");
            else
                Debug.Log($"�ü��ܲ�����");
        }

        /// <summary>
        /// ��ȡ�б���ļ���
        /// </summary>
        /// <param name="ID">����ID</param>
        /// <returns>���ض�Ӧ���ܣ�������ܲ������򷵻�null</returns>
        public Skill Get(string ID)
        {
            return skills.FirstOrDefault(cus => cus.info.id == ID);//�õ���һ��ID��ͬ�ļ���
        }

        public void Clear()
        {
            skills.Clear();
        }

        public void SkillEvnetsInvoke(string SkillEventName)
        {
            skillEvents[SkillEventName]?.Invoke();
        }


    }

}
