using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestSO", menuName = "QuestSO")]
public class QuestSO : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;

    public List<QuestObjective> objectives;


}

[System.Serializable] 
public class QuestObjective
{
    public string description;

    [SerializeField] private Object target;
    public ActorSO targetNPC => target as ActorSO;
    public LocationSO targetLocation => target as LocationSO;
    public EnemySO targetEnemy => target as EnemySO;

    public int requiredAmount;
    public int amount;
}
