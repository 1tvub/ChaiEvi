using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private string gameSceneName = "GameScene";

    private const string VOLUME_KEY = "Volume";

    private void Start()
    {
        AudioManager.Olustur();

        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);

        startButton.onClick.AddListener(OnStartClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        backButton.onClick.AddListener(OnBackClicked);

        // Kaydedilmiş ses ayarını yükle
        volumeSlider.value = PlayerPrefs.GetFloat(VOLUME_KEY, 0.75f);
        AudioListener.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnSettingsClicked()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void OnBackClicked()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VOLUME_KEY, value);
    }
}
