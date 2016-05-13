﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class TextWindow : BasicWindow, IDrawable
    {
        public TextWindow(string text, int x, int y) : base(text,x,y,text.Length,1)
        {
            IsInteractable = false;
        }

        void IDrawable.Draw()
        {
            for(int i = 0; i < Name.Length; ++i)
            {
                Console.SetCursorPosition(PositionX + i, PositionY);
                Console.Write(Name[i]);
            }
        }
        void IDrawable.Clean()
        {
            for (int i = 0; i < Name.Length; ++i)
            {
                Console.SetCursorPosition(PositionX + i, PositionY);
                Console.Write(' ');
            }
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void FromParentAction(ref BasicWindow activeWindow)
        {
           
        }
        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            
        }
    }
}