using Infrastructure.ExceptionReporter;
using Microsoft.Win32;
using Prism.Mvvm;
using SimplePrism.Presentation.Common.Commands;
using SimplePrism.Presentation.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplePrism.Presentation.Common
{
    internal class ErrorReportViewModel : BindableBase
    {
        public ErrorReportViewModel(IEnumerable<Exception> exceptions)
        {
            Model = new ExceptionReportInfo { AppAssembly = Assembly.GetCallingAssembly() };
            Model.SetExceptions(exceptions);
            CopyCommand = new CaptionCommand<string>("复制", Copy);
            SaveCommand = new CaptionCommand<string>("保存", Save);
            SubmitCommand = new CaptionCommand<string>("提交", Submit);
            CloseCommand = new CaptionCommand<string>("关闭", Close);
        }

        private bool? m_dialogResult;
        public bool? DialogResult
        {
            get { return m_dialogResult; }
            set { SetProperty(ref m_dialogResult, value); }
        }

        public ExceptionReportInfo Model { get; set; }

        public CaptionCommand<string> CopyCommand { get; private set; }

        public CaptionCommand<string> SaveCommand { get; private set; }

        public CaptionCommand<string> SubmitCommand { get; private set; }

        public CaptionCommand<string> CloseCommand { get; private set; }

        private string m_errorReportAsText;
        public string ErrorReportAsText
        {
            get
            {
                if (m_errorReportAsText == null)
                {
                    m_errorReportAsText = GenerateReport();
                }
                return m_errorReportAsText;
            }
            set
            {
                m_errorReportAsText = value;
            }
        }


        public string ErrorMessage
        {
            get { return Model.UserExplanation; }
            set
            {
                Model.UserExplanation = value;
                m_errorReportAsText = string.Empty;
                RaisePropertyChanged(nameof(ErrorReportAsText));
            }
        }

        public string UserMessage
        {
            get
            {
                return Model.UserExplanation;
            }
            set
            {
                Model.UserExplanation = value;
                m_errorReportAsText = null;
                RaisePropertyChanged(nameof(ErrorReportAsText));
            }
        }

        private void Submit(string obj)
        {
            if (string.IsNullOrEmpty(UserMessage) && !InteractionService.UserIntraction.AskQuestion(""))
            {
                return;
            }
            //DialogResult = false;
            SubmitError();
        }

        public void SubmitError()
        {
            //TODO: 向指定网址提交错误日志
        }

        private void Save(string obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            bool result = saveFileDialog.ShowDialog().GetValueOrDefault();
            if (result)
            {
                string fileName = saveFileDialog.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    SaveReportToFile(fileName);
                }
            }
        }

        private void Copy(string obj)
        {
            Clipboard.SetText(ErrorReportAsText);
        }

        private void Close(string obj)
        {
            this.DialogResult = true;
        }

        private string GenerateReport()
        {
            ExceptionReportGenerator exceptionReportGenerator = new ExceptionReportGenerator(Model);
            return exceptionReportGenerator.CreateExceptionReport();
        }

        public string GetErrorReport()
        {
            m_errorReportAsText = string.Empty;
            return ErrorReportAsText;
        }

        public void SaveReportToFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            try
            {
                using (FileStream stream = File.OpenWrite(fileName))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(ErrorReportAsText);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                InteractionService.UserIntraction.GiveFeedBack(string.Format("Unable to save file '{0}' : {1}", fileName, ex.Message));
            }
        }
    }
}
