using System.Collections;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField] GameObject[] sectionsPrefabs;

    GameObject[] sectionsPool = new GameObject[20];
    GameObject[] sections = new GameObject[10];

    Transform playerCarTransform;

    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    const float sectionLength = 20f;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        CreatePool();
        SpawnInitialSections();

        StartCoroutine(UpdateLessOftenCO());
    }

    void CreatePool()
    {
        int prefabIndex = 0;
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;
            if (prefabIndex >= sectionsPrefabs.Length)
                prefabIndex = 0;
        }
    }

    void SpawnInitialSections()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject newSection = GetRandomAvailableSection();
            newSection.transform.position = new Vector3(0, 0, i * sectionLength);
            newSection.SetActive(true);
            sections[i] = newSection;
        }
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            if (sections[i].transform.position.z < playerCarTransform.position.z - sectionLength)
            {
                Vector3 oldPos = sections[i].transform.position;

                sections[i].SetActive(false);

                GameObject newSection = GetRandomAvailableSection();
                newSection.transform.position = new Vector3(0, 0, oldPos.z + sectionLength * sections.Length);
                newSection.SetActive(true);

                sections[i] = newSection;
            }
        }
    }

    GameObject GetRandomAvailableSection()
    {
        for (int attempt = 0; attempt < 30; attempt++)
        {
            int randomIndex = Random.Range(0, sectionsPool.Length);
            if (!sectionsPool[randomIndex].activeInHierarchy)
                return sectionsPool[randomIndex];
        }

        for (int i = 0; i < sectionsPool.Length; i++)
        {
            if (!sectionsPool[i].activeInHierarchy)
                return sectionsPool[i];
        }

        Debug.LogWarning("All sections are active! Returning a random one.");
        return sectionsPool[Random.Range(0, sectionsPool.Length)];
    }

    // ===== NEW RESET METHOD =====
    public void ResetLevel()
    {
        // Disable all sections
        foreach (var section in sectionsPool)
        {
            section.SetActive(false);
        }

        // Respawn initial sections
        SpawnInitialSections();

        // Reset car position to start
        if (playerCarTransform != null)
        {
            playerCarTransform.position = new Vector3(0, 0.5f, 0); // adjust Y if needed
            playerCarTransform.rotation = Quaternion.identity;
        }
    }
}
