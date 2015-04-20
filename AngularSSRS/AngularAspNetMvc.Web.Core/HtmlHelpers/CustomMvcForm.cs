using System;
using System.Web.Mvc;

namespace AngularAspNetMvc.Web.Core.HtmlHelpers
{
    public class CustomMvcForm : IDisposable
    {
        public const string CurrentFormViewDataKey = "CurrentForm";
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public CustomMvcForm(ViewContext viewContext, string formName)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }
            if (string.IsNullOrEmpty(formName))
            {
                throw new ArgumentNullException("formName");
            }
            _viewContext = viewContext;
            viewContext.ViewData[CurrentFormViewDataKey] = formName;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                _viewContext.Writer.Write("</form>");
                _viewContext.FormContext = null;
            }
        }
    }
}