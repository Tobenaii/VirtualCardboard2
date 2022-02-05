using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRequirement : CardActionData
{
    [SerializeReference] private TargetingSystem _targetingSystems = new TargetingSystem();
    [SerializeReference] private List<RequirementResolver> _requirements = new List<RequirementResolver>();
    [SerializeReference] private List<CardActionData> _actionsWhenMet = new List<CardActionData>();

    public override void Execute(EntityInstance player)
    {
        var targets = _targetingSystems.Execute(player);
        foreach (var requirement in _requirements)
        {
            if (!requirement.CanResolve(targets))
                return;
        }

        foreach (var requirement in _requirements)
        {
            requirement.Resolve(targets);
        }
        ContinueExecution(player);
    }

    public void ContinueExecution(EntityInstance player)
    {
        foreach (var action in _actionsWhenMet)
            action.Execute(player);
    }
}