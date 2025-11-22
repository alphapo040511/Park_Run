using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Plane Settings")]
    [Range(4, 16)]public int planeCount = 12;         // 한 라인당 발판 개수 (4 ~ 16 까지 범위 제한)

    [Header("Line Settings")]
    [Min(1)]public int lineRadius = 3;
    public int defaultLineCount = 5;
    public int viewLineCount = 20;
    public PlaneLine lineReference;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float hidePoint = 10;

    [Header("Obstacle Settings")]
    public int tryLineCount = 15;
    public float generateRate = 0.3f;
    private int lastObstacleLine = 0;

    private Queue<PlaneLine> lineQueue = new Queue<PlaneLine>();
    private List<PlaneLine> activeLines = new List<PlaneLine>();

    public int totalLines { get; private set; } = 0;

    private bool initialized = false;

    private int score = 0;

    void Start()
    {
        Initialized();
        FirstLinesSetting();
    }

    private void Update()
    {
        if (PlayManager.instance.isPlaying == false)
        {
            if(Input.anyKeyDown && initialized)
            {
                PlayManager.instance.isPlaying = true;
            }
            return;
        }
        MoveLines();
        ReplacePlane();
    }

    void MoveLines()
    {
        List<PlaneLine> lineCopy = new List<PlaneLine>(activeLines.ToArray());

        foreach (var line in lineCopy)
        {
            line.transform.position -= transform.forward * moveSpeed * Time.deltaTime;

            if(line.transform.position.z <= -hidePoint)
            {
                activeLines.Remove(line);
                LineEnqueue(line);
                score++;
                PlayManager.instance.ScoreUpdate(score);
            }
        }
    }

    void ReplacePlane()
    {
        float width = PlaneLine.GetWidth(planeCount, lineRadius);

        if (activeLines.Count < viewLineCount && activeLines.Count > 0)
        {
            Vector3 targetPos = activeLines[^1].transform.position;
            targetPos += Vector3.forward * width;

            PlaneLine line = LineDequeue(targetPos);
            line.RandomPlane();

            activeLines.Add(line);
        }
    }

    void Initialized()
    {
        
        for(int i = 0; i < viewLineCount + 2; i++)      // 여유롭게 표시 개수보다 2개 더 많게 미리 생성
        {
            PlaneLine line = Instantiate(lineReference, Vector3.zero, Quaternion.identity);
            line.transform.SetParent(transform);
            line.InitializeLine(planeCount, lineRadius);
            line.gameObject.SetActive(false);
            lineQueue.Enqueue(line);
        }
    }

    public void FirstLinesSetting()
    {
        float width = PlaneLine.GetWidth(planeCount, lineRadius);
        for (int i = 0; i < viewLineCount; i++)
        {
            Vector3 targetPos = Vector3.forward * (i * width - hidePoint);      // 시작 타일은 조금 뒤에 배치

            PlaneLine line = LineDequeue(targetPos);

            if (i > defaultLineCount)
            {
                line.RandomPlane();
            }

            activeLines.Add(line);
        }

        initialized = true;
    }

    public PlaneLine LineDequeue(Vector3 position)
    {
        if (lineQueue.Count == 0) return null;

        PlaneLine line = lineQueue.Dequeue();


        if (totalLines > 0 && totalLines % 50 == 0)      // 50 칸마다
        {
            line.healPack.SetActive(true);
        }
        else if (lastObstacleLine + tryLineCount <= totalLines)     // 힐펙이 안 생긴 경우
        {
            if(Random.value < generateRate)
            {
                lastObstacleLine = totalLines;
                line.ActiveObstacle();
            }
        }



        line.Show(position, totalLines++);

        return line;
    }

    public void LineEnqueue(PlaneLine line)
    {
        line.Hide();
        line.ResetLine();
        lineQueue.Enqueue(line);
    }

    public int GetRound()
    {
        return (int)Mathf.Repeat(totalLines / 50, 3);
    }
}
