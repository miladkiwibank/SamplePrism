using Infrastructure.Data.Entity;
using Infrastructure.Data.Validation;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Common.Services;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common.ModelBase
{
    public abstract class EntityCollectionViewModelBase : VisibleViewModelBase
    {
        public EntityCollectionViewModelBase()
        {
            AddItemCommand = new CaptionCommand<object>("添加" + ModelTitle, OnAddItem, CanAddItem);
            EditItemCommand = new CaptionCommand<object>("编辑" + ModelTitle, OnEditItem, CanEditItem);
            DeleteItemCommand = new CaptionCommand<object>("删除" + ModelTitle, OnDeleteItem, CanDeleteItem);
            DuplicateItemCommand = new CaptionCommand<object>("克隆" + ModelTitle, OnDuplicateItem, CanDuplicateItem);
            DeleteSelectedItemsCommand = new CaptionCommand<IEnumerable>("删除选中", OnDeleteSelectedItems, CanDeleteSelectedItems);

            CustomCommands = new List<ICaptionCommand>();
        }

        public ICaptionCommand AddItemCommand { get; private set; }

        public ICaptionCommand EditItemCommand { get; private set; }

        public ICaptionCommand DeleteItemCommand { get; private set; }

        public ICaptionCommand DuplicateItemCommand { get; private set; }

        public ICaptionCommand DeleteSelectedItemsCommand { get; private set; }

        public IList<ICaptionCommand> CustomCommands { get; private set; }

        private string m_modelTitle;

        public string ModelTitle
        {
            get { return m_modelTitle ?? (m_modelTitle = GetModelTitle()); }
        }

        private IList<ICaptionCommand> m_allCommands;

        public IList<ICaptionCommand> AllCommands
        {
            get { return m_allCommands ?? (m_allCommands = GetCommands().ToList()); }
        }

        private IEnumerable<ICaptionCommand> GetCommands()
        {
            var result = new List<ICaptionCommand> { AddItemCommand, EditItemCommand, DeleteItemCommand };
            result.AddRange(CustomCommands);
            return result;
        }

        public void RefreshCommands()
        {
            m_allCommands = null;
            RaisePropertyChanged(nameof(AllCommands));
        }

        public void RemoveCommand(ICaptionCommand command)
        {
            if (AllCommands.Contains(command))
            {
                AllCommands.Remove(command);
                RaisePropertyChanged(nameof(AllCommands));
            }
        }

        public void InsertCommand(ICaptionCommand command, int index = -1)
        {
            if (index > -1)
            {
                AllCommands.Insert(index, command);
            }
            else
            {
                AllCommands.Add(command);
            }
        }

        public abstract string GetModelTitle();

        protected abstract void OnDeleteItem(object obj);

        protected abstract bool CanDeleteItem(object obj);

        protected abstract void OnAddItem(object obj);

        protected abstract bool CanAddItem(object obj);

        protected abstract void OnEditItem(object obj);

        protected abstract bool CanEditItem(object obj);

        protected abstract void OnDuplicateItem(object obj);

        protected abstract bool CanDuplicateItem(object obj);

        protected abstract void OnDeleteSelectedItems(IEnumerable obj);

        protected abstract bool CanDeleteSelectedItems(IEnumerable arg);

        protected override string GetHeader()
        {
            return ModelTitle;
        }

        public override Type GetViewType()
        {
            return typeof(EntityCollectionBaseView);
        }

        public abstract void RefreshItems();
    }

    public class EntityCollectionViewModelBase<TViewModel, TModel> : EntityCollectionViewModelBase
       where TViewModel : EntityViewModelBase<TModel>
       where TModel : class, IEntity, new()
    {
        public EntityCollectionViewModelBase()
        {
            //Limit = LocalSettings.DefaultRecordLimit;
            OpenViewModels = new List<EntityViewModelBase<TModel>>();
            BatchCreateItemsCommand = new CaptionCommand<TModel>(""/*string.Format(Resources.BatchCreate_f, PluralModelTitle)*/, OnBatchCreateItems, CanBatchCreateItems);
            SortItemsCommand = new CaptionCommand<TModel>(""/*string.Format(Resources.Sort_f, PluralModelTitle)*/, OnSortItems);
            RemoveLimitCommand = new CaptionCommand<TModel>(""/*Resources.RemoveLimit*/, OnRemoveLimit);
            ToggleOrderByIdCommand = new CaptionCommand<TModel>(""/*Resources.ChangeSortOrder*/, OnToggleOrderById);

            //if (typeof(TViewModel).GetInterfaces().Any(x => x == typeof(IEntityCreator<TModel>)))
            //    CustomCommands.Add(BatchCreateItemsCommand);

            //CustomCommands.Add(typeof(TModel).GetInterfaces().Any(x => x == typeof(IOrderable))
            //                       ? SortItemsCommand
            //                       : ToggleOrderByIdCommand);

            _token = EventServiceFactory.EventService.GetEvent<GenericEvent<EntityViewModelBase<TModel>>>().Subscribe(x =>
            {
                if (x.Topic == EventTopicNames.AddedModelSaved)
                    if (x.Value is TViewModel)
                        Items.Add(x.Value as TViewModel);

                if (x.Topic == EventTopicNames.ModelAddedOrDeleted)
                {
                    foreach (var openViewModel in OpenViewModels)
                    {
                        if (!openViewModel.CanSave())
                            openViewModel.RollbackModel();
                    }

                    if (x.Value is TViewModel)
                    {
                        //Dao.RemoveFromCache(x.Value.Model as ICacheable);
                        //_workspace.Update(x.Value.Model);
                        //_workspace.CommitChanges();
                        //_workspace.Refresh(x.Value.Model);
                    }
                }
            });

            _token2 = EventServiceFactory.EventService.GetEvent<GenericEvent<VisibleViewModelBase>>().Subscribe(
                  s =>
                  {
                      if (s.Topic == EventTopicNames.ViewClosed)
                      {
                          if (s.Value is EntityViewModelBase<TModel> && OpenViewModels.Contains(s.Value))
                              OpenViewModels.Remove(s.Value as EntityViewModelBase<TModel>);
                      }
                  });
        }

        private void OnToggleOrderById(TModel obj)
        {
            ToggleOrderBy();
        }

        private void OnRemoveLimit(TModel obj)
        {
            Limit = 0;
            _items = null;
            RaisePropertyChanged(nameof(Items));
            RaisePropertyChanged(nameof(DisplayLimitWarning));
        }

        private void OnSortItems(TModel obj)
        {
            var list = Items.Select(x => x.Model).ToList();
            //InteractionService.UserIntraction.SortItems(list.Cast<IOrderable>(), string.Format(Resources.Sort_f, PluralModelTitle), Resources.ChangeSortOrderHint);
            //Workspace.CommitChanges();
            _items = null;
            RaisePropertyChanged(nameof(Items));
        }

        private static bool CanBatchCreateItems(TModel arg)
        {
            return true;
            //return typeof(TViewModel).GetInterfaces().Any(x => x == typeof(IEntityCreator<TModel>));
        }

        private void OnBatchCreateItems(TModel obj)
        {
            var title = "";//string.Format(Resources.BatchCreate_f, PluralModelTitle);
            var description = "";//string.Format(Resources.BatchCreateInfo_f, PluralModelTitle);
            //var data = InteractionService.UserIntraction.GetStringFromUser(title, description);
            //if (data.Length > 0)
            //{
            //    var items = ((IEntityCreator<TModel>)InternalCreateNewViewModel(new TModel())).CreateItems(data);
            //    foreach (var item in items) Workspace.Add(item);
            //    Workspace.CommitChanges();
            //    _items = null;
            //    RaisePropertyChanged(nameof(Items));
            //}
        }

        public ICaptionCommand BatchCreateItemsCommand { get; set; }
        public ICaptionCommand SortItemsCommand { get; set; }
        public ICaptionCommand RemoveLimitCommand { get; set; }
        public ICaptionCommand ToggleOrderByIdCommand { get; set; }

        public bool DisplayLimitWarning => Limit > 0 && _items != null && _items.Count == Limit;

        //private readonly IWorkspace _workspace = WorkspaceFactory.Create();
        //public IWorkspace Workspace { get { return _workspace; } }

        private ObservableCollection<TViewModel> _items;
        public ObservableCollection<TViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = GetItemsList();
                    RaisePropertyChanged(nameof(DisplayLimitWarning));
                }
                return _items;
            }
        }

        public string Filter { get; set; }
        public int Limit { get; set; }
        public bool OrderByDescending
        {
            get { return EntityCollectionSortManager.GetOrderByDesc<TModel>(); }
            set { EntityCollectionSortManager.SetOrderByDesc<TModel>(value); }
        }

        public override void RefreshItems()
        {
            _items = null;
            RaisePropertyChanged(nameof(Items));
        }

        protected virtual ObservableCollection<TViewModel> GetItemsList()
        {
            return BuildViewModelList(SelectItems());
        }

        protected virtual IEnumerable<TModel> SelectItems()
        {
            //var filter = (Filter ?? "").ToLower();
            //return !string.IsNullOrEmpty(filter)
            //    ? _workspace.Query<TModel>(x => x.Name.ToLower().Contains(filter), Limit, OrderByDescending)
            //    : _workspace.Query<TModel>(Limit, OrderByDescending);
            return null;
        }

        protected virtual void BeforeDeleteItem(TModel item)
        {
            // override if needed.
        }

        private void DoDeleteItem(TModel item)
        {
            BeforeDeleteItem(item);
            //_workspace.Delete(item);
            //_workspace.CommitChanges();
            //TODO: delete and commit
        }

        private readonly SubscriptionToken _token;
        private readonly SubscriptionToken _token2;
        public IList<EntityViewModelBase<TModel>> OpenViewModels { get; set; }

        private TViewModel _selectedItem;
        public TViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public override string GetModelTitle()
        {
            return CreateNewViewModel(new TModel()).GetModelTypeString();
        }

        protected virtual string CanDeleteItem(TModel model)
        {
            return ValidatorRegistry.GetDeleteErrorMessage(model);
        }

        protected override bool CanAddItem(object obj)
        {
            return true;
        }

        protected override bool CanClose(object arg)
        {
            return OpenViewModels.Count == 0;
        }

        protected override void OnDeleteItem(object obj)
        {
            if (/*MessageBox.Show(string.Format(Resources.DeleteItemConfirmation_f, ModelTitle, SelectedItem.Model.Name), Resources.Confirmation, MessageBoxButton.YesNo) == MessageBoxResult.Yes*/
                InteractionService.UserIntraction.AskQuestion(""))
            {
                var errorMessage = CanDeleteItem(SelectedItem.Model);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (SelectedItem.Model.Id > 0)
                    {
                        DoDeleteItem(SelectedItem.Model);
                        SelectedItem.Model.PublishEvent(EventTopicNames.ModelAddedOrDeleted);
                    }
                    Items.Remove(SelectedItem);
                }
                else
                {
                    //MessageBox.Show(errorMessage, Resources.Warning);
                    InteractionService.UserIntraction.GiveFeedBack(errorMessage);
                }
            }
        }

        protected override void OnAddItem(object obj)
        {
            var model = new TModel();
            PublishViewModel(model);
        }

        public void PublishViewModel(TModel model)
        {
            VisibleViewModelBase wm = InternalCreateNewViewModel(model);
            if (wm != null) OpenViewModels.Add(wm as EntityViewModelBase<TModel>);
            wm.PublishEvent(EventTopicNames.ViewAdded);
        }

        protected override void OnDuplicateItem(object obj)
        {
            var duplicate = SelectedItem.Model;//ObjectCloner.EntityClone(SelectedItem.Model);
            duplicate.Id = 0;
            //EntityIdFixer.FixEntityIdNumber(duplicate, x => 0);
            //duplicate.Name = "_" + duplicate.Name;
            VisibleViewModelBase wm = InternalCreateNewViewModel(duplicate);
            if (wm != null) OpenViewModels.Add(wm as EntityViewModelBase<TModel>);
            wm.PublishEvent(EventTopicNames.ViewAdded);
        }

        protected override void OnDeleteSelectedItems(IEnumerable obj)
        {
            var errors = new StringBuilder();

            if (/*MessageBox.Show(Resources.DeleteSelectedItems + "?", Resources.Confirmation, MessageBoxButton.YesNo) != MessageBoxResult.Yes*/!InteractionService.UserIntraction.AskQuestion("")) return;
            obj.Cast<TViewModel>().ToList().ForEach(
                model =>
                {
                    var errorMessage = CanDeleteItem(model.Model);
                    if (!string.IsNullOrEmpty(errorMessage))
                        errors.AppendLine(errorMessage);
                    else
                    {
                        if (model.Model.Id > 0)
                        {
                            BeforeDeleteItem(model.Model);
                            //Workspace.Delete(model.Model);
                            //TODO: delete model
                        }
                        Items.Remove(model);
                    }
                });
            //Workspace.CommitChanges();
            //TODO: commit changes
            if (!string.IsNullOrEmpty(errors.ToString()))
                //MessageBox.Show(errors.ToString());
                InteractionService.UserIntraction.GiveFeedBack(errors.ToString());
        }

        protected override bool CanDeleteSelectedItems(IEnumerable arg)
        {
            return SelectedItem != null;
        }

        protected override bool CanDuplicateItem(object arg)
        {
            return SelectedItem != null;
        }

        protected override bool CanEditItem(object obj)
        {
            return SelectedItem != null;
        }

        protected override bool CanDeleteItem(object obj)
        {
            return SelectedItem != null && !OpenViewModels.Contains(SelectedItem);
        }

        protected override void OnEditItem(object obj)
        {
            if (!OpenViewModels.Contains(SelectedItem)) OpenViewModels.Add(SelectedItem);
            (SelectedItem as VisibleViewModelBase).PublishEvent(EventTopicNames.ViewAdded);
        }

        protected ObservableCollection<TViewModel> BuildViewModelList(IEnumerable<TModel> itemsList)
        {
            //if (typeof(TModel).GetInterfaces().Any(x => x == typeof(IOrderable)))
            //    return new ObservableCollection<TViewModel>(itemsList.OrderBy(x => ((IOrderable)x).SortOrder).ThenBy(x => x.Id).Select(InternalCreateNewViewModel));
            return new ObservableCollection<TViewModel>(itemsList.Select(InternalCreateNewViewModel));
        }

        protected TViewModel CreateNewViewModel(TModel model)
        {
            TViewModel result;
            try
            {
                result = ServiceLocator.Current.GetInstance<TViewModel>();
            }
            catch (Exception)
            {
                result = Activator.CreateInstance<TViewModel>();
            }
            result.Model = model;
            return result;
        }

        protected TViewModel InternalCreateNewViewModel(TModel model)
        {
            var result = CreateNewViewModel(model);
            result.Initialize(/*_workspace,*/ model);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_workspace != null)
                //    _workspace.Dispose();
                EventServiceFactory.EventService.GetEvent<GenericEvent<EntityViewModelBase<TModel>>>().Unsubscribe(_token);
                EventServiceFactory.EventService.GetEvent<GenericEvent<VisibleViewModelBase>>().Unsubscribe(_token2);
            }
        }

        private void ToggleOrderBy()
        {
            OrderByDescending = !OrderByDescending;
            _items = null;
            RaisePropertyChanged(nameof(Items));
        }

        public int GetCount()
        {
            return Items.Count;
        }
    }
}
