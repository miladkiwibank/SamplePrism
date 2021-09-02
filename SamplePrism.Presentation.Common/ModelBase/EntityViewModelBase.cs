using Prism.Commands;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.Services;
using FluentValidation;
using Infrastructure.Data.Entity;
using Infrastructure.Data.Validation;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class EntityViewModelBase<TModel> : VisibleViewModelBase
        where TModel : class, IEntity, new()
    {
        private bool m_modelSaved;
        private IValidator<TModel> m_validator;

        protected EntityViewModelBase()
        {

        }

        [Browsable(false)]
        public TModel Model { get; set; }

        [Browsable(false)]
        public ICaptionCommand CaptionCommand { get; private set; }

        [Browsable(false)]
        public string ErrorMessage { get; set; }


        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value.Trim();
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string m_error;

        [Browsable(false)]
        public string Error
        {
            get { return m_error; }
            set
            {
                m_error = value;
                RaisePropertyChanged(nameof(Error));
            }
        }

        [Browsable(false)]
        public string Fireground => GetForeground();

        protected virtual string GetForeground()
        {
            return "White";
        }

        public abstract string GetModelTypeString();

        public void Initialize(TModel model)
        {
            m_modelSaved = false;
            Model = model;
            Initialize();
        }

        protected virtual void Initialize()
        {
            // override to initialize
        }

        public override void OnShown()
        {
            //base.OnShutdown();
            m_modelSaved = false;
        }

        public override void OnClosed()
        {
            //base.OnClosed();
            if (!m_modelSaved)
                RollbackModel();
        }

        protected override string GetHeader()
        {
            //if (Model.Id > 0)
            //    return string.Format("编辑{0}", Name);
            return string.Format("编辑");
        }

        protected virtual void OnSave(string value)
        {
            if (CanSave())
            {
                m_modelSaved = true;
                if (Model.Id == 0)
                    this.PublishEvent(EventTopicNames.AddedModelSaved);
                this.PublishEvent(EventTopicNames.ModelAddedOrDeleted);
                ((VisibleViewModelBase)this).PublishEvent(EventTopicNames.ViewClosed);
            }
            else
            {
                if (string.IsNullOrEmpty(Name))
                    ErrorMessage = string.Format("{0}", GetModelTypeString());
                InteractionService.UserIntraction.GiveFeedBack("");
                ErrorMessage = "";
            }
        }

        public bool CanSave()
        {
            ErrorMessage = GetSaveErrorMessage();
            return !string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ErrorMessage) && CanSave("");
        }

        protected virtual string GetSaveErrorMessage()
        {
            return ValidatorRegistry.GetSaveErrorMessage(Model);
        }

        protected virtual bool CanSave(string arg)
        {
            return Validate();
        }

        private bool Validate()
        {
            var validator = m_validator ?? (m_validator = GetValidator());
            var vs = validator.Validate(Model);
            if (!vs.IsValid)
            {
                Error = string.Join("\r", vs.Errors.Select(x => x.ErrorMessage));
                return false;
            }

            Error = string.Empty;
            return true;
        }

        protected virtual AbstractValidator<TModel> GetValidator()
        {
            return new EntityValidator<TModel>();
        }

        public void RollbackModel()
        {
            if (Model.Id > 0)
            {
                //TODO: Refresh model
                RaisePropertyChanged(nameof(Name));
            }
        }
    }

    public class EntityValidator<TModel> : AbstractValidator<TModel>
        where TModel : IEntity
    {
        public EntityValidator()
        {
            //RuleFor(x => x.Name).NotEmpty();
        }
    }
}
