using System.Collections;
using System.Collections.Generic;
using FinTOKMAK.SkillSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SkillLogicManagerTest
{
    // A Test behaves as an ordinary method
    //[Test]
    //public void NewTestScriptSimplePasses()
    //{
    //    // Use the Assert class to test conditions

    //}

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    private GameObject gameObject;
    private SkillLogicManager skillLogic;
    private TestSkillLogic Logic;
    [SetUp]
    public void Init()
    {
        gameObject = new GameObject();
        skillLogic = gameObject.AddComponent<SkillLogicManager>();
        Logic = new TestSkillLogic();
        Logic.continueDeltaTime = 0.02f;
        Logic.effectType = SkillEffectType.ARModel;
        Logic.continueStopTimeOverlay = true;
        Logic.continueTime = 1f;
        Logic.id = "Logic";
    }
    [TearDown]
    public void Destroy()
    {
        GameObject.Destroy(gameObject);
        skillLogic = null;
        Logic = null;
    }

    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_Add()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Assert.IsFalse(Logic.RunAdd);
        skillLogic.Add(Logic);
        Assert.IsTrue(Logic.RunAdd);
        yield return null;
    }

    /// <summary>
    /// ��������Remove��������������
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_Remove()
    {

        //�����ܼ���skillLogic
        skillLogic.Add(Logic);
        //���ȷ�ϼ��ܵ�OnRemove����δִ��
        Assert.IsFalse(Logic.RunRemove);
        //����Remove�����Ƴ�����
        skillLogic.Remove(Logic.id);
        //����Ƴ����ܴ�����OnRemove�����Ƿ�ִ����
        Assert.IsTrue(Logic.RunRemove);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_AutoRemove()
    {
        //���ü��ܳ���ʱ��Ϊ1��
        Logic.continueTime = 1f;
        //�����ܼ���skillLogic
        skillLogic.Add(Logic);
        //���ȷ�ϼ��ܵ�OnRemove����δִ��
        Assert.IsFalse(Logic.RunRemove);
        yield return new WaitForSeconds(0.5f);
        //�ȴ�0.5�룬����Ƿ���ǰִ����OnRemove
        Assert.IsFalse(Logic.RunRemove);
        //�ȴ�2��󣬲鿴�Ƿ��Զ�OnRemove����
        yield return new WaitForSeconds(2);
        Assert.IsTrue(Logic.RunRemove);
        
    }
    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_Continue()
    {
        //���ü��ܳ���ʱ��Ϊ1��
        Logic.continueTime = 1f;
        //���ü��ܳ���ִ�м��Ϊ0.1��
        Logic.continueDeltaTime = 0.1f;
        //����������ΪARCģʽ,������ִ��Add,Remove��Continue����
        Logic.effectType = SkillEffectType.ARModelAndContinue;
        //���Continue����δִ��
        Assert.IsFalse(Logic.RunContinue);
        //��������ӽ�ȥ
        skillLogic.Add(Logic);
        //�ٴμ��ȷ��Continue����δִ��
        Assert.IsFalse(Logic.RunContinue);
        //�ȴ�2��
        yield return new WaitForSeconds(2);
        //���ȷ��Continue�����Ѿ�ִ��
        Assert.IsTrue(Logic.RunContinue);
    }

    /// <summary>
    /// ���ܴ�����ĳ�������������
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_ContinueRunDelta()
    {
        //���ü��ܳ���ʱ��Ϊ1��
        Logic.continueTime = 1f;
        //���ü��ܳ���ִ�м��Ϊ0.2��
        Logic.continueDeltaTime = 0.2f;
        //����������ΪARCģʽ,������ִ��Add,Remove��Continue����
        Logic.effectType = SkillEffectType.ARModelAndContinue;
        //��������ӽ�ȥ
        skillLogic.Add(Logic);
        //�ȴ�2��
        yield return new WaitForSeconds(2);
        var count = Logic.continueTime / Logic.continueDeltaTime;
        //���ȷ��Continue������ִ�д����Ƿ�����
        Assert.IsTrue(Logic.RunContinueCount == count);
    }


    public class TestSkillLogic : SkillLogic
    {
        public bool RunAdd = false;
        public bool RunRemove = false;
        public bool RunContinue =  false;
        public int RunContinueCount = 0;

        public override void OnAdd(SkillLogicManager targer, SkillLogic self)
        {
            base.OnAdd(targer, self);
            RunAdd = true;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            RunRemove = true;
        }

        public override void OnContinue()
        {
            base.OnContinue();
            RunContinue = true;
            RunContinueCount++;
            Debug.Log($"ContinueCoun:{RunContinueCount}");
        }
    }

}
