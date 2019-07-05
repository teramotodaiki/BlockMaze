# BlockMaze

Unity 練習用のプロジェクト。迷路の自動生成を行う

## 準備

- `git clone https://github.com/teramotodaiki/BlockMaze.git`

[![Image from Gyazo](https://i.gyazo.com/ed53d757da2fc825d64cd37f53373729.gif)](https://gyazo.com/ed53d757da2fc825d64cd37f53373729)

- `BlockMaze` フォルダを Unity で開く
- `Scenes/SampleScene.unity` をダブルクリックして開く

[![Image from Gyazo](https://i.gyazo.com/7f007b54853107c2785addb948a486ff.gif)](https://gyazo.com/7f007b54853107c2785addb948a486ff)

- Play

[![Image from Gyazo](https://i.gyazo.com/2caef1b419c8687018bdf5826fe2931f.png)](https://gyazo.com/2caef1b419c8687018bdf5826fe2931f)

## 設定

### 迷路の大きさを変える

- `Ground` というオブジェクトがある。これは床を表していて、大きさ (scale) を変更することで、迷路のサイズを変更できる
[![Image from Gyazo](https://i.gyazo.com/76a4a1d796664559369f749cbd0eb727.gif)](https://gyazo.com/76a4a1d796664559369f749cbd0eb727)

### カメラとの相対位置を変える

- `Main Camera` というオブジェクトがある。これは世界を撮影するカメラであり、プレイヤーに追従する
- カメラをシーンビューで移動させると、プレイヤーとカメラとの相対位置を設定できる
 
## 迷路生成について

- [MazeGenerator.cs](/Assets/MazeGenerator.cs) に全て書かれている
- ここでは[棒倒し法](http://algoful.com/Archive/Algorithm/MazeBar)というアルゴリズムを使っている
- `Ground` に与えられた床オブジェクトの大きさを元にして迷路の大きさを判断する
  - ただし、棒倒し法の制約上、幅と奥行きが奇数でなければならないため、数字を切り詰めている
- `GenerateAround()` と `GenerateEven()` では、緑色の壁を置いている
- `Traverse` では、赤色の壁を１つずつ置いている
  - 見た目がわかりやすいように、これを `Update` の中で呼んでいる
- `table` という変数は、 x-z 平面の壁の存在を表している
  - 初期値は 0 で、通路を表す。 1 のとき、壁があることを表す
 
 
