using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameStates : int
{
    Idle = 1,
    PlayingSequence = 2,
    ArrangeItem = 3,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameStates currentState;
    
    public List<Table> tables;
    public List<GameObject> items;
    public List<Material> colors;
    public List<AudioClip> tones;
    public AudioClip successSound;

    public List<Transform> spawnPoints;

    public int currentScore = 0;
    public List<ItemCode> trueSequence = new List<ItemCode>();

    public ScoreScreen scoreScreen;

    private AudioSource audioSource; 

    void Start()
    {
        currentState = GameStates.Idle;
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonPressed()
    {
        if (currentState == GameStates.Idle)
        {
            scoreScreen.UpdateScore(currentScore);
            currentState = GameStates.PlayingSequence;
            StartCoroutine(PlaySequence());
        }
    }

    public void OnEmergencyButtonPressed() { 
    
       if (currentState == GameStates.ArrangeItem)
       {
            currentScore = currentScore - 1;
            if (currentScore < 0)
            {
                currentScore = 0;
            }
            currentState = GameStates.Idle;
            OnButtonPressed();
       }
    }

    IEnumerator PlaySequence()
    {
        GenerateRandomSequence();

        foreach (Table table in tables)
        {
            table.SetColor(GetColorByItemCode(ItemCode.Black));
        }

        MoveItemsToSpawnPosition();
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < trueSequence.Count; i++)
        {
            ItemCode code = trueSequence[i];

            tables[i].SetCode(code);
            tables[i].SetColor(GetColorByItemCode(code));
            audioSource.clip = GetToneByCode(code);
            audioSource.Play();

            yield return new WaitForSeconds(1.0f);
            tables[i].SetColor(GetColorByItemCode(ItemCode.Black));
        }

        currentState = GameStates.ArrangeItem;        
    }

    void MoveItemsToSpawnPosition()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition = spawnPoints[i].position;
            items[i].transform.localRotation = Quaternion.identity;
        }
    }

    public Material GetColorByItemCode(ItemCode code)
    {
        return colors[((int)code)];
    }

    public AudioClip GetToneByCode(ItemCode code)
    {
        return tones[((int)code)];
    }

    void GenerateRandomSequence()
    {
        trueSequence.Clear();

        List<ItemCode> sequence = new List<ItemCode>();
        sequence.Add(ItemCode.Red); 
        sequence.Add(ItemCode.Green);
        sequence.Add(ItemCode.Blue);
        sequence.Add(ItemCode.Yellow);

        while(sequence.Count > 0)
        {
            int idx = Random.Range(0, sequence.Count);
            trueSequence.Add(sequence[idx]);
            sequence.RemoveAt(idx);
        }

    }
    public void CheckSequence()
    {
        bool success = true;

        foreach(Table table in tables)
        {
            if (!table.HasTheCorrectItem())
            {
                success = false;
            }
        }

        if (success)
        {
            audioSource.clip = successSound;
            audioSource.Play();

            foreach (Table table in tables)
            {
                table.SetColor(GetColorByItemCode(table.itemCode));
            }

            currentState = GameStates.Idle;

            currentScore += 1;

            scoreScreen.UpdateScore(currentScore);
        }
    }

}
