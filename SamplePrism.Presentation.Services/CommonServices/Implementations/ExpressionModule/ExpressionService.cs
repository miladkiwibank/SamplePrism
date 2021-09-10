﻿using System.Collections.Generic;
using Fluentscript.Lib.AST;
using SamplePrism.Infrastructure.Data;
using SamplePrism.Persistance;
using SamplePrism.Services.Implementations.ExpressionModule.Accessors;

namespace SamplePrism.Services.Implementations.ExpressionModule
{
    public class ExpressionService : IExpressionService
    {
        private readonly IAutomationDao _automationDao;
        private Dictionary<string, string> _scripts;
        private Dictionary<string, string> Scripts { get { return _scripts ?? (_scripts = _automationDao.GetScripts()); } }

        public ExpressionService(IAutomationDao automationDao, IEntityService entityService)
        {
            _automationDao = automationDao;
            ExpressionEngine.RegisterFunction("Call", CallFunction);
            EntityAccessor.EntityService = entityService;
        }

        private object CallFunction(string s, string s1, FunctionCallExpr arg3)
        {
            return EvalCommand(s, "", null, default(object));
        }

        public string Eval(string expression)
        {
            return ExpressionEngine.Eval(expression);
        }

        public T Eval<T>(string expression, object dataObject, T defaultValue = default(T))
        {
            return ExpressionEngine.Eval(expression, dataObject, defaultValue);
        }

        public T EvalCommand<T>(string functionName, IEntityClass entity, object dataObject, T defaultValue = default(T))
        {
            var entityName = entity != null ? "_" + entity.Name : "";
            return EvalCommand(functionName, entityName, dataObject, defaultValue);
        }

        public T EvalCommand<T>(string functionName, string entityName, object dataObject, T defaultValue = default(T))
        {
            var script = GetScript(functionName, entityName);
            return string.IsNullOrEmpty(script) ? defaultValue : Eval(script, dataObject, defaultValue);
        }

        public string ReplaceExpressionValues(string data, string template = "\\[=([^\\]]+)\\]")
        {
            return ExpressionEngine.ReplaceExpressionValues(data, template);
        }

        public string ReplaceExpressionValues(string data, object dataObject, string template = "\\[=([^\\]]+)\\]")
        {
            return ExpressionEngine.ReplaceExpressionValues(data, dataObject, template);
        }

        private string GetScript(string functionName, string entityName)
        {
            if (Scripts.ContainsKey(functionName + entityName))
                return Scripts[functionName + entityName];
            if (Scripts.ContainsKey(functionName + "_*"))
                return Scripts[functionName + "_*"];
            return "";
        }

        public void ResetCache()
        {
            _scripts = null;
        }
    }
}
