using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCastleBehaviour : TutorialBehaviour, ITutorialAnalyticsEventSource
{
    [Header("Tutorial Objects")]
    [SerializeField] private TutorialConveyor _conveyor;
    [SerializeField] private TutorialBuyZone _tutorialBuyZone;
    [SerializeField] private TutorialRemoveFromStack _takeGears;
    [SerializeField] private TutorialDronePlatformEnter _tutorialDronePlatformEnter;
    [SerializeField] private TutorialRemoveFromStack _placeAmmo;

    public event Action<string> EventSended;

    public override void Initialize(Action onInitialized = null) => 
        onInitialized?.Invoke();

    protected override List<ITutorialObjectEventSource> GetTutorialObjectEventSources()
    {
        return new List<ITutorialObjectEventSource>()
            {_tutorialBuyZone, _conveyor, _takeGears, _tutorialDronePlatformEnter, _placeAmmo};
    }

    protected override ITutorialStep GetTutorialSequanceSteps()
    {
        var tutorialSequanceSteps = SequenceSteps.Create(
            SequenceSteps.Create(
                new PointerStep(_tutorialBuyZone.Point, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_tutorialBuyZone.Condition)
            ),
            SequenceSteps.Create(
                new PointerStep(_takeGears.Point, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_takeGears.RemoveFromStackCondition)
            ),
            SequenceSteps.Create(
                CreateOpenObjectSequanceSteps(_conveyor.GameObject, _conveyor.InPoint, _conveyor.CameraTrigger),
                new WaitConditionStep(_conveyor.InCondition),
                new PointerStep(null, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_conveyor.OutAddedCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_conveyor.OutPoint, ObjectArrow, PlayerArrow),
                new DelayStep(OpenDuration),
                new WaitConditionStep(_conveyor.OutRemovedCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_placeAmmo.Point, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_placeAmmo.RemoveFromStackCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_tutorialDronePlatformEnter.Point, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_tutorialDronePlatformEnter.Condition)
            ),

            new PointerStep(null, ObjectArrow, PlayerArrow),
            new ActionStep(() => EventSended?.Invoke("tutorial_complete"))
        );

        return tutorialSequanceSteps;
    }

    private SequenceSteps CreateOpenObjectSequanceSteps(GameObject gameObject, Transform point, string cameraTrigger)
    {
        return SequenceSteps.Create(
            new PointerStep(point, ObjectArrow, PlayerArrow),
            new DelayStep(DelayBeforeOpen),
            new OpenObjectStep(gameObject, OpenDuration),
            new DelayStep(DelayBeforeOpen)
        );
    }
}