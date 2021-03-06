﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;

namespace TrailsEditor
{
    public class BehaviourTreeEditor : EditorWindow
    {

        [SerializeField]
        private Texture mTextureGrid_;

        [SerializeField]
        private BTAsset mBTAsset_;

        [SerializeField]
        // private BTNavigationHistory mNavigationHistory_;


        private BTEditorGrid mGrid_;
        private BTEditorGraph mGraph_;
        private BTEditorCanvas mCanvas_;


        private bool isDisposed_;

        private void OnEnable()
        {
            if (mTextureGrid_ == null)
            {
                mTextureGrid_ = Resources.Load<Texture>("Trails/EditorGUI/background");
            }

            if (mCanvas_ == null)
            {
                mCanvas_ = new BTEditorCanvas();
                BTEditorCanvas.Current = mCanvas_;
            }

            if (mGrid_ == null)
            {
                mGrid_ = new BTEditorGrid(mTextureGrid_);
            }

            if (mGraph_ == null)
            {
                mGraph_ = BTEditorGraph.Create();
            }



        }
        private void OnGUI()
        {
            if (mBTAsset_ != null)
            {
                Rect navHistoryRect = new Rect(0.0f, 0.0f, position.width, 20.0f);
                Rect optionsRect = new Rect(position.width - 20.0f, 0.0f, 20.0f, 20.0f);
                Rect footerRect = new Rect(0.0f, position.height - 18.0f, position.width, 20.0f);
                Rect canvasRect = new Rect(0.0f, navHistoryRect.yMax, position.width, position.height - (footerRect.height + navHistoryRect.height));

                BTEditorStyle.EnsureStyle();
                mGrid_.DrawGUI(position.size);
                mGraph_.DrawGUI(canvasRect);
                mCanvas_.HandleEvents(canvasRect, position.size);
                DrawFooter(footerRect);
                DrawOptions(optionsRect);
               
                if (mCanvas_.IsDebuging)
                {
                    OnRepaint();
                }
            }
        }

        private void DrawFooter(Rect footer)
        {
            string behaviourTreePath = AssetDatabase.GetAssetPath(mBTAsset_).Substring(7);
            EditorGUI.LabelField(footer, behaviourTreePath, BTEditorStyle.EditorFooter);
        }

        private void DrawOptions(Rect options)
        {
            if (GUI.Button(options, BTEditorStyle.OptionsIcon, EditorStyles.toolbarButton))
            {
                GenericMenu menu = BTContextMenuFactory.CreateBehaviourTreeEditorMenu(this);
                menu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
            }
        }

        private void OnDisable()
        {
            Dispose();

        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {

            if (!isDisposed_)
            {
                if (mGraph_ != null)
                {
                    BTEditorGraph.DestroyImmediate(mGraph_);
                    mGraph_ = null;
                }

                if (mBTAsset_ != null)
                {

                    mBTAsset_.Dispose();
                }

                isDisposed_ = true;

            }
        }

        private void SetBTAsset(BTAsset asset, bool clearNavigationHistory)
        {
            if (asset != null && (clearNavigationHistory || asset != mBTAsset_))
            {

                if (mBTAsset_ != null)
                {
                    mBTAsset_.Dispose();
                    mBTAsset_ = null;
                }

                BehaviourTree mBT_ = asset.GetEditModeTree();
                if (mBT_ != null)
                {
                    mBTAsset_ = asset;
                    mGraph_.SetBehaviourTree(mBTAsset_,mBT_);
                    mCanvas_.Area = mBTAsset_.CanvasArea;

                }
                else
                {
                    CrashEditor("Failed to deserialize BT, Try to enable text serialization and manually edit the asset file to fix the behaviour tree. ");
                }


            }

        }

        private void CrashEditor(string message)
        {
            Debug.Log(message);
        }

        public void CreateNewBehaviourTree()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create new behaviour tree", "behaviour_tree", "asset", "");

            if (!string.IsNullOrEmpty(path))
            {
                BTAsset asset = ScriptableObject.CreateInstance<BTAsset>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.Refresh();

            }
        }

        public void OnRepaint()
        {
            Repaint();
        }

        public static void Open(BTAsset behaviourTree)
        {
            var window = EditorWindow.GetWindow<BehaviourTreeEditor>("Trails");
            window.SetBTAsset(behaviourTree, true);
        }

        public static void OpenSubtree(BTAsset behaviourTree)
        {
            var window = EditorWindow.GetWindow<BehaviourTreeEditor>("Trails");
            window.SetBTAsset(behaviourTree, false);
        }


        public void OpenBehaviourTree(BTAsset behaviourTree)
        {
            string path = EditorUtility.OpenFilePanel("Open behaviour tree", "", "asset");
            if (!string.IsNullOrEmpty(path))    // Comprueba error de ruta
            {
                // Numero de assets
                int index = path.IndexOf("Assets");
                if (index >= 0)
                {
                    path = path.Substring(index);
                    SetBTAsset(AssetDatabase.LoadAssetAtPath<BTAsset>(path), true);
                }
            }
        }
    }
}