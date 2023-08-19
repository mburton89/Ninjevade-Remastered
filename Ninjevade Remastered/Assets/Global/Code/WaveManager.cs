using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    int currentWave;
    [SerializeField] TextMeshProUGUI currentWaveText;

    int numberOfNinjasRemainingInWave;
    [SerializeField] TextMeshProUGUI numberOfNinjasText;

    public float initialNinjaRunSpeed;
    [SerializeField] List<float> ninjaRunSpeedAddifier;
    [HideInInspector] public float currentNinjaRunSpeed;

    [HideInInspector] public bool waveComplete;

    private void Awake()
    {
        Instance = this;
        EstablishWave();

    }

    void Start()
    {
        //PlayerPrefs.SetInt("WaveToLoad", 0);
    }

    public void EstablishWave()
    {
        currentWave = PlayerPrefs.GetInt("WaveToLoad");
        currentWaveText.SetText("Wave: " + currentWave);
        currentNinjaRunSpeed = initialNinjaRunSpeed + ninjaRunSpeedAddifier[currentWave];

        numberOfNinjasRemainingInWave = (int)currentNinjaRunSpeed + (int)ninjaRunSpeedAddifier[currentWave];
        numberOfNinjasText.SetText("Ninjas Remaining: " + numberOfNinjasRemainingInWave);
    }

    public void HandleNinjaKilled()
    {
        numberOfNinjasRemainingInWave--;
        numberOfNinjasText.SetText("Ninjas Remaining: " + numberOfNinjasRemainingInWave);

        if (numberOfNinjasRemainingInWave <= 0)
        {
            HandleWaveComplete();
        }
    }

    public void HandleWaveComplete()
    {
        waveComplete = true;
        currentWaveText.SetText("Wave " + currentWave + " complete!");
        PlayerPrefs.SetInt("WaveToLoad", currentWave + 1);
	StartCoroutine(LoadNextWave());
    }

    private IEnumerator LoadNextWave()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
