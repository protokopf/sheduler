﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.ExtraTypes
{
    class Drawer
    {
        private List<IDrawable> mListOfGoals;
        
        public Drawer()
        {
            mListOfGoals = new List<IDrawable>();
        }

        // данный метод будет вызываться из рекурсивной функции, которая предоставит Drawer'у ссылки на
        // все рисуемые объекты
        public void AddGoal(IDrawable dr)
        {
            mListOfGoals.Add(dr);
        }
        public void RemoveGoal(IDrawable dr)
        {
            mListOfGoals.Remove(dr);
        }

        public void InitialDrawing()
        {
            foreach (var goal in mListOfGoals)
                goal.Draw();
        }
        public void Draw()
        {
            var changedItems = (from chI in mListOfGoals where (chI.IsChanged() == true) select chI);
            foreach(var it in changedItems)
            {
                it.Clean();
                it.Draw();
            }
        }
    }
}
