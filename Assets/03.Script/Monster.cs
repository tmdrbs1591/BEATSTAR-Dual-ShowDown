using UnityEngine;

public class MonsterSpawner : MonoBehaviour // 코드 삭제 예정
{
    public GameObject monsterPrefab; // 생성할 몬스터 프리팹
    double currentTime = 0d;
    int noteCount = 0; // 생성된 노트의 수

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
        // 노트의 생성 간격에 따라 몬스터의 생성 위치 설정
        NoteManager noteManager = FindObjectOfType<NoteManager>();
        if (noteManager != null)
        {
            float bpm = noteManager.bpm;
            double beatInterval = 60d / bpm;

            currentTime += Time.deltaTime;

            if (noteCount < 16) // 처음 16개의 노트는 2박자로 생성
            {
                if (currentTime >= beatInterval * 1.295f)
                {
                    SpawnMonster(RandomPosition());
                    currentTime -= beatInterval * 1.295f;
                    noteCount++;
                }
            }
            else if (noteCount < 19) // 16개 이후 4박자로 3개 생성
            {
                if (currentTime >= beatInterval)
                {
                    SpawnMonster(RandomPosition());
                    currentTime -= beatInterval;
                    noteCount++;
                }
                else if (noteCount < 23) // 19개 이후 4박자로 4개 생성
                {
                    if (currentTime >= beatInterval / 1.7f)
                    {
                        SpawnMonster(RandomPosition());
                        currentTime -= beatInterval / 1.7f;
                        noteCount++;
                    }
                }
                else if (noteCount < 26) // 23개 이후 4박자로 3개 생성
                {
                    if (currentTime >= beatInterval * 0.9f)
                    {
                        SpawnMonster(RandomPosition());
                        currentTime -= beatInterval * 0.9f;
                        noteCount++;
                    }
                }
                else if (noteCount < 30) // 26개 이후 4박자로 4개 생성
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
            // 몬스터 생성
            Instantiate(monsterPrefab, position.position, Quaternion.identity);
        }

        Transform RandomPosition()
        {
            // 랜덤한 위치 선택
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
                    return QPos; // 기본적으로 QPos를 반환하도록 설정
            }
        }
    }
}