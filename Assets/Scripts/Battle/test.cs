using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    int mapW;
    int mapH;

    public int offsetX;
    public int offsetZ;

    public int beginX;
    public int beginZ;

    GameObject gridFather;

    static Dictionary<string, GameObject> cubes;

   //public static bool showPlaceGirds=false;

    //---------------------------------------------

    void Start()
    {
        cubes = new Dictionary<string, GameObject>();
        GameObject gridFather = new GameObject();
        gridFather.name = "map";
        GridManage.Instance.Init();
        mapH = GridManage.Instance.hight;
        mapW = GridManage.Instance.width;
        for(int i = 0; i < mapH; i++)
        {
            for(int j = 0; j < mapW; j++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX+i*offsetX,0,beginZ+j*offsetZ);
                obj.name = i + "-" + j;
                obj.tag ="Grid";
                obj.layer =LayerMask.NameToLayer("Grid");
                cubes.Add(obj.name,obj);
                if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.homePoint)
                {
                    obj.GetComponent<Renderer>().material.color = Color.blue;
                }
               else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.enemyPoint)
                {
                    obj.GetComponent<Renderer>().material.color = Color.red;
                }
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.obstacle)
                {
                    obj.GetComponent<Renderer>().material.color = Color.black;
                }
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.ground)
                {
                    obj.GetComponent<Renderer>().material.color = Color.white;
                obj.transform.position = new Vector3(beginX+i*offsetX,-1,beginZ+j*offsetZ);
                }
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.terrace)
                {
                    obj.GetComponent<Renderer>().material.color = Color.grey;
                }
                obj.transform.parent = gridFather.transform;
            }
        }
        gridFather.transform.Rotate(new Vector3(0,90,0));
    }

    void Update()
    {







        if (Input.GetMouseButton(0))
        {
            //先判断是否再UI上
            if (!EventSystem.current.IsPointerOverGameObject())
            {
               // Debug.Log("UI");
            }

            //其次判断是否再格子上面
            else
            {
                //判断是否有人
                //没人
                //点击时判断格子属性
                //特殊属性时，时间减缓或者跳出提示


                //有人
                //时间减缓
                //跳出人物属性右下技能，左上撤销按钮
            }




        }
    }
    public static void ShowPlaceGirds(List<Pos> girds)
    {
        for (int i = 0; i < girds.Count; i++)
        {
            string cubeName = girds[i].posX.ToString() + "-" + girds[i].posY.ToString();
            cubes[cubeName].GetComponent<Renderer>().material.color = Color.green;
        }
    }
    public static void ClosePlaceGirds(List<Pos> girds)
    {
       
        for (int i=0;i<girds.Count;i++)
        {
            string cubeName = girds[i].posX.ToString() + "-" + girds[i].posY.ToString();
            cubes[cubeName].GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
