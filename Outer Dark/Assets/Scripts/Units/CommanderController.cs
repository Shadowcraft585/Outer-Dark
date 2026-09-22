using UnityEngine;

public class CommanderController : MonoBehaviour
{
    public CommanderView SelectedCommanderView;

    public void SelectCommander(CommanderView commanderView)
    {
        SelectedCommanderView = commanderView;
    }

    public void ClearSelection()
    {
        SelectedCommanderView = null;
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

    public void MoveSelectedCommanderToward(Vector2Int targetGridPosition,
        TerrainType terrain)
    {
        if (SelectedCommanderView == null) return;

        CommanderUnit commander = SelectedCommanderView.Data;
        int moveCost = MovementCostCalculator.GetCost(terrain);

        while (commander.Position != targetGridPosition &&
            commander.CurrentMovementPoints >= moveCost)
        {
            Vector2Int nextPosition = commander.Position;
            Vector2Int distance = targetGridPosition - commander.Position;

            if (distance.x != 0)
            {
                nextPosition.x += distance.x > 0 ? 1 : -1;
            }
            else if (distance.y != 0)
            {
                nextPosition.y += distance.y > 0 ? 1 : -1;
            }

            commander.Move(nextPosition, moveCost);
        }

        SelectedCommanderView.SyncPosition();
    }
}