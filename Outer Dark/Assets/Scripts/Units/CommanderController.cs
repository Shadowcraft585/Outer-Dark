using UnityEngine;

public class CommanderController : MonoBehaviour
{
    public CommanderView SelectedCommanderView;

    public void SelectCommander(CommanderView commanderView)
    {
        SelectedCommanderView = commanderView;
    }

    public void MoveSelectedCommander(Vector2Int targetGridPosition,
        TerrainType terrain)
    {
        if (SelectedCommanderView == null) return;

        CommanderUnit commander = SelectedCommanderView.Data;
        int moveCost = MovementCostCalculator.GetCost(terrain);

        if (moveCost > commander.CurrentMovementPoints) return;

        commander.Move(targetGridPosition, moveCost);
        SelectedCommanderView.SyncPosition();
    }
}