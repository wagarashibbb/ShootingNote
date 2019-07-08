using System;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace _MyAssets.Scripts.Common
{
    // マウス操作の状態
    public enum MouseButtonState
    {
        Void, // 無操作
        ButtonDown, // ボタン押下
        Press, // ボタン押下継続
        ButtonUp // ボタンリリース時
    }
    
    /// <summary>
    /// 入力操作スクリプト
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        // 操作可能かどうか
        private static bool _controllable = true;

        private static bool Controllable => _controllable;

        // マウスのワールド座標
        private static Vector3 _mouseWorldPoint;

        public static Vector3 MouseWorldPoint => _mouseWorldPoint;

        // マウスのボタン状態
        private static MouseButtonState _mouseButtonState;
        // 押下開始のとき
        private static bool _buttonDown;

        public static bool ButtonDown => _buttonDown;

        public static MouseButtonState MouseButtonState
        {
            get
            {
                //UpdateState();
                return _mouseButtonState;
            }
        }

        // 監視するボタン番号
        private static int _mouseLeftButton = 0;

        private void Update()
        {
            UpdateState();
        }

        /// <summary>
        /// 押下状態を保存
        /// </summary>
        private static void UpdateState()
        {

            // 制御不可能のとき入力を無視
            if (!_controllable) return;

            // 制御可能のとき

            // マウスの場所を記憶
            _mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 押下開始を初期化
            _buttonDown = false;
            
            // マウスの状態を記憶
            if (Input.GetMouseButtonDown(_mouseLeftButton))
            {
                // 押下時
                //_mouseButtonState = MouseButtonState.ButtonDown;
                _buttonDown = true;
            }
            
            if (Input.GetMouseButton(_mouseLeftButton))
            {
                // 押下継続
                _mouseButtonState = MouseButtonState.Press;
            }
            else if (Input.GetMouseButtonUp(_mouseLeftButton))
            {
                // リリース時
                _mouseButtonState = MouseButtonState.ButtonUp;
            }
            else
            {
                // 無操作時
                _mouseButtonState = MouseButtonState.Void;
            }


        }
    }
}