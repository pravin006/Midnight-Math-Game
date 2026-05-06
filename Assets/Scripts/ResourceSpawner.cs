using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnArea
    {
        public Vector2 min; // (xMin, zMin)
        public Vector2 max; // (xMax, zMax)
    }

    [System.Serializable]
    public struct SpawnOption
    {
        public GameObject prefab;
        [Range(0f, 100f)] public float chance; // percent, relative weight
        public bool alignToNormal; // optional for rocks/trees on slopes
    }

    [Header("Spawn options (set chances like Tree=30, Box=2)")]
    public SpawnOption[] options;

    [Header("Overall chance to spawn anything at a point")]
    [Range(0f, 100f)] public float spawnAnyChance = 40f;

    [Header("Raycast setup")]
    public float distanceBetweenCheck = 5f;
    public float heightOfCheck = 100f;
    public float rangeOfCheck = 200f;
    public LayerMask layerMask;

    [Header("Areas")]
    public SpawnArea[] areas;

    private void Start()
    {
        SpawnResources();
    }

    void SpawnResources()
    {
        if (options == null || options.Length == 0) return;
        if (areas == null || areas.Length == 0) return;

        foreach (var area in areas)
        {
            float xMin = Mathf.Min(area.min.x, area.max.x);
            float xMax = Mathf.Max(area.min.x, area.max.x);
            float zMin = Mathf.Min(area.min.y, area.max.y);
            float zMax = Mathf.Max(area.min.y, area.max.y);

            for (float x = xMin; x < xMax; x += distanceBetweenCheck)
            {
                for (float z = zMin; z < zMax; z += distanceBetweenCheck)
                {
                    // Roll: spawn nothing most of the time
                    if (Random.value * 100f >= spawnAnyChance)
                        continue;

                    // One raycast per point
                    if (!Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down,
                        out RaycastHit hit, rangeOfCheck, layerMask))
                        continue;

                    // Pick which thing to spawn (tree vs box) by weighted chance
                    var chosen = ChooseOption(options);
                    if (chosen.prefab == null) continue;

                    Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                    if (chosen.alignToNormal)
                    {
                        // aligns "up" to the ground normal (good for rocks), keeps random yaw
                        rot = Quaternion.FromToRotation(Vector3.up, hit.normal) * rot;
                    }

                    Instantiate(chosen.prefab, hit.point, rot, transform);
                }
            }
        }
    }

    static SpawnOption ChooseOption(SpawnOption[] opts)
    {
        float total = 0f;
        for (int i = 0; i < opts.Length; i++)
            total += Mathf.Max(0f, opts[i].chance);

        if (total <= 0f) return default;

        float r = Random.value * total;
        for (int i = 0; i < opts.Length; i++)
        {
            float c = Mathf.Max(0f, opts[i].chance);
            r -= c;
            if (r <= 0f) return opts[i];
        }

        return opts[opts.Length - 1];
    }

}