using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.RuleTile.TilingRuleOutput;
public delegate void AttackDelegate();
[CreateAssetMenu(menuName = "EnemyStates/AttackState")]
public class AttackSO : EnemyStateSO
{
    private Animator anim;
    private VariableGrabber varGrabber;
    private EAim aim;
    private FirePointEnemy melee;
    private float time;
    private EnemyEffectManager efm;
    [SerializeField] bool navigateDuringAttack;
    [Header("Spell Settings")]
    [SerializeField] private ProjectilesSpellSO spell;

    private Dictionary<Attacktype, AttackDelegate> attackActions;
    //hit.GetComponent<IEffectable>().ApplyEffect(effect);

    public enum Attacktype
    {
        spell,
        melee,
        both,
        grab,
        special,
        None,
    }

    public enum NextStateType
    {
        defense,
        move,
        firstAttack,
        secondmove,
        secondAttack,
    }

    [SerializeField] private Attacktype attackType;
    [SerializeField] private NextStateType nextState;

    // Override clearly to accept a passed spell
    public override void SetSpell(SpellSO newSpell)
    {
        if (newSpell is ProjectilesSpellSO projSpell)
            spell = projSpell;
        else
            Debug.LogError($"Incorrect Spell type assigned to {name}. Expected ProjectilesSpellSO.");
    }

    public override void Enter()
    {
        time = 0;
        if (ESMM == null)
        {
            Debug.LogError("ESMM not initialized in AttackSO. Call Initialize first.");
            return;
        }

        efm = ESMM.GetComponent<EnemyEffectManager>();
        anim = ESMM.GetComponentInParent<Animator>();
        varGrabber = ESMM.GetComponent<VariableGrabber>();
        aim = ESMM.GetComponentInChildren<EAim>();
        attackActions = new Dictionary<Attacktype, AttackDelegate>
        {
            { Attacktype.melee, HandleMelee },
            { Attacktype.spell, () => HandleSpell(spell) },
            { Attacktype.special, HandleSpecial },
            { Attacktype.grab, HandleGrab },
            { Attacktype.both, HandleBoth },
            {Attacktype.None, HandleNone },
        };

    }

    public override void Tick()
    {
        if (Vector3.Distance(ESMM.gameObject.transform.position, ESMM.Player.gameObject.transform.position) >= ESMM.AttackRadius)
        {
            Exit();
        }
        if (melee == null)
        {
            melee = ESMM.GetComponentInChildren<FirePointEnemy>();

        }

        if (varGrabber.ShadowCircle != null && varGrabber.AimingRet != null)
        {
            varGrabber.ShadowCircle.gameObject.SetActive(false);
            varGrabber.AimingRet.gameObject.SetActive(true);
        }

        ESMM.GetComponent<Renderer>().enabled = true;
        ESMM.GetComponent<BoxCollider2D>().enabled = true;
        time += Time.deltaTime;
        aim.Target(ESMM.Player.gameObject.transform.position);

        if (time >= ESMM.MaxAttackTime && attackType == Attacktype.spell)
        {
            if (attackActions.TryGetValue(attackType, out AttackDelegate attackAction))
                attackAction();
            else
                Debug.LogWarning("Unhandled attack type: " + attackType);
            Exit();

        }
        if (time >= ESMM.MaxAttackTime && attackType == Attacktype.melee)
        {
            if (attackActions.TryGetValue(attackType, out AttackDelegate attackAction))
                attackAction();
            else
                Debug.LogWarning("Unhandled attack type: " + attackType);


        }

        if (navigateDuringAttack)
        {

            ESMM.gameObject.transform.position = Vector3.Lerp(
                    ESMM.gameObject.transform.position,
                    ESMM.Player.gameObject.transform.position,
                    ESMM.Speed * Time.deltaTime
                );
        }

    }

    public override void Exit()
    {
        time = 0;

        // Transition to first next state
        switch (nextState)
        {
            case NextStateType.defense:
                ESMM.ChangeState(EnemyStateType.Defense);
                break;
            case NextStateType.move:
                ESMM.ChangeState(EnemyStateType.Move);
                break;
            case NextStateType.secondmove:
                ESMM.ChangeState(EnemyStateType.SecondMove);
                break;
            case NextStateType.secondAttack:
                ESMM.ChangeState(EnemyStateType.SecondAttack);
                break;
            case NextStateType.firstAttack:
                ESMM.ChangeState(EnemyStateType.Attack);
                break;
        }
        if (anim != null)
            anim.enabled = false;
    }

    void HandleSpell(ProjectilesSpellSO projSpell)
    {
        if (projSpell == null)
        {
            Debug.LogError("ProjectileSpellSO not set on AttackSO.");
            return;
        }

        Vector3 targetDirection = (ESMM.Player.transform.position - ESMM.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

        GameObject projectileObj = Instantiate(projSpell.Projectile, varGrabber.AimingRet.transform.position, varGrabber.AimingRet.gameObject.transform.rotation);

        ProjectileComplex projectile = projectileObj.GetComponent<ProjectileComplex>();
        projectile.InitializeProjectile(projSpell, varGrabber.AimingRet.gameObject.transform.position, DamageSource.Enemy, varGrabber.AimingRet.gameObject.transform.position);
        //projectile.setDamageSource(DamageSource.Enemy);
        projectileObj.tag = "Ebullet";

        Shoot(projSpell);


    }

    void HandleMelee()
    {
        time += Time.deltaTime;

        if (time >= ESMM.MaxAttackTime)
            melee.meleeDown();

        if (time >= ESMM.MaxAttackTime + 1)
        {
            melee.meleeUp();
            Exit();
        }

    }

    void HandleBoth()
    {
        // Implement clearly if needed
    }

    void HandleGrab()
    {
        // Implement clearly if needed
    }

    void HandleSpecial()
    {
        // Implement clearly if needed
    }
    public void HandleNone()
    {



        return;
    }


    void Shoot(ProjectilesSpellSO _spell)
    {
        ESMM.StartCoroutine(ESMM.ShootCoroutine(_spell));
    }


}
