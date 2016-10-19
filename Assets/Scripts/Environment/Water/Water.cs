using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
    [Header("Disposition")]
    [SerializeField]
    private bool _isUnderAnotherWaterObject = false;

    [Header("Physics Constants")]
    [SerializeField]
    private float SPRING_CONSTANT = 0.02f;
    [SerializeField]
    private float DAMPING = 0.1f;
    [SerializeField]
    private float SPREAD = 0.04f;
    [SerializeField]
    private float Z_POS = -1f;

    //The GameObject we're using for a mesh
    [Header("Visual Element")]
    [SerializeField]
    private GameObject _watermesh;

    [Header("Area Element")]
    [SerializeField]
    private GameObject _areaDimensions;

    //Our physics arrays
    private float[] _xpositions;
    private float[] _ypositions;
    private float[] _velocities;
    private float[] _accelerations;

    //Our meshes and colliders
    private GameObject[] _meshobjects;
    private GameObject[] _colliders;
    private Mesh[] _meshes;

    //The properties of our water
    private float _baseheight;
    private float _bottom;


    void Start()
    {
        if (_areaDimensions != null)
        {
            Transform waterDimensionStart = _areaDimensions.transform.GetChild(0);
            Transform waterDimensionEnd = _areaDimensions.transform.GetChild(1);

            //Spawning our water
            SpawnWater(waterDimensionStart.position.x,
                       waterDimensionEnd.position.x - waterDimensionStart.position.x,
                       waterDimensionStart.position.y,
                       waterDimensionEnd.position.y);
        }
    }

    public void SpawnWater(float Left, float Width, float Top, float Bottom)
    {
        //Bonus exercise: Add a box collider to the water that will allow things to float in it.
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().transform.position = new Vector3(Left + Width / 2, Top + (Bottom - Top) / 2, transform.position.z);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Width, Top - Bottom);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;


        //Calculating the number of edges and nodes we have
        int edgecount = Mathf.RoundToInt(Width) * 5;
        int nodecount = edgecount + 1;

        //Declare our physics arrays
        _xpositions = new float[nodecount];
        _ypositions = new float[nodecount];
        _velocities = new float[nodecount];
        _accelerations = new float[nodecount];

        //Declare our mesh arrays
        _meshobjects = new GameObject[edgecount];
        _meshes = new Mesh[edgecount];
        _colliders = new GameObject[edgecount];

        //Set our variables
        _baseheight = Top;
        _bottom = Bottom;

        //For each node, set the line renderer and our physics arrays
        for (int i = 0; i < nodecount; i++)
        {
            _ypositions[i] = Top;
            _xpositions[i] = Left + Width * i / edgecount;
            _accelerations[i] = 0;
            _velocities[i] = 0;
        }

        //Setting the meshes now:
        for (int i = 0; i < edgecount; i++)
        {
            //Make the mesh
            _meshes[i] = new Mesh();

            //Create the corners of the mesh
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(_xpositions[i], _ypositions[i], Z_POS);
            Vertices[1] = new Vector3(_xpositions[i + 1], _ypositions[i + 1], Z_POS);
            Vertices[2] = new Vector3(_xpositions[i], _bottom, Z_POS);
            Vertices[3] = new Vector3(_xpositions[i + 1], _bottom, Z_POS);

            //Set the UVs of the texture
            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //Set where the triangles should be.
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            //Add all this data to the mesh.
            _meshes[i].vertices = Vertices;
            _meshes[i].uv = UVs;
            _meshes[i].triangles = tris;

            //Create a holder for the mesh, set it to be the manager's child
            _meshobjects[i] = Instantiate(_watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            _meshobjects[i].GetComponent<MeshFilter>().mesh = _meshes[i];
            _meshobjects[i].transform.parent = transform;

            //Create our colliders, set them be our child
            _colliders[i] = new GameObject();
            _colliders[i].name = "Trigger";
            _colliders[i].AddComponent<BoxCollider2D>();
            _colliders[i].transform.parent = transform;

            //Set the position and scale to the correct dimensions
            _colliders[i].transform.position = new Vector3(Left + Width * (i + 0.5f) / edgecount, Top - 0.5f, 0);
            _colliders[i].transform.localScale = new Vector3(Width / edgecount, 1, 1);

            //Add a WaterDetector and make sure they're triggers
            _colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            _colliders[i].AddComponent<WaterDetector>();

        }




    }

    public void Splash(float xpos, float velocity)
    {
        if (!_isUnderAnotherWaterObject)
        {
            //If the position is within the bounds of the water:
            if (xpos >= _xpositions[0] && xpos <= _xpositions[_xpositions.Length - 1])
            {
                //Offset the x position to be the distance from the left side
                xpos -= _xpositions[0];

                //Find which spring we're touching
                int index = Mathf.RoundToInt((_xpositions.Length - 1) * (xpos / (_xpositions[_xpositions.Length - 1] - _xpositions[0])));

                //Add the velocity of the falling object to the spring
                _velocities[index] += velocity;
            }
        }
    }

    //Same as the code from in the meshes before, set the new mesh positions
    void UpdateMeshes()
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

    //Called regularly by Unity
    void FixedUpdate()
    {
        //Here we use the Euler method to handle all the physics of our springs:
        for (int i = 0; i < _xpositions.Length; i++)
        {
            float force = SPRING_CONSTANT * (_ypositions[i] - _baseheight) + _velocities[i] * DAMPING;
            _accelerations[i] = -force;
            _ypositions[i] += _velocities[i];
            _velocities[i] += _accelerations[i];
        }

        //Now we store the difference in heights:
        float[] leftDeltas = new float[_xpositions.Length];
        float[] rightDeltas = new float[_xpositions.Length];

        //We make 8 small passes for fluidity:
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < _xpositions.Length; i++)
            {
                //We check the heights of the nearby nodes, adjust velocities accordingly, record the height differences
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

            //Now we apply a difference in position
            for (int i = 0; i < _xpositions.Length; i++)
            {
                if (i > 0)
                    _ypositions[i - 1] += leftDeltas[i];
                if (i < _xpositions.Length - 1)
                    _ypositions[i + 1] += rightDeltas[i];
            }
        }
        //Finally we update the meshes to reflect this
        UpdateMeshes();
    }

    private void OnDrawGizmos()
    {
        if (_areaDimensions == null)
            return;

        // Draw the current water object's border
        Transform waterDimensionStart = _areaDimensions.transform.GetChild(0);
        Transform waterDimensionEnd = _areaDimensions.transform.GetChild(1);

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(new Vector2(waterDimensionStart.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionEnd.position.x, waterDimensionStart.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionEnd.position.x, waterDimensionEnd.position.y), new Vector2(waterDimensionStart.position.x, waterDimensionEnd.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionStart.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionStart.position.x, waterDimensionEnd.position.y));
        Gizmos.DrawLine(new Vector2(waterDimensionEnd.position.x, waterDimensionStart.position.y), new Vector2(waterDimensionEnd.position.x, waterDimensionEnd.position.y));
    }
}