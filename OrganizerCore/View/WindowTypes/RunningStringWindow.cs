﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class RunningStringWindow : BasicWindow, IDrawable
    {
        private Timer mTimer;
        private Queue<string> mMessagesQueue;

        private const string mDefaultString = "There is no messages";
        private string Label { get; set; }

        private int mFirst;
        private int mLast;
        private int mInnerIndex;

        private void MoveStringAlongWindow(object source, ElapsedEventArgs e)
        {
            WinHasChanged();
            if(mLast == 0)
            {
                mFirst = Width;
                mLast = Width + Label.Length - 1;
                mInnerIndex = 0;
                return;
            }
            if(mFirst == 0)
            {
                ++mInnerIndex;
                --mLast;
                return;
            }
            --mFirst;
            --mLast;
        }

        private void SetDefaultMessage()
        {
            Label = mDefaultString;
            this.FontColor = ConsoleColor.Black;

            mFirst = Width;
            mLast = Width + Label.Length - 1;
            mInnerIndex = 0;
        }
        private void PeekLastMessage()
        {
            Label = mMessagesQueue.Dequeue();
            this.FontColor = ConsoleColor.DarkGreen;

            mFirst = Width;
            mLast = Width + Label.Length;
            mInnerIndex = 0;
        }

        public RunningStringWindow(string name,int x, int y, int w, int h) : base(name,x, y, w, h)
        {
            Label = mDefaultString;
            mMessagesQueue = new Queue<string>();

            mFirst = Width;
            mLast = Width + Label.Length;
            mInnerIndex = 0;

            mTimer = new Timer(100);
            mTimer.Elapsed += MoveStringAlongWindow;
            mTimer.Start();
        }

        void IDrawable.Draw()
        {
            Console.ForegroundColor = FontColor;
            int yPos = (Height / 2) + PositionY;

            for(int i = mFirst, cursor = mInnerIndex; i < Width - 1 && i < mLast; ++i, ++cursor)
            {
                Console.SetCursorPosition(PositionX + ((i==0)?1:i), yPos);
                Console.Write(Label[cursor]);
            }
        }
        void IDrawable.Clean()
        {
            int yPos = (Height / 2) + PositionY;
            for (int i = 1; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(i, yPos);
                Console.Write(' ');
            }
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
 
        }
        public override void FromParentAction(ref BasicWindow activeWindow)
        {
            if (Label != mDefaultString)
            {
                if (mMessagesQueue.Count > 0)
                    PeekLastMessage();
                else
                    SetDefaultMessage();
                this.IsWindowChanged = true;
            }
        }
        public override void ReactMethod(object sender, ActionEventArgs e)
        {
            mMessagesQueue.Enqueue(e.Storage["Message"]);
            if (Label == mDefaultString)
                PeekLastMessage();
            this.IsWindowChanged = true;
        }
    }
}
