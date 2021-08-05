using System.Collections;
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
    private TestSkillLogic Logic;
    private SkillLogicManager skillLogic;

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
        Object.Destroy(gameObject);
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
    ///     主动调用Remove方法来结束技能
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_Remove()
    {
        //将技能加入skillLogic
        skillLogic.Add(Logic);
        //检查确认技能的OnRemove方法未执行
        Assert.IsFalse(Logic.RunRemove);
        //调用Remove方法移除技能
        skillLogic.Remove(Logic.id);
        //检查移除技能触发的OnRemove方法是否执行了
        Assert.IsTrue(Logic.RunRemove);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_AutoRemove()
    {
        //设置技能持续时间为1秒
        Logic.continueTime = 1f;
        //将技能加入skillLogic
        skillLogic.Add(Logic);
        //检查确认技能的OnRemove方法未执行
        Assert.IsFalse(Logic.RunRemove);
        yield return new WaitForSeconds(0.5f);
        //等待0.5秒，检查是否提前执行了OnRemove
        Assert.IsFalse(Logic.RunRemove);
        //等待2秒后，查看是否自动OnRemove技能
        yield return new WaitForSeconds(2);
        Assert.IsTrue(Logic.RunRemove);
    }

    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_Continue()
    {
        //设置技能持续时间为1秒
        Logic.continueTime = 1f;
        //设置技能持续执行间隔为0.1秒
        Logic.continueDeltaTime = 0.1f;
        //将技能设置为ARC模式,这样会执行Add,Remove和Continue方法
        Logic.effectType = SkillEffectType.ARModelAndContinue;
        //检查Continue方法未执行
        Assert.IsFalse(Logic.RunContinue);
        //将技能添加进去
        skillLogic.Add(Logic);
        //再次检查确认Continue方法未执行
        Assert.IsFalse(Logic.RunContinue);
        //等待2秒
        yield return new WaitForSeconds(2);
        //检查确认Continue方法已经执行
        Assert.IsTrue(Logic.RunContinue);
    }

    /// <summary>
    ///     技能触发后的持续触发间隔检测
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SkillLogic_Manager_Test_ContinueRunDelta()
    {
        //设置技能持续时间为1秒
        Logic.continueTime = 1f;
        //设置技能持续执行间隔为0.2秒
        Logic.continueDeltaTime = 0.2f;
        //将技能设置为ARC模式,这样会执行Add,Remove和Continue方法
        Logic.effectType = SkillEffectType.ARModelAndContinue;
        //将技能添加进去
        skillLogic.Add(Logic);
        //等待2秒
        yield return new WaitForSeconds(2);
        var count = Logic.continueTime / Logic.continueDeltaTime;
        //检查确认Continue方法的执行次数是否有误
        Assert.IsTrue(Logic.RunContinueCount == count);
    }


    public class TestSkillLogic : SkillLogic
    {
        public bool RunAdd;
        public bool RunRemove;
        public bool RunContinue;
        public int RunContinueCount;

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