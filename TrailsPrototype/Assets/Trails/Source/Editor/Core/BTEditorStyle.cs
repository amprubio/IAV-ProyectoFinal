﻿using UnityEditor;
using UnityEngine;
using Trails;

namespace TrailsEditor
{
    public static class BTEditorStyle
    {

        private static GUISkin mEditorSkin_;
        private static Texture mArrowUP_;
        private static Texture mArrowDOWN_;
        private static Texture mOptionsIcon_;
        private static Texture bannerRed_;
        private static Texture bannerOrange_;
        private static Texture bannerPurple_;


        private static GUIStyle mHeaderLabel_;
        private static GUIStyle mBoldLabel_;
        private static GUIStyle mEditorFooter_;
        private static GUIStyle mSelectionBoxStyle_;
        private static GUIStyle mMultilineTextArea_;

        private static GUIStyle mListHeader_;
        private static GUIStyle mListBackGround_;
        private static GUIStyle mListButton_;
        private static GUIStyle mListDragHandle_;


        private static GUIStyle mArrowUPButton_;
        private static GUIStyle mArrowDownButton_;

        private static GUIStyle mSeparatorStyle_;
        private static GUIStyle mRegionBackground_;

        enum NodeStyles { CompositeStyle, DecoratorStyle, ActionStyle, NodeGroup }
        private static BTGraphNodeStyle[] mStyles_;

        public static Texture bannerRed
        {
            get
            {
                return bannerRed_;
            }
        }
        public static Texture bannerOrange
        {
            get
            {
                return bannerOrange_;
            }
        }
        public static Texture bannerPurple
        {
            get
            {
                return bannerPurple_;
            }
        }
        public static Texture ArrowUP
        {
            get
            {
                return mArrowUP_;
            }
        }

        public static Texture ArrowDown
        {
            get
            {
                return mArrowDOWN_;
            }
        }
        public static Texture OptionsIcon
        {
            get
            {
                return mOptionsIcon_;
            }
        }

        public static GUIStyle HeaderLabel
        {
            get
            {
                return mHeaderLabel_;
            }
        }

        public static GUIStyle BoldLabel
        {
            get
            {
                return mBoldLabel_;
            }
        }
        public static GUIStyle SelectionBox
        {
            get
            {
                return mSelectionBoxStyle_;
            }
        }
        public static GUIStyle MultilineTextArea
        {
            get
            {
                return mMultilineTextArea_;
            }
        }
        public static GUIStyle ListHeader
        {
            get
            {
                return mListHeader_;
            }
        }

        public static GUIStyle ListBackground
        {
            get
            {
                return mListBackGround_;
            }
        }

        public static GUIStyle ListButton
        {
            get
            {
                return mListButton_;
            }
        }

        public static GUIStyle mListDragHandle
        {
            get
            {
                return mListDragHandle_;
            }
        }
        public static GUIStyle ArrowUPButton
        {
            get
            {
                return mArrowUPButton_;
            }
        }
        public static GUIStyle ArrowDOWNButton
        {
            get
            {
                return mArrowDownButton_;
            }
        }
        public static GUIStyle EditorFooter
        {
            get
            {
                return mEditorFooter_;
            }
        }

        public static GUIStyle SeparatorStyle
        {
            get
            {
                return mSeparatorStyle_;
            }
        }

        public static GUIStyle RegionBG
        {
            get
            {
                return mRegionBackground_;
            }
        }

        public static BTEditorTreeLayout TreeLayout
        {
            get
            {
                return BTEditorTreeLayout.Vertical;
            }
            set
            {

                //  EditorPrefs.SetInt("TrailsEditor.TreeLayout", value);
            }
        }

        public static void EnsureStyle()
        {
            LoadGUISkin();
            LoadTextures();
            CreateNodeStyles();
            CreateGUIStyles();
        }

        private static void LoadGUISkin()
        {
            if (mEditorSkin_ != null)
            {
                return;
            }
            else
            {
                mEditorSkin_ = Resources.Load<GUISkin>("Trails/EditorGUI/editor_style");
            }
        }

