using UnityEditor;
using UnityEngine;

using UnityEngine.UIElements;

public class UIMainGame : MonoBehaviour
{
    [Header("* * * UI Document * * * ")]
    public UIDocument uid;

    private VisualElement _root;
    private Label _scoreText;      //スコアのテキスト表情
    private Button _gameQuitButton; //ゲーム終了ボタン
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ルート要素(親)の取得
        _root = uid.rootVisualElement;

        // === スコアテキストの取得 === //
        _scoreText = _root.Q<Label>("score-label");
        // スコアテキストの更新
        _scoreText.text = "こ↑こ↓にスコアの文字列を表示する。";
        //ゲーム終了のボタンの取得
        _gameQuitButton = _root.Q<Button>("game-quit-button");
        // ボタンの押下イベントの設定
        _gameQuitButton.clicked += () => { 
            Debug.Log("ゲーム終了"); 
            // エディタの再生を止める(エディタ用)
            EditorApplication. isPlaying = false;
            // アプリケーションを終了させる(ビルド用)
            Application.Quit();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
