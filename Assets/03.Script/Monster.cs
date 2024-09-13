using UnityEngine;

public class MonsterSpawner : MonoBehaviour // �ڵ� ���� ����
{
    public GameObject monsterPrefab; // ������ ���� ������
    double currentTime = 0d;
    int noteCount = 0; // ������ ��Ʈ�� ��

    public Transform QPos;
    public Transform WPos;
    public Transform EPos;

    enum BeatType
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    void Update()
    {
        // ��Ʈ�� ���� ���ݿ� ���� ������ ���� ��ġ ����
        NoteManager noteManager = FindObjectOfType<NoteManager>();
        if (noteManager != null)
        {
            float bpm = noteManager.bpm;
            double beatInterval = 60d / bpm;

            currentTime += Time.deltaTime;

            if (noteCount < 16) // ó�� 16���� ��Ʈ�� 2���ڷ� ����
            {
                if (currentTime >= beatInterval * 1.295f)
                {
                    SpawnMonster(RandomPosition());
                    currentTime -= beatInterval * 1.295f;
                    noteCount++;
                }
            }
            else if (noteCount < 19) // 16�� ���� 4���ڷ� 3�� ����
            {
                if (currentTime >= beatInterval)
                {
                    SpawnMonster(RandomPosition());
                    currentTime -= beatInterval;
                    noteCount++;
                }
                else if (noteCount < 23) // 19�� ���� 4���ڷ� 4�� ����
                {
                    if (currentTime >= beatInterval / 1.7f)
                    {
                        SpawnMonster(RandomPosition());
                        currentTime -= beatInterval / 1.7f;
                        noteCount++;
                    }
                }
                else if (noteCount < 26) // 23�� ���� 4���ڷ� 3�� ����
                {
                    if (currentTime >= beatInterval * 0.9f)
                    {
                        SpawnMonster(RandomPosition());
                        currentTime -= beatInterval * 0.9f;
                        noteCount++;
                    }
                }
                else if (noteCount < 30) // 26�� ���� 4���ڷ� 4�� ����
                {
                    if (currentTime >= beatInterval / 1.6f)
                    {
                        SpawnMonster(RandomPosition());
                        currentTime -= beatInterval / 1.6f;
                        noteCount++;
                    }
                }
            }
        }

        void SpawnMonster(Transform position)
        {
            // ���� ����
            Instantiate(monsterPrefab, position.position, Quaternion.identity);
        }

        Transform RandomPosition()
        {
            // ������ ��ġ ����
            int randomIndex = Random.Range(0, 3);
            switch (randomIndex)
            {
                case 0:
                    return QPos;
                case 1:
                    return WPos;
                case 2:
                    return EPos;
                default:
                    return QPos; // �⺻������ QPos�� ��ȯ�ϵ��� ����
            }
        }
    }
}