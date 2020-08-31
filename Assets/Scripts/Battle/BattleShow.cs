using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleShow : MonoBehaviour
{
    int mapW;
    int mapH;

    public int offsetX;
    public int offsetZ;

    public int beginX;
    public int beginZ;

    GameObject gridFather;

    GameObject ground;
    GameObject terrace;
    GameObject obstacle;
    GameObject generate;

    Material me1;
    Material me2;
    static Dictionary<string, GameObject> cubes;

    static BattleShow instance;

    static GameObject attackRange;


    //public static bool showPlaceGirds=false;

    //---------------------------------------------
    public static BattleShow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleShow();
            }
            return instance;
        }
    }
  
    private void Awake()
    {
        terrace = (GameObject)Resources.Load("prefabs/map/mesh/S_Ground_tiles_Qcity_032");
        ground = (GameObject)Resources.Load("prefabs/map/mesh/S_Ground_tiles_Qcity_043");
        obstacle = (GameObject)Resources.Load("prefabs/map/mesh/S_Ground_tiles_Qcity_0182");
        generate = (GameObject)Resources.Load("prefabs/map/mesh2/pPlane2");

        me1 = Resources.Load<Material>("nu/level_main_02-05/TX_LM_effect_01");
        attackRange = Resources.Load<GameObject>("prefabs/battleEffect/AttackRange");

        //lastAttackRange = new List<Pos>();
        //Debug.Log(me1==null);
        //var me4 = Resources.Load<GameObject>("nu/level_main_02-05").transform.GetChild(1);
        //Debug.Log(me4.name );

        //装格子用的 
        cubes = new Dictionary<string, GameObject>();

       
    }
    void Start()
    {
        //设置父节点
        GameObject gridFather = new GameObject();
        gridFather.name = "map";

        //初始化地图管理
        GridManage.Instance.Init();

        //为长宽赋值
        mapH = GridManage.Instance.hight;
        mapW = GridManage.Instance.width;
        for (int i = 0; i < mapH; i++)
        {
            for (int j = 0; j < mapW; j++)
            {
                GameObject obj = null;
                //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //家
                if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.homePoint)
                {
                    obj = Instantiate(generate, new Vector3(beginX + i * offsetX, 0, beginZ + j * offsetZ), Quaternion.identity);
                    obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
                //敌人点
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.enemyPoint)
                {
                    obj = Instantiate(generate, new Vector3(beginX + i * offsetX, 0, beginZ + j * offsetZ), Quaternion.identity);
                    obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                //障碍物
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.obstacle)
                {
                    obj = Instantiate(obstacle, new Vector3(beginX + i * offsetX, 0, beginZ + j * offsetZ), Quaternion.identity);
                    obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                }
                //地面
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.ground)
                {
                    obj = Instantiate(ground, new Vector3(beginX + i * offsetX, -20, beginZ + j * offsetZ), Quaternion.identity);
                    obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = me1;
                    //obj.transform.position = new Vector3(beginX + i * offsetX, -1, beginZ + j * offsetZ);
                }
                else if (GridManage.Instance.levelMap[i, j].Type == Grid_Type.terrace)
                {
                    obj = Instantiate(terrace, new Vector3(beginX + i * offsetX, 0, beginZ + j * offsetZ), Quaternion.identity);
                    obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = me1;
                }

                obj.name = i + "-" + j;
                //obj.tag = "Grid";
                obj.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Grid");
                obj.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
                cubes.Add(obj.name, obj);




                obj.transform.parent = gridFather.transform;

            }
        }
        gridFather.transform.Rotate(new Vector3(0, 90, 0));
        gridFather.transform.localScale=new Vector3(0.01f,0.01f,0.01f);
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
    public  void ShowPlaceGirds(List<Pos> girds)
    {
        for (int i = 0; i < girds.Count; i++)
        {
            string cubeName = girds[i].posX.ToString() + "-" + girds[i].posY.ToString();
            cubes[cubeName].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }
    }
    public  void ClosePlaceGirds(List<Pos> girds)
    {

        for (int i = 0; i < girds.Count; i++)
        {
            string cubeName = girds[i].posX.ToString() + "-" + girds[i].posY.ToString();
            cubes[cubeName].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void ShowAttackRnage(List<Pos> girds)
    {
        if (girds==null)
        {
            return;
        }
        foreach(var item in girds)
        {
            //Debug.Log("PosX:" + item.posX + "poxY:" + item.posY);
            if (item.posX > mapW || item.posX < 0 || item.posY > mapH || item.posY < 0)
            {
            }

            string cubeName = item.posX.ToString() + "-" +item.posY.ToString();
            Vector3 girdPos = cubes[cubeName].transform.position;
            var obj = Instantiate(attackRange, new Vector3(girdPos.x, girdPos.y + 0.3f, girdPos.z), Quaternion.Euler(90, 0, 0));
            obj.tag = "AttackRange";
        }
        //GameObject obj = Instantiate(attackRange,new Vector3(girdPos.x,girdPos.y+0.3f,girdPos.z),Quaternion.identity);
    }
    public void ColosShowAttackRange()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("AttackRange");
        if (temp == null)
        {
            return;
        }
        foreach (var item in temp)
        {
            Destroy(item);
        }
    }
}
