using _MyAssets.Scripts.Character.Note;
using _MyAssets.Scripts.Common;
using UnityEngine;

namespace _MyAssets.Scripts.Note
{
   
    /// <summary>
    /// ノートを再生するライン
    /// プレイヤーが衝突するとノートを再生
    /// </summary>
    public class ReleaseLine: MonoBehaviour
    { 
        [SerializeField]
        private NoteManager _melody;
        [SerializeField]
        private NoteManager _uraMelody;
        [SerializeField]
        private NoteManager _bass;
        [SerializeField]
        private NoteManager _percussion;
        [SerializeField]
        private NoteManager _effect;
        [SerializeField]
        private NoteManager _fillin;
        [SerializeField]
        private GameObject _HitEffect;
        
        // エフェクトの位置
        [SerializeField] 
        private Transform _effectPosition;

        // スプライトレンダラー
        private SpriteRenderer _myRenderer;
        // アイコンのスプライトレンダラー
        [SerializeField] private SpriteRenderer _iconRenderer;
        
        // スプライトの色のキャッシュ
        private Color _cacheColor;
        // 押下したときの色
        [SerializeField] Color _pressedColor;
        
        // スプライト画像のキャッシュ
        private Sprite _cacheSprite;
        // 謳歌したときのアイコン
        [SerializeField] private Sprite _pressedSprite;

        // スプライトの色の変更期間
        [SerializeField]
        private float _colorDuration = 1.2f;

        private void Start()
        {
            _myRenderer = GetComponent<SpriteRenderer>();
            // 色をキャッシュする
            _cacheColor = _myRenderer.color;
            _cacheSprite = _iconRenderer.sprite;
        }

        private void Update()
        {
            if (PlayerInput.MouseButtonState.Equals(MouseButtonState.Press))
            {
                // マウス押下中
                // スプライトを押下時のものに変更する
                _myRenderer.color = _pressedColor;
                _iconRenderer.sprite = _pressedSprite;
            }
            else
            {
                // 無操作時
                // スプライトをデフォルトに
                _myRenderer.color = Color.Lerp(_myRenderer.color, _cacheColor, Time.deltaTime * _colorDuration);
                _iconRenderer.sprite = _cacheSprite;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("ReleaseNote"))
            {
                // マウス押下時
                // releaseNoteを削除する
                Destroy(other.gameObject);
                // リリースする
                Release();
                // ノートを削除する
                DestroyAllNote();
            }
        }

        /// <summary>
        /// リリースする
        /// </summary>
        private void Release()
        {
            // エフェクトを生成
            ReleaseEffect();
                
            // ノートを再生
            if (_melody != null)
                _melody.PlayClip();
                
            if (_uraMelody != null)
                _uraMelody.PlayClip();
                
            if (_bass != null)
                _bass.PlayClip();
                
            if (_percussion != null)
                _percussion.PlayClip();

            if (_effect != null)
                _effect.PlayClip();
                
            if (_fillin != null)
                _fillin.PlayClip();
            
            // ノートを削除する
            DestroyAllNote();
        }

        /// <summary>
        /// ノートを削除する
        /// </summary>
        private void DestroyAllNote()
        {
            var objs = GameObject.FindGameObjectsWithTag("Note");
            foreach (var obj in objs)
            {
                var n = obj.GetComponent<NoteCtl>();
                if (n.CanDestroy())
                {
                    n.DestroyThis();
                }
            }
        }

        /// <summary>
        /// エフェクトを生成する
        /// </summary>
        private void ReleaseEffect()
        {
            if (_HitEffect != null)
            {
                var obj = Instantiate(_HitEffect);
                if (_effectPosition != null)
                    obj.transform.position = _effectPosition.position;
            }
        }

    }
}