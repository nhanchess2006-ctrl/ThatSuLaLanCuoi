using UnityEngine;

public class Enemy_ReaperSpellCastState : EnemyState
{
    private Enemy_Reaper enemyReaper;

    private float spellCastLoopDuration = 0.01f;
    private float spellCastLoopTimer;

    private bool castLoopStarted;
    private bool exitingState;

    public Enemy_ReaperSpellCastState(
        Enemy enemy,
        StateMachine stateMachine,
        string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }

    public override void Enter()
    {
        base.Enter();

        enemyReaper.SetVelocity(0, 0);

        enemyReaper.SetSpellCastPreformed(false);
        enemyReaper.SetSpellCastOnCooldown();

        castLoopStarted = false;
        exitingState = false;

        spellCastLoopTimer = 0;

        anim.SetBool("spellCast_Performed", false);

        // Vẫn có thể bị đánh trong đoạn chuẩn bị cast
        enemyReaper.MakeUntargetable(false);
    }

    public override void Update()
    {
        base.Update();

        enemyReaper.SetVelocity(0, 0);

        // =========================================
        // CAST PERFORM XONG -> BẮT ĐẦU CAST LOOP
        // =========================================

        if (enemyReaper.spellCastPreformed && castLoopStarted == false)
        {
            castLoopStarted = true;

            anim.SetBool("spellCast_Performed", true);

            spellCastLoopTimer = spellCastLoopDuration;

            // Trong Cast Loop không cho Player đánh trúng
            enemyReaper.MakeUntargetable(true);
        }

        // =========================================
        // CAST LOOP
        // =========================================

        if (castLoopStarted == false)
            return;

        spellCastLoopTimer -= Time.deltaTime;

        if (spellCastLoopTimer <= 0)
        {
            FinishSpellCast();
        }
    }

    private void FinishSpellCast()
    {
        if (exitingState)
            return;

        exitingState = true;
        castLoopStarted = false;

        // Cho phép bị đánh trở lại trước khi đổi state
        enemyReaper.MakeUntargetable(false);

        if (enemyReaper.ShouldTeleport())
        {
            stateMachine.ChangeState(
                enemyReaper.reaperTeleportState
            );
        }
        else
        {
            stateMachine.ChangeState(
                enemyReaper.reaperBattleState
            );
        }
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("spellCast_Performed", false);

        // Cực kỳ quan trọng:
        // dù state bị interrupt bằng cách nào cũng trả layer lại
        enemyReaper.MakeUntargetable(false);

        castLoopStarted = false;
        exitingState = false;
        spellCastLoopTimer = 0;
    }
}