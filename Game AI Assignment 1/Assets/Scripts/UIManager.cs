using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public Text scoreText = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }

    public void UpdateScoreText() => scoreText.text = $"Score {GameManager.Instance.Score.ToString()}";
}
