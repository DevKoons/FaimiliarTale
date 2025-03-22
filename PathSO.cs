using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Creates an Empty Method placeholder that can accept anything of the signature void.

//We don't reenable ourselves from HiddenMove state even switching to the attack state.
public delegate void PathDelegate();

[CreateAssetMenu(menuName = "EnemyStates/PathState")]
public class PathSO : EnemyStateSO
{
    [SerializeField] private bool usesHiddenMovement;

    private Animator anim;
    private VariableGrabber varGrabber;
    private int currentPathIndex = 0;

    //our enums to set in editor to choose our pathing type the unit will take, it also ties into our delegation by being a Key placeholder to pass into the delegation as a value
    public enum PathingType
    {
        Path,
        Roam,
        Look,
        Approach,
    }
    public enum NextStateType
    {
        defense,
        move,
        secondmove,
        secondAttack,
    }

    [SerializeField] private PathingType pathingType;
    [SerializeField] private NextStateType nextState;

    //Create a dictionary of our pathing type enums, that type being the pathingtype key that passed into our delgation as a value
    private Dictionary<PathingType, PathDelegate> pathingActions;


    //Create an action for the system to hold.
    Action switchpathingAction;
    public override void Enter()
    {
        //give the action a paramater of void and have it wait until a condition is met, once the condition is met the Lambda occurs
        switchpathingAction = () =>
        {
            if (ESMM.GetComponent<EnemyHealth>().Health <= 10)
            {
                
                Debug.Log("This Lambda expression has worked" + "Congradulations!");
            }
        };

        

        //start a new instance of dictionary and set the proper pathing types (enum "states") and values (Methods to pass in)
        pathingActions = new Dictionary<PathingType, PathDelegate>
        {
            { PathingType.Path, HandlePathing },
            { PathingType.Roam, HandleRoaming},
            {PathingType.Look, HandleLooking},
            {PathingType.Approach,HandleApproach },
             
        };

        anim = ESMM.GetComponentInParent<Animator>();
        varGrabber = ESMM.GetComponent<VariableGrabber>();

        if (usesHiddenMovement)
        {
            varGrabber.ShadowCircle.gameObject.SetActive(true);
            varGrabber.AimingRet.gameObject.SetActive(false);
            ESMM.GetComponent<Renderer>().enabled = false;
            ESMM.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    public override void Tick()
    {
        //Test lambda activation, If we have an action, invoke that action.
        switchpathingAction?.Invoke();

        //grab our pathing actions try to get a pathing action of any type as long as it's a pathing type, push that out to our delegate as an action to pass into our system.
        //The system will check all pathingTypes and assign the one passed in from the values selected in editor.
        if(pathingActions.TryGetValue(pathingType, out PathDelegate pathingAction))
        {
            pathingAction();

           
        }
        else
        {
            Debug.LogWarning("UnHandled pathing type: " + pathingType);
        }

        if(Vector3.Distance(ESMM.gameObject.transform.position, ESMM.Player.gameObject.transform.position) <= ESMM.AttackRadius)
        {
            Exit();
        }
    }
    public override void Exit()
    {
      
        varGrabber = ESMM.GetComponent<VariableGrabber>();

        if (varGrabber.AimingRet != null)
        {
            varGrabber.AimingRet.gameObject.SetActive(true);

        }
        ESMM.GetComponent<SpriteRenderer>().enabled = true;
        ESMM.GetComponent<Collider2D>().enabled = true;
        ESMM.ChangeState(EnemyStateType.Attack);

        if (anim != null)
        {
            anim.enabled = false;
        }
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
        }
    }

    private void HandlePathing()
    {
        if (ESMM.PathPoints == null || ESMM.PathPoints.Length == 0)
        {
            Debug.LogWarning("No path points assigned for pathing.");
            return;
        }

        // Find the nearest path point if off course
        if (currentPathIndex < 0 || currentPathIndex >= ESMM.PathPoints.Length)
        {
            currentPathIndex = FindClosestPathIndex();
        }

        Vector3 targetPosition = ESMM.PathPoints[currentPathIndex].transform.position;

        // Move towards the target point
        ESMM.transform.position = Vector3.MoveTowards(
            ESMM.transform.position,
            targetPosition,
            ESMM.Speed * Time.deltaTime
        );

        // Check if the AI reached the target position before switching to the next waypoint
        if (Vector3.Distance(ESMM.transform.position, targetPosition) < 0.5f)
        {
            currentPathIndex = (currentPathIndex + 1) % ESMM.PathPoints.Length; // Loop through path points
        }

        // Exit if within attack range
        if (Vector3.Distance(ESMM.transform.position, ESMM.Player.transform.position) <= ESMM.AttackRadius)
        {
            Exit();
        }
    }

    // Finds the closest path index to the AI's position if it's off track
    private int FindClosestPathIndex()
    {
        int closestIndex = 0;
        float minDistance = float.MaxValue;

        for (int i = 0; i < ESMM.PathPoints.Length; i++)
        {
            float distance = Vector3.Distance(ESMM.transform.position, ESMM.PathPoints[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
    public void HandleApproach()
    {
            ESMM.gameObject.transform.position = Vector3.Lerp(ESMM.gameObject.transform.position, ESMM.Player.gameObject.transform.position, .75f * Time.deltaTime);
    }

    private void HandleRoaming()
    {
        if (currentPathIndex < 0 || currentPathIndex >= ESMM.PathPoints.Length)
        {
            currentPathIndex = 0;
        }

        for (int i = 0; i < ESMM.PathPoints.Length; i++)
        {
            Vector3 targetPosition = ESMM.PathPoints[UnityEngine.Random.Range(0, ESMM.PathPoints.Length)].transform.position;

            if (Vector2.Distance(ESMM.transform.position, targetPosition) < 1f)
            {
                currentPathIndex = (currentPathIndex + 1) % ESMM.PathPoints.Length; // Loop through path points
            }
         
            ESMM.transform.position = Vector3.MoveTowards(
                ESMM.transform.position,
                targetPosition,
                ESMM.Speed * Time.deltaTime
            );
        }

        Debug.Log("Roaming");

        if (Vector2.Distance(ESMM.transform.position, ESMM.Player.transform.position) <= ESMM.AttackRadius)
        {
            Exit();
        }
    }

    private void HandleLooking()
    {
        Debug.Log("Handling Looking Logic");
        // Implement specific logic for Looking
    }
}
