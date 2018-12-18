# 視窗程式設計
#### Minecraft作業說明書
工科110朱雁丞
工科110仲其宇
## 一. 特色
隨機地圖產生：地圖為全隨機產生，包含泥土、石頭。
怪物隨機生成：夜晚時怪物生成機率會大增，會有殭屍、史萊姆攻擊，殭屍主要為碰撞攻擊，史萊姆會在接近時膨脹限制玩家行動。
1. 創造/生存模式
2. 開發/執行環境
3. 開發環境：Unity 2017.3.1f1 (64-bit)、Microsoft Visual Studio 2017
4. 執行環境：Windows 10 64bit
5. 遊戲設計方法
## 二. 架構
本專題將主要的常數與基本方法藉由靜態類別Const定義在Manager.cs中，遊戲中的實體都會賦予由Const.GameItemID 所定義的ID，物件可以藉由該ID彼此識別與互動。在ItemMap中定義了各Item的常數，例如生命值、Texture位置等，並且提供可以將Item.name轉換為Const.GameItemID的方法，以及識別是否為生物的方法。

#### 生命管理系統(LiveManager.cs)
使用此component賦予object擁有生命值，有基本Function Attack、Relive。其中ItemCtrl繼承自LiveManager，用來控制方塊(非生物)的壽命，isAlive用來耗損物品壽命並且回傳該物體剩餘壽命百分比，並藉此對物件損壞程度做不同貼圖。

#### 隨機地圖產生機(Ground.cs)
本類別會根據Const靜態類別中定義的mapSize及mapOrigin生成三維Const.GameItemID矩陣，先在矩陣中生成地圖，最後再一次實體化。地圖產生分為以下幾個部分：
1. 基底：由Const.groundLevel決定基底高度。
2. 山坡：發想自高低起伏的數學函數，故使用三角函數$y=α_1  cos⁡(xπ/w)+α_2  cos⁡(zπ/l)$ 製造山坡，其中α1 、α2 分別控制X方向與Z方向的山脈高度，最高點處即為α1+α2。w、l 為X方向與Z方向山脈長度，分別用Random產生以上參數，獲得一山坡模型，再隨機產生山脈位置進行疊合。
3. 地層：地表地形建立完成後，需針對不同地形高度分配Dirt、Stone，透過exponential收斂函數發想，使Stone產生之機率隨身度增加$r=1-e^{-distance/5}+rand()$，當r小於5時指定該物件為Dirt，當位於第一層時貼圖為草皮。
4. Instantiate：根據map中定義的ID生成地形。

#### 遊戲流程控制(LightControllor.cs)
透過對Time.time取餘數得到當下的相對時間，並提供isNight方法供其他函數取得是否為晚上。

#### 怪物生成系統(EnemyControllor.cs)
當時間為晚上，會透過Random逐漸補滿Enemies List，該List透過pushNewEnemy方法隨機產生怪物類型，再根據不同的Const.GameItemID展現不同特性。共同行為：
1. 自動導向遊戲主角：當與遊戲主角小於trackDistance會透過AutoMove自動導航。
2. 攻擊：當與遊戲主角小於attackDistance會進行攻擊。
3. 播放音效：當與遊戲主角小於audioDistance會播放音效。

#### 怪物自動導向(AutoMove.cs)
給定目標位置後，擁有該component的物件會朝向目標移動，並且藉由map判斷前方是否有障礙物，否則透過跳躍繼續前進。

#### 工具箱管理系統(ToolboxControllor.cs)
將物件破壞後，會透過Const.GameItemID取得物件類型，並產生相對應的icon顯示於此。
1. 破壞方塊時，方塊會有生命值，不會按一下就消滅，且方塊有破裂的貼圖動畫。
2. 模擬真實Minecraft UI，並增加生存及創造模式。
3. 在生存模式下，怪獸的攻擊會使玩家扣血並且死亡，但在創造模式下玩家不會死亡且可以自由飛行。
4. 破壞方塊後可以獲得該方塊並放進自己的背包。
5. 怪獸有兩種，且其中粉紅色的史萊姆接近後會急速放大至10倍。
6. 地圖自動隨機生成，並且以函數曲線來呈現真實的地形。
7. 地讀的表層是長滿草的泥土，但底層會隨機生成不同物質。
8. 打碎方塊時有音效。
9. 怪物靠近時有音效。
10. 增加玩家生命值，可供怪物攻擊時扣血。
## 四. Feedback
我是覺得作業有點太難了，這次的Minecraft作業和小專題差不多，要花很多時間，而且demo時間和專題checkpoint時間很近，會卡在一起。教的內容我覺得很充實也很實用，也可以快速的讓我們熟悉Unity的環境。之前曾經想要自己試著學Unity可是學習的資源有點混亂，所以一個禮拜就沒有然後了，很感謝有這次機會可以讓我們熟悉Unity。
