using UnityEngine;

public class SceneLoadInteraction : TriggerInteractionBase
{

    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }


    [Header("Spawn TO")]
    [SerializeField] private DoorToSpawnAt DoorToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("THIS Door")]
    public DoorToSpawnAt CurrentDoorPosition;

    public override void Interact()
    {
        SceneSwapManager.SwapSceneFromDoorUse(_sceneToLoad,DoorToSpawnTo);
    }
}
