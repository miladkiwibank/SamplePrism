using SamplePrism.Presentation.Common;
using SamplePrism.Presentation.Common.Commands;
using SamplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SamplePrism.Presentation.Controls.Interaction
{
    public class UserInteraction : IUserInteraction
    {
        private SplashScreenForm m_splashScreen;
        private readonly PopupDataViewModel m_popupDataViewModel;
        private PopupWindow m_popupWindow;

        public UserInteraction(IApplicationState applicationState)
        {
            m_popupDataViewModel = new PopupDataViewModel(applicationState);
        }

        public PopupWindow PopupWindow => m_popupWindow ?? (m_popupWindow = CreatePopupWindow());

        private PopupWindow CreatePopupWindow()
        {
            return new PopupWindow { DataContext = m_popupDataViewModel };
        }

        public void ToggleSplashScreen()
        {
            if (m_splashScreen != null)
            {
                m_splashScreen.Hide();
                m_splashScreen = null;
            }
            else
            {
                m_splashScreen = new SplashScreenForm();
                m_splashScreen.Show();
            }
            //if (m_splashScreen == null)
            //{
            //    Thread thread = new Thread(new ThreadStart(this.CreateSplash));
            //    thread.SetApartmentState(ApartmentState.STA);
            //    thread.IsBackground = true;
            //    thread.Start();
            //    return;
            //}
            //if (m_splashScreen.Dispatcher.CheckAccess())
            //{
            //    m_splashScreen.Close();
            //    return;
            //}
            //m_splashScreen.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(this.m_splashScreen.Close));
        }

        //private void CreateSplash()
        //{
        //    m_splashScreen = new SplashScreenForm();
        //    m_splashScreen.ShowDialog();
        //}

        //public void ActivateSplashScreen()
        //{
        //    if (this.m_splashScreen.Dispatcher.CheckAccess())
        //    {
        //        this.ActivateSplash();
        //        return;
        //    }
        //    this.m_splashScreen.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(this.ActivateSplash));
        //}

        //private void ActivateSplash()
        //{
        //    this.m_splashScreen.Activate();
        //}

        public IEnumerable<T> ImportPopup<T>(Dictionary<string, string> tableHeader, string fileName)
        {
            //List<string> importHeaders = ExcelHelper.GetHeaders(fileName);
            //ImportMapForm form = new ImportMapForm();
            //var formViewModel = new ImportMapFormViewModel<T>(tableHeader, importHeaders, fileName, form.Close);
            //form.DataContext = formViewModel;
            //form.ShowDialog();

            //if (formViewModel.Result)
            //    return formViewModel.ResultData;
            return null;
        }

        public bool AskQuestion(string question)
        {
            return MessageBox.Show(question, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)
                == MessageBoxResult.Yes;
        }

        public void GiveFeedBack(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = new FeedbackWindow { MessageText = { Text = message }, Topmost = true };
                window.ShowDialog();
            });
        }

        public void DisplayPopup(string name, string title, string content, string headerColor = "DarkRed",
            Action<object> action = null, object actionParameter = null)
        {
            m_popupDataViewModel.Add(name, title, content, headerColor, action, actionParameter);
            PopupWindow.Show();
            m_popupDataViewModel.DisplayPopups();
        }

        //private static void ReorderItems(IEnumerable<IOrderable> list)
        //{
        //    int num = 10;
        //    foreach (IOrderable current in list)
        //    {
        //        current.SortOrder = num;
        //        num += 10;
        //    }
        //}
    }

    public class PopupDataViewModel
    {
        private readonly IApplicationState m_applicationState;
        private readonly IList<PopupData> m_popupCache;
        private readonly ObservableCollection<PopupData> m_popupList;

        public PopupDataViewModel(IApplicationState applicationState)
        {
            m_applicationState = applicationState;
            m_popupCache = new List<PopupData>();
            m_popupList = new ObservableCollection<PopupData>();
            ClickButtonCommand = new CaptionCommand<PopupData>("Click", OnButtonClick);
        }

        public ObservableCollection<PopupData> PopupList { get { return m_popupList; } }

        public CaptionCommand<PopupData> ClickButtonCommand { get; set; }

        private void OnButtonClick(PopupData obj)
        {
            if (obj.Action != null)
                obj.Action(obj.ActionParameter);
            m_popupList.Remove(obj);
            if (obj.Name != "")
                m_applicationState.NotifyEvent("", new { Name = obj.Name, Data = obj.ActionParameter ?? "" });
            Application.Current.MainWindow.Focus();
        }

        public void Add(string name, string title, string content, string headerColor, Action<object> action, object actionParameter)
        {
            m_popupCache.Add(new PopupData
            {
                Name = name,
                Title = title,
                Content = content,
                HeaderColor = headerColor,
                Action = action,
                ActionParameter = actionParameter
            });
        }

        public void DisplayPopups()
        {
            if (m_popupCache.Count == 0) return;
            foreach (var popupData in m_popupCache)
            {
                m_popupList.Add(popupData);
            }

            m_popupCache.Clear();
        }
    }

    public class PopupData
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Action<object> Action { get; set; }

        public object ActionParameter { get; set; }

        private string m_headerColor;

        public string HeaderColor
        {
            get { return m_headerColor; }
            set
            {
                m_headerColor = value;
                ContentColor = UpdateContentColor();
            }
        }

        public string ContentColor { get; set; }

        private string UpdateContentColor()
        {
            var color = ((TypeConverter)new ColorConverter()).ConvertFromString(HeaderColor);
            var result = color is Color ? (Color)color : new Color();
            return Lerp(result, Colors.White, 0.8f).ToString();
        }

        private Color Lerp(Color colour, Color to, float amount)
        {
            float sr = colour.R, sg = colour.G, sb = colour.B;

            float er = to.R, eg = to.G, eb = to.B;

            byte r = (byte)(sr + ((er - sr) * amount)),
                 g = (byte)(sg + ((eg - sr) * amount)),
                 b = (byte)(sb + ((eb - sr) * amount));

            return Color.FromArgb(colour.A, r, b, g);
        }
    }
}