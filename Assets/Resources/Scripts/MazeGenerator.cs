using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

    public int width;
    public int height;
    public float blockWidth;
    public float blockHeight;
    public float wallBreakProb;
    public bool hasBorder;
    [HideInInspector]
    public Block[,] mazeArray;

    public Vector2 middlePosition;
    private Vector2 upperLeftPosition;

    public GameObject wallPrefabs;
    public GameObject nodePrefabs;
    public Camera minimapCamera;

    public class Block{
        public int way;
        public int value;
        public bool left,right,up,down;
        public bool inUse; 
        public Vector2 position;
        public Block(int x, int y)
        {
            left = false;
            right = false;
            up = false;
            down = false;
            inUse = false;
            value = 0;
            way = 0;
            position = new Vector2(x, y);
        }

        public void allowWay(int dir)
        {
            switch(dir)
            {
                case 0: if (!left) { left = true; way++;} break;
                case 1: if (!right) { right = true; way++; } break;
                case 2: if (!up) { up = true; way++; } break;
                case 3: if (!down) { down = true; way++; } break;
            }
        }
    }

	void Awake () {
	    upperLeftPosition = new Vector2(width* blockWidth / 2, height * blockHeight / 2);

        mazeArray = new Block[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mazeArray[i, j] = new Block(i,j);
            }
        }

        dfsMaze(0,0);
        generateMaze();
        minimapCamera.orthographicSize = Mathf.Max(width * 11 / 5 + 1, height * 13 / 5 + 1);
    }

    private void dfsMaze(int x, int y)
    {
        mazeArray[x,y].inUse = true;

        int[] dirList = { 2, 3, 0, 1};
        shuffleArray(dirList);
        Vector2[] directionArray = { new Vector2(-1,0),new Vector2(1,0), new Vector2(0,1), new Vector2(0,-1)};
        for (int i = 0; i < dirList.Length; i++)
        {
            Vector2 dir = directionArray[dirList[i]];
            int dx = (int)dir.x;
            int dy = (int)dir.y;
            if (conditionChecking(x, y, dx, dy))
            {
                int directionValue = dirList[i];
                int mul = directionValue / 2;
                int inverseDirection = (mul * 2) + (1 - (directionValue % 2));
                float ran = Random.Range(0f,100);
                if (!mazeArray[x + dx, y + dy].inUse)
                {
                    mazeArray[x, y].allowWay(directionValue);
                    mazeArray[x + dx, y + dy].allowWay(inverseDirection);
                    dfsMaze(x + dx, y + dy);
                }
                else if(ran <= wallBreakProb)
                {
                    mazeArray[x, y].allowWay(directionValue);
                    mazeArray[x + dx, y + dy].allowWay(inverseDirection);
                }

            }
        }
    }

    private bool conditionChecking(int x, int y, int dx, int dy)
    {
        return x + dx >= 0 && x + dx < width && y + dy >= 0 && y + dy < height;
    }

    public void shuffleArray(int[] list){
        for (int i = 0; i < list.Length - 1; i++)
        {
            int x = Random.Range(i,list.Length);
            
            int temp = list[x];
            list[x] = list[i];
            list[i] = temp;
        }
    }

    public void generateMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i < width - 1 && !mazeArray[i, j].right)
                {
                    GameObject wallRight = Instantiate(wallPrefabs, getPosition(i,j) + new Vector2(blockWidth / 2, 0), Quaternion.identity) as GameObject;
                    wallRight.transform.localScale = new Vector3(0.1f, 0.5f, 1);
                }
                if (j < height - 1 && !mazeArray[i, j].up)
                {
                    GameObject wallDown = Instantiate(wallPrefabs, getPosition(i, j) + new Vector2(0, blockHeight / 2), Quaternion.identity) as GameObject;
                    wallDown.transform.localScale = new Vector3(0.5f, 0.1f, 1);
                }
                bool lr = mazeArray[i, j].left || mazeArray[i, j].right;
                bool ud = mazeArray[i, j].up || mazeArray[i, j].down;
                
                if (lr && ud)
                {
                    Instantiate(nodePrefabs, getPosition(i, j), Quaternion.identity);
                }
            }
        }
        if (hasBorder)
        {
            GameObject wallRightMost = Instantiate(wallPrefabs, getPosition(width - 1, 0) + new Vector2(blockWidth / 2, blockHeight * (height - 1) / 2), Quaternion.identity) as GameObject;
            wallRightMost.transform.localScale = new Vector3(0.1f, 0.5f * height, 1);
            GameObject wallLeftMost = Instantiate(wallPrefabs, getPosition(0, 0) + new Vector2(-blockWidth / 2, blockHeight * (height - 1) / 2), Quaternion.identity) as GameObject;
            wallLeftMost.transform.localScale = new Vector3(0.1f, 0.5f * height, 1);
            GameObject wallUpMost = Instantiate(wallPrefabs, getPosition(0, height - 1) + new Vector2(blockWidth * (width - 1) / 2, blockHeight / 2), Quaternion.identity) as GameObject;
            wallUpMost.transform.localScale = new Vector3(0.5f * width, 0.1f, 1);
            GameObject wallDownMost = Instantiate(wallPrefabs, getPosition(0, 0) + new Vector2(blockWidth * (width - 1) / 2, -blockHeight / 2), Quaternion.identity) as GameObject;
            wallDownMost.transform.localScale = new Vector3(0.5f * width, 0.1f, 1);
        }
    }

    public Vector2 getPosition(int x, int y)
    {
        float posX = (x - ((width-1) / 2.0f)) * blockWidth;
        float posY = (y - ((height-1) / 2.0f)) * blockHeight;
        return new Vector2(posX, posY) + middlePosition;
    }
}
