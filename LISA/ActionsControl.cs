using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static ODDating.Program;
using static ODDating.Variables;
using ODDating.Interfaces;
using LogLevels;
using ODDating.Actions;
using static ZPBase.Base;
using ODDating.Entityes;
using ODDating.ProjectBase;

namespace LISA
{
    public class ActionsControl : Service // Life Imitation System Accounts (LISA)
    {
        public ActionsControl() : base()
        {
            RegisterActions();
        }
        public void StartActions()
        {
            do
            {
                try 
                {
                    RunAction();
                }
                catch 
                {
                   throw new Fatal($"{nameof(RunAction)} не выполнен.");
                }
            }
            while (CheckLimits());
            new Info("Закончили работу с аккаунтом");
        }
        private void RunAction()
        {
            Type type = RegisteredActions[new Random().Next(0, RegisteredActions.Count())];
            IAction action = (IAction)Activator.CreateInstance(type);
            action.RunAction();
        }
        private void RegisterActions()
        {
            try
            {
                RegisteredActions = (Type[])Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.Namespace == "ODDating.Actions");
            }
            catch 
            { 
                throw new Fatal("Не найдено ни одного Action! " +
                "Добавьте хотя бы один Action. Завершили работу."); 
            }
        }

    }
}
