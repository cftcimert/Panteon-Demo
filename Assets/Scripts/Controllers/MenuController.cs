using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] Text rankText;
    [SerializeField] Slider paintSlider;
    [SerializeField] GameObject starsParticle;

    public void ActivedGamePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ActivedPaintSlider()
    {
        paintSlider.gameObject.SetActive(true);
    }

    public void SetRankText(int rank)
    {
        rankText.text = "#" + rank;
    }

    public bool CheckSliderValue()
    {
        return paintSlider.value == paintSlider.maxValue;
    }

    public void SetSliderValue(float value)
    {
        paintSlider.value = value;
    }

    public void CreateStars(Vector3 startPoint)
    {
        Instantiate(starsParticle, startPoint, Quaternion.identity);
    }
}