        private static void LoadTextures()
        {
            if (mArrowUP_ == null)
            {
                mArrowUP_ = Resources.Load<Texture>("Trails/EditorGUI/arrow_2_up");
            }
            if (mArrowDOWN_ == null)
            {
                mArrowDOWN_ = Resources.Load<Texture>("Trails/EditorGUI/arrow_2_down");
            }
            if (mOptionsIcon_ == null)
            {
                mOptionsIcon_ = Resources.Load<Texture>("Trails/EditorGUI/options_icon");
            }
            if (bannerRed_ == null)
            {
                bannerRed_ = Resources.Load<Texture>("Trails/EditorGUI/banner-Red");
            }
            if (bannerOrange_ == null)
            {
                bannerOrange_ = Resources.Load<Texture>("Trails/EditorGUI/banner-Orange");
            }
            if (bannerPurple_ == null)
            {
                bannerPurple_ = Resources.Load<Texture>("Trails/EditorGUI/banner-Purple");

            }

        }
        private static void CreateNodeStyles()
        {
            mStyles_ = new BTGraphNodeStyle[4];

            for (int i = 0; i < 4; i++)
            {
                if (mStyles_[i] == null)
                {

                    if (i == (int)(NodeStyles.CompositeStyle))
                    {
                        mStyles_[(int)(NodeStyles.CompositeStyle)] = new BTGraphNodeStyle("flow node 1", "flow node 1 on",
                                                        "flow node 6", "flow node 6 on",
                                                        "flow node 4", "flow node 4 on",
                                                        "flow node 3", "flow node 3 on");
                    }

                    else if (i == (int)(NodeStyles.DecoratorStyle))
                    {
                        mStyles_[(int)(NodeStyles.DecoratorStyle)] = new BTGraphNodeStyle("flow node 1", "flow node 1 on",
                                                        "flow node 6", "flow node 6 on",
                                                        "flow node 4", "flow node 4 on",
                                                        "flow node 3", "flow node 3 on");
                    }

                    else if (i == (int)(NodeStyles.ActionStyle))
                    {
                        mStyles_[(int)(NodeStyles.ActionStyle)] = new BTGraphNodeStyle("flow node 1", "flow node 1 on",
                                                        "flow node 6", "flow node 6 on",
                                                        "flow node 4", "flow node 4 on",
                                                        "flow node 3", "flow node 3 on");
                    }

                    else if (i == (int)(NodeStyles.NodeGroup))
                    {
                        mStyles_[(int)(NodeStyles.NodeGroup)] = new BTGraphNodeStyle("flow node hex 1", "flow node hex 1 on",
                                                    "flow node hex 6", "flow node hex 6 on",
                                                    "flow node hex 4", "flow node hex 4 on",
                                                    "flow node hex 3", "flow node hex 3 on");
                    }

                }
            }

        }
        private static void CreateGUIStyles()
        {

            if (mHeaderLabel_ == null)
            {
                mHeaderLabel_ = new GUIStyle(EditorStyles.boldLabel);
                mHeaderLabel_.alignment = TextAnchor.UpperLeft;
                mHeaderLabel_.contentOffset = new Vector2(0, -2);
                mHeaderLabel_.fontSize = 20;
                mHeaderLabel_.fontStyle = FontStyle.Bold;
            }

            if (mBoldLabel_ == null)
            {
                mBoldLabel_ = new GUIStyle(EditorStyles.boldLabel);

            }

            if (mEditorFooter_ == null)
            {
                mEditorFooter_ = new GUIStyle(EditorStyles.boldLabel);
                mEditorFooter_.alignment = TextAnchor.MiddleRight;
                mEditorFooter_.contentOffset = new Vector2(-10, 0);
            }

            if (mSelectionBoxStyle_ == null)
            {
                mSelectionBoxStyle_ = mEditorSkin_.FindStyle("selection_box");
                if (mSelectionBoxStyle_ == null)
                {
                    mSelectionBoxStyle_ = mEditorSkin_.box;
                }

            }

            if (mMultilineTextArea_ == null)
            {
                mMultilineTextArea_ = new GUIStyle(EditorStyles.textField);
                mMultilineTextArea_.wordWrap = true;
            }


            if (mListHeader_ == null)
            {
                mListHeader_ = new GUIStyle();
                mListHeader_.normal.textColor = Color.black;
                mListHeader_.alignment = TextAnchor.MiddleLeft;
                mListHeader_.contentOffset = new Vector2(10, 0);
                mListHeader_.fontSize = 11;

            }

            if (mListBackGround_ == null)
            {
                mListBackGround_ = new GUIStyle("RL Background");
            }

            if (mListButton_ == null)
            {
                mListButton_ = new GUIStyle("RL FooterButton");
            }

            if (mListDragHandle_ == null)
            {
                mListDragHandle_ = new GUIStyle("RL DragHandle");
            }

            if (mArrowUPButton_ == null)
            {
                mArrowUPButton_ = mEditorSkin_.FindStyle("arrow_up");
            }

            if (mArrowDownButton_ == null)
            {
                mArrowDownButton_ = mEditorSkin_.FindStyle("arrow_down");
            }

            if (mSeparatorStyle_ == null)
            {
                mSeparatorStyle_ = new GUIStyle("sv_iconselector_sep");
            }

            if (mRegionBackground_ == null)
            {
                mRegionBackground_ = new GUIStyle("RegionBg");
                mRegionBackground_.contentOffset = new Vector2(0, -5);
                mRegionBackground_.alignment = TextAnchor.MiddleCenter;
            }

        }


        public static BTGraphNodeStyle GetStyle(BehaviourNode node)
        {
            if (node == null)
            {
                return null;
            }
            else
            {
                if (node is NodeGroup)
                {
                    return mStyles_[(int)NodeStyles.NodeGroup];
                }
                else if (node is Trails.Action)
                {
                    return mStyles_[(int)NodeStyles.ActionStyle];
                }
                else if (node is Composite)
                {
                    return mStyles_[(int)NodeStyles.CompositeStyle];
                }
                else if (node is Decorator)
                {
                    return mStyles_[(int)NodeStyles.DecoratorStyle];
                }

                return null;
            }
        }

        public static Color GetTransitionColor(BehaviourNodeStatus status)
        {
            switch (status)
            {
                case BehaviourNodeStatus.Failure:
                    return Color.red;
                case BehaviourNodeStatus.Running:
                    return Color.yellow;
                case BehaviourNodeStatus.Success:
                    return Color.green;
            }

            return Color.white;
        }

        public static Vector2 GetNodeSize(BehaviourNode node)
        {
            return new Vector2(180, 40);
        }
    }
}