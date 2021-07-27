using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinTOKMAK.SkillSystem
{
    /// <summary>
    /// ��������Ϸ�����ϵļ��ܹ���ϵͳ
    /// </summary>
    public class SkillLogicManager : MonoBehaviour
    {
        /// <summary>
        /// �����б�
        /// </summary>
        List<SkillLogic> skillList = new List<SkillLogic>();


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="logic">Ҫ��ӵļ�������</param>
        public void Add(SkillLogic logic)
        {
            var theSkillLogic = skillList.FirstOrDefault(cus => cus.id == logic.id);//�õ���һ��ID��ͬ�ļ���

            if (theSkillLogic == null)
                skillList.Add(logic);
            else
                logic = theSkillLogic;


            //if (logic.continueStopTime > time)//ֹͣʱ����ڵ�ǰʱ�䣬˵������ûʧЧ
            //{

            //}
            //else//ֹͣʱ��С�ڵ�ǰʱ�䣬˵�����ܹ��ڣ�Ӧ�ñ��Ƴ�
            //{

            //}

            if (logic.continueStopTimeOverlay)//���ܸ���ģʽ
                logic.continueStopTime = (int)(time + (logic.continueTime * 1000f));//����
            else
                logic.continueStopTime += (int)(logic.continueTime * 1000f);//�Ǹ��ǣ�ʱ���ۼ�ģʽ

            logic.continueDeltaTimeNext = time + logic.continueDeltaTime*1000f;
            logic.OnAdd(this, theSkillLogic);
            Debug.Log($"AddSKill[{logic.continueStopTimeOverlay}]:{logic.id},time{time},stop{ logic.continueStopTime},[{logic.continueTime*1000f}]");
           
        }

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="id">����ID</param>
        public void Remove(string id)
        {
            skillList.RemoveAll((x) => {
                if (x.id == id)
                {
                    x.OnRemove();
                    
                    return true;
                }
                return false;
            }); 
        }

        /// <summary>
        /// �������еļ���
        /// </summary>
        public void Clear()
        {
            if (skillList.Count != 0)
                skillList.Clear();
        }

        /// <summary>
        /// ��ȡ���еļ���
        /// </summary>
        /// <param name="id">���ܵ�ID</param>
        /// <returns>�������еļ���,���û�У��򷵻�Null</returns>
        public SkillLogic Get(string id)
        {
           return skillList.FirstOrDefault(cus => cus.id == id);//�õ���һ��ID��ͬ�ļ���
        }
        private int time;
        public void Update()
        {
            time = (int)(Time.realtimeSinceStartup * 1000f);
            skillList.RemoveAll((x) => {
                if (x.continueStopTime >= time)//ֹͣʱ����ڵ�ǰʱ�䣬˵������ûʧЧ
                {
                    if (x.effectType!=SkillEffectType.ARModel&&x.continueDeltaTimeNext <= time)//��鼼��ģʽ�ͳ���ִ�м��
                    {
                        Debug.Log($"ContinueSkill:{x.id},ContinueDeltaTimeNext:{x.continueDeltaTimeNext},Time:{time}");
                        x.OnContinue();
                        x.continueDeltaTimeNext += x.continueDeltaTime * 1000f;//�����´�ִ�м��
                        Debug.Log($"NewContinueDeltaTimeNext={x.continueDeltaTimeNext}");
                    }

                    return false;
                }
                else//ֹͣʱ��С�ڵ�ǰʱ�䣬˵�����ܹ��ڣ�Ӧ�ñ��Ƴ�
                {
                    Debug.Log($"RemoveSkill:{x.id},Time:{time}");
                    x.OnRemove(); 
                    return true;
                }
            });
        }
    }
}