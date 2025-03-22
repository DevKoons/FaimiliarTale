using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum EnemyStateType
{
    Default,
    Attack,
    SecondAttack,
    Defense,
    SecondDefense,
    Move,
    SecondMove,
    Idle,
    underStatus
}

public class EnemyStateMachineModular : MonoBehaviour
{
    [Header("InitialState")]
    [SerializeField] private EnemyStateSO pathState; //IdleState


    [Header("TestStateChecking")]
    [SerializeField] private EnemyStateSO currentState;
    [SerializeField] private EnemyStateSO previousState;

    [Header("State Configuration")]
    [SerializeField] private EnemyStateSO attackState;
    [SerializeField] private EnemyStateSO moveState;
    [SerializeField] private EnemyStateSO defenseState;
    [SerializeField] private EnemyStateSO underStatusState;

    [Header("Second State Configuration")]
    [SerializeField] private EnemyStateSO secondAttackState;
    [SerializeField] private EnemyStateSO secondMoveState;
    [SerializeField] private EnemyStateSO secondDefenseState;
    

    [Header("State Management")]
    private Dictionary<EnemyStateType, EnemyStateSO> stateDictionary;

    [Header("References & Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private bool usesSecondStates;
    [SerializeField] private GameObject[] pathPoints; // Store positions directly as Vector3s

    [SerializeField] EffectSO effect;
   
    private bool usedFirstState; // Corrected backing field
    private EnemyStateType currentStateType;

    public GameObject[] PathPoints
    {
        get => pathPoints;
        set => pathPoints = value;
    }
    public EnemyStateType CurrentStateType
    {
        get => currentStateType;
        set => currentStateType = value;
    }
    public bool UsesSecondStates
    {
        get => usesSecondStates;
        set => usesSecondStates = value;
    }

    public bool UsedFirstState
    {
        get => usedFirstState; // fixed recursion
        set => usedFirstState = value; // fixed recursion
    }
   

    private PlayerCommand player;
    private FirePointEnemy fpe;
    private EnemyHealth enemyHealth;
    private VariableGrabber varGrabber;
    [SerializeField] float attackRadius;
    EnemyEffectManager efm;
    [SerializeField] float maxAttackTime;
    private float zigzagTimer = 0f;
    public float MaxAttackTime => maxAttackTime;
    public float ZigzagTimer
    {
        get { return zigzagTimer; }
        set { zigzagTimer = value; }
    }
 


    public float Speed => speed;
    public PlayerCommand Player => player;
    public EnemyEffectManager EffectManager => efm;
    public float AttackRadius
    {
        get { return attackRadius; }
        set { attackRadius = value; }
    }
  
    private void Start()
    {
        Debug.Log($"{name} - Initializing State Machine");
        InitializeStateDictionary();
        fpe = GetComponentInChildren<FirePointEnemy>();
        efm = GetComponent<EnemyEffectManager>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = FindObjectOfType<PlayerCommand>();
        varGrabber = GetComponent<VariableGrabber>();


        if (pathState != null)
        {
            SetStateInternal(pathState);
        }
        else
        {
            Debug.LogError($"{name} - Default state is not assigned in the inspector.");
        }

    }

    public void InitializeStateDictionary()
    {
        stateDictionary = new Dictionary<EnemyStateType, EnemyStateSO>
        {
            { EnemyStateType.Attack, attackState },
            { EnemyStateType.Defense, defenseState },
            { EnemyStateType.Move, moveState },
            { EnemyStateType.Idle, pathState },
            { EnemyStateType.underStatus, underStatusState },
            {EnemyStateType.SecondAttack, secondAttackState },
            {EnemyStateType.SecondMove, secondMoveState },
            {EnemyStateType.SecondDefense, secondDefenseState },
        };

        foreach (var state in stateDictionary.Values)
        {
            if (state != null)
            {
                state.Initialize(this);
                Debug.Log($"{name} - State {state.GetType().Name} initialized.");
            }
            else
            {
                Debug.LogError($"{name} - One or more states in the dictionary are null.");
            }
        }
    }

    public void ChangeState(EnemyStateType stateType)
    {
        if (stateDictionary.TryGetValue(stateType, out var newState))
        {
            maxAttackTime = UnityEngine.Random.Range(1, maxAttackTime);

            if (newState == null)
            {
                Debug.LogError($"{name} - State {stateType} is not assigned in the inspector.");
                return;
            }

            SetStateInternal(newState);
        }
        else
        {
            Debug.LogError($"{name} - State {stateType} not found in the dictionary.");
        }
    }

    private bool isExitingState = false;

    private void SetStateInternal(EnemyStateSO newState)
    {
        if (isExitingState) return;

        isExitingState = true;
        currentState?.Exit();
        previousState = currentState;
        currentState = Instantiate(newState); // Ensure unique instance
        currentState.Initialize(this);
        Debug.Log($"{name} - Entering state: {currentState?.GetType().Name}");
        currentState.Enter();
        if (!UsesSecondStates)
            UsedFirstState = false;

        isExitingState = false;
    }

    private void Update()
    {
        
        currentState?.Tick();
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D wall in collisions)
        {
            if (wall.CompareTag("wall"))
            {
                MoveTowardsPlayer();
            }
        }
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Case sensitive during Editor design all items must be called Mook duplicating will break this. 
        //Mook(1) to Mook 
        ///etc.
        if (collision.gameObject.layer == 6 && this.gameObject.name.ToString() == "Mook")
        {
            collision.gameObject.GetComponentInChildren<IEffectable>().ApplyEffect(effect);

        }

        if(collision.gameObject.layer == 6 && this.gameObject.name.ToString() == "Crushing")
        {
            collision.gameObject.GetComponent<IDamageable>().Damage(1, DamageType.Normal);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void ResetToPreviousState()
    {
        SetStateInternal(previousState);
    }


    //public IEnumerator HealCoroutine(HealSpellSO _heal)
    //{
    //    HealSpellSO heal = _heal;
    //    for (int i = heal.HealTicks; i > 0; i--)
    //    {
    //        enemyHealth.amount += heal.HealAmount;
    //        yield return new WaitForSeconds(heal.HealTickDelay);
    //    }
    //}

    //void ConeSpell(AreaSpellSO _spell)
    //{
    //    Vector2 origin = new(this.GetComponentInChildren<VariableGrabber>().AimingRet.transform.position.x, this.GetComponentInChildren<VariableGrabber>().AimingRet.transform.position.y);
    //    Collider2D[] hits = new Collider2D[_spell.TargetMax];
    //    int hitCount = Physics2D.OverlapCircleNonAlloc(origin, _spell.Length, hits);
    //    foreach (Collider2D hit in hits)
    //    {
    //        Vector3 angle = (hit.transform.position - this.GetComponentInChildren<VariableGrabber>().AimingRet.transform.position).normalized;
    //        float dot = Vector3.Dot(angle / 2, this.GetComponentInChildren<VariableGrabber>().AimingRet.transform.rotation.eulerAngles);
    //        if (dot >= Mathf.Cos(_spell.Width))
    //        {
    //            hit.GetComponent<IDamageable>().Damage(_spell.DamageAmount, _spell.DamageType);
    //            foreach (EffectSO effect in _spell.Effects)
    //            {
    //                hit.GetComponent<IEffectable>().ApplyEffect(effect);
    //            }
    //        }
    //    }
    //}
    public void Recharge(SpellSO _spell)
    {
        switch (_spell.RechargeType)
        {
            case RechargeType.Cooldown:
                if (_spell.CurrentCount >= _spell.MaxCount) { StartCoroutine(Cooldown(_spell)); }
                break;
            case RechargeType.ChargeUp:
                if (_spell.CurrentCount > 0)
                {
                    if (_spell.IsRecharging) return;
                    _spell.RechargeCurrent = 0f;
                    _spell.IsRecharging = true;
                    StartCoroutine(ChargeUp(_spell));
                }
                break;
            default:
                break;
        }
    }
    // IEnumerator AgeShield(GameObject _obj, ShieldSpellSO _spell)
    // {
    //     float timeElapsed = 0f;
    //     while (timeElapsed < _spell.Duration)
    //     {
    //         timeElapsed += Time.deltaTime;
    //         yield return new WaitForFixedUpdate();
    //     }
    //     Destroy(_obj);
    // }
    //public IEnumerator CastCone(AreaSpellSO _spell)
    // {
    //     int ticksDealt = 0;
    //     while (ticksDealt < _spell.SpellTicks)
    //     {
    //         ConeSpell(_spell);
    //         ticksDealt++;
    //         yield return new WaitForSeconds(_spell.TimeBetweenTicks);
    //     }
    // }

    IEnumerator Cooldown(SpellSO _spell)
    {
        _spell.RechargeCurrent = 0f;
        while (_spell.RechargeCurrent < _spell.RechargeAmount)
        {
            _spell.RechargeCurrent += Time.deltaTime /** resources.SpellHasteModifier*/;
            yield return null;
        }
        _spell.CurrentCount = 0f;
    }

    IEnumerator ChargeUp(SpellSO _spell)
    {
        while (_spell.RechargeCurrent < _spell.RechargeAmount)
        {
            _spell.RechargeCurrent += Time.deltaTime /** resources.SpellHasteModifier*/;
            yield return null;
        }
        _spell.CurrentCount -= 1f;
        _spell.IsRecharging = false;
        Recharge(_spell);
    }
    public IEnumerator ShootCoroutine(ProjectilesSpellSO _spell)
    {
        //ProjectilesSpellSO spell = _spell;
        for (int i = _spell.BlastsPerBurst; i > 0; i--)
        {
            Fire(_spell);
            yield return new WaitForSeconds(_spell.TimeBetweenBlasts);
        }
    }

    public IEnumerator HoldShootCoroutine(ProjectilesSpellSO _spell)
    {
        while (_spell.IsHeld)
        {
            Fire(_spell);
            yield return new WaitForSeconds(_spell.TimeBetweenBlasts);
        }
    }
    public void Fire(ProjectilesSpellSO _spell)
    {

        for (int b = 0; b < _spell.BulletsPerBlast; b++)
        {
            // Prep our Offset
            float offset = b * .5f * _spell.BurstPointsDist;
            if (b % 2 != 0) { offset *= -1f; }

            // get our start point
            Vector3 firePos = varGrabber.AimingRet.transform.position + offset * transform.up; // needs to account alternating up and down

            float skewRoll = UnityEngine.Random.Range(0, _spell.Skew) - _spell.Skew / 2;
            Quaternion skewQuaternion = Quaternion.Euler(varGrabber.AimingRet.gameObject.transform.rotation.eulerAngles.x, varGrabber.AimingRet.gameObject.transform.rotation.eulerAngles.y, varGrabber.AimingRet.gameObject.transform.rotation.eulerAngles.z + skewRoll);
            GameObject projectile = GameObject.Instantiate(_spell.Projectile, firePos, skewQuaternion);

            // Prep our Offset
            float burstSpread = b * _spell.BurstSpreadDist;
            // we want our burst spread to go from -.5X to .5X, instead of from 0 to X
            // so we need to take the first one, and subtract a certain number of burst spread
            burstSpread -= (_spell.BurstSpreadDist * _spell.BulletsPerBlast * .5f);

            // rotate the game object
            projectile.transform.Rotate(0, 0, burstSpread);
            //spell.transform.localEulerAngles = facing.localEulerAngles;
            projectile.tag = "Ebullet";

        }
    }

}
