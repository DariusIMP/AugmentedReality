// Author: Mathias Soeholm, edited to fit this project
// Date: 05/10/2016
// Source: https://gist.github.com/mathiassoeholm/15f3eeda606e9be543165360615c8bef
// No license, do whatever you want with this script

using UnityEngine;
using UnityEngine.Serialization;


[ExecuteInEditMode]
public class TubeRenderer : MonoBehaviour
{
    [SerializeField] Vector3[] _positions;

    public int Sides;
    public float RadiusOne;
    public float RadiusTwo;
    public bool UseWorldSpace = true;
    public bool UseTwoRadii = false;

    private Vector3[] _vertices;
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    public Material material
    {
        get { return _meshRenderer.material; }
        set { _meshRenderer.material = value; }
    }

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        if (_meshFilter == null)
        {
            _meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer == null)
        {
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void OnEnable()
    {
        _meshRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _meshRenderer.enabled = false;
    }

    void Update()
    {
        GenerateMesh();
    }

    private void OnValidate()
    {
        Sides = Mathf.Max(3, Sides);
    }

    public void SetPositions(Vector3[] positions)
    {
        _positions = positions;
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        if (_mesh == null || _positions == null || _positions.Length <= 1)
        {
            _mesh = new Mesh();
            return;
        }

        var verticesLength = Sides * _positions.Length;
        if (_vertices == null || _vertices.Length != verticesLength)
        {
            _vertices = new Vector3[verticesLength];

            var indices = GenerateIndices();
            var uvs = GenerateUVs();

            if (verticesLength > _mesh.vertexCount)
            {
                _mesh.vertices = _vertices;
                _mesh.triangles = indices;
                _mesh.uv = uvs;
            }
            else
            {
                _mesh.triangles = indices;
                _mesh.vertices = _vertices;
                _mesh.uv = uvs;
            }
        }

        var currentVertIndex = 0;

        for (int i = 0; i < _positions.Length; i++)
        {
            var circle = CalculateCircle(i);
            foreach (var vertex in circle)
            {
                _vertices[currentVertIndex++] = UseWorldSpace ? transform.InverseTransformPoint(vertex) : vertex;
            }
        }

        _mesh.vertices = _vertices;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _meshFilter.mesh = _mesh;
    }

    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[_positions.Length * Sides];

        for (int segment = 0; segment < _positions.Length; segment++)
        {
            for (int side = 0; side < Sides; side++)
            {
                var vertIndex = (segment * Sides + side);
                var u = side / (Sides - 1f);
                var v = segment / (_positions.Length - 1f);

                uvs[vertIndex] = new Vector2(u, v);
            }
        }

        return uvs;
    }

    private int[] GenerateIndices()
    {
        // Two triangles and 3 vertices
        var indices = new int[_positions.Length * Sides * 2 * 3];

        var currentIndicesIndex = 0;
        for (int segment = 1; segment < _positions.Length; segment++)
        {
            for (int side = 0; side < Sides; side++)
            {
                var vertIndex = (segment * Sides + side);
                var prevVertIndex = vertIndex - Sides;

                // Triangle one
                indices[currentIndicesIndex++] = prevVertIndex;
                indices[currentIndicesIndex++] = (side == Sides - 1) ? (vertIndex - (Sides - 1)) : (vertIndex + 1);
                indices[currentIndicesIndex++] = vertIndex;


                // Triangle two
                indices[currentIndicesIndex++] = (side == Sides - 1) ? (prevVertIndex - (Sides - 1)) : (prevVertIndex + 1);
                indices[currentIndicesIndex++] = (side == Sides - 1) ? (vertIndex - (Sides - 1)) : (vertIndex + 1);
                indices[currentIndicesIndex++] = prevVertIndex;
            }
        }

        return indices;
    }

    private Vector3[] CalculateCircle(int index)
    {
        var dirCount = 0;
        var forward = Vector3.zero;

        if (index > 0)
        {
            forward += (_positions[index] - _positions[index - 1]).normalized;
            dirCount++;
        }
        else if (index < _positions.Length - 1)
        {
            forward += (_positions[index + 1] - _positions[index]).normalized;
            dirCount++;
        }
        else
        {
            // Forward is the average of the connecting edges directions
            Vector3 prevLine = _positions[index - 1] - _positions[index];
            Vector3 nextLine = _positions[index + 1] - _positions[index];
            forward = ((prevLine + nextLine) / 2).normalized;
        }

        var side = Vector3.Cross(forward, forward + new Vector3(.123564f, .34675f, .756892f)).normalized;
        var up = Vector3.Cross(forward, side).normalized;

        var circle = new Vector3[Sides];
        var angle = 0f;
        var angleStep = (2 * Mathf.PI) / Sides;

        var t = index / (_positions.Length - 1f);
        var radius = UseTwoRadii ? Mathf.Lerp(RadiusOne, RadiusTwo, t) : RadiusOne;

        for (int i = 0; i < Sides; i++)
        {
            var x = Mathf.Cos(angle);
            var y = Mathf.Sin(angle);

            circle[i] = _positions[index] + side * x * radius + up * y * radius;

            angle += angleStep;
        }

        return circle;
    }
}