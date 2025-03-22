using System.Collections.Generic;
using UnityEngine;

public delegate void MoveDelegate();

[CreateAssetMenu(menuName = "EnemyStates/MoveState")]
public class MoveSO : EnemyStateSO
{
    private Animator anim;
    private VariableGrabber varGrabber;
    private int currentPathIndex = 0;
    [SerializeField] float zigzagFrequency;
    [SerializeField] float zigzagAmplitude;
    [SerializeField] bool usesHiddenMovement;
    Transform targetEnemy;
    public enum MoveType
    {
        HiddenMovment,
        Rush,
        Retreat,
        ZigZag,
        Agress,
        HiddenRNG,
        Regroup,
        
    }

    public enum NextStateType
    {
        FirstAttack,
        SecondAttack,
        FirstMove,
        SecondMove,
    }
   
    [SerializeField] private MoveType moveType;
    [SerializeField] private NextStateType nextState;

    private Dictionary<MoveType, MoveDelegate> moveActions;

    public override void Enter()
    {
        anim = ESMM.GetComponentInParent<Animator>();
        varGrabber = ESMM.GetComponent<VariableGrabber>();

        moveActions = new Dictionary<MoveType, MoveDelegate>
        {
            { MoveType.HiddenMovment, HandleHiddenMove },
            { MoveType.Retreat, HandleRetreat },
            { MoveType.ZigZag, HandleZigZag },
            { MoveType.Agress, HandleAgressing },
            { MoveType.HiddenRNG, HandleHiddenMoveRandomPosition },
            {MoveType.Regroup, Regroup },
        };
    }
   
    float time;
    public override void Tick()
    {
        // Clearly check the distance to trigger attacks
        float playerDistance = Vector3.Distance(ESMM.transform.position, ESMM.Player.transform.position);
       
    

        time += Time.deltaTime;
        if (playerDistance <= ESMM.AttackRadius && time >= 3) //Maybe switch this out so ESMM takes a max time in states to pass through the system?
        {
            time = 0;
            HandleStateTransition();
            return;
        }
        if (playerDistance >= ESMM.AttackRadius && time >= 3) //Maybe switch this out so ESMM takes a max time in states to pass through the system?
        {
            time = 0;
            HandleStateTransition();
            return;
        }
        // Reset scale when out of attack radius
        ESMM.transform.localScale = Vector3.one;

        // Execute move logic clearly
        if (moveActions.TryGetValue(moveType, out MoveDelegate moveAction))
        {
            moveAction();
        }
        else
        {
            Debug.LogWarning("Unhandled move type: " + moveType);
        }
    }

    private void HandleStateTransition()
    {
        if (!ESMM.UsesSecondStates)
        {
            ESMM.ChangeState(EnemyStateType.Attack);
            return;
        }

        if (!ESMM.UsedFirstState)
        {
            // First move → first attack
            ESMM.ChangeState(EnemyStateType.Attack);
            ESMM.UsedFirstState = true;
        }
        else
        {
            // Second move → second attack
            ESMM.ChangeState(EnemyStateType.SecondAttack);
            ESMM.UsedFirstState = false;
        }
    }


    public override void Exit()
    {
      
            // Transition to first next state
            switch (nextState)
            {
                case NextStateType.FirstAttack:
                    ESMM.ChangeState(EnemyStateType.Attack);
                    break;
                case NextStateType.FirstMove:
                    ESMM.ChangeState(EnemyStateType.Move);
                    break;
                case NextStateType.SecondAttack:
                    ESMM.ChangeState(EnemyStateType.SecondAttack);
                    break;
                case NextStateType.SecondMove:
                    ESMM.ChangeState(EnemyStateType.SecondMove);
                    break;
            }
        
      
       
        if (anim != null)
            anim.enabled = false;
    }

    public void HandleRetreat()
    {
        ESMM.transform.position = Vector3.Lerp(
       ESMM.transform.position,
       -ESMM.Player.transform.position,
       ESMM.Speed * Time.deltaTime);
    }

    private void HandleHiddenMove()
    {
        if (varGrabber.ShadowCircle && varGrabber.AimingRet)
        {
            varGrabber.ShadowCircle.SetActive(true);
            varGrabber.AimingRet.SetActive(false);
        }

        ESMM.GetComponent<Renderer>().enabled = false;
        ESMM.GetComponent<BoxCollider2D>().enabled = false;

        ESMM.transform.position = Vector3.Lerp(
            ESMM.transform.position,
            ESMM.Player.transform.position,
            ESMM.Speed * Time.deltaTime
        );
    }

    private void HandleHiddenMoveRandomPosition()
    {
        float radius = 5f;
        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        ESMM.transform.position = ESMM.Player.transform.position + offset;

        ESMM.GetComponent<Renderer>().enabled = false;
        ESMM.GetComponent<BoxCollider2D>().enabled = false;
        var light = ESMM.GetComponentInChildren<Light>();
        if (light != null) light.enabled = false;
    }

    private void HandleAgressing()
    {
        ESMM.transform.position = Vector3.Lerp(
            ESMM.transform.position,
            ESMM.Player.transform.position,
            ESMM.Speed * Time.deltaTime
        );
        // Implement aggressive move logic here
    }

    private void HandleZigZag()
    {
        Vector3 dirToPlayer = (ESMM.Player.transform.position - ESMM.transform.position).normalized;
        Vector3 perpDir = Vector3.Cross(dirToPlayer, Vector3.up).normalized;

        ESMM.ZigzagTimer += Time.deltaTime;
        Vector3 zigzagOffset = perpDir * Mathf.Sin(ESMM.ZigzagTimer * zigzagFrequency) * zigzagAmplitude;

        Vector3 targetPosition = ESMM.transform.position + (dirToPlayer * ESMM.Speed * Time.deltaTime) + (zigzagOffset * Time.deltaTime);
        ESMM.transform.position = targetPosition;
    }

    void Regroup()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(ESMM.transform.position, 10);

        float closestDistance = float.MaxValue;
        targetEnemy = null;

        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.transform == ESMM.transform) continue; // Skip self

            float distance = Vector3.Distance(ESMM.transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy.transform;

                Vector3.Lerp(ESMM.gameObject.transform.position, targetEnemy.transform.position, distance);
            }
        }
    }

    
}
