using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NextTurnButton : MonoBehaviour
{
    public Button NextTurnBtn;
    public Button CreateNewGameButton;
    public Canvas GameRel;
    public TMP_Dropdown GameDrop;
    public TMP_Text moneyText;
    public TMP_Text turnText;
    public GameObject GameLogicObject;
    public Canvas ProgressBarCanvas;
    private bool isProcessing = false;
    void Start()
    {
        GameLogic logic = GameLogicObject.GetComponent<GameLogic>();
        Button btn = NextTurnBtn.GetComponent<Button>();
        btn.onClick.AddListener(delegate { OnClick(logic); });
    }
    public void OnClick(GameLogic logic)
    {
        if (isProcessing)
        {
            return;
        }

        // Start the turn process as a fire-and-forget task
        ProcessTurnAsync(logic).Forget();
    }
    private async UniTaskVoid ProcessTurnAsync(GameLogic logic)
    {
        if (isProcessing) return;
        isProcessing = true;
        try
        {
            if (!logic.IsNewTurnCalculationCompleted())
            {
                ProgressBarCanvas.enabled = true;
            }
            await UniTask.WaitUntil(() => logic.IsNewTurnCalculationCompleted(),
                                   PlayerLoopTiming.Update,
                                   cancellationToken: this.GetCancellationTokenOnDestroy());
            ProgressBarCanvas.enabled = false;
            GameLogic.turn++;
            await logic.ConsumeNewTurnCalculation();
            logic.UpdateMoney(-GameLogic.costPerTurn, "Maintenance");
            logic.UpdateMoneyTurnText();
            logic.GenerateIncome();
            if (GameLogic.isGameInProgress)
            {
                GameLogic.gameInProgress[5] = (Convert.ToInt32(GameLogic.gameInProgress[5]) - 1).ToString();
                for (int i = 1; i <= 3; i++)
                {
                    ExpGain(GameLogic.atributeId[GameLogic.gameInProgress[i]]);
                }
            }
            if (GameLogic.isGameInProgress && Convert.ToInt32(GameLogic.gameInProgress[5]) <= 0)
            {
                logic.GameReleased();
            }
            logic.IncomeCostTextGenerator();
            logic.StartNewTurnCalculation().Forget();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Turn processing failed: {ex.Message}");
        }
        finally
        {
            isProcessing = false;
            ProgressBarCanvas.enabled = false; // Ensure hidden on error or completion
        }
    }
    public void ExpGain(int atributeValue)
    {
        string[] aAttribute = GameLogic.employees[0, atributeValue].Split(".");
        int expGenre = (10 - Convert.ToInt32(aAttribute[0])) * 5;
        int level = Convert.ToInt32(aAttribute[0]);
        int percent = (Convert.ToInt32(aAttribute[1]) + expGenre);
        if (percent >= 100)
        {
            level += 1;
            percent -= 100;
        }
        GameLogic.employees[0, atributeValue] = level.ToString() + "." + percent.ToString();
    }
}
