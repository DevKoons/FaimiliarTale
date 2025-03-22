using UnityEngine;

public abstract class EnemyStateSO : ScriptableObject
{
    protected EnemyStateMachineModular ESMM;

    // Instead of using a constructor, use an Initialize method
    public void Initialize(EnemyStateMachineModular ESMM)
    {
        this.ESMM = ESMM;
    }
    public virtual void SetSpell(SpellSO spell) { }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}
