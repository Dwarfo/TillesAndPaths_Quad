using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lr;
    public LineRenderer rectangleDrawer;
    public GameObject emptyGo;

    void Start()
    {
        //if(lr == null || rectangleDrawer == null)
            InitLineDrawers();
    }

    private void InitLineDrawers()
    {
        lr              = AddRenderer(Instantiate(emptyGo, transform), "Line Renderer");
        lr.positionCount = 0;
        lr.SetPositions(new Vector3[1]);

        rectangleDrawer = AddRenderer(Instantiate(emptyGo, transform), "Rectangle drawer");
        rectangleDrawer.sortingOrder = 1;
    }

    private LineRenderer AddRenderer(GameObject go, string goName) 
    {
        go.name = goName;
        go.transform.SetParent(transform);
        LineRenderer renderer = go.AddComponent<LineRenderer>();
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.numCornerVertices  = 4;
        renderer.numCapVertices     = 4;

        renderer.sortingLayerName = "LinesAndDebugGFX";
        return renderer;

    }

    public void DrawLine(Path path)
    {
        if (path == null) 
        {
            ClearLine();
            return;
        }
            
        lr.widthMultiplier = 0.2f;
        lr.positionCount = path.fullPath.Count - path.currentPos;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        rectangleDrawer.material.color = Color.cyan;
        Tile[] vec = path.fullPath.ToArray();
        List<Vector3> v2 = new List<Vector3>();
        for(int i = path.currentPos; i < vec.Length; i++)
            v2.Add((Vector2)vec[i].Index);

        lr.SetPositions(v2.ToArray());

        foreach (var pp in path.fullPath)
        {
            //Debug.Log(pp);
        }

    }

    public void DrawRectangle(Vector2Int centerPos, float sizeX, float sizeY) 
    {
        if (rectangleDrawer == null)
            return;
        
        rectangleDrawer.widthMultiplier = 0.08f;
        rectangleDrawer.positionCount   = 5;
        rectangleDrawer.startColor      = Color.green;
        rectangleDrawer.endColor        = Color.green;
        rectangleDrawer.material.color  = Color.green;

        Vector3[] rectPoints = new Vector3[rectangleDrawer.positionCount];
        rectPoints[0] = new Vector3((float)centerPos.x + sizeX, (float)centerPos.y + sizeY, 2);
        rectPoints[1] = new Vector3((float)centerPos.x + sizeX, (float)centerPos.y - sizeY, 2);
        rectPoints[2] = new Vector3((float)centerPos.x - sizeX, (float)centerPos.y - sizeY, 2);
        rectPoints[3] = new Vector3((float)centerPos.x - sizeX, (float)centerPos.y + sizeY, 2);
        rectPoints[4] = new Vector3((float)centerPos.x + sizeX, (float)centerPos.y + sizeY, 2);

        rectangleDrawer.SetPositions(rectPoints);
    }

    //Sizes are half diagonals
    public void DrawIsoRectangle(Vector2 tileIndex, float sizeX, float sizeY)
    {
        if (rectangleDrawer == null)
            return;

        rectangleDrawer.widthMultiplier = 0.08f;
        rectangleDrawer.positionCount = 5;
        rectangleDrawer.startColor = Color.green;
        rectangleDrawer.endColor = Color.green;
        rectangleDrawer.material.color = Color.green;

        float centerXPos = tileIndex.x * sizeX - tileIndex.y * sizeX ;
        float centerYPos = tileIndex.y * sizeY + tileIndex.x * sizeY + sizeY;


        Vector2 center = new Vector2(centerXPos, centerYPos);

        Vector3[] rectPoints = new Vector3[rectangleDrawer.positionCount];
        rectPoints[0] = new Vector3((float)center.x - sizeX, (float)center.y, 2);
        rectPoints[1] = new Vector3((float)center.x, (float)center.y - sizeY, 2);
        rectPoints[2] = new Vector3((float)center.x + sizeX, (float)center.y, 2);
        rectPoints[3] = new Vector3((float)center.x, (float)center.y + sizeY, 2);
        rectPoints[4] = new Vector3((float)center.x - sizeX, (float)center.y, 2);

        rectangleDrawer.SetPositions(rectPoints);
    }

    public void DrawHexagon(Vector2 centerPos, float sideSize) {

        if (rectangleDrawer == null)
            return;


        float dx = sideSize * 0.875f;
        float dy = sideSize * 0.5f;

        rectangleDrawer.widthMultiplier = 0.08f;
        rectangleDrawer.positionCount = 7;
        rectangleDrawer.startColor = Color.green;
        rectangleDrawer.endColor = Color.green;
        rectangleDrawer.material.color = Color.green;

        Vector3[] rectPoints = new Vector3[rectangleDrawer.positionCount];
        rectPoints[0] = new Vector3((float)centerPos.x - dx, (float)centerPos.y + dy, 2);
        rectPoints[1] = new Vector3((float)centerPos.x - dx, (float)centerPos.y - dy, 2);
        rectPoints[2] = new Vector3((float)centerPos.x, (float)centerPos.y - sideSize, 2);
        rectPoints[3] = new Vector3((float)centerPos.x + dx, (float)centerPos.y - dy, 2);
        rectPoints[4] = new Vector3((float)centerPos.x + dx, (float)centerPos.y + dy, 2);
        rectPoints[5] = new Vector3((float)centerPos.x, (float)centerPos.y + sideSize, 2);
        rectPoints[6] = new Vector3((float)centerPos.x - dx, (float)centerPos.y + dy, 2);

        rectangleDrawer.SetPositions(rectPoints);
    }


    public void ClearLine()
    {
        lr.positionCount = 0;
    }
}