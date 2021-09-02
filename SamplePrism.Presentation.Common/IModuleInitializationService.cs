using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common
{
    public enum ModuleInitializationStage
    {
        PreInitialization,
        Initialization,
        PostInitialization,
        StartUp
    }

    public interface IStagedSequenceService<TStageEnum>
    {
        void RegisterForStage(Action action, TStageEnum stage);
    }

    public class StagedSequenceService<TStageEnum> : IStagedSequenceService<TStageEnum>
    {
        private readonly List<Action>[] m_stages;

        public StagedSequenceService()
        {
            m_stages = new List<Action>[NumberOfEnumValues()];

            for (int i = 0; i < m_stages.Length; i++)
            {
                m_stages[i] = new List<Action>();
            }
        }

        public virtual void ProcessSequence()
        {
            foreach (var stage in m_stages)
            {
                foreach (var action in stage)
                {
                    action();
                }
            }
        }

        public void RegisterForStage(Action action, TStageEnum stage)
        {
            m_stages[Convert.ToInt32(stage)].Add(action);
        }

        private static int NumberOfEnumValues()
        {
            return typeof(TStageEnum).GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        }
    }

    public interface IModuleInitializationService : IStagedSequenceService<ModuleInitializationStage>
    {
        void Initialize();
    }

    public class ModuleInitializationService : StagedSequenceService<ModuleInitializationStage>, IModuleInitializationService
    {
        public void Initialize()
        {
            base.ProcessSequence();
        }
    }
}
