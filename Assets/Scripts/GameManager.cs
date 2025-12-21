using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMove player;
    [SerializeField] private TMP_Text guide;

    private bool isFinished = false;

    public void FinishGame()
    {
        if (isFinished) return;

        isFinished = true;
        player.Finish();
        guide.text = "Success!!";
    }
}
