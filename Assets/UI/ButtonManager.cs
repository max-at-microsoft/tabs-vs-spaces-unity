using UnityEngine;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    private Button tabButton;
    private Button spaceButton;
    private Button closeButton;
    private Label tabScoreLabel;
    private Label spaceScoreLabel;
    private int tabScore = 0;
    private int spaceScore = 0;

     private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        tabButton = root.Q<Button>("tabButton");
        spaceButton = root.Q<Button>("spaceButton");
        closeButton = root.Q<Button>("closeButton");
        tabScoreLabel = root.Q<Label>("tabScore");
        spaceScoreLabel = root.Q<Label>("spaceScore");

        tabButton.clicked += OnTabButtonClicked;
        spaceButton.clicked += OnSpaceButtonClicked;
        closeButton.clicked += OnCloseButtonClicked;
    }

    private void OnTabButtonClicked()
    {
        OnButtonClicked("TabPrefab");
        UpdateScore(ref tabScore, tabScoreLabel, 4, "Tabs");
    }

    private void OnSpaceButtonClicked()
    {
        OnButtonClicked("SpacePrefab");
        UpdateScore(ref spaceScore, spaceScoreLabel, 1, "Spaces");
    }

    private void UpdateScore(ref int score, Label scoreLabel, int increment, string scoreType)
    {
        score += increment;
        scoreLabel.text = $"{scoreType}: {score}";
    }

    private void OnDisable()
    {
        tabButton.clicked -= OnTabButtonClicked;
        spaceButton.clicked -= OnSpaceButtonClicked;
        closeButton.clicked -= OnCloseButtonClicked;
    }

    private void OnButtonClicked(string prefabName)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab);
            Camera mainCamera = Camera.main;
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 1.0f;
            instance.transform.position = spawnPosition;
    
            Rigidbody rb = instance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = mainCamera.transform.forward * 1000; // Adjust the force as needed
                rb.AddForce(force);
    
                // Add a random spin
                Vector3 randomSpin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                rb.AddTorque(randomSpin * 20f, ForceMode.Impulse); // Adjust the torque as needed
            }
        }
        else
        {
            Debug.LogError($"{prefabName} not found in Resources folder");
        }
    }

    private void OnCloseButtonClicked()
    {
        Debug.Log("Close");
    }
}