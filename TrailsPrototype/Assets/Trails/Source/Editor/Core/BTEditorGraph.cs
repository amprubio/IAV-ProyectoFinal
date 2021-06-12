﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails;
using UnityEditor;
using System;

namespace TrailsEditor{
    public class BTEditorGraph : ScriptableObject{
        private const int SELECT_MOUSE_BUTTON = 0;
		private const int CONTEXT_MOUSE_BUTTON = 1;
		private List<BTEditorGraphNode> mSelection_;
        private BTEditorGraphNode mMasterRoot_;
        private Stack<BTEditorGraphNode> mRootStack_;

        private bool mDrawSelectionBox_;
        private bool mBehaviourTreeIsReadOnly_=false;
        private bool mCanBeginBoxSelection_;
        private Vector2 mSelectionBoxPos_;

        public bool ReadOnly{
            get{
            return mBehaviourTreeIsReadOnly_;
            }
        }

        public int Depth{
            get{
                return Mathf.Max(mRootStack_.Count - 1, 0);
            }
        }

		public Rect? SelectionBox { get; set; }

        private BTEditorGraphNode WorkingRoot
		{
			get
			{
				return mRootStack_.Peek();
			}
		}

        private void OnCreated(){
            mMasterRoot_=null;
            mRootStack_=new Stack<BTEditorGraphNode>();
            mDrawSelectionBox_= false;
            mBehaviourTreeIsReadOnly_=false;
        }

         private void OnDestroy() {
            BTEditorGraphNode.DestroyImmediate(mMasterRoot_);
            mMasterRoot_=null;
            if(mRootStack_!=null)
                mRootStack_.Clear();
        }

        public void SetBehaviourTree(BehaviourTree bT){
            if(mMasterRoot_!=null){
                return;
            }
            
            mBehaviourTreeIsReadOnly_=bT.isReadOnly;
            mMasterRoot_ = BTEditorGraphNode.CreateRoot(this, bT.Root);
            mRootStack_.Push(mMasterRoot_);

        }

        public void DrawGUI(Rect dest)
		{
            if(WorkingRoot == null)
			{
                return;
            }
			
			WorkingRoot.Update();
			WorkingRoot.Draw();
			DrawSelectionBox();
			HandleEvents(dest);
			
		}
        private void DrawSelectionBox()
		{
            if(mDrawSelectionBox_){
                Vector2 mousePosition= BTEditorCanvas.Current.Event.mousePosition;
                Rect pos= new Rect();
                pos.x=0;
                pos.y=0;
                pos.width=0;
                pos.height=0;

                GUI.Box(pos,"",BTEditorStyle.SelectionBox);
                BTEditorCanvas.Current.Repaint();

                SelectionBox= new Rect(BTEditorCanvas.Current.WindowSpaceToCanvasSpace(pos.position), pos.size);


            }
            else{
                SelectionBox=null;
            }

        }

        private void HandleEvents(Rect dest)
		{
            if(dest.Contains(BTEditorCanvas.Current.Event.mousePosition)){            
                switch(BTEditorCanvas.Current.Event.type){
                    case EventType.MouseDown:
                    
                    if(BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON){
                        ClearSelection();
                        mCanBeginBoxSelection_=true;
                        mSelectionBoxPos_= BTEditorCanvas.Current.Event.mousePosition;
                        BTEditorCanvas.Current.Event.Use();
                    }

                    break;
                
                    case EventType.MouseUp:
                    
                    if(BTEditorCanvas.Current.Event.button == SELECT_MOUSE_BUTTON){

                    }
                    
                    else if(BTEditorCanvas.Current.Event.button == CONTEXT_MOUSE_BUTTON){

                    }

                    BTEditorCanvas.Current.Event.Use();
                    break;
                
                    case EventType.MouseDrag:
                    if(!mDrawSelectionBox_ && mCanBeginBoxSelection_)
					{
						mDrawSelectionBox_ = true;
					}

					BTEditorCanvas.Current.Event.Use();
                    
                    break;

                }
            }

        }

        public void OnPushNodeGroup(BTEditorGraphNode node)
		{


            
        }

        // Seleccion de nodos
        public void OnNodeSelect(BTEditorGraphNode node){
            
        }
        public void OnNodeDeselect(BTEditorGraphNode node){

        }

        // mover nodos
        public void OnNodeBeginDrag(BTEditorGraphNode node, Vector2 pos){
            if(!mSelection_.Contains(node)){
                return;
            }
            // drag the rest of the tree
            for(int i=0;i<mSelection_.Count; i++){
                mSelection_[i].OnBeginDrag(pos);
            }

        }
        public void OnNodeDrag(BTEditorGraphNode node, Vector2 pos, bool recursive=false){
        
        }
        public void OnNodeEndDrag(BTEditorGraphNode node){
            if(!mSelection_.Contains(node)){
                return;
            }
            
            for(int i=0;i<mSelection_.Count; i++){
                mSelection_[i].OnEndDrag();
            }

        }


		
        private void ClearSelection(){

        }



	    public bool IsRoot(BTEditorGraphNode node)
		{
			return node == WorkingRoot;
		}
        
        public static BTEditorGraph Create()
		{
            BTEditorGraph graph = ScriptableObject.CreateInstance<BTEditorGraph>();
			graph.OnCreated();
			graph.hideFlags = HideFlags.HideAndDontSave;
			graph.mSelection_ = new List<BTEditorGraphNode>();
            
			return graph;

        }


    }
}