﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    Transform MazeParent;
    public GameObject Ground;
    public GameObject GreenWallPrefab;
    public GameObject RedWallPrefab;

    int MazeWidth;
    int MazeHeight;

    int[,] table; // [x, z]

    void Validate()
    {
        // 迷路のサイズは奇数でなければならない
        if (MazeWidth % 2 == 0 || MazeHeight % 2 == 0)
        {
            throw new System.Exception("MazeWidth and MazeHeight must be odd number!");
        }
        // 迷路の大きさは５以上でなければならない
        if (MazeWidth < 5 || MazeHeight < 5)
        {
            throw new System.Exception("MazeWidth and MazeHeight must be 5 or more than 5.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ground のサイズから MazeWidth, MazeHeight を決定する
        var scale = Ground.transform.localScale;
        var width = (int)scale.x;
        MazeWidth = width % 2 == 1 ? width : width + 1;
        var height = (int)scale.z;
        MazeHeight = height % 2 == 1 ? height : height + 1;

        Validate();

        Initialize();
    }

    int cursor;
    bool prevKeyDown;
    // Update is called once per frame
    void Update()
    {
        Validate();

        Traverse(cursor++);

        if (Input.GetButtonDown("Jump"))
        {
            Initialize();
        }
    }

    /// <summary>
    /// 迷路を初期化する
    /// </summary>
    void Initialize()
    {
        if (MazeParent) Destroy(MazeParent.gameObject);

        table = new int[MazeWidth, MazeHeight];

        MazeParent = new GameObject("MazeParent").transform;
        MazeParent.parent = Ground.transform;
        MazeParent.localPosition = new Vector3(-0.5f, 0.5f, -0.5f);

        GenerateAround();
        GenerateEven();

        cursor = 0;
    }

    /// <summary>
    /// 周囲のマスを壁にする
    /// </summary>
    void GenerateAround()
    {
        for (int x = 0; x < MazeWidth; x++)
        {
            PutWall(x, 0, GreenWallPrefab);
            PutWall(x, MazeHeight - 1, GreenWallPrefab);
        }
        for (int y = 1; y < MazeHeight - 1; y++)
        {
            PutWall(0, y, GreenWallPrefab);
            PutWall(MazeWidth - 1, y, GreenWallPrefab);
        }
    }

    /// <summary>
    /// １マス置きに壁を置く
    /// </summary>
    void GenerateEven()
    {
        for (int x = MazeWidth - 3; x > 0; x -= 2)
        {
            for (int z = MazeHeight - 3; z > 0; z -= 2)
            {
                PutWall(x, z, GreenWallPrefab);
            }
        }
    }

    /// <summary>
    /// index 番目の棒を倒す
    /// 内側の壁(棒)を走査し、ランダムな方向に倒して壁とします。(ただし以下に当てはまる方向以外に倒します。)
    /// 1行目の内側の壁以外では上方向に倒してはいけない。
    /// すでに棒が倒され壁になっている場合、その方向には倒してはいけない。
    /// http://algoful.com/Archive/Algorithm/MazeBar
    /// </summary>
    /// <param name="index"></param>
    void Traverse(int index)
    {
        var length = (MazeWidth - 3) / 2;
        var x = (index % length) * 2 + 2;
        var z = index / length * 2 + 2;

        if (z > MazeHeight - 2) return; // 置き終わった

        var candidacies = new List<int[]>();
        if (z == 2 && table[x, z - 1] == 0) candidacies.Add(new int[2] { x, z - 1 });
        if (table[x + 1, z] == 0) candidacies.Add(new int[2] { x + 1, z });
        if (table[x, z + 1] == 0) candidacies.Add(new int[2] { x, z + 1 });
        if (table[x - 1, z] == 0) candidacies.Add(new int[2] { x - 1, z });

        var number = Random.Range(0, candidacies.Count);
        var pos = candidacies[number];
        PutWall(pos[0], pos[1], RedWallPrefab);
    }

    /// <summary>
    /// 壁を設置して、テーブル情報を更新する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    void PutWall(int x, int z, GameObject prefab)
    {
        table[x, z] = 1;
        
        var wall = Instantiate(prefab, MazeParent);
        wall.transform.localPosition = new Vector3(x, 0, z);
        wall.transform.Translate(wall.transform.localScale / 2);
    }
}
