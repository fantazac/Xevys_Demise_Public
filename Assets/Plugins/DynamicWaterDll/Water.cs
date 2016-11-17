//Source du code : https://github.com/tutsplus/unity-2d-water-effect/

using UnityEngine;

/// <summary>
/// Cette classe gère tout le visuel du mouvement de l'eau en plus de la créer.
/// </summary>
public class Water : MonoBehaviour
{
    [Header("Physics Constants")]
    [SerializeField]
    private float SPRING_CONSTANT = 0.02f;
    [SerializeField]
    private float DAMPING = 0.1f;
    [SerializeField]
    private float SPREAD = 0.04f;
    [SerializeField]
    private float Z_POS = -1f;

    //L'objet visuel qu'on utilise pour le mesh.
    [Header("Visual Element")]
    [SerializeField]
    private GameObject _watermesh;

    [Header("Area Element")]
    [SerializeField]
    private GameObject _areaDimensions;

    //Tableaux pour les éléments physiques.
    private float[] _xpositions;
    private float[] _ypositions;
    private float[] _velocities;
    private float[] _accelerations;

    //Tableaux pour les mesh et les boîtes de collisions.
    private GameObject[] _meshobjects;
    private GameObject[] _colliders;
    public GameObject[] Colliders { get { return _colliders; } }
    private Mesh[] _meshes;

    //Les propriétés de notre eau.
    private float _baseheight;
    private float _bottom;


    private void Start()
    {
        if (_areaDimensions != null)
        {
            Transform waterDimensionStart = _areaDimensions.transform.GetChild(0);
            Transform waterDimensionEnd = _areaDimensions.transform.GetChild(1);

            SpawnWater(waterDimensionStart.position.x,
                       waterDimensionEnd.position.x - waterDimensionStart.position.x,
                       waterDimensionStart.position.y,
                       waterDimensionEnd.position.y);
        }
    }

    private void SpawnWater(float Left, float Width, float Top, float Bottom)
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().transform.position = new Vector3(Left + Width / 2, Top + (Bottom - Top) / 2, transform.position.z);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Width, Top - Bottom);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        //Calculer le nombre de coins et de segment[s].
        int edgecount = Mathf.RoundToInt(Width) * 5;
        int nodecount = edgecount + 1;

        _xpositions = new float[nodecount];
        _ypositions = new float[nodecount];
        _velocities = new float[nodecount];
        _accelerations = new float[nodecount];

        _meshobjects = new GameObject[edgecount];
        _meshes = new Mesh[edgecount];
        _colliders = new GameObject[edgecount];

        _baseheight = Top;
        _bottom = Bottom;

        for (int i = 0; i < nodecount; i++)
        {
            _ypositions[i] = Top;
            _xpositions[i] = Left + Width * i / edgecount;
            _accelerations[i] = 0;
            _velocities[i] = 0;
        }

        for (int i = 0; i < edgecount; i++)
        {
            _meshes[i] = new Mesh();

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(_xpositions[i], _ypositions[i], Z_POS);
            Vertices[1] = new Vector3(_xpositions[i + 1], _ypositions[i + 1], Z_POS);
            Vertices[2] = new Vector3(_xpositions[i], _bottom, Z_POS);
            Vertices[3] = new Vector3(_xpositions[i + 1], _bottom, Z_POS);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            _meshes[i].vertices = Vertices;
            _meshes[i].uv = UVs;
            _meshes[i].triangles = tris;

            _meshobjects[i] = Instantiate(_watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            _meshobjects[i].GetComponent<MeshFilter>().mesh = _meshes[i];
            _meshobjects[i].transform.parent = transform;

            _colliders[i] = new GameObject();
            _colliders[i].name = "Trigger";
            _colliders[i].AddComponent<BoxCollider2D>();
            _colliders[i].transform.parent = transform;

            _colliders[i].transform.position = new Vector3(Left + Width * (i + 0.5f) / edgecount, Top - 0.5f, 0);
            _colliders[i].transform.localScale = new Vector3(Width / edgecount, 1, 1);

            _colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
        }




    }

    /// <summary>
    /// Cette méthode s'occupe de transférer l'énergie (velocity) du corp qui entre dans l'eau au segment de la surface de l'eau qu'il a touché.
    /// </summary>
    /// <param name="xpos">La position en "x" du corp qui entre dans l'eau</param>
    /// <param name="velocity">La vitesse à laquelle le corp entre dans l'eau</param>
    public void Splash(float xpos, float velocity)
    {
        if (xpos >= _xpositions[0] && xpos <= _xpositions[_xpositions.Length - 1])
        {
            xpos -= _xpositions[0];
            int index = Mathf.RoundToInt((_xpositions.Length - 1) * (xpos / (_xpositions[_xpositions.Length - 1] - _xpositions[0])));
            _velocities[index] += velocity;
        }
    }

    private void UpdateMeshes()
    {
        for (int i = 0; i < _meshes.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(_xpositions[i], _ypositions[i], Z_POS);
            Vertices[1] = new Vector3(_xpositions[i + 1], _ypositions[i + 1], Z_POS);
            Vertices[2] = new Vector3(_xpositions[i], _bottom, Z_POS);
            Vertices[3] = new Vector3(_xpositions[i + 1], _bottom, Z_POS);

            _meshes[i].vertices = Vertices;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _xpositions.Length; i++)
        {
            float force = SPRING_CONSTANT * (_ypositions[i] - _baseheight) + _velocities[i] * DAMPING;
            _accelerations[i] = -force;
            _ypositions[i] += _velocities[i];
            _velocities[i] += _accelerations[i];
        }

        float[] leftDeltas = new float[_xpositions.Length];
        float[] rightDeltas = new float[_xpositions.Length];

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < _xpositions.Length; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = SPREAD * (_ypositions[i] - _ypositions[i - 1]);
                    _velocities[i - 1] += leftDeltas[i];
                }
                if (i < _xpositions.Length - 1)
                {
                    rightDeltas[i] = SPREAD * (_ypositions[i] - _ypositions[i + 1]);
                    _velocities[i + 1] += rightDeltas[i];
                }
            }

            for (int i = 0; i < _xpositions.Length; i++)
            {
                if (i > 0)
                    _ypositions[i - 1] += leftDeltas[i];
                if (i < _xpositions.Length - 1)
                    _ypositions[i + 1] += rightDeltas[i];
            }
        }
        UpdateMeshes();
    }

    private void OnDrawGizmos()
    {
        if (_areaDimensions == null)
            return;

        //Affiche les bordures de notre eau dans l'éditeur.
        Transform waterDimensionStart = _areaDimensions.transform.GetChild(0);
        Transform waterDimensionEnd = _areaDimensions.transform.GetChild(1);

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(new Vector2(waterDimensionStart.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionEnd.position.x, waterDimensionStart.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionEnd.position.x, waterDimensionEnd.position.y), new Vector2(waterDimensionStart.position.x, waterDimensionEnd.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionStart.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionStart.position.x, waterDimensionEnd.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionEnd.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionEnd.position.x, waterDimensionEnd.position.y));
    }
}