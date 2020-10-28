using UnityEngine;
using UnityEngine.UI;

public class FPSBehaviour : UnityEngine.MonoBehaviour
{
    private Text _text;
    private float _waitTime = 0f;
    private int _fps = 0;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 0;
        }
        _waitTime -= Time.smoothDeltaTime;
        _fps++;
        if (!(_waitTime < 0)) return;
        _text.text = "FPS: " + _fps;
        _fps = 0;
        _waitTime = 1f;
    }
}